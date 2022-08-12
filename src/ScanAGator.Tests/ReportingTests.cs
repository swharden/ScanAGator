﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanAGator.Tests
{
    internal class ReportingTests
    {
        [Test]
        public void Test()
        {
            LineScan.LineScanFolder2 lsFolder = new(SampleData.MultipleGreenOverRedFolder);

            PixelRange baseline = new(0, 80);
            PixelRange structure = StructureDetection.GetBrightestStructure(lsFolder.GreenImages[0]);
            LineScanSettings settings = new(baseline, structure, filterSizePx: 20);

            Reporting.AnalyzeLinescanFolder(lsFolder, settings);
        }
    }
}