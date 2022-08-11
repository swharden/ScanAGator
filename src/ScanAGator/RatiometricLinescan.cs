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
    public int Samples => G.Values.Length;
    public readonly PixelRange Baseline;
    public readonly PixelRange Structure;
    public double PointsPerSecond => 1.0 / SecPerPixel;
    public readonly int FilterSizePixels;
    public int FilterSpanPixels => FilterSizePixels * 2 + 1;

    public RatiometricLinescan(ImageData green, ImageData red, double msPerPx, LineScanSettings settings)
    {
        MsecPerPixel = msPerPx;
        FilterSizePixels = settings.FilterSizePixels;
        Baseline = settings.Baseline;
        Structure = settings.Structure;

        G = new(ImageDataTools.GetAverageTopdown(green, Structure), msPerPx);
        R = new(ImageDataTools.GetAverageTopdown(red, Structure), msPerPx);

        BaselineG = G.BaselineMean(Baseline); // calculate the mean from pre-filtered data

        if (FilterSizePixels > 0)
        {
            G = G.LowPassFiltered(FilterSizePixels);
            R = R.LowPassFiltered(FilterSizePixels);
        }

        DG = G.SubtractedBy(BaselineG);
        DGR = DG.DividedBy(R);
    }

    public void SaveCsv(string filePath)
    {
        System.Text.StringBuilder sb = new();
        sb.AppendLine("Time, G, R, ΔG/R");
        sb.AppendLine("ms, AFU, AFU, %");

        for (int i = 0; i < Samples; i++)
        {
            sb.AppendLine($"{i * MsecPerPixel:0.000}, {G.Values[i]:0.000}, {R.Values[i]:0.000}, {DGR.Values[i]:0.000}");
        }

        System.IO.File.WriteAllText(filePath, sb.ToString());
    }
}
