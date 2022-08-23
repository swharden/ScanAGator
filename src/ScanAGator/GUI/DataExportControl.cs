using BitMiracle.LibTiff.Classic;
using ScanAGator.Analysis;
using ScottPlot.Drawing.Colormaps;
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

        private void btnCopyPeak_Click(object sender, EventArgs e)
        {
            if (Result is null)
                return;

            Clipboard.SetText(Result.SmoothDeltaGreenOverRedCurve.GetPeak().ToString());
        }

        private void btnCopyXs_Click(object sender, EventArgs e)
        {
            if (Result is null)
                return;

            CopyValues(Result.SmoothDeltaGreenOverRedCurve.GetTimes());
        }

        private void btnCopyGoR_Click(object sender, EventArgs e)
        {
            if (Result is null)
                return;

            CopyValues(Result.SmoothDeltaGreenOverRedCurve.Values);
        }

        private void CopyValues(double[] values)
        {
            Clipboard.SetText(string.Join("\n", values.Select(x => x.ToString())));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Result is null)
                return;

            Result.Save();
        }

        private void btnSaveAndCopy_Click(object sender, EventArgs e)
        {
            if (Result is null)
                return;

            string csvPath = Result.Save();

            Clipboard.SetText($"LoadLinescanCSV \"{csvPath}\";");
        }
    }
}
