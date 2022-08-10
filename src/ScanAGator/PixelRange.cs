namespace ScanAGator;

public struct PixelRange
{
    public readonly int FirstPixel;
    public readonly int LastPixel;
    public readonly double UnitsPerPixel;
    public readonly int SpanPixels => LastPixel - FirstPixel;
    public readonly double SpanUnits => SpanPixels * UnitsPerPixel;

    public PixelRange(int firstPixel, int lastPixel, double unitsPerPixel)
    {
        FirstPixel = firstPixel;
        LastPixel = lastPixel;
        UnitsPerPixel = unitsPerPixel;
    }
}
