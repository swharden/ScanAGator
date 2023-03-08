using System.IO;

namespace ImageRatioTool;

public static class ImageOperations
{
    public static Bitmap[] MakeDisplayImages(SciTIF.Image[] red, SciTIF.Image[] green)
    {
        if (red.Length != green.Length)
            throw new ArgumentException("number of images must be the same");

        Bitmap[] bmps = new Bitmap[red.Length];
        for (int i = 0; i < red.Length; i++)
            bmps[i] = MakeDisplayImage(red[i], green[i]);

        return bmps;
    }

    public static Bitmap MakeDisplayImage(SciTIF.Image red, SciTIF.Image green)
    {
        int divisionFactor = 1 << (13 - 8); // 13-bit to 8-bit
        var displayRed = red / divisionFactor;
        var displayGreen = green / divisionFactor;
        SciTIF.ImageRGB displayMerge = new(displayRed, displayGreen, displayRed);
        Bitmap bmp = displayMerge.GetBitmap();
        return bmp;
    }

    public static Bitmap Annotate(Bitmap reference, RoiAnalysis roi)
    {
        Bitmap bmp = new(reference.Width, reference.Height);
        using Graphics gfx = Graphics.FromImage(bmp);
        using Font font = new("Consolas", 8);
        gfx.DrawImage(reference, 0, 0);
        DrawRoiRectangle(gfx, roi.Rect);
        gfx.DrawString(roi.GetSummary(), font, Brushes.Yellow, new Point(5, 5));
        return bmp;
    }

    private static void DrawRoiRectangle(Graphics gfx, Rectangle rect, int r = 2)
    {
        gfx.DrawRectangle(Pens.Yellow, rect);

        Point[] corners = {
            new(rect.Left, rect.Top),
            new(rect.Right, rect.Top),
            new(rect.Right, rect.Bottom),
            new(rect.Left, rect.Bottom),
        };

        foreach (Point corner in corners)
        {
            Rectangle cornerRect = new(corner.X - r, corner.Y - r, r * 2 + 1, r * 2 + 1);
            gfx.FillRectangle(Brushes.White, cornerRect);
            gfx.DrawRectangle(Pens.Black, cornerRect);
        }
    }

    public static void DrawRoiRectangle(Graphics gfx, RoiRectangle roi)
    {
        gfx.DrawRectangle(Pens.Yellow, roi.Rect);

        int r = 2;

        foreach (Point corner in roi.Corners)
        {
            Rectangle cornerRect = new(corner.X - r, corner.Y - r, r * 2 + 1, r * 2 + 1);
            gfx.FillRectangle(Brushes.White, cornerRect);
            gfx.DrawRectangle(Pens.Black, cornerRect);
        }

        foreach (Point corner in roi.Centers)
        {
            Rectangle cornerRect = new(corner.X - r, corner.Y - r, r * 2 + 1, r * 2 + 1);
            gfx.FillRectangle(Brushes.White, cornerRect);
            gfx.DrawRectangle(Pens.Black, cornerRect);
        }
    }

    public static (SciTIF.Image[] red, SciTIF.Image[] green, Bitmap[] display) GetMultiFrameRatiometricImages(string tifFilePath)
    {
        tifFilePath = Path.GetFullPath(tifFilePath);
        if (!File.Exists(tifFilePath))
            throw new FileNotFoundException(tifFilePath);

        SciTIF.TifFile tif = new(tifFilePath);

        if (tif.Frames < 2)
            throw new ArgumentException($"TIF must have multiple frames: {tifFilePath}");

        if (tif.Channels != 2)
            throw new ArgumentException($"TIF must have 2 channels: {tifFilePath}");

        if (tif.Slices != 1)
            throw new ArgumentException($"TIF must have 1 slice: {tifFilePath}");

        SciTIF.Image[] red = new SciTIF.Image[tif.Frames];
        SciTIF.Image[] green = new SciTIF.Image[tif.Frames];
        Bitmap[] display = new Bitmap[tif.Frames];

        for (int i = 0; i < tif.Frames; i++)
        {
            red[i] = tif.GetImage(i, 0, 0);
            green[i] = tif.GetImage(i, 0, 1);
            display[i] = MakeDisplayImage(red[i], green[i]);
        }

        return (red, green, display);
    }

    public static RectangleF GetRectangle(PointF pt, int radius)
    {
        return new RectangleF(pt.X - radius, pt.Y - radius, radius * 2, radius * 2);
    }

    public static void FillCircle(Graphics gfx, PointF pt, Brush brush, int r = 2)
    {
        gfx.FillEllipse(brush, GetRectangle(pt, r));
    }

    public static void DrawCircle(Graphics gfx, PointF pt, Pen pen, int r = 2)
    {
        gfx.DrawEllipse(pen, GetRectangle(pt, r));
    }

    public static void FillRectangle(Graphics gfx, PointF pt, Brush brush, int r = 2)
    {
        gfx.FillRectangle(brush, GetRectangle(pt, r));
    }

    public static void DrawRectangle(Graphics gfx, PointF pt, Pen pen, int r = 2)
    {
        gfx.DrawRectangle(pen, GetRectangle(pt, r));
    }
}
