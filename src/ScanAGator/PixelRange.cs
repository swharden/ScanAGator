using System;

namespace ScanAGator;

public struct PixelRange
{
    public readonly int FirstPixel;
    public readonly int LastPixel;
    public int Min => Math.Min(FirstPixel, LastPixel);
    public int Max => Math.Max(FirstPixel, LastPixel);

    public readonly int SpanPixels => LastPixel - FirstPixel;

    public PixelRange(int firstPixel, int lastPixel)
    {
        FirstPixel = firstPixel;
        LastPixel = lastPixel;
    }

    /// <summary>
    /// Return the value clamped to a range between min/max (inclusive)
    /// </summary>
    private static int Clamp(int value, int min, int max)
    {
        if (value < min)
            return min;
        if (value > max)
            return max;
        return value;
    }

    public PixelRange Clamp(int min, int max)
    {
        return new PixelRange(Clamp(FirstPixel, min, max), Clamp(LastPixel, min, max));
    }
}
