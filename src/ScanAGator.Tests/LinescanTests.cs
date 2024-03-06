using FluentAssertions;
using NUnit.Framework;
using System.Linq;

namespace ScanAGator.Tests;

public class LinescanTests
{
    [Test]
    public void Test_Linescan_Type()
    {
        LinescanMode[] sampleDataModes = SampleData.LinescanXmlFiles
            .Select(x => new Prairie.ParirieXmlFile(x))
            .Select(x => x.Mode)
            .ToArray();

        sampleDataModes.Distinct().Count().Should().Be(2);
    }
}
