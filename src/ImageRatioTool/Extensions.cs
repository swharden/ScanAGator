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
}
