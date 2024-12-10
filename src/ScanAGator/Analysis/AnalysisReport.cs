using ScanAGator.Imaging;
using System.Drawing;
using System.IO;
using System.Text;

namespace ScanAGator.Analysis;

public static class AnalysisReport
{
    public static string Generate(AnalysisResult result)
    {
        StringBuilder sb = new();
        sb.AppendLine("<!doctype html>");
        sb.AppendLine("<html lang=\"en\">");
        sb.AppendLine("<head>");
        sb.AppendLine("<meta charset=\"utf-8\">");
        sb.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">");
        sb.AppendLine("<link href=\"http://192.168.1.9/css/bootstrap.min.css\" rel=\"stylesheet\">");
        sb.AppendLine("<style>img{max-width: 100%;}</style>");
        sb.AppendLine("</head>");
        sb.AppendLine("<body>");
        sb.AppendLine("<div class='container my-4'>");
        AddRecordingInfo(result, sb);

        sb.AppendLine("<div class='row my-5'>");
        sb.AppendLine("<div class='col-6'>");
        AddGreenOverlap(result, sb);
        sb.AppendLine("</div>");
        sb.AppendLine("<div class='col-6'>");
        AddGreenConsecutive(result, sb);
        sb.AppendLine("</div>");
        sb.AppendLine("</div>");

        sb.AppendLine("<div class='row my-5'>");
        sb.AppendLine("<div class='col-6'>");
        AddRedOverlap(result, sb);
        sb.AppendLine("</div>");
        sb.AppendLine("<div class='col-6'>");
        AddRedConsecutive(result, sb);
        sb.AppendLine("</div>");
        sb.AppendLine("</div>");

        sb.AppendLine("<div class='row my-5'>");
        sb.AppendLine("<div class='col-6'>");
        AddRatioOverlap(result, sb);
        sb.AppendLine("</div>");
        sb.AppendLine("<div class='col-6'>");
        AddRatioConsecutive(result, sb);
        sb.AppendLine("</div>");
        sb.AppendLine("</div>");

        sb.AppendLine("</div>");
        sb.AppendLine("</body>");
        sb.AppendLine("</html>");

        string outputFolder = result.Settings.GetOutputFolder();
        string htmlFilePath = Path.Combine(outputFolder, "curves.html");
        File.WriteAllText(htmlFilePath, sb.ToString());

        return htmlFilePath;
    }

    private static void AddRecordingInfo(AnalysisResult result, StringBuilder sb)
    {
        string title = Path.GetFileName(result.Settings.Xml.FolderPath);
        sb.AppendLine($"<h1>{title}</h1>");
        sb.AppendLine($"<div class='mb-5'><code>{result.Settings.Xml.FolderPath}</code></div>");
    }

    #region Overlaps

    private static void AddGreenOverlap(AnalysisResult result, StringBuilder sb)
    {
        ScottPlot.Plot plot = new(600, 400);

        for (int i = 0; i < result.Settings.SecondaryImages.Length; i++)
        {
            RatiometricImage img = result.Settings.SecondaryImages[i];
            AnalysisResult curve = new(img, result.Settings);

            var scatter = plot.AddScatterLines(curve.SmoothGreenCurve.Times, curve.SmoothGreenCurve.Values);
            scatter.OnNaN = ScottPlot.Plottable.ScatterPlot.NanBehavior.Ignore;
            scatter.LineColor = Color.FromArgb(200, Color.Green);
        }

        plot.Title("Overlapping Green Traces");
        plot.YLabel("PMT Value (AFU)");
        plot.XLabel("Time (milliseconds)");
        sb.AppendLine(plot.GetImageHTML());
    }

    private static void AddRedOverlap(AnalysisResult result, StringBuilder sb)
    {
        ScottPlot.Plot plot = new(600, 400);

        for (int i = 0; i < result.Settings.SecondaryImages.Length; i++)
        {
            RatiometricImage img = result.Settings.SecondaryImages[i];
            AnalysisResult curve = new(img, result.Settings);

            var scatter = plot.AddScatterLines(curve.SmoothRedCurve.Times, curve.SmoothRedCurve.Values);
            scatter.OnNaN = ScottPlot.Plottable.ScatterPlot.NanBehavior.Ignore;
            scatter.LineColor = Color.FromArgb(200, Color.Red);
        }

        plot.Title("Overlapping Red Traces");
        plot.YLabel("PMT Value (AFU)");
        plot.XLabel("Time (milliseconds)");
        sb.AppendLine(plot.GetImageHTML());
    }

    private static void AddRatioOverlap(AnalysisResult result, StringBuilder sb)
    {
        ScottPlot.Plot plot = new(600, 400);

        for (int i = 0; i < result.Settings.SecondaryImages.Length; i++)
        {
            RatiometricImage img = result.Settings.SecondaryImages[i];
            AnalysisResult curve = new(img, result.Settings);

            var scatter = plot.AddScatterLines(curve.SmoothDeltaGreenOverRedCurve.Times, curve.SmoothDeltaGreenOverRedCurve.Values);
            scatter.OnNaN = ScottPlot.Plottable.ScatterPlot.NanBehavior.Ignore;
            scatter.LineColor = Color.FromArgb(200, plot.Palette.GetColor(0));
        }

        plot.Title("Overlapping G/R Traces");
        plot.YLabel("PMT Value (AFU)");
        plot.XLabel("Time (milliseconds)");
        sb.AppendLine(plot.GetImageHTML());
    }

    #endregion

    #region Consecutive

    private static void AddGreenConsecutive(AnalysisResult result, StringBuilder sb)
    {
        ScottPlot.Plot plot = new(600, 400);

        for (int i = 0; i < result.Settings.SecondaryImages.Length; i++)
        {
            RatiometricImage img = result.Settings.SecondaryImages[i];
            AnalysisResult curve = new(img, result.Settings);

            var scatter = plot.AddScatterLines(curve.SmoothGreenCurve.Times, curve.SmoothGreenCurve.Values);
            scatter.OnNaN = ScottPlot.Plottable.ScatterPlot.NanBehavior.Ignore;
            scatter.LineColor = Color.FromArgb(200, Color.Green);
            scatter.OffsetX = i * 15000;
        }

        plot.Title("Consecutive Green Traces");
        plot.YLabel("PMT Value (AFU)");
        plot.XLabel("Time (milliseconds)");
        sb.AppendLine(plot.GetImageHTML());
    }

    private static void AddRedConsecutive(AnalysisResult result, StringBuilder sb)
    {
        ScottPlot.Plot plot = new(600, 400);

        for (int i = 0; i < result.Settings.SecondaryImages.Length; i++)
        {
            RatiometricImage img = result.Settings.SecondaryImages[i];
            AnalysisResult curve = new(img, result.Settings);

            var scatter = plot.AddScatterLines(curve.SmoothRedCurve.Times, curve.SmoothRedCurve.Values);
            scatter.OnNaN = ScottPlot.Plottable.ScatterPlot.NanBehavior.Ignore;
            scatter.LineColor = Color.FromArgb(200, Color.Red);
            scatter.OffsetX = i * 15000;
        }

        plot.Title("Consecutive Red Traces");
        plot.YLabel("PMT Value (AFU)");
        plot.XLabel("Time (milliseconds)");
        sb.AppendLine(plot.GetImageHTML());
    }

    private static void AddRatioConsecutive(AnalysisResult result, StringBuilder sb)
    {
        ScottPlot.Plot plot = new(600, 400);

        for (int i = 0; i < result.Settings.SecondaryImages.Length; i++)
        {
            RatiometricImage img = result.Settings.SecondaryImages[i];
            AnalysisResult curve = new(img, result.Settings);

            var scatter = plot.AddScatterLines(curve.SmoothDeltaGreenOverRedCurve.Times, curve.SmoothDeltaGreenOverRedCurve.Values);
            scatter.OnNaN = ScottPlot.Plottable.ScatterPlot.NanBehavior.Ignore;
            scatter.LineColor = Color.FromArgb(200, plot.Palette.GetColor(0));
            scatter.OffsetX = i * 15000;
        }

        plot.Title("Consecutive G/R Traces");
        plot.YLabel("PMT Value (AFU)");
        plot.XLabel("Time (milliseconds)");
        sb.AppendLine(plot.GetImageHTML());
    }

    #endregion
}
