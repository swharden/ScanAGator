using NUnit.Framework;
using ScanAGator.Analysis;
using System;

namespace ScanAGator.Tests;

internal class AnalysisTests
{
    [Test]
    public void Test_Analysis_Workflow()
    {
        // read data from disk
        Prairie.FolderContents pvFolder = new(SampleData.MultiFrameRatiometricFolderPath);
        Prairie.ParirieXmlFile pvXml = new(pvFolder.XmlFilePath);
        Imaging.RatiometricImages images = new(pvFolder);

        // prepare the data and settings to perform analysis
        Imaging.RatiometricImage averageImage = images.Average;
        AnalysisSettings settings = new(
            img: images.Average,
            img2: images.Frames,
            baseline: new BaselineRange(20, 60),
            structure: new StructureRange(21, 25),
            filterPx: 20,
            floorPercentile: 20,
            xml: pvXml);

        // execute the analysis
        AnalysisResult result = new(averageImage, settings);
        Console.WriteLine(result);
    }
}
