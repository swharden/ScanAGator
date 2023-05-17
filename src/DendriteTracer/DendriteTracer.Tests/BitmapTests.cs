using DendriteTracer.Core;

namespace DendriteTracer.Tests;

public class BitmapTests
{
    [Test]
    public void Test_RedAndGreen_Merge()
    {
        SciTIF.TifFile tif = new(SampleData.TSERIES_2CH_PATH);
        SciTIF.Image red = tif.GetImage(frame: 0, slice: 0, channel: 0);
        SciTIF.Image green = tif.GetImage(frame: 0, slice: 0, channel: 1);

        red.AutoScale();
        green.AutoScale();

        Bitmap bmp = new(red.Width, red.Height);

        for (int y = 0; y < red.Height; y++)
        {
            for (int x = 0; x < red.Width; x++)
            {
                byte r = red.GetPixelByte(x, y, true);
                byte g = green.GetPixelByte(x, y, true);
                Color c = new(r, g, r);

                bmp.SetPixel(x, y, c);
            }
        }

        Rectangle rect1 = new(50, 60, 70, 80);
        bmp.FillRect(rect1, Colors.Blue);

        Rectangle rect2 = new(13, 69, 123, 222);
        bmp.DrawRect(rect2, Colors.Yellow);

        bmp.DrawLine(13, 69, 123, 222, Colors.White);

        bmp.TestSave("test1.png");
    }
}