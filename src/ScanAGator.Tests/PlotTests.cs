using NUnit.Framework;
using System;
using System.Drawing;
using System.Linq;

namespace ScanAGator.Tests
{
    internal class PlotTests
    {
        [Test]
        public void Test_DeltaGreenOverRed_PlotCurve()
        {
            var lsFolder = SampleData.GreenOverRed();

            var plt = new ScottPlot.Plot(600, 400);

            // baseline
            double baselineTime1 = lsFolder.timesMsec[lsFolder.baseline1];
            double baselineTime2 = lsFolder.timesMsec[lsFolder.baseline2];
            plt.PlotHSpan(baselineTime1, baselineTime2, color: Color.Blue, alpha: .1);
            plt.PlotHLine(0, lineStyle: ScottPlot.LineStyle.Dash, color: Color.Black);

            // dGoR data points
            double[] xs = lsFolder.timesMsec;
            double[] dGoR = lsFolder.curveDeltaGoR;
            plt.PlotScatter(xs, dGoR, lineWidth: 0, color: Color.FromArgb(50, Color.Black));

            // low-pass-filtered data points
            double[] filteredXs = lsFolder.GetFilteredXs();
            double[] filteredDGoR = lsFolder.GetFilteredYs(lsFolder.curveDeltaGoR);
            plt.PlotScatter(filteredXs, filteredDGoR, markerSize: 0, lineWidth: 2, color: Color.Black);

            // peak dGoR
            double peakDGoR = filteredDGoR.Max();
            plt.PlotHLine(peakDGoR, color: Color.Red, lineStyle: ScottPlot.LineStyle.Dash);

            // customize the plot
            plt.YLabel("dG/R");
            plt.XLabel("Time (milliseconds)");
            plt.SaveFig(TestContext.CurrentContext.TestDirectory + "/testGR_dGoR.png");
        }

        [Test]
        public void Test_GreenOnly_PlotCurve()
        {
            var lsFolder = SampleData.GreenOnly();

            var plt = new ScottPlot.Plot(600, 400);

            // baseline
            double baselineTime1 = lsFolder.timesMsec[lsFolder.baseline1];
            double baselineTime2 = lsFolder.timesMsec[lsFolder.baseline2];
            plt.PlotHSpan(baselineTime1, baselineTime2, color: Color.Blue, alpha: .1);
            plt.PlotHLine(0, lineStyle: ScottPlot.LineStyle.Dash, color: Color.Black);

            // green data points
            double[] xs = lsFolder.timesMsec;
            double[] green = lsFolder.curveDeltaG;
            plt.PlotScatter(xs, green, lineWidth: 0, color: Color.FromArgb(50, Color.Black));

            // low-pass-filtered data points
            double[] filteredXs = lsFolder.GetFilteredXs();
            double[] filteredDeltaG = lsFolder.GetFilteredYs(lsFolder.curveDeltaG);
            plt.PlotScatter(filteredXs, filteredDeltaG, markerSize: 0, lineWidth: 2, color: Color.Black);

            // customize the plot
            plt.YLabel("dG");
            plt.XLabel("Time (milliseconds)");
            plt.SaveFig(TestContext.CurrentContext.TestDirectory + "/testGR_dG.png");
        }

        [Test]
        public void Test_MultipleScan_Averaging()
        {
            LineScanFolder lsFolder = SampleData.MultipleGreenOverRed();

            var plt = new ScottPlot.Plot(600, 400);
        }
    }
}
