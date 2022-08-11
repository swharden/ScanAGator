namespace ScanAGator;

public struct PixelRange
{
    public readonly int FirstPixel;
    public readonly int LastPixel;
    public readonly double UnitsPerPixel;
    public readonly int SpanPixels => LastPixel - FirstPixel;
    public readonly double SpanUnits => SpanPixels * UnitsPerPixel;
    public bool HasUnits => !double.IsNaN(UnitsPerPixel);

    public PixelRange(int firstPixel, int lastPixel)
    {
        FirstPixel = firstPixel;
        LastPixel = lastPixel;
        UnitsPerPixel = double.NaN;
    }

    public PixelRange(int firstPixel, int lastPixel, double unitsPerPixel)
    {
        FirstPixel = firstPixel;
        LastPixel = lastPixel;
        UnitsPerPixel = unitsPerPixel;
    }
}
