using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanAGator.Tests;

internal class LineScanComparisonTests
{
    /* These tests ensure new analyses agree with old locked-in ones */

    [Test]
    public void Test_Values_Close()
    {
        string scanFolderPath = SampleData.MultipleGreenOverRedFolder;

        // analyze the old way
        LineScanFolder lsf1 = new(scanFolderPath, false);
        lsf1.SetFrame(0);
        lsf1.BaselineIndex1 = 0;
        lsf1.BaselineIndex2 = 80;
        lsf1.StructureIndex1 = 21;
        lsf1.StructureIndex2 = 25;
        lsf1.FilterSizePixels = 20;
        lsf1.GenerateAnalysisCurves();

        // analyze the new way
        PixelRange baseline = new(0, 80);
        PixelRange structure = new(21, 25);
        LineScanSettings settings = new(baseline, structure, 0);
        LineScan.LineScanFolder2 lsf2 = new(scanFolderPath);
        RatiometricLinescan ls = lsf2.GetRatiometricLinescanFrame(0, settings);

        for (int i = 0; i < ls.Samples; i++)
        {
            // source data is perfect
            Assert.That(lsf1.CurveG[i], Is.EqualTo(ls.G.Values[i]));
            Assert.That(lsf1.CurveR[i], Is.EqualTo(ls.R.Values[i]));

            // slightly different because of baseline logic
            Assert.That(lsf1.CurveDeltaG[i], Is.EqualTo(ls.DG.Values[i]).Within(10));

            // all points are within 10% of the original
            Assert.That(lsf1.CurveDeltaGoR[i], Is.EqualTo(ls.DGR.Values[i]).Within(10));
        }
    }
}
