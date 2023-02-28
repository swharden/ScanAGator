using System.Text;

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

    public static Bitmap Annotate(Bitmap reference, Rectangle rect, RoiAnalysis roi)
    {
        Bitmap bmp = new(reference.Width, reference.Height);
        using Graphics gfx = Graphics.FromImage(bmp);
        using Font font = new("Consolas", 8);
        gfx.DrawImage(reference, 0, 0);

        DrawRoiRectangle(gfx, rect);

        StringBuilder sb = new();
        sb.AppendLine($"X: [{rect.Left}, {rect.Right}] W={rect.Width}");
        sb.AppendLine($"Y: [{rect.Top}, {rect.Bottom}] H={rect.Height}");
        sb.AppendLine($"Noise floor: {roi.NoiseFloor:N2}");
        sb.AppendLine($"Signal threshold: {roi.Threshold:N2}");
        sb.AppendLine($"Pixels above threshold: {roi.FractionAboveThreshold * 100:N1}%");
        if (roi.PixelsAboveThreshold > 0)
        {
            sb.AppendLine($"G/R: {roi.MedianRatio * 100:N3}%");
        }
        else
        {
            sb.AppendLine($"G/R: No pixels in ROI above threshold");
        }

        gfx.DrawString(sb.ToString(), font, Brushes.Yellow, new Point(5, 5));

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
