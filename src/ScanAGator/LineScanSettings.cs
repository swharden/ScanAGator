﻿namespace ScanAGator;

public struct LineScanSettings
{
    public readonly PixelRange Baseline;
    public readonly PixelRange Structure;
    public readonly int FilterSizePixels;
    
    public LineScanSettings(PixelRange baseline, PixelRange structure, int filterSizePx)
    {
        Baseline = baseline;
        Structure = structure;
        FilterSizePixels = filterSizePx;
    }

    public LineScanSettings(int b1, int b2, int s1, int s2, int filterSizePx)
    {
        Baseline = new PixelRange(b1, b2);
        Structure = new PixelRange(s1, s2);
        FilterSizePixels = filterSizePx;
    }
}
