using NUnit.Framework;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScanAGator.Analysis;
using ScottPlot.Plottable;
using ScottPlot;

namespace ScanAGator.Tests
{
    internal class AnalysisTests
    {
        [Test]
        public void Test_Analysis_Workflow()
        {
            // read data from disk
            Prairie.FolderContents pvFolder = new(SampleData.MultiFrameRatiometricFolderPath);
            Prairie.ParirieXmlFile pvXml = new(pvFolder.XmlFilePath);
            Imaging.RatiometricImages images = new(pvFolder);

            // prepare the data and settings to perform analysis
            Imaging.RatiometricImage averageImage = images.Average;
            AnalysisSettings settings = new(
                img: images.Average,
                img2: images.Frames,
                baseline: new BaselineRange(20, 60),
                structure: new StructureRange(21, 25),
                filterPx: 20,
                floorPercentile: 20,
                xml: pvXml);

            // execute the analysis
            AnalysisResult result = new(averageImage, settings);

            // display result
            PlotGreenAndRed(result);
            PlotDeltaGreenOverRed(result);
        }

        private void PlotGreenAndRed(AnalysisResult result)
        {
            ScottPlot.Plot plt = new();
            double[] xs = result.GreenCurve.Times;
            plt.AddScatterPoints(xs, result.GreenCurve.Values, Color.FromArgb(30, Color.Green));
            plt.AddScatterPoints(xs, result.RedCurve.Values, Color.FromArgb(30, Color.Red));
            plt.AddScatterLines(xs, result.SmoothGreenCurve.Values, Color.Green);
            plt.AddScatterLines(xs, result.SmoothRedCurve.Values, Color.Red);

            plt.AddHorizontalSpan(
                xMin: result.Settings.Baseline.Min * result.Settings.Xml.MsecPerPixel,
                xMax: result.Settings.Baseline.Max * result.Settings.Xml.MsecPerPixel,
                color: Color.FromArgb(20, Color.Black));

            plt.YLabel("Fluorescence (AFU)");

            EnableNanOnScatterPlots(plt);
            TestTools.SaveFig(plt, "PlotGreenAndRed.png");
        }

        private void PlotDeltaGreenOverRed(AnalysisResult result)
        {
            ScottPlot.Plot plt = new();
            plt.AddScatterLines(
                xs: result.SmoothDeltaGreenOverRedCurve.Times,
                ys: result.SmoothDeltaGreenOverRedCurve.Values);

            plt.AddHorizontalSpan(
                xMin: result.Settings.Baseline.Min * result.Settings.Xml.MsecPerPixel,
                xMax: result.Settings.Baseline.Max * result.Settings.Xml.MsecPerPixel,
                color: Color.FromArgb(20, Color.Black));

            plt.AddHorizontalLine(0, Color.Black, 1, ScottPlot.LineStyle.Dash);
            plt.YLabel("ΔF/F (%)");

            EnableNanOnScatterPlots(plt);
            TestTools.SaveFig(plt, "PlotDeltaGreenOverRed.png");
        }

        private static void EnableNanOnScatterPlots(Plot plt)
        {
            plt.GetPlottables()
                .Where(x => x is ScatterPlot)
                .Cast<ScatterPlot>()
                .ToList()
                .ForEach(x => x.OnNaN = ScatterPlot.NanBehavior.Ignore);
        }
    }
}
