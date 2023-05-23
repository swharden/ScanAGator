using DendriteTracer.Core;

namespace DendriteTracer.Tests;

internal class DendritePathTests
{
    [Test]
    public void Test_DendritePath_Drawing()
    {
        SciTIF.TifFile tif = new(SampleData.TSERIES_2CH_PATH);
        SciTIF.Image red = tif.GetImage(frame: 0, slice: 0, channel: 0);
        SciTIF.Image green = tif.GetImage(frame: 0, slice: 0, channel: 1);
        Bitmap bmp = ImageOperations.MakeBitmap(red, green);

        DendritePath dp = SampleData.DendritePath();
        Pixel[] roiCenters = dp.GetEvenlySpacedPoints(10);
        dp.Draw(bmp);

        RoiSet rois = new();
        rois.AddRange(roiCenters);
        rois.Draw(bmp);

        bmp.TestSave("trace-draw.bmp");
    }
}