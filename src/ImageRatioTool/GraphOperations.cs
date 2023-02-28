using ScottPlot;

namespace ImageRatioTool;

internal static class GraphOperations
{
    public static void PlotIntensities(FormsPlot fp, RoiAnalysis roi)
    {
        fp.Plot.Clear();

        fp.Plot.AddSignal(roi.SortedValues);

        var floorLine = fp.Plot.AddHorizontalLine(roi.NoiseFloor, Color.Blue, 1, ScottPlot.LineStyle.Dot);
        floorLine.PositionLabel = true;
        floorLine.PositionLabelOppositeAxis = true;
        floorLine.PositionFormatter = (double x) => $"{x:N0}";
        floorLine.PositionLabelBackground = floorLine.Color;

        var thresholdLine = fp.Plot.AddHorizontalLine(roi.Threshold, Color.Green, 1, ScottPlot.LineStyle.Dash);
        thresholdLine.PositionLabel = true;
        thresholdLine.PositionLabelOppositeAxis = true;
        thresholdLine.PositionFormatter = (double x) => $"{x:N0}";
        thresholdLine.PositionLabelBackground = thresholdLine.Color;

        fp.Plot.YLabel("Intensity (AFU)");
        fp.Plot.Legend(true, Alignment.UpperLeft);
        fp.Plot.Layout(right: 50);
        fp.Refresh();
    }

    public static void PlotRatios(FormsPlot fp, RoiAnalysis roi)
    {
        if (roi.SortedRatios.Length == 0)
        {
            fp.Plot.Clear();
            fp.Refresh();
            return;
        }

        fp.Visible = true;
        fp.Plot.Clear();
        fp.Plot.AddSignal(roi.SortedRatios);

        fp.Plot.AddVerticalLine(roi.MedianIndex, Color.Black, 1, LineStyle.Dot);
        var hline = fp.Plot.AddHorizontalLine(roi.MedianRatio, Color.Black, 1, LineStyle.Dash);
        hline.PositionLabel = true;
        hline.PositionLabelOppositeAxis = true;
        hline.PositionFormatter = (double x) => $"{x * 100:N2}%";

        fp.Plot.YLabel("G/R Ratio");
        fp.Plot.Layout(right: 50);
        fp.Refresh();
    }
}
