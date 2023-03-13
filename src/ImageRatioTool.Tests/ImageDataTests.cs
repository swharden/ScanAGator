using BitMiracle.LibTiff.Classic;
using FluentAssertions;
using System.Windows.Forms;
using System.Text;

namespace ImageRatioTool.Tests;

public class Tests
{
    [Test]
    public void Test_SampleData_RatiometricSingleImage()
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

    [Test]
    public void Test_SampleData_RatiometricTSeries()
    {
        SciTIF.TifFile tif = new(SampleData.RatiometricImageSeries);
        tif.Frames.Should().Be(24);
        tif.Slices.Should().Be(1);
        tif.Channels.Should().Be(2);
    }

    [Test]
    public void Test_SampleData_ReadScaleMetadata()
    {
        double micronsPerPixel = TifFileOperations.GetMicronsPerPixel(SampleData.RatiometricImageSeries);
        micronsPerPixel.Should().BeApproximately(0.848324, precision: 1e-8);
    }
}