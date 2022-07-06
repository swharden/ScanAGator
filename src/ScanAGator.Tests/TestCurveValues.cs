using NUnit.Framework;
using System;
using System.Drawing;
using System.Linq;

namespace Tests
{
    public class TestCurveValues
    {
        private string LinescanFolder => TestContext.CurrentContext.TestDirectory + "/../../../../../data/linescans/";
        private string SavePrefix => TestContext.CurrentContext.TestDirectory + "/";

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
            plt.SaveFig(SavePrefix + "testGR_dGoR.png");
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
            plt.SaveFig(SavePrefix + "testGR_dG.png");
        }

        private static int SimpleHash(double[] input)
        {
            byte[] bytes = input.SelectMany(n => BitConverter.GetBytes(n)).ToArray();
            int hash = 0;
            foreach (byte b in bytes)
                hash = (hash * 31) ^ b;
            return hash;
        }

        [Test]
        public void Test_DeltaGreenOverRed_CheckCurveValues()
        {
            var lsFolder = SampleData.GreenOverRed();
            lsFolder.GenerateAnalysisCurves();

            double peakDeltaGreenOverRed = lsFolder.GetFilteredYs(lsFolder.curveDeltaGoR).Max();
            Assert.AreEqual(112.45, peakDeltaGreenOverRed, .1);

            Assert.AreEqual(-1684596658, SimpleHash(lsFolder.curveG));
            Assert.AreEqual(-1497736758, SimpleHash(lsFolder.curveR));
            Assert.AreEqual(-559501835, SimpleHash(lsFolder.curveGoR));
            Assert.AreEqual(-33284870, SimpleHash(lsFolder.curveDeltaG));
            Assert.AreEqual(-307337996, SimpleHash(lsFolder.curveDeltaGoR));
        }

        [Test]
        public void Test_GreenOnly_CheckCurveValues()
        {
            var lsFolder = SampleData.GreenOnly();
            lsFolder.GenerateAnalysisCurves();

            Assert.AreEqual(241425423, SimpleHash(lsFolder.curveG));
            Assert.AreEqual(1707214479, SimpleHash(lsFolder.curveDeltaG));
        }

        [Test]
        public void Test_Metadata_Export()
        {
            string lsFolderPath = System.IO.Path.Combine(LinescanFolder, "LineScan-03272018-1330-2145");
            var lsFolder = new ScanAGator.LineScanFolder(lsFolderPath);

            string metadata = lsFolder.GetMetadataJson();
            Assert.That(metadata, Is.Not.Null);
            Assert.That(metadata, Is.Not.Empty);
            Console.WriteLine(metadata);
        }
    }
}