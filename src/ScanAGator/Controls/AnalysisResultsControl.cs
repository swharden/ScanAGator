using ScottPlot;
using ScottPlot.Plottable;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ScanAGator.GUI
{
    public partial class AnalysisResultsControl : UserControl
    {
        Analysis.AnalysisResult? Result;

        private double PeakDFF => Result is null
            ? double.NaN
            : Result.SmoothDeltaGreenOverRedCurve.GetPeak();

        public AnalysisResultsControl()
        {
            InitializeComponent();
            formsPlot2.AxesChanged += FormsPlot2_AxesChanged;
        }

        private void FormsPlot2_AxesChanged(object sender, EventArgs e)
        {
            formsPlot1.Plot.MatchAxis(formsPlot2.Plot, horizontal: true, vertical: false);
            formsPlot1.Refresh();
        }

        public void ShowResult(Analysis.AnalysisResult result)
        {
            Result = result;
            lblPeak.Text = $"{PeakDFF:N2}%";

            double[] xs = GetTimesMsec(result);

            formsPlot1.Plot.Clear();

            //formsPlot1.Plot.AddScatterPoints(xs, result.GreenCurve.Values, Color.FromArgb(20, Color.Green));
            //formsPlot1.Plot.AddScatterPoints(xs, result.RedCurve.Values, Color.FromArgb(20, Color.Red));

            formsPlot1.Plot.AddScatterLines(xs, result.SmoothGreenCurve.Values, Color.Green);
            formsPlot1.Plot.AddScatterLines(xs, result.SmoothRedCurve.Values, Color.Red);
            EnableNanOnScatterPlots(formsPlot1.Plot);
            ShadeBaseline(formsPlot1.Plot, result);
            formsPlot1.Plot.SetAxisLimits(yMin: 0);
            formsPlot1.Plot.YLabel("Fluorescence (AFU)");
            formsPlot1.Refresh();

            formsPlot2.Plot.Clear();
            formsPlot2.Plot.AddScatterLines(xs, result.SmoothDeltaGreenOverRedCurve.Values);
            EnableNanOnScatterPlots(formsPlot2.Plot);
            ShadeBaseline(formsPlot2.Plot, result);
            formsPlot2.Plot.AddHorizontalLine(0, Color.Black, 0, ScottPlot.LineStyle.Dash);
            formsPlot2.Plot.YLabel("ΔF/F (%)");
            formsPlot2.Plot.MatchLayout(formsPlot1.Plot);
            formsPlot2.Refresh();
        }

        private static void EnableNanOnScatterPlots(Plot plt)
        {
            plt.GetPlottables()
                .Where(x => x is ScatterPlot)
                .Cast<ScatterPlot>()
                .ToList()
                .ForEach(x => x.OnNaN = ScatterPlot.NanBehavior.Ignore);
        }

        private static void ShadeBaseline(Plot plt, Analysis.AnalysisResult result)
        {
            double x1 = (result.Settings.Baseline.Min - .5) * result.Settings.Xml.MsecPerPixel;
            double x2 = (result.Settings.Baseline.Max + .5) * result.Settings.Xml.MsecPerPixel;
            plt.AddHorizontalSpan(x1, x2, Color.FromArgb(20, Color.Black));
        }

        private double[] GetTimesMsec(Analysis.AnalysisResult result)
        {
            return DataGen.Consecutive(result.GreenCurve.Values.Length, result.Settings.Xml.MsecPerPixel);
        }

        private void btnCopyPeak_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Math.Round(PeakDFF, 5).ToString());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Result is not null)
            {
                string csvPath = Result.Save();
                Clipboard.SetText($"LoadLinescanCSV \"{csvPath}\";");
            }
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            if (Result is not null)
            {
                System.Diagnostics.Process.Start("explorer.exe", Result.Settings.GetOutputFolder());
            }
        }
    }
}
