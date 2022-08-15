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
    public partial class FormMainV3 : Form
    {
        /// <summary>
        /// The currently selected folder contents and analyzed data
        /// </summary>
        LineScanFolder LineScanFolder;

        /// <summary>
        /// Flag used to block new UI inputs while a linescan is processing
        /// </summary>
        bool IgnoreGuiUpdates;

        /// <summary>
        /// Holds the curve which will get copied to the clipboard
        /// </summary>
        public double[] LatestCurve;

        /// <summary>
        /// Flag used to allow frame dropping if UI events take a long time to process
        /// </summary>
        public bool IsUpdating { get; set; } = false;

        public FormMainV3()
        {
            InitializeComponent();
            InitializeProgramIcon();
            Text = Versioning.GetVersionString();
        }

        private void InitializeProgramIcon()
        {
            System.IO.Stream iconStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ScanAGator.icon64.ico");
            if (iconStream != null)
                Icon = new Icon(iconStream);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            string[] initialFolders =
            {
                @"X:\Data\zProjects\Oxytocin Biosensor\experiments\2P bpAP NMDA\2022-08-09\2p",
                @"X:\Data\OT-Cre\calcium-mannitol\2020-02-13 puff MT 2P\20218000\distal_MT_1sp_2",
                @"X:\Data\OTR-Cre\GCaMP6f PFC injection patch and linescan\2019-02-20\slice1",
                "./"
            };

            foreach (string folderPath in initialFolders)
            {
                if (System.IO.Directory.Exists(folderPath))
                {
                    SetFolder(folderPath);
                    return;
                }
            }
        }

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

            LineScanFolder = new LineScanFolder(path, analyzeFirstFrameImmediately: false);
            UpdateGuiFromLinescanFirst();
            UpdateGuiFromLinescan();
            if (!LineScanFolder.IsValid)
                SaveNeeded(false);
            if (LineScanFolder.IsValid && System.IO.File.Exists(LineScanFolder.IniFilePath))
                SaveNeeded(false);

            // unlock the treeview
            treeViewDirUC1.Enabled = true;
        }

        public void UpdateGuiFromLinescanFirst()
        {
            // call this when first loading a linescan to set limits and control gray-states

            IgnoreGuiUpdates = true;

            // reference image
            if (LineScanFolder.IsValid)
            {
                pbRef.BackgroundImage = LineScanFolder.GetRefImage(0);
                hScrollRef.Maximum = LineScanFolder.ReferenceImagePaths.Length - 1;
            }

            // lock or unlock settings
            gbDisplay.Enabled = LineScanFolder.IsValid;
            gbFrame.Enabled = LineScanFolder.IsValid;
            gbFilter.Enabled = LineScanFolder.IsValid;
            gbBaseline.Enabled = LineScanFolder.IsValid;
            gbStructure.Enabled = LineScanFolder.IsValid;
            gbSettings.Enabled = LineScanFolder.IsValid;
            gbAuto.Enabled = LineScanFolder.IsValid;
            gbDisplayType.Enabled = LineScanFolder.IsValid;
            gbPeak.Enabled = LineScanFolder.IsValid;
            cbRatio.Enabled = LineScanFolder.IsValid && LineScanFolder.IsRatiometric;
            formsPlot1.Visible = LineScanFolder.IsValid;
            formsPlot2.Visible = LineScanFolder.IsValid;
            dataToolStripMenuItem.Enabled = LineScanFolder.IsValid;

            if (LineScanFolder.IsRatiometric)
            {
                cbRatio.Checked = true;
                cbR.Checked = true;
                cbG.Checked = true;
            }
            else
            {
                cbG.Checked = true;
            }

            if (LineScanFolder.IsValid)
            {
                cbG.Enabled = (LineScanFolder.DataImagePathsG.Length > 0) ? true : false;
                cbR.Enabled = (LineScanFolder.DataImagePathsR.Length > 0) ? true : false;

                if (LineScanFolder.IsRatiometric)
                {
                    cbRatio.Checked = true;
                    cbDelta.Checked = true;
                }
                else
                {
                    cbDelta.Checked = true;
                }

                nudFrame.Minimum = 1;
                nudFrame.Maximum = LineScanFolder.DataImagePathsG.Length;
                gbFrame.Text = $"Frame (of {LineScanFolder.DataImagePathsG.Length})";
                if (LineScanFolder.DataImagePathsG.Length == 1)
                    nudFrame.Enabled = false;
                else
                    nudFrame.Enabled = true;

                nudFilter.Maximum = (int)(LineScanFolder.BmpDataG.Height / 3.0);

                nudBaseline1.Maximum = LineScanFolder.BmpDataG.Height;
                nudBaseline2.Maximum = LineScanFolder.BmpDataG.Height;
                tbBaseline1.Maximum = LineScanFolder.BmpDataG.Height;
                tbBaseline2.Maximum = LineScanFolder.BmpDataG.Height;

                nudStructure1.Maximum = LineScanFolder.BmpDataG.Width;
                nudStructure2.Maximum = LineScanFolder.BmpDataG.Width;
                tbStructure1.Maximum = LineScanFolder.BmpDataG.Width;
                tbStructure2.Maximum = LineScanFolder.BmpDataG.Width;
            }
            IgnoreGuiUpdates = false;
        }

        public void UpdateGuiFromLinescan()
        {
            // update control values from linescan object
            // AVOID PROCESSING DATA AND LOADING IMAGES!

            if (IgnoreGuiUpdates)
                return;

            // linescan image
            if ((cbR.Checked && cbR.Enabled) && (cbG.Checked && cbG.Enabled))
                pbLinescan.BackgroundImage = LineScanFolder.MarkLinescan(LineScanFolder.BmpDataMerge);
            else if (cbR.Checked && cbR.Enabled)
                pbLinescan.BackgroundImage = LineScanFolder.MarkLinescan(LineScanFolder.BmpDataR);
            else if (cbG.Checked && cbG.Enabled)
                pbLinescan.BackgroundImage = LineScanFolder.MarkLinescan(LineScanFolder.BmpDataG);
            else
                pbLinescan.BackgroundImage = null;

            // update sliders and NUDs
            tbBaseline1.Value = tbBaseline1.Maximum - LineScanFolder.BaselineIndex1;
            tbBaseline2.Value = tbBaseline2.Maximum - LineScanFolder.BaselineIndex2;
            tbStructure1.Value = LineScanFolder.StructureIndex1;
            tbStructure2.Value = LineScanFolder.StructureIndex2;
            nudBaseline1.Value = LineScanFolder.BaselineIndex1;
            nudBaseline2.Value = LineScanFolder.BaselineIndex2;
            nudStructure1.Value = LineScanFolder.StructureIndex1;
            nudStructure2.Value = LineScanFolder.StructureIndex2;
            nudFilter.Value = LineScanFolder.FilterSizePixels;
            lblFilterMs.Text = LineScanFolder.IsValid ? $"{Math.Round(LineScanFolder.FilterSizePixels * LineScanFolder.ScanLinePeriodMsec, 2)} ms" : "None";

            // generate and plot curves
            UpdateGraphs();

            SaveNeeded(true);
        }

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

            if (!LineScanFolder.IsValid)
                return;

            if (skipUpdatesIfBusy && IsUpdating)
                return;
            else
                IsUpdating = true;

            // do the analysis
            LineScanFolder.GenerateAnalysisCurves();

            // update the structure profile plot
            double[] columnPixelIndices = new double[LineScanFolder.ImgG.width];
            for (int i = 0; i < columnPixelIndices.Length; i++)
                columnPixelIndices[i] = i;
            formsPlot2.Plot.Clear();
            if ((cbR.Enabled && cbR.Checked))
            {
                double[] columnBrightnessR = ImageDataTools.GetAverageLeftright(LineScanFolder.ImgR);
                formsPlot2.Plot.AddScatter(columnPixelIndices, columnBrightnessR, markerSize: 0, color: red);
            }
            if ((cbG.Enabled && cbG.Checked))
            {
                double[] columnBrightnessG = ImageDataTools.GetAverageLeftright(LineScanFolder.ImgG);
                formsPlot2.Plot.AddScatter(columnPixelIndices, columnBrightnessG, markerSize: 0, color: green);
            }
            formsPlot2.Plot.AxisAuto(0, .1);
            formsPlot2.Plot.AddVerticalLine(LineScanFolder.StructureIndex1, color: Color.Black);
            formsPlot2.Plot.AddVerticalLine(LineScanFolder.StructureIndex2, color: Color.Black);

            // update styling before the render
            formsPlot2.Plot.YLabel("Intensity");
            formsPlot2.Plot.XLabel("Position (pixel number)");
            formsPlot2.Render();

            // update the time series plot
            LatestCurve = null;
            formsPlot1.Plot.Clear();
            if (cbRatio.Checked && cbRatio.Enabled)
            {
                if (cbDelta.Enabled && cbDelta.Checked)
                {
                    LatestCurve = LineScanFolder.GetFilteredYs(LineScanFolder.CurveDeltaGoR);
                    formsPlot1.Plot.AddScatter(LineScanFolder.timesMsec, LineScanFolder.CurveDeltaGoR, color: lightBlue, lineWidth: 0, markerSize: 2);
                    formsPlot1.Plot.AddScatter(LineScanFolder.GetFilteredXs(), LatestCurve, markerSize: 0, color: blue, lineWidth: 2);

                    formsPlot1.Plot.AddVerticalLine(LineScanFolder.BaselineIndex1 * LineScanFolder.ScanLinePeriodMsec, color: colorBaselineMarks, width: 2);
                    formsPlot1.Plot.AddVerticalLine(LineScanFolder.BaselineIndex2 * LineScanFolder.ScanLinePeriodMsec, color: colorBaselineMarks, width: 2);
                    formsPlot1.Plot.AddHorizontalLine(0, color: colorZero, width: 2);
                    formsPlot1.Plot.AddHorizontalLine(LatestCurve.Max(), color: colorPeak, width: 2);

                    formsPlot1.Plot.YLabel("Delta G/R (%)");

                }
                else
                {
                    LatestCurve = LineScanFolder.GetFilteredYs(LineScanFolder.CurveGoR);
                    formsPlot1.Plot.AddScatter(LineScanFolder.timesMsec, LineScanFolder.CurveGoR, color: lightBlue, lineWidth: 0, markerSize: 2);
                    formsPlot1.Plot.AddScatter(LineScanFolder.GetFilteredXs(), LatestCurve, markerSize: 0, color: blue, lineWidth: 2);
                    formsPlot1.Plot.YLabel("G/R (%)");
                    formsPlot1.Plot.AddHorizontalLine(LatestCurve.Max(), color: colorPeak, width: 2);
                }
            }
            else if ((cbR.Checked && cbR.Enabled) && (cbG.Checked && cbG.Enabled))
            {
                formsPlot1.Plot.AddScatter(LineScanFolder.timesMsec, LineScanFolder.CurveG, markerSize: 0, color: green);
                formsPlot1.Plot.AddScatter(LineScanFolder.timesMsec, LineScanFolder.CurveR, markerSize: 0, color: red);
                formsPlot1.Plot.YLabel("G and R (AFU)");
            }
            else if (cbR.Checked && cbR.Enabled)
            {
                formsPlot1.Plot.AddScatter(LineScanFolder.timesMsec, LineScanFolder.CurveR, markerSize: 0, color: red);
                formsPlot1.Plot.YLabel("R (AFU)");
            }
            else if (cbG.Checked && cbG.Enabled)
            {
                if (cbDelta.Enabled && cbDelta.Checked)
                {
                    LatestCurve = LineScanFolder.GetFilteredYs(LineScanFolder.CurveDeltaG);
                    formsPlot1.Plot.AddScatter(LineScanFolder.timesMsec, LineScanFolder.CurveDeltaG, lineWidth: 0, color: lightGreen, markerSize: 2);
                    formsPlot1.Plot.AddScatter(LineScanFolder.GetFilteredXs(), LatestCurve, markerSize: 0, color: green, lineWidth: 2);

                    formsPlot1.Plot.AddVerticalLine(LineScanFolder.BaselineIndex1 * LineScanFolder.ScanLinePeriodMsec, color: colorBaselineMarks, width: 2);
                    formsPlot1.Plot.AddVerticalLine(LineScanFolder.BaselineIndex2 * LineScanFolder.ScanLinePeriodMsec, color: colorBaselineMarks, width: 2);
                    formsPlot1.Plot.AddHorizontalLine(0, color: colorZero, width: 2);
                    formsPlot1.Plot.AddHorizontalLine(LatestCurve.Max(), color: colorPeak, width: 2);

                    formsPlot1.Plot.YLabel("Delta G (%)");
                }
                else
                {
                    LatestCurve = LineScanFolder.GetFilteredYs(LineScanFolder.CurveG);
                    formsPlot1.Plot.AddScatter(LineScanFolder.timesMsec, LineScanFolder.CurveG, lineWidth: 0, color: lightGreen, markerSize: 2);
                    formsPlot1.Plot.AddScatter(LineScanFolder.GetFilteredXs(), LatestCurve, markerSize: 0, color: green, lineWidth: 2);
                    formsPlot1.Plot.AddHorizontalLine(LatestCurve.Max(), color: colorPeak, width: 2);
                    formsPlot1.Plot.YLabel("G (AFU)");
                }

            }
            formsPlot1.Plot.AxisAuto(.05, .1);

            // update styling before the render
            formsPlot1.Plot.XLabel("Time (milliseconds)");
            formsPlot1.Render();

            // update peak label
            if (LatestCurve == null)
                lblPeak.Text = $"";
            else
                lblPeak.Text = $"{Math.Round(LatestCurve.Max(), 2)}%";

            Application.DoEvents();
            IsUpdating = false;
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
            SetFolder(LineScanFolder.FolderPath, true);
        }

        private void treeViewDirUC1_PathSelected(object sender, EventArgs e)
        {
            SetFolder(treeViewDirUC1.selectedPath, false);
        }

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
            if (!IgnoreGuiUpdates)
                LineScanFolder.StructureIndex1 = tbStructure1.Value;
            UpdateGuiFromLinescan();
        }

        private void tbStructure2_Scroll(object sender, EventArgs e)
        {
            if (!IgnoreGuiUpdates)
                LineScanFolder.StructureIndex2 = tbStructure2.Value;
            UpdateGuiFromLinescan();
        }

        private void tbBaseline1_Scroll(object sender, EventArgs e)
        {
            if (!IgnoreGuiUpdates)
                LineScanFolder.BaselineIndex1 = tbBaseline1.Maximum - tbBaseline1.Value;
            UpdateGuiFromLinescan();
        }

        private void tbBaseline2_Scroll(object sender, EventArgs e)
        {
            if (!IgnoreGuiUpdates)
                LineScanFolder.BaselineIndex2 = tbBaseline2.Maximum - tbBaseline2.Value;
            UpdateGuiFromLinescan();
        }

        private void nudFrame_ValueChanged(object sender, EventArgs e)
        {
            if (!IgnoreGuiUpdates)
                LineScanFolder.SetFrame((int)nudFrame.Value - 1);
            UpdateGuiFromLinescan();
        }

        private void nudBaseline1_ValueChanged(object sender, EventArgs e)
        {
            if (!IgnoreGuiUpdates)
                LineScanFolder.BaselineIndex1 = (int)nudBaseline1.Value;
            UpdateGuiFromLinescan();
        }

        private void nudBaseline2_ValueChanged(object sender, EventArgs e)
        {
            if (!IgnoreGuiUpdates)
                LineScanFolder.BaselineIndex2 = (int)nudBaseline2.Value;
            UpdateGuiFromLinescan();
        }

        private void nudStructure1_ValueChanged(object sender, EventArgs e)
        {
            if (!IgnoreGuiUpdates)
                LineScanFolder.StructureIndex1 = (int)nudStructure1.Value;
            UpdateGuiFromLinescan();
        }

        private void nudStructure2_ValueChanged(object sender, EventArgs e)
        {
            if (!IgnoreGuiUpdates)
                LineScanFolder.StructureIndex2 = (int)nudStructure2.Value;
            UpdateGuiFromLinescan();
        }

        private void nudFilter_ValueChanged(object sender, EventArgs e)
        {
            if (!IgnoreGuiUpdates)
                LineScanFolder.FilterSizePixels = (int)nudFilter.Value;
            UpdateGuiFromLinescan();
        }

        private void btnAutoBase_Click(object sender, EventArgs e)
        {
            LineScanFolder.AutoBaseline();
            UpdateGuiFromLinescan();
        }

        private void btnAutoStructure_Click(object sender, EventArgs e)
        {
            LineScanFolder.AutoStructure();
            UpdateGuiFromLinescan();
        }

        private void btnPeakCopy_Click(object sender, EventArgs e)
        {
            if (LatestCurve == null)
                return;
            Clipboard.SetText(Math.Round(LatestCurve.Max(), 3).ToString());
        }

        private void CopyCurve()
        {
            if (LatestCurve == null)
                return;

            var sb = new StringBuilder();
            foreach (var val in LatestCurve)
                sb.AppendLine($"{Math.Round(val, 3)}");

            Clipboard.SetText(sb.ToString());
        }

        private void btnCurveCopy_Click(object sender, EventArgs e) => CopyCurve();

        private void btnCopyAllData_Click(object sender, EventArgs e) => CopyAll();

        private void CopyAll()
        {
            string csv = LineScanFolder.GetCsvAllData();
            string tsv = csv.Replace(",", "\t");
            Clipboard.SetText(tsv);
        }

        private void SaveCSV()
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = $"{LineScanFolder.FolderName}.csv";
            savefile.Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*";
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(savefile.FileName, LineScanFolder.GetCsvAllData());
                System.IO.File.WriteAllText(savefile.FileName + ".json", LineScanFolder.GetMetadataJson());
            }
        }

        private void btnSaveAllData_Click(object sender, EventArgs e) => SaveCSV();

        private void btnSave_Click(object sender, EventArgs e) => SaveAnalysis();

        private void SaveAnalysis(bool launch = false)
        {
            SaveAnalysisOLD();
            SaveAnalysisNEW();

            if (launch)
                System.Diagnostics.Process.Start("explorer.exe", LineScanFolder.SaveFolderPath);

            UpdateGuiFromLinescan();
            SaveNeeded(false);
        }

        private void SaveAnalysisOLD()
        {
            if (!System.IO.Directory.Exists(LineScanFolder.SaveFolderPath))
                System.IO.Directory.CreateDirectory(LineScanFolder.SaveFolderPath);
            string csvFilePath = System.IO.Path.Combine(LineScanFolder.SaveFolderPath, "LineScanAnalysis.csv");
            LineScanFolder.SaveSettingsINI();
            System.IO.File.WriteAllText(csvFilePath, LineScanFolder.GetCsvAllData());
            System.IO.File.WriteAllText(csvFilePath + ".json", LineScanFolder.GetMetadataJson());
        }

        private void SaveAnalysisNEW()
        {
            LineScan.LineScanFolder2 lsFolder = new(LineScanFolder.FolderPath);
            PixelRange baseline = new(LineScanFolder.BaselineIndex1, LineScanFolder.BaselineIndex2);
            PixelRange structure = StructureDetection.GetBrightestStructure(lsFolder.GreenImages[0]);
            LineScanSettings settings = new(baseline, structure, filterSizePx: 20);
            Reporting.AnalyzeLinescanFolder(lsFolder, settings);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LineScanFolder.LoadSettingsINI();
            UpdateGuiFromLinescan();
            SaveNeeded(false);
        }

        private void hScrollRef_Scroll(object sender, ScrollEventArgs e)
        {
            pbRef.BackgroundImage = LineScanFolder.GetRefImage(hScrollRef.Value);
        }

        private void treeViewDirUC1_Load(object sender, EventArgs e)
        {

        }

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
