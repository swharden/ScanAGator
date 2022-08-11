using System;
using System.IO;
using System.Drawing;

namespace ScanAGator;

public static class Plot
{
    public static void PlotRaw(RatiometricLinescan[] linescans, string title, string saveAs)
    {
        ScottPlot.Plot plt = new(600, 400);

        foreach (RatiometricLinescan ls in linescans)
        {
            var sig1 = plt.AddSignal(ls.G.Values, ls.PointsPerSecond, Color.FromArgb(128, Color.Green));
            sig1.MinRenderIndex = ls.FilterSpanPixels;
            sig1.MaxRenderIndex = sig1.Ys.Length - 1 - ls.FilterSpanPixels;

            var sig2 = plt.AddSignal(ls.R.Values, ls.PointsPerSecond, Color.FromArgb(128, Color.Red));
            sig2.MinRenderIndex = ls.FilterSpanPixels;
            sig2.MaxRenderIndex = sig1.Ys.Length - 1 - ls.FilterSpanPixels;
        }

        plt.Title(title);
        plt.YLabel("PMT Value (AFU)");
        plt.XLabel("Time (sec)");
        plt.SaveFig(saveAs);
    }

    public static void PlotDeltaGreen(RatiometricLinescan[] linescans, string title, string saveAs)
    {
        ScottPlot.Plot plt = new(600, 400);

        for (int i = 0; i < linescans.Length; i++)
        {
            RatiometricLinescan ls = linescans[i];
            var sig1 = plt.AddSignal(ls.DG.Values, ls.PointsPerSecond);
            sig1.MinRenderIndex = ls.FilterSpanPixels;
            sig1.MaxRenderIndex = sig1.Ys.Length - 1 - ls.FilterSpanPixels;
            sig1.Label = $"Frame {i + 1}";
        }

        AddBaseline(plt, linescans[0]);
        plt.Title(title);
        plt.YLabel("Δ Green (AFU)");
        plt.XLabel("Time (sec)");
        plt.Legend(true, ScottPlot.Alignment.UpperRight);
        plt.SaveFig(saveAs);
    }

    public static void PlotDGoR(RatiometricLinescan[] linescans, string title, string saveAs)
    {
        ScottPlot.Plot plt = new(600, 400);

        for (int i = 0; i < linescans.Length; i++)
        {
            RatiometricLinescan ls = linescans[i];
            var sig1 = plt.AddSignal(ls.DGR.Values, ls.PointsPerSecond, label: $"Frame {i + 1}");
            sig1.MinRenderIndex = ls.FilterSpanPixels;
            sig1.MaxRenderIndex = sig1.Ys.Length - 1 - ls.FilterSpanPixels;
        }

        AddBaseline(plt, linescans[0]);
        plt.Title(title);
        plt.YLabel("ΔG/R (%)");
        plt.XLabel("Time (sec)");
        plt.Legend(true, ScottPlot.Alignment.UpperRight);
        plt.SaveFig(saveAs);
    }

    public static void PlotDGoRAverage(RatiometricLinescan ls, string title, string saveAs)
    {
        ScottPlot.Plot plt = new(600, 400);

        var sig1 = plt.AddSignal(ls.DGR.Values, ls.PointsPerSecond);
        sig1.MinRenderIndex = ls.FilterSpanPixels;
        sig1.MaxRenderIndex = sig1.Ys.Length - 1 - ls.FilterSpanPixels;

        AddBaseline(plt, ls);
        plt.Title(title);
        plt.YLabel("ΔG/R (%)");
        plt.XLabel("Time (sec)");
        plt.Legend(true, ScottPlot.Alignment.UpperRight);
        plt.SaveFig(saveAs);
    }

    private static void AddBaseline(ScottPlot.Plot plt, RatiometricLinescan linescan)
    {
        double baselineStart = linescan.Baseline.FirstPixel * linescan.SecPerPixel;
        double baselineEnd = linescan.Baseline.LastPixel * linescan.SecPerPixel;
        System.Diagnostics.Debug.WriteLine($"START: {baselineStart}");
        System.Diagnostics.Debug.WriteLine($"END: {baselineEnd}");
        plt.AddHorizontalSpan(baselineStart, baselineEnd, Color.FromArgb(20, Color.Black));
        plt.AddHorizontalLine(0, Color.Black, 1, ScottPlot.LineStyle.Dash);
    }
}
