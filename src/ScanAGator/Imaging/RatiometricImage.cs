using System.Drawing;
using System.Runtime.InteropServices;

namespace ScanAGator.Imaging;

public class RatiometricImage
{
    public readonly ImageData GreenData;
    public readonly ImageData RedData;
    public double FloorPercentile = 0;

    private Bitmap? _Green = null;
    private Bitmap? _Red = null;
    private Bitmap? _Merge = null;

    public Bitmap Green
    {
        get
        {
            if (_Green is null)
            {
                _Green = GreenData.ToBitmap();
            }
            return _Green;
        }
    }

    public Bitmap Red
    {
        get
        {
            if (_Red is null)
            {
                _Red = RedData.ToBitmap();
            }
            return _Red;
        }
    }

    public Bitmap Merge
    {
        get
        {
            if (_Merge is null)
            {
                _Merge = ImageDataTools.Merge(RedData, GreenData, RedData);
            }
            return _Merge;
        }
    }

    public RatiometricImage(ImageData green, ImageData red)
    {
        GreenData = green;
        RedData = red;
    }

    public RatiometricImage(string greenFilePath, string redFilePath)
    {
        GreenData = ImageDataTools.ReadTif(greenFilePath);
        RedData = ImageDataTools.ReadTif(redFilePath);
    }

    public void SubtractFloor(double percent)
    {
        FloorPercentile = percent;
        GreenData.SubtractFloor(percent);
        RedData.SubtractFloor(percent);
        _Merge = null;
    }
}
