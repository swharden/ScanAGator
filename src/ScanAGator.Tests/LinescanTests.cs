using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;

namespace ScanAGator.Tests;

public class LinescanTests
{
    [Test]
    public void Test_Linescan_Mode()
    {
        LinescanMode[] sampleDataModes = SampleData.LinescanXmlFiles
            .Select(x => new Prairie.ParirieXmlFile(x))
            .Select(x => x.Mode)
            .ToArray();

        sampleDataModes.Distinct().Count().Should().Be(2);
    }

    [Test]
    public void Test_Linescan_Points()
    {
        foreach (string xmlFilePath in SampleData.LinescanXmlFiles)
        {
            Prairie.ParirieXmlFile pv = new(xmlFilePath);
            pv.Points.Should().NotBeEmpty();
        };
    }
}
