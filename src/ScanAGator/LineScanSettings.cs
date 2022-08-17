namespace ScanAGator;

public struct LineScanSettings
{
    public readonly BaselineRange Baseline;
    public readonly StructureRange Structure;
    public readonly int FilterSizePixels;
    
    public LineScanSettings(BaselineRange baseline, StructureRange structure, int filterSizePx)
    {
        Baseline = baseline;
        Structure = structure;
        FilterSizePixels = filterSizePx;
    }

    public LineScanSettings(int b1, int b2, int s1, int s2, int filterSizePx)
    {
        Baseline = new BaselineRange(b1, b2);
        Structure = new StructureRange(s1, s2);
        FilterSizePixels = filterSizePx;
    }
}
