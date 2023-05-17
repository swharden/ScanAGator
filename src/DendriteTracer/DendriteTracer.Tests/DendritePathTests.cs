using DendriteTracer.Core;

namespace DendriteTracer.Tests;

internal class DendritePathTests
{
    [Test]
    public void Test_DendritePath_Drawing()
    {
        DendritePath dp = SampleData.DendritePath();

        SciTIF.TifFile tif = new(SampleData.TSERIES_2CH_PATH);
        SciTIF.Image red = tif.GetImage(frame: 0, slice: 0, channel: 0);
        SciTIF.Image green = tif.GetImage(frame: 0, slice: 0, channel: 1);

        Bitmap bmp = ImageOperations.MakeBitmap(red, green);
        Bitmap bmp2 = dp.Draw(bmp);
        bmp2.TestSave("trace-draw.bmp");
    }
}