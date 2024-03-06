using System;

namespace ScanAGator;

/// <summary>
/// Defines the edges of a structure between two X positions (inclusive)
/// </summary>
public struct StructureRange
{
    public int Min { get; private set; }
    public int Max { get; private set; }
    public int ImageWidth { get; private set; }
    public readonly int Span => Max - Min + 1;
    public readonly float MinFraction => (float)Min / ImageWidth;
    public readonly float MaxFraction => (float)Max / ImageWidth;

    public StructureRange(int x1, int x2, int imageWidth)
    {
        Min = Math.Min(x1, x2);
        Max = Math.Max(x1, x2);
        ImageWidth = imageWidth;
    }
}
