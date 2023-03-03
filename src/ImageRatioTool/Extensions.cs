namespace ImageRatioTool;

public static class Extensions
{
    public static Bitmap GetBitmap(this SciTIF.ImageRGB img)
    {
        byte[] bytes = img.GetBitmapBytes();
        using MemoryStream ms = new(bytes);
        Bitmap bmp = new(ms);
        return bmp;
    }

    public static SciTIF.Image Crop(this SciTIF.Image img, int xMin, int xMax, int yMin, int yMax)
    {
        int width = xMax - xMin;
        int height = yMax - yMin;

        SciTIF.Image img2 = new(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                img2.SetPixel(x, y, img.GetPixel(xMin + x, yMin + y));
            }
        }

        return img2;
    }

    public static double DistanceFrom(this Point pt1, Point pt2)
    {
        double dX = Math.Abs(pt1.X - pt2.X);
        double dY = Math.Abs(pt1.Y - pt2.Y);
        return Math.Sqrt(dX * dX + dY * dY);
    }
}
