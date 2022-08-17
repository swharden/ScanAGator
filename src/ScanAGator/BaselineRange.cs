using System;

namespace ScanAGator;

/// <summary>
/// Defines the edges of the baseline range of pixels between two Y positions (inclusive)
/// </summary>
public struct BaselineRange
{
    public int Min { get; private set; }
    public int Max { get; private set; }
    public int Size => Max - Min + 1;

    public BaselineRange(int y1, int y2)
    {
        Min = Math.Min(y1, y2);
        Max = Math.Max(y1, y2);
    }
}
