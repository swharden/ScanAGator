using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScanAGator.GUI
{
    public partial class AnalysisResultsControl : UserControl
    {
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
            double samplePeriod = result.Settings.Xml.MsecPerPixel;
            double sampleRate = 1.0 / samplePeriod;
            double[] xs = ScottPlot.DataGen.Consecutive(result.GreenCurve.Values.Length, samplePeriod);

            formsPlot1.Plot.Clear();
            formsPlot1.Plot.AddScatterPoints(xs, result.GreenCurve.Values, Color.FromArgb(30, Color.Green));
            formsPlot1.Plot.AddScatterPoints(xs, result.RedCurve.Values, Color.FromArgb(30, Color.Red));
            formsPlot1.Plot.AddSignal(result.SmoothGreenCurve.Values, sampleRate, Color.Green);
            formsPlot1.Plot.AddSignal(result.SmoothRedCurve.Values, sampleRate, Color.Red);
            formsPlot1.Plot.YLabel("Fluorescence (AFU)");
            formsPlot1.Refresh();

            formsPlot2.Plot.Clear();
            formsPlot2.Plot.AddSignal(result.SmoothDeltaGreenOverRedCurve.Values, sampleRate);
            formsPlot2.Plot.AddHorizontalSpan(
                xMin: result.Settings.Baseline.Min * samplePeriod, 
                xMax: result.Settings.Baseline.Max * samplePeriod, 
                color: Color.FromArgb(20, Color.Black));
            formsPlot2.Plot.AddHorizontalLine(0, Color.Black, 0, ScottPlot.LineStyle.Dash);
            formsPlot2.Plot.YLabel("ΔF/F (%)");
            formsPlot2.Refresh();
        }
    }
}
