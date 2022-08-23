using System;
using System.Linq;
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
            double peak = result.Curves.SmoothDeltaGreenOverRedCurve.GetPeak();
            lblPeak.Text = $"{peak:N2}%";
        }

        private void btnCopyPeak_Click(object sender, EventArgs e)
        {
            if (Result is null)
                return;

            Clipboard.SetText(Result.Curves.SmoothDeltaGreenOverRedCurve.GetPeak().ToString());
        }

        private void btnCopyXs_Click(object sender, EventArgs e)
        {
            if (Result is null)
                return;

            CopyValues(Result.Curves.SmoothDeltaGreenOverRedCurve.Times);
        }

        private void btnCopyGoR_Click(object sender, EventArgs e)
        {
            if (Result is null)
                return;

            CopyValues(Result.Curves.SmoothDeltaGreenOverRedCurve.Values);
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Delete all old analysis files?", 
                "Delete Files", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                Result?.ClearOutputFolder();
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (Result is null)
                return;

            System.Diagnostics.Process.Start("explorer.exe", Result.GetOutputFolder());
        }
    }
}
