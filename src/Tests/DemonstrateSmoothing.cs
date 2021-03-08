using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class DemonstrateSmoothing
    {
        private void SaveFigure(ScottPlot.Plot plt, string fileName)
        {
            string filePath = System.IO.Path.Combine(TestContext.CurrentContext.TestDirectory, fileName);
            filePath = System.IO.Path.GetFullPath(filePath);
            plt.SaveFig(filePath);
            Console.WriteLine(filePath);
        }

        [Test]
        public void Test_SmoothExample_PlotCurves()
        {
            var lsFolder = SampleData.GreenOverRed();
            double[] curve = lsFolder.curveDeltaGoR;
            double[] curveXs = ScottPlot.DataGen.Consecutive(curve.Length);

            double[] curveSmooth = ScanAGator.ImageDataTools.GaussianFilter1d(curve, lsFolder.filterPx);
            double[] curveSmoothXs = ScottPlot.DataGen.Consecutive(curveSmooth.Length);

            var plt = new ScottPlot.Plot();
            plt.PlotScatter(curveXs, curve, lineWidth: 0);
            plt.PlotScatter(curveSmoothXs, curveSmooth);
            plt.PlotVLine(lsFolder.filterPx * 2, Color.Red);
            plt.PlotVLine(curve.Length - lsFolder.filterPx * 2, Color.Red);
            SaveFigure(plt, "smooth_curve.png");
        }

        [Test]
        public void Test_SmoothExample_PlotGaussianWindow()
        {
            var lsFolder = SampleData.GreenOverRed();
            int windowSize = lsFolder.filterPx * 2 - 1;
            double[] curve = ScanAGator.ImageDataTools.GetGaussianCurve(windowSize, half: true);
            double[] curveXs = ScottPlot.DataGen.Consecutive(curve.Length);

            var plt = new ScottPlot.Plot();
            plt.PlotScatter(curveXs, curve);

            SaveFigure(plt, "smooth_gaussian_kernel.png");
        }
    }
}