using FluentAssertions;

namespace ImageRatioTool.Tests;

public class Tests
{
    [Test]
    public void Test1()
    {
        SciTIF.TifFile tif = new(SampleData.RatiometricImage);
        tif.Frames.Should().Be(1);
        tif.Slices.Should().Be(1);
        tif.Channels.Should().Be(2);

        SciTIF.Image red = tif.GetImage(0, 0, 0);
        red.GetPixel(13, 17).Should().Be(162); // checked with ImageJ

        SciTIF.Image green = tif.GetImage(0, 0, 1);
        green.GetPixel(13, 17).Should().Be(422); // checked with ImageJ
    }
}