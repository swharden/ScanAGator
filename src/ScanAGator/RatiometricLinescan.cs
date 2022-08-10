namespace ScanAGator;

public class RatiometricLinescan
{
    public readonly IntensityCurve G;
    public readonly IntensityCurve R;
    public readonly IntensityCurve DG;
    public readonly IntensityCurve DGR;
    public readonly double BaselineG;

    public RatiometricLinescan(ImageData green, ImageData red, double msPerPx, PixelRange baseline, PixelRange structure)
    {
        G = new(ImageDataTools.GetAverageTopdown(green, structure), msPerPx);
        R = new(ImageDataTools.GetAverageTopdown(red, structure), msPerPx);
        BaselineG = G.BaselineMean(baseline);
        DG = G.SubtractedBy(BaselineG);
        DGR = DG.DividedBy(R);
    }
}
