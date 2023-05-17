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

        List<Pixel> pixels = new() {
            new((int)(0.4925*256), (int)(0.4912*256)),
            new((int)(0.5840*256), (int)(0.4922*256)),
            new((int)(0.6211*256), (int)(0.4277*256)),
            new((int)(0.7148*256), (int)(0.4297*256)),
            new((int)(0.8867*256), (int)(0.5039*256)),
            new((int)(0.9258*256), (int)(0.6191*256)),
            new((int)(0.9395*256), (int)(0.6855*256)),
            new((int)(0.9004*256), (int)(0.7207*256)),
        };
        bmp.DrawLines(pixels, Colors.White);

        foreach (Pixel px in pixels)
            bmp.DrawRect(new Rectangle(px, 2), Colors.Yellow);

        bmp.TestSave("test1.png");
    }
}