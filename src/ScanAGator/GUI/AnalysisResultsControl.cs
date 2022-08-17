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
        }

        public void ShowResult(Analysis.AnalysisResult result)
        {
            formsPlot1.Plot.Clear();
            formsPlot1.Plot.AddScatter(result.Xs, result.Green, Color.Green);
            formsPlot1.Plot.AddScatter(result.Xs, result.Red, Color.Red);
            formsPlot1.Plot.SetAxisLimits(yMin: 0);
            formsPlot1.Refresh();

            formsPlot2.Plot.Clear();
            var sp2 = formsPlot2.Plot.AddScatter(result.Xs, result.DeltaGreenOverRedPercent);
            sp2.OnNaN = ScottPlot.Plottable.ScatterPlot.NanBehavior.Ignore;
            formsPlot2.Refresh();
        }
    }
}
