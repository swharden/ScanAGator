using System.Drawing;

namespace ScanAGator.Imaging;

public class RatiometricImage
{
    public readonly ImageData GreenData;
    public readonly ImageData RedData;

    public readonly Bitmap Green;
    public readonly Bitmap Red;
    public readonly Bitmap Merge;

    public RatiometricImage(ImageData green, ImageData red)
    {
        GreenData = green;
        RedData = red;

        Green = GreenData.GetBitmapIndexed();
        Red = RedData.GetBitmapIndexed();
        Merge = ImageDataTools.Merge(RedData, GreenData, RedData);
    }

    public RatiometricImage(string greenFilePath, string redFilePath)
    {
        GreenData = new(greenFilePath);
        RedData = new(redFilePath);

        Green = GreenData.GetBitmapIndexed();
        Red = RedData.GetBitmapIndexed();
        Merge = ImageDataTools.Merge(RedData, GreenData, RedData);
    }
}
