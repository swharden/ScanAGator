using System;
using System.IO;
using System.Drawing;
using System.Linq;

namespace ScanAGator;

public static class Plot
{
    public static void PlotDGoRAverage(double[] avg, double[] stdev, double sampleRate, int filterSizePx, string title, string saveAs)
    {
        ScottPlot.Plot plt = new(600, 400);

        double[] xs = Enumerable.Range(0, avg.Length)
            .Select(x => x * (1.0 / sampleRate))
            .ToArray();

        int subIndex1 = filterSizePx * 2 + 1;
        int subIndex2 = avg.Length - 1 - subIndex1;
        int subLength = subIndex2 - subIndex1;

        double[] xs2 = new double[subLength];
        Array.Copy(xs, subIndex1, xs2, 0, subLength);

        double[] avg2 = new double[subLength];
        Array.Copy(avg, subIndex1, avg2, 0, subLength);

        double[] stdev2 = new double[subLength];
        Array.Copy(stdev, subIndex1, stdev2, 0, subLength);

        var spErr = plt.AddErrorBars(xs2, avg2, null, stdev2, ColorTranslator.FromHtml("#C0C0C0"));

        var spAvg = plt.AddScatterLines(xs2, avg2, Color.Black);

        plt.AddHorizontalLine(0, Color.Black, 1, ScottPlot.LineStyle.Dash);
        plt.Title(title);
        plt.YLabel("ΔG/R (%)");
        plt.XLabel("Time (sec)");
        plt.Legend(true, ScottPlot.Alignment.UpperRight);
        plt.SaveFig(saveAs);
    }

    public static ScottPlot.Plot AFU(IntensityCurve curve)
    {
        ScottPlot.Plot plt = new();
        double[] xs = curve.GetTimes();
        plt.AddScatter(xs, curve.Values);
        return plt;
    }
}
