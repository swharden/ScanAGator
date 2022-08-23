using ScanAGator.Imaging;

namespace ScanAGator.Analysis;

public class AnalysisResultCurves
{
    public readonly IntensityCurve GreenCurve;
    public readonly IntensityCurve RedCurve;
    public readonly IntensityCurve SmoothGreenCurve;
    public readonly IntensityCurve SmoothRedCurve;
    public readonly IntensityCurve SmoothDeltaGreenCurve;
    public readonly IntensityCurve SmoothDeltaGreenOverRedCurve;

    public AnalysisResultCurves(RatiometricImage img, BaselineRange baseline, StructureRange structure, int filterPx, double period)
    {
        // get the intensity curves for the structure of interest
        GreenCurve = new IntensityCurve(img.GreenData, period, structure);
        RedCurve = new IntensityCurve(img.RedData, period, structure);

        // calculate smooth curves
        SmoothGreenCurve = GreenCurve.LowPassFiltered(filterPx);
        SmoothRedCurve = RedCurve.LowPassFiltered(filterPx);

        // calculate ratios from smoothed curves
        double greenCurveBaseline = GreenCurve.GetMean(baseline);
        SmoothDeltaGreenCurve = SmoothGreenCurve - greenCurveBaseline;
        SmoothDeltaGreenOverRedCurve = SmoothDeltaGreenCurve / SmoothRedCurve * 100;
    }
}
