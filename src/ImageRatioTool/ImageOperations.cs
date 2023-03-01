namespace ImageRatioTool;

public static class ImageOperations
{
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
}
