namespace ScanAGator;

/// <summary>
/// This object holds data values for a single-channel 2D image.
/// </summary>
public class ImageData
{
    public readonly double[] Values;
    public readonly int Width;
    public readonly int Height;
    public int pixelCount => Width * Height;

    public ImageData(double[] data, int width, int height)
    {
        Values = data;
        Width = width;
        Height = height;
    }
}
