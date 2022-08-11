namespace ScanAGator;

public class RatiometricLinescan
{
    public readonly IntensityCurve G;
    public readonly IntensityCurve R;
    public readonly IntensityCurve DG;
    public readonly IntensityCurve DGR;
    public readonly double BaselineG;
    public readonly double MsecPerPixel;
    public double SecPerPixel => MsecPerPixel / 1000;
    public readonly PixelRange Baseline;
    public readonly PixelRange Structure;
    public double PointsPerSecond => 1.0 / SecPerPixel;
    public readonly int FilterSizePixels;
    public int FilterSpanPixels => FilterSizePixels * 2 + 1;

    public RatiometricLinescan(ImageData green, ImageData red, double msPerPx, PixelRange baseline, PixelRange structure, int filterSizePixels)
    {
        MsecPerPixel = msPerPx;
        FilterSizePixels = filterSizePixels;
        Baseline = baseline;
        Structure = structure;
        G = new(ImageDataTools.GetAverageTopdown(green, structure), msPerPx);
        R = new(ImageDataTools.GetAverageTopdown(red, structure), msPerPx);

        // calculate the mean from pre-filtered data
        BaselineG = G.BaselineMean(baseline);

        if (filterSizePixels > 0)
        {
            G = G.LowPassFiltered(filterSizePixels);
            R = R.LowPassFiltered(filterSizePixels);
        }

        DG = G.SubtractedBy(BaselineG);
        DGR = DG.DividedBy(R);
    }
}
