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
    public partial class DataExportControl : UserControl
    {
        Analysis.AnalysisResult? Result;

        public DataExportControl()
        {
            InitializeComponent();
        }

        public void ShowResult(Analysis.AnalysisResult result)
        {
            Result = result;
            double peak = result.SmoothDeltaGreenOverRedCurve.GetPeak();
            lblPeak.Text = $"{peak:N2}%";
        }
    }
}
