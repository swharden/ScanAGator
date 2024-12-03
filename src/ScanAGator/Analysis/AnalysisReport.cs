using ScanAGator.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
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
        sb.AppendLine("<link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH\" crossorigin=\"anonymous\">");
        sb.AppendLine("<style>img{max-width: 100%;}</style>");
        sb.AppendLine("</head>");
        sb.AppendLine("<body>");
        sb.AppendLine("<div class='container'>");
        AddRecordingInfo(result, sb);

        sb.AppendLine("<div class='row'>");
        sb.AppendLine("<div class='col-6'>");
        AddGreenOverlap(result, sb);
        sb.AppendLine("</div>");
        sb.AppendLine("<div class='col-6'>");
        AddGreenConsecutive(result, sb);
        sb.AppendLine("</div>");
        sb.AppendLine("</div>");

        sb.AppendLine("<div class='row'>");
        sb.AppendLine("<div class='col-6'>");
        AddRedOverlap(result, sb);
        sb.AppendLine("</div>");
        sb.AppendLine("<div class='col-6'>");
        AddRedConsecutive(result, sb);
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
}
