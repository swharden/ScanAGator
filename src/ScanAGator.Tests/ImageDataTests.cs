using NUnit.Framework;
using FluentAssertions;

namespace ScanAGator.Tests;

/// <summary>
/// This class loads actual data, performs measurements, and asserts they are identical
/// to the same operations performed with ImageJ
/// </summary>
internal class ImageDataTests
{
    [Test]
    public void Test_Image_Dimensions()
    {
        ImageData img = ImageDataTools.ReadTif(SampleData.GreenLinescanImagePath);

        // known dimensions were verified by ImageJ (title bar)
        img.Width.Should().Be(60);
        img.Height.Should().Be(1000);
    }

    [Test]
    public void Test_Image_PixelValue()
    {
        ImageData img = ImageDataTools.ReadTif(SampleData.GreenLinescanImagePath);

        // known values were measured using ImageJ (mouseover)
        img.GetValue(23, 42).Should().Be(2341);
    }

    [Test]
    public void Test_Image_Average()
    {
        ImageData img = ImageDataTools.ReadTif(SampleData.GreenLinescanImagePath);

        // known values were measured using ImageJ (multi-measure)
        img.Average().Should().BeApproximately(417.913, .01);
    }

    [Test]
    public void Test_Image_AverageByRow()
    {
        ImageData img = ImageDataTools.ReadTif(SampleData.GreenLinescanImagePath);
        double[] avg = img.AverageByRow();

        // known values were measured using ImageJ (rotate, plot profile)
        avg[0].Should().BeApproximately(348.983, .01);
        avg[1].Should().BeApproximately(381.417, .01);
        avg[2].Should().BeApproximately(361.483, .01);
        avg[321].Should().BeApproximately(475.367, .01);
        avg[999].Should().BeApproximately(374.083, .01);
    }

    [Test]
    public void Test_Image_AverageByRowWithinRange()
    {
        ImageData img = ImageDataTools.ReadTif(SampleData.GreenLinescanImagePath);
        double[] avg = img.AverageByRow(21, 25);
        avg.Should().HaveCount(img.Height);

        // known values were measured using ImageJ (crop, rotate, plot profile)
        avg[0].Should().BeApproximately(1923.000, .01);
        avg[1].Should().BeApproximately(2134.800, .01);
        avg[2].Should().BeApproximately(1841.200, .01);
        avg[321].Should().BeApproximately(3026.000, .01);
        avg[999].Should().BeApproximately(1984.800, .01);
    }

    [Test]
    public void Test_Image_AverageByColumn()
    {
        ImageData img = ImageDataTools.ReadTif(SampleData.GreenLinescanImagePath);
        double[] avg = img.AverageByColumn();
        avg.Should().HaveCount(img.Width);

        // known values were measured using ImageJ (plot profile)
        avg[0].Should().BeApproximately(254.320, .01);
        avg[1].Should().BeApproximately(265.132, .01);
        avg[2].Should().BeApproximately(270.109, .01);
        avg[42].Should().BeApproximately(223.015, .01);
        avg[59].Should().BeApproximately(230.558, .01);
    }

    [Test]
    public void Test_Image_AverageByColumnWithinRange()
    {
        ImageData img = ImageDataTools.ReadTif(SampleData.GreenLinescanImagePath);
        double[] avg = img.AverageByColumn(10, 23);
        avg.Should().HaveCount(img.Width);

        // known values were measured using ImageJ (plot profile)
        avg[0].Should().BeApproximately(239.286, .01);
        avg[1].Should().BeApproximately(271.357, .01);
        avg[2].Should().BeApproximately(246.357, .01);
        avg[42].Should().BeApproximately(182.643, .01);
        avg[59].Should().BeApproximately(247.357, .01);
    }

    [Test]
    public void Test_Image_AverageRectangle()
    {
        ImageData img = ImageDataTools.ReadTif(SampleData.GreenLinescanImagePath);

        // known values were measured using ImageJ (draw selection, multi-measure)
        img.Average(21, 25, 3, 11).Should().BeApproximately(2077.911, .01);
    }
}
