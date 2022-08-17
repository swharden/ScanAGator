using NUnit.Framework;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanAGator.Tests
{
    internal class AnalysisTests
    {
        [Test]
        public void Test_Analysis_Workflow()
        {
            // user defines settings
            PixelRange baseline = new(20, 60);
            PixelRange structure = new(21, 25);
            int lowpassFilterPixels = 20;

            // images loaded and turned into raw curves
            ImageData greenImage = ImageDataTools.ReadTif(SampleData.GreenLinescanImagePath);
            ImageData redImage = ImageDataTools.ReadTif(SampleData.RedLinescanImagePath);

            IntensityCurve green = new(greenImage, structure);
            IntensityCurve red = new(redImage, structure);

            // green baseline calculated from raw green curve
            double baselineGreen = green.GetMean(baseline);

            // calculate smooth curves
            IntensityCurve smoothGreen = green.LowPassFiltered(lowpassFilterPixels);
            IntensityCurve smoothRed = red.LowPassFiltered(lowpassFilterPixels);

            // calculate ratios from smoothed curves
            IntensityCurve smoothDeltaGreen = smoothGreen - baselineGreen;
            IntensityCurve smoothDeltaGreenOverRed = smoothDeltaGreen / smoothRed * 100;

            // display result
            PlotGreenAndRed(green, red, smoothGreen, smoothRed);
            PlotDeltaGreenOverRed(smoothDeltaGreenOverRed, baseline);
        }

        private void PlotGreenAndRed(IntensityCurve green, IntensityCurve red, IntensityCurve smoothGreen, IntensityCurve smoothRed)
        {
            ScottPlot.Plot plt = new();
            double[] xs = green.GetTimes();
            plt.AddScatterPoints(xs, green.Values, Color.FromArgb(30, Color.Green));
            plt.AddScatterPoints(xs, red.Values, Color.FromArgb(30, Color.Red));
            plt.AddScatterLines(xs, smoothGreen.Values, Color.Green);
            plt.AddScatterLines(xs, smoothRed.Values, Color.Red);
            plt.YLabel("Fluorescence (AFU)");
            Console.WriteLine(plt.SaveFig("PlotGreenAndRed.png"));
        }

        private void PlotDeltaGreenOverRed(IntensityCurve dgor, PixelRange baseline)
        {
            ScottPlot.Plot plt = new();
            double[] xs = dgor.GetTimes();
            plt.AddScatterLines(xs, dgor.Values);
            plt.AddHorizontalSpan(baseline.Min, baseline.Max, Color.FromArgb(20, Color.Black));
            plt.AddHorizontalLine(0, Color.Black, 1, ScottPlot.LineStyle.Dash);
            plt.YLabel("ΔF/F (%)");
            Console.WriteLine(plt.SaveFig("PlotDeltaGreenOverRed.png"));
        }
    }
}
