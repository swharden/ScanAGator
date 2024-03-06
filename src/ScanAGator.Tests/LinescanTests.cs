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
    public void Test_Linescan_Freehand()
    {
        Prairie.ParirieXmlFile pv = SampleData.LinescanXmlFiles
            .Select(x => new Prairie.ParirieXmlFile(x))
            .Where(x => x.Mode == LinescanMode.FreeHand)
            .First();

        pv.FreehandPoints.Should().NotBeEmpty();
    }
}
