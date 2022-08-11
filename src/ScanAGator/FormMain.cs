using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScanAGator
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            System.IO.Stream iconStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ScanAGator.icon64.ico");
            if (iconStream != null) Icon = new Icon(iconStream);

            Version ver = typeof(LineScanFolder).Assembly.GetName().Version;
            Text = $"Scan-A-Gator v{ver.Major}.{ver.Minor}";
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            string developerStartupFolder1 = @"X:\Data\OT-Cre\calcium-mannitol\2020-02-13 puff MT 2P\20218000\distal_MT_1sp_2";
            string developerStartupFolder2 = @"X:\Data\OTR-Cre\GCaMP6f PFC injection patch and linescan\2019-02-20\slice1";

            if (System.IO.Directory.Exists(developerStartupFolder1))
                SetFolder(developerStartupFolder1);
            else if (System.IO.Directory.Exists(developerStartupFolder2))
                SetFolder(developerStartupFolder2);
            else
                SetFolder("./");
        }

        public LineScanFolder lsFolder;
        private bool ignoreGuiUpdates;
        private void SetFolder(string path, bool updateTree = true)
        {
            // stop people from clicking on a new path while this one is loading
            treeViewDirUC1.Enabled = false;

            if (updateTree)
            {
                // update the tree (will can an event to re-call fhis function)
                treeViewDirUC1.SelectPath(path);
                return;
            }

            lsFolder = new LineScanFolder(path, analyzeFirstFrameImmediately: false);
            UpdateGuiFromLinescanFirst();
            UpdateGuiFromLinescan();
            if (!lsFolder.IsValid)
                SaveNeeded(false);
            if (lsFolder.IsValid && System.IO.File.Exists(lsFolder.IniFilePath))
                SaveNeeded(false);

            // unlock the treeview
            treeViewDirUC1.Enabled = true;
        }

        public void UpdateGuiFromLinescanFirst()
        {
            // call this when first loading a linescan to set limits and control gray-states

            ignoreGuiUpdates = true;

            // reference image
            if (lsFolder.IsValid)
            {
                pbRef.BackgroundImage = lsFolder.GetRefImage(0);
                hScrollRef.Maximum = lsFolder.ReferenceImagePaths.Length - 1;
            }

            // lock or unlock settings
            gbDisplay.Enabled = lsFolder.IsValid;
            gbFrame.Enabled = lsFolder.IsValid;
            gbFilter.Enabled = lsFolder.IsValid;
            gbBaseline.Enabled = lsFolder.IsValid;
            gbStructure.Enabled = lsFolder.IsValid;
            gbSettings.Enabled = lsFolder.IsValid;
            gbAuto.Enabled = lsFolder.IsValid;
            gbDisplayType.Enabled = lsFolder.IsValid;
            gbPeak.Enabled = lsFolder.IsValid;
            cbRatio.Enabled = lsFolder.IsValid && lsFolder.IsRatiometric;
            formsPlot1.Visible = lsFolder.IsValid;
            formsPlot2.Visible = lsFolder.IsValid;
            dataToolStripMenuItem.Enabled = lsFolder.IsValid;

            if (lsFolder.IsRatiometric)
            {
                cbRatio.Checked = true;
                cbR.Checked = true;
                cbG.Checked = true;
            }
            else
            {
                cbG.Checked = true;
            }

            if (lsFolder.IsValid)
            {
                cbG.Enabled = (lsFolder.DataImagePathsG.Length > 0) ? true : false;
                cbR.Enabled = (lsFolder.DataImagePathsR.Length > 0) ? true : false;

                if (lsFolder.IsRatiometric)
                {
                    cbRatio.Checked = true;
                    cbDelta.Checked = true;
                }
                else
                {
                    cbDelta.Checked = true;
                }

                nudFrame.Minimum = 1;
                nudFrame.Maximum = lsFolder.DataImagePathsG.Length;
                gbFrame.Text = $"Frame (of {lsFolder.DataImagePathsG.Length})";
                if (lsFolder.DataImagePathsG.Length == 1)
                    nudFrame.Enabled = false;
                else
                    nudFrame.Enabled = true;

                nudFilter.Maximum = (int)(lsFolder.BmpDataG.Height / 3.0);

                nudBaseline1.Maximum = lsFolder.BmpDataG.Height;
                nudBaseline2.Maximum = lsFolder.BmpDataG.Height;
                tbBaseline1.Maximum = lsFolder.BmpDataG.Height;
                tbBaseline2.Maximum = lsFolder.BmpDataG.Height;

                nudStructure1.Maximum = lsFolder.BmpDataG.Width;
                nudStructure2.Maximum = lsFolder.BmpDataG.Width;
                tbStructure1.Maximum = lsFolder.BmpDataG.Width;
                tbStructure2.Maximum = lsFolder.BmpDataG.Width;
            }
            ignoreGuiUpdates = false;
        }

        public void UpdateGuiFromLinescan()
        {
            // update control values from linescan object
            // AVOID PROCESSING DATA AND LOADING IMAGES!

            if (ignoreGuiUpdates)
                return;

            // linescan image
            if ((cbR.Checked && cbR.Enabled) && (cbG.Checked && cbG.Enabled))
                pbLinescan.BackgroundImage = lsFolder.MarkLinescan(lsFolder.BmpDataMerge);
            else if (cbR.Checked && cbR.Enabled)
                pbLinescan.BackgroundImage = lsFolder.MarkLinescan(lsFolder.BmpDataR);
            else if (cbG.Checked && cbG.Enabled)
                pbLinescan.BackgroundImage = lsFolder.MarkLinescan(lsFolder.BmpDataG);
            else
                pbLinescan.BackgroundImage = null;

            // update sliders and NUDs
            tbBaseline1.Value = tbBaseline1.Maximum - lsFolder.BaselineIndex1;
            tbBaseline2.Value = tbBaseline2.Maximum - lsFolder.BaselineIndex2;
            tbStructure1.Value = lsFolder.StructureIndex1;
            tbStructure2.Value = lsFolder.StructureIndex2;
            nudBaseline1.Value = lsFolder.BaselineIndex1;
            nudBaseline2.Value = lsFolder.BaselineIndex2;
            nudStructure1.Value = lsFolder.StructureIndex1;
            nudStructure2.Value = lsFolder.StructureIndex2;
            nudFilter.Value = lsFolder.FilterSizePixels;
            lblFilterMs.Text = lsFolder.IsValid ? $"{Math.Round(lsFolder.FilterSizePixels * lsFolder.ScanLinePeriodMsec, 2)} ms" : "None";

            // generate and plot curves
            UpdateGraphs();

            SaveNeeded(true);
        }

        public double[] curveToCopy;
        public bool busyUpdating = false;
        public void UpdateGraphs(bool skipUpdatesIfBusy = true)
        {
            // use pretty colors
            Color blue = System.Drawing.ColorTranslator.FromHtml("#1f77b4");
            Color lightBlue = System.Drawing.ColorTranslator.FromHtml("#a2d1f2");
            Color red = System.Drawing.ColorTranslator.FromHtml("#d62728");
            Color lightRed = System.Drawing.ColorTranslator.FromHtml("#e63738");
            Color green = System.Drawing.ColorTranslator.FromHtml("#2ca02c");
            Color lightGreen = System.Drawing.ColorTranslator.FromHtml("#98df8a");
            Color colorBaselineMarks = System.Drawing.ColorTranslator.FromHtml("#666666");
            Color colorPeak = System.Drawing.ColorTranslator.FromHtml("#d62728");
            Color colorZero = System.Drawing.ColorTranslator.FromHtml("#000000");

            if (!lsFolder.IsValid)
                return;

            if (skipUpdatesIfBusy && busyUpdating)
                return;
            else
                busyUpdating = true;

            // do the analysis
            lsFolder.GenerateAnalysisCurves();

            // update the structure profile plot
            double[] columnPixelIndices = new double[lsFolder.ImgG.width];
            for (int i = 0; i < columnPixelIndices.Length; i++)
                columnPixelIndices[i] = i;
            formsPlot2.plt.Clear();
            if ((cbR.Enabled && cbR.Checked))
            {
                double[] columnBrightnessR = ImageDataTools.GetAverageLeftright(lsFolder.ImgR);
                formsPlot2.plt.PlotScatter(columnPixelIndices, columnBrightnessR, markerSize: 0, color: red);
            }
            if ((cbG.Enabled && cbG.Checked))
            {
                double[] columnBrightnessG = ImageDataTools.GetAverageLeftright(lsFolder.ImgG);
                formsPlot2.plt.PlotScatter(columnPixelIndices, columnBrightnessG, markerSize: 0, color: green);
            }
            formsPlot2.plt.AxisAuto(0, .1);
            formsPlot2.plt.PlotVLine(lsFolder.StructureIndex1, color: Color.Black);
            formsPlot2.plt.PlotVLine(lsFolder.StructureIndex2, color: Color.Black);

            // update styling before the render
            formsPlot2.plt.YLabel("Intensity");
            formsPlot2.plt.XLabel("Position (pixel number)");
            formsPlot2.Render();

            // update the time series plot
            curveToCopy = null;
            formsPlot1.plt.Clear();
            if (cbRatio.Checked && cbRatio.Enabled)
            {
                if (cbDelta.Enabled && cbDelta.Checked)
                {
                    curveToCopy = lsFolder.GetFilteredYs(lsFolder.CurveDeltaGoR);
                    formsPlot1.plt.PlotScatter(lsFolder.timesMsec, lsFolder.CurveDeltaGoR, color: lightBlue, lineWidth: 0, markerSize: 2);
                    formsPlot1.plt.PlotScatter(lsFolder.GetFilteredXs(), curveToCopy, markerSize: 0, color: blue, lineWidth: 2);

                    formsPlot1.plt.PlotVLine(lsFolder.BaselineIndex1 * lsFolder.ScanLinePeriodMsec, color: colorBaselineMarks, lineWidth: 2);
                    formsPlot1.plt.PlotVLine(lsFolder.BaselineIndex2 * lsFolder.ScanLinePeriodMsec, color: colorBaselineMarks, lineWidth: 2);
                    formsPlot1.plt.PlotHLine(0, color: colorZero, lineWidth: 2);
                    formsPlot1.plt.PlotHLine(curveToCopy.Max(), color: colorPeak, lineWidth: 2);

                    formsPlot1.plt.YLabel("Delta G/R (%)");

                }
                else
                {
                    curveToCopy = lsFolder.GetFilteredYs(lsFolder.CurveGoR);
                    formsPlot1.plt.PlotScatter(lsFolder.timesMsec, lsFolder.CurveGoR, color: lightBlue, lineWidth: 0, markerSize: 2);
                    formsPlot1.plt.PlotScatter(lsFolder.GetFilteredXs(), curveToCopy, markerSize: 0, color: blue, lineWidth: 2);
                    formsPlot1.plt.YLabel("G/R (%)");
                    formsPlot1.plt.PlotHLine(curveToCopy.Max(), color: colorPeak, lineWidth: 2);
                }
            }
            else if ((cbR.Checked && cbR.Enabled) && (cbG.Checked && cbG.Enabled))
            {
                formsPlot1.plt.PlotScatter(lsFolder.timesMsec, lsFolder.CurveG, markerSize: 0, color: green);
                formsPlot1.plt.PlotScatter(lsFolder.timesMsec, lsFolder.CurveR, markerSize: 0, color: red);
                formsPlot1.plt.YLabel("G and R (AFU)");
            }
            else if (cbR.Checked && cbR.Enabled)
            {
                formsPlot1.plt.PlotScatter(lsFolder.timesMsec, lsFolder.CurveR, markerSize: 0, color: red);
                formsPlot1.plt.YLabel("R (AFU)");
            }
            else if (cbG.Checked && cbG.Enabled)
            {
                if (cbDelta.Enabled && cbDelta.Checked)
                {
                    curveToCopy = lsFolder.GetFilteredYs(lsFolder.CurveDeltaG);
                    formsPlot1.plt.PlotScatter(lsFolder.timesMsec, lsFolder.CurveDeltaG, lineWidth: 0, color: lightGreen, markerSize: 2);
                    formsPlot1.plt.PlotScatter(lsFolder.GetFilteredXs(), curveToCopy, markerSize: 0, color: green, lineWidth: 2);

                    formsPlot1.plt.PlotVLine(lsFolder.BaselineIndex1 * lsFolder.ScanLinePeriodMsec, color: colorBaselineMarks, lineWidth: 2);
                    formsPlot1.plt.PlotVLine(lsFolder.BaselineIndex2 * lsFolder.ScanLinePeriodMsec, color: colorBaselineMarks, lineWidth: 2);
                    formsPlot1.plt.PlotHLine(0, color: colorZero, lineWidth: 2);
                    formsPlot1.plt.PlotHLine(curveToCopy.Max(), color: colorPeak, lineWidth: 2);

                    formsPlot1.plt.YLabel("Delta G (%)");
                }
                else
                {
                    curveToCopy = lsFolder.GetFilteredYs(lsFolder.CurveG);
                    formsPlot1.plt.PlotScatter(lsFolder.timesMsec, lsFolder.CurveG, lineWidth: 0, color: lightGreen, markerSize: 2);
                    formsPlot1.plt.PlotScatter(lsFolder.GetFilteredXs(), curveToCopy, markerSize: 0, color: green, lineWidth: 2);
                    formsPlot1.plt.PlotHLine(curveToCopy.Max(), color: colorPeak, lineWidth: 2);
                    formsPlot1.plt.YLabel("G (AFU)");
                }

            }
            formsPlot1.plt.AxisAuto(.05, .1);

            // make ticks smaller than typical
            var tickFont = new Font(FontFamily.GenericMonospace, (float)10.0);
            //formsPlot1.plt.Ticks(font: tickFont);
            //formsPlot2.plt.Ticks(font: tickFont);

            // update styling before the render
            formsPlot1.plt.XLabel("Time (milliseconds)");
            formsPlot1.Render();

            // update peak label
            if (curveToCopy == null)
                lblPeak.Text = $"";
            else
                lblPeak.Text = $"{Math.Round(curveToCopy.Max(), 2)}%";

            Application.DoEvents();
            busyUpdating = false;
        }

        public void SaveNeeded(bool needed = true)
        {
            if (needed)
            {
                btnSave.BackColor = Color.Salmon;
            }
            else
            {
                btnSave.UseVisualStyleBackColor = true;
            }
        }

        #region drop-down menus

        private void documentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/swharden/Scan-A-Gator");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void setFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    SetFolder(fbd.SelectedPath);
                }
            }
        }

        private void refreshFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetFolder(lsFolder.FolderPath, true);
        }

        #endregion

        #region tree directory browser

        private void treeViewDirUC1_PathSelected(object sender, EventArgs e)
        {
            SetFolder(treeViewDirUC1.selectedPath, false);
        }

        #endregion

        #region GUI bindings

        private void cbR_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGuiFromLinescan();
        }

        private void cbG_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGuiFromLinescan();
        }

        private void cbRatio_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGuiFromLinescan();
        }

        private void cbDelta_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGuiFromLinescan();
        }

        private void tbStructure1_Scroll(object sender, EventArgs e)
        {
            if (!ignoreGuiUpdates)
                lsFolder.StructureIndex1 = tbStructure1.Value;
            UpdateGuiFromLinescan();
        }

        private void tbStructure2_Scroll(object sender, EventArgs e)
        {
            if (!ignoreGuiUpdates)
                lsFolder.StructureIndex2 = tbStructure2.Value;
            UpdateGuiFromLinescan();
        }

        private void tbBaseline1_Scroll(object sender, EventArgs e)
        {
            if (!ignoreGuiUpdates)
                lsFolder.BaselineIndex1 = tbBaseline1.Maximum - tbBaseline1.Value;
            UpdateGuiFromLinescan();
        }

        private void tbBaseline2_Scroll(object sender, EventArgs e)
        {
            if (!ignoreGuiUpdates)
                lsFolder.BaselineIndex2 = tbBaseline2.Maximum - tbBaseline2.Value;
            UpdateGuiFromLinescan();
        }

        private void nudFrame_ValueChanged(object sender, EventArgs e)
        {
            if (!ignoreGuiUpdates)
                lsFolder.SetFrame((int)nudFrame.Value - 1);
            UpdateGuiFromLinescan();
        }

        private void nudBaseline1_ValueChanged(object sender, EventArgs e)
        {
            if (!ignoreGuiUpdates)
                lsFolder.BaselineIndex1 = (int)nudBaseline1.Value;
            UpdateGuiFromLinescan();
        }

        private void nudBaseline2_ValueChanged(object sender, EventArgs e)
        {
            if (!ignoreGuiUpdates)
                lsFolder.BaselineIndex2 = (int)nudBaseline2.Value;
            UpdateGuiFromLinescan();
        }

        private void nudStructure1_ValueChanged(object sender, EventArgs e)
        {
            if (!ignoreGuiUpdates)
                lsFolder.StructureIndex1 = (int)nudStructure1.Value;
            UpdateGuiFromLinescan();
        }

        private void nudStructure2_ValueChanged(object sender, EventArgs e)
        {
            if (!ignoreGuiUpdates)
                lsFolder.StructureIndex2 = (int)nudStructure2.Value;
            UpdateGuiFromLinescan();
        }

        private void nudFilter_ValueChanged(object sender, EventArgs e)
        {
            if (!ignoreGuiUpdates)
                lsFolder.FilterSizePixels = (int)nudFilter.Value;
            UpdateGuiFromLinescan();
        }

        private void btnAutoBase_Click(object sender, EventArgs e)
        {
            lsFolder.AutoBaseline();
            UpdateGuiFromLinescan();
        }

        private void btnAutoStructure_Click(object sender, EventArgs e)
        {
            lsFolder.AutoStructure();
            UpdateGuiFromLinescan();
        }

        private void btnPeakCopy_Click(object sender, EventArgs e)
        {
            if (curveToCopy == null)
                return;
            Clipboard.SetText(Math.Round(curveToCopy.Max(), 3).ToString());
        }

        private void CopyCurve()
        {
            if (curveToCopy == null)
                return;

            var sb = new StringBuilder();
            foreach (var val in curveToCopy)
                sb.AppendLine($"{Math.Round(val, 3)}");

            Clipboard.SetText(sb.ToString());
        }

        private void btnCurveCopy_Click(object sender, EventArgs e) => CopyCurve();

        private void btnCopyAllData_Click(object sender, EventArgs e) => CopyAll();

        private void CopyAll()
        {
            string csv = lsFolder.GetCsvAllData();
            string tsv = csv.Replace(",", "\t");
            Clipboard.SetText(tsv);
        }

        private void SaveCSV()
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = $"{lsFolder.FolderName}.csv";
            savefile.Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*";
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(savefile.FileName, lsFolder.GetCsvAllData());
                System.IO.File.WriteAllText(savefile.FileName + ".json", lsFolder.GetMetadataJson());
            }
        }

        private void btnSaveAllData_Click(object sender, EventArgs e) => SaveCSV();

        private void btnSave_Click(object sender, EventArgs e) => SaveAnalysis();

        private void SaveAnalysis(bool launch = false)
        {
            if (!System.IO.Directory.Exists(lsFolder.SaveFolderPath))
                System.IO.Directory.CreateDirectory(lsFolder.SaveFolderPath);

            string csvFilePath = System.IO.Path.Combine(lsFolder.SaveFolderPath, "LineScanAnalysis.csv");

            lsFolder.SaveSettingsINI(); // TODO: collapse INI and JSON metadata file into one
            System.IO.File.WriteAllText(csvFilePath, lsFolder.GetCsvAllData());
            System.IO.File.WriteAllText(csvFilePath + ".json", lsFolder.GetMetadataJson());

            if (launch)
            {
                System.Diagnostics.Process.Start("explorer.exe", lsFolder.SaveFolderPath);
            }

            UpdateGuiFromLinescan();
            SaveNeeded(false);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            lsFolder.LoadSettingsINI();
            UpdateGuiFromLinescan();
            SaveNeeded(false);
        }

        private void hScrollRef_Scroll(object sender, ScrollEventArgs e)
        {
            pbRef.BackgroundImage = lsFolder.GetRefImage(hScrollRef.Value);
        }

        private void treeViewDirUC1_Load(object sender, EventArgs e)
        {

        }
        #endregion

        private void treeViewDirUC1_PathDragDropped(object sender, EventArgs e)
        {
            SetFolder(treeViewDirUC1.selectedPath, true);
        }

        private void copyCurveToolStripMenuItem_Click(object sender, EventArgs e) => CopyCurve();

        private void copyAllToolStripMenuItem_Click(object sender, EventArgs e) => CopyAll();

        private void saveCSVToolStripMenuItem_Click(object sender, EventArgs e) => SaveCSV();

        private void btnCopy_Click(object sender, EventArgs e) => CopyAll();

        private void btnExport_Click(object sender, EventArgs e) => SaveAnalysis(true);

        private void cbFrame_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
