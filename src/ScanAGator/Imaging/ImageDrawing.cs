using System.Drawing;
using System.Linq;

namespace ScanAGator.Imaging;

public static class ImageDrawing
{
    public static void DrawLinescan(Bitmap bmp, Prairie.ParirieXmlFile? pv)
    {
        if (pv is null)
            return;

        if (pv.Points.Length < 2)
            return;

        PointF[] points = pv.Points
            .Select(x => new PointF(bmp.Width * x.X, bmp.Height * x.Y))
            .ToArray();

        using Graphics gfx = Graphics.FromImage(bmp);
        gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        using Pen pen = new(Color.FromArgb(100, 255, 255, 0), width: 5);
        gfx.DrawLines(pen, points);
    }
}
