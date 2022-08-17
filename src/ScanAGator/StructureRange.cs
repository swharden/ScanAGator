using System;

namespace ScanAGator;

/// <summary>
/// Defines the edges of a structure between two X positions (inclusive)
/// </summary>
public struct StructureRange
{
    public int Min { get; private set; }
    public int Max { get; private set; }
    public int Size => Max - Min + 1;

    public StructureRange(int x1, int x2)
    {
        Min = Math.Min(x1, x2);
        Max = Math.Max(x1, x2);
    }
}
