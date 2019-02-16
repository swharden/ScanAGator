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
            DebugLogHide();

            Text = $"Scan-A-Gator v{Properties.Resources.ResourceManager.GetString("version")}";
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //SetFolder(@"C:\Users\scott\Documents\GitHub\Scan-A-Gator\data\linescans\LineScan-02132019-1317-2778");
            SetFolder("./");
        }

        public LineScanFolder lsFolder;
        private bool ignoreGuiUpdates;
        private void SetFolder(string path, bool updateTree = true)
        {
            if (updateTree)
            {
                // update the tree (will can an event to re-call fhis function)
                treeViewDirUC1.SelectPath(path);
                return;
            }

            lsFolder = new LineScanFolder(path);
            UpdateGuiFromLinescanFirst();
            UpdateGuiFromLinescan();
            if (!lsFolder.isValid)
                SaveNeeded(false);
            if (lsFolder.isValid && System.IO.File.Exists(lsFolder.pathIniFile))
                SaveNeeded(false);
        }

        public void UpdateGuiFromLinescanFirst()
        {
            // call this when first loading a linescan to set limits and control gray-states

            ignoreGuiUpdates = true;

            // reference image
            if (lsFolder.isValid)
                pbRef.BackgroundImage = lsFolder.bmpRef;
            else
                pbRef.BackgroundImage = null;

            // lock or unlock settings
            gbDisplay.Enabled = lsFolder.isValid;
            gbFrame.Enabled = lsFolder.isValid;
            gbFilter.Enabled = lsFolder.isValid;
            gbBaseline.Enabled = lsFolder.isValid;
            gbStructure.Enabled = lsFolder.isValid;
            gbSettings.Enabled = lsFolder.isValid;
            gbData.Enabled = lsFolder.isValid;
            gbAuto.Enabled = lsFolder.isValid;
            gbDisplayType.Enabled = lsFolder.isValid;
            gbPeak.Enabled = lsFolder.isValid;
            cbRatio.Enabled = lsFolder.isRatiometric;
            scottPlotUC1.Visible = lsFolder.isValid;
            scottPlotUC2.Visible = lsFolder.isValid;

            if (lsFolder.isRatiometric)
            {
                cbRatio.Checked = true;
                cbR.Checked = true;
                cbG.Checked = true;
            }
            else
            {
                cbG.Checked = true;
            }

            if (lsFolder.isValid)
            {
                cbG.Enabled = (lsFolder.pathsDataG.Length > 0) ? true : false;
                cbR.Enabled = (lsFolder.pathsDataR.Length > 0) ? true : false;

                if (lsFolder.isRatiometric)
                {
                    cbRatio.Checked = true;
                    cbDelta.Checked = true;
                }
                else
                {
                    cbDelta.Checked = true;
                }

                nudFrame.Minimum = 1;
                nudFrame.Maximum = lsFolder.pathsDataG.Length;
                gbFrame.Text = $"Frame (of {lsFolder.pathsDataG.Length})";
                if (lsFolder.pathsDataG.Length == 1)
                    nudFrame.Enabled = false;
                else
                    nudFrame.Enabled = true;

                nudFilter.Maximum = (int)(lsFolder.bmpDataG.Height / 3.0);

                nudBaseline1.Maximum = lsFolder.bmpDataG.Height;
                nudBaseline2.Maximum = lsFolder.bmpDataG.Height;
                tbBaseline1.Maximum = lsFolder.bmpDataG.Height;
                tbBaseline2.Maximum = lsFolder.bmpDataG.Height;

                nudStructure1.Maximum = lsFolder.bmpDataG.Width;
                nudStructure2.Maximum = lsFolder.bmpDataG.Width;
                tbStructure1.Maximum = lsFolder.bmpDataG.Width;
                tbStructure2.Maximum = lsFolder.bmpDataG.Width;
            }
            ignoreGuiUpdates = false;
        }

        public void UpdateGuiFromLinescan()
        {
            // update control values from linescan object
            // AVOID PROCESSING DATA AND LOADING IMAGES!

            if (ignoreGuiUpdates)
                return;

            // log window
            tbLog.Text = lsFolder.log.Replace("\n", "\r\n");
            if (lsFolder.isValid)
                tbLog.BackColor = SystemColors.Control;
            else
                tbLog.BackColor = Color.Salmon;

            // linescan image
            if ((cbR.Checked && cbR.Enabled) && (cbG.Checked && cbG.Enabled))
                pbLinescan.BackgroundImage = lsFolder.MarkLinescan(lsFolder.bmpDataM);
            else if (cbR.Checked && cbR.Enabled)
                pbLinescan.BackgroundImage = lsFolder.MarkLinescan(lsFolder.bmpDataR);
            else if (cbG.Checked && cbG.Enabled)
                pbLinescan.BackgroundImage = lsFolder.MarkLinescan(lsFolder.bmpDataG);
            else
                pbLinescan.BackgroundImage = null;

            // update sliders and NUDs
            tbBaseline1.Value = tbBaseline1.Maximum - lsFolder.baseline1;
            tbBaseline2.Value = tbBaseline2.Maximum - lsFolder.baseline2;
            tbStructure1.Value = lsFolder.structure1;
            tbStructure2.Value = lsFolder.structure2;
            nudBaseline1.Value = lsFolder.baseline1;
            nudBaseline2.Value = lsFolder.baseline2;
            nudStructure1.Value = lsFolder.structure1;
            nudStructure2.Value = lsFolder.structure2;
            nudFilter.Value = lsFolder.filterPx;
            lblFilterMs.Text = $"{Math.Round(lsFolder.filterPx * lsFolder.scanLinePeriod, 2)} ms";

            // generate and plot curves
            UpdateGraph();

            SaveNeeded(true);
        }

        public double[] curveToCopy;
        public bool busyUpdating = false;
        public void UpdateGraph(bool skipUpdatesIfBusy = true)
        {

            if (!lsFolder.isValid)
                return;

            if (skipUpdatesIfBusy && busyUpdating)
                return;
            else
                busyUpdating = true;

            // do the analysis
            lsFolder.GenerateAnalysisCurves();

            // update the structure profile plot
            double[] columnPixelIndices = new double[lsFolder.imgG.width];
            for (int i = 0; i < columnPixelIndices.Length; i++)
                columnPixelIndices[i] = i;
            scottPlotUC2.plt.data.Clear();
            if ((cbR.Enabled && cbR.Checked))
            {
                double[] columnBrightnessR = ImageDataTools.GetAverageLeftright(lsFolder.imgR);
                scottPlotUC2.plt.data.AddScatter(columnPixelIndices, columnBrightnessR, markerSize: 0, lineColor: Color.Red);
            }
            if ((cbG.Enabled && cbG.Checked))
            {
                double[] columnBrightnessG = ImageDataTools.GetAverageLeftright(lsFolder.imgG);
                scottPlotUC2.plt.data.AddScatter(columnPixelIndices, columnBrightnessG, markerSize: 0, lineColor: Color.Green);
            }
            scottPlotUC2.plt.settings.AxisFit(0, .1);
            scottPlotUC2.plt.data.AddVertLine(lsFolder.structure1, lineColor: Color.Black);
            scottPlotUC2.plt.data.AddVertLine(lsFolder.structure2, lineColor: Color.Black);
            //scottPlotUC2.plt.data.AddHorizLine(0, lineColor: Color.Black);

            // update styling before the render
            scottPlotUC2.plt.settings.figureBgColor = SystemColors.Control;
            scottPlotUC2.plt.settings.title = "";
            scottPlotUC2.plt.settings.axisLabelY = "Intensity";
            scottPlotUC2.plt.settings.axisLabelX = "Position (pixel number)";
            scottPlotUC2.plt.settings.SetDataPadding(60, 5, 45, 5);
            scottPlotUC2.Render();

            // update the time series plot
            curveToCopy = null;
            scottPlotUC1.plt.data.Clear();
            if (cbRatio.Checked && cbRatio.Enabled)
            {
                if (cbDelta.Enabled && cbDelta.Checked)
                {
                    curveToCopy = lsFolder.GetFilteredYs(lsFolder.curveDeltaGoR);
                    scottPlotUC1.plt.data.AddScatter(lsFolder.timesMsec, lsFolder.curveDeltaGoR, markerColor: Color.LightBlue, lineWidth: 0, markerSize: 2);
                    scottPlotUC1.plt.data.AddScatter(lsFolder.GetFilteredXs(), curveToCopy, markerSize: 0, lineColor: Color.Blue);

                    scottPlotUC1.plt.data.AddVertLine(lsFolder.baseline1 * lsFolder.scanLinePeriod, lineColor: Color.Black);
                    scottPlotUC1.plt.data.AddVertLine(lsFolder.baseline2 * lsFolder.scanLinePeriod, lineColor: Color.Black);
                    scottPlotUC1.plt.data.AddHorizLine(0, lineColor: Color.Black);
                    scottPlotUC1.plt.data.AddHorizLine(curveToCopy.Max(), lineColor: Color.Black);

                    scottPlotUC1.plt.settings.axisLabelY = "Delta G/R (%)";

                }
                else
                {
                    curveToCopy = lsFolder.GetFilteredYs(lsFolder.curveGoR);
                    scottPlotUC1.plt.data.AddScatter(lsFolder.timesMsec, lsFolder.curveGoR, markerColor: Color.LightBlue, lineWidth: 0, markerSize: 2);
                    scottPlotUC1.plt.data.AddScatter(lsFolder.GetFilteredXs(), curveToCopy, markerSize: 0, lineColor: Color.Blue);
                    scottPlotUC1.plt.settings.axisLabelY = "G/R (%)";
                    scottPlotUC1.plt.data.AddHorizLine(curveToCopy.Max(), lineColor: Color.Black);
                }
            }
            else if ((cbR.Checked && cbR.Enabled) && (cbG.Checked && cbG.Enabled))
            {
                scottPlotUC1.plt.data.AddScatter(lsFolder.timesMsec, lsFolder.curveG, markerSize: 0, lineColor: Color.Green);
                scottPlotUC1.plt.data.AddScatter(lsFolder.timesMsec, lsFolder.curveR, markerSize: 0, lineColor: Color.Red);
                scottPlotUC1.plt.settings.axisLabelY = "G and R (AFU)";
            }
            else if (cbR.Checked && cbR.Enabled)
            {
                scottPlotUC1.plt.data.AddScatter(lsFolder.timesMsec, lsFolder.curveR, markerSize: 0, lineColor: Color.Red);
                scottPlotUC1.plt.settings.axisLabelY = "R (AFU)";
            }
            else if (cbG.Checked && cbG.Enabled)
            {
                if (cbDelta.Enabled && cbDelta.Checked)
                {
                    curveToCopy = lsFolder.GetFilteredYs(lsFolder.curveDeltaG);
                    scottPlotUC1.plt.data.AddScatter(lsFolder.timesMsec, lsFolder.curveDeltaG, lineWidth: 0, markerColor: Color.LightGreen, markerSize: 2);
                    scottPlotUC1.plt.data.AddScatter(lsFolder.GetFilteredXs(), curveToCopy, markerSize: 0, lineColor: Color.Green);

                    scottPlotUC1.plt.data.AddVertLine(lsFolder.baseline1 * lsFolder.scanLinePeriod, lineColor: Color.Black);
                    scottPlotUC1.plt.data.AddVertLine(lsFolder.baseline2 * lsFolder.scanLinePeriod, lineColor: Color.Black);
                    scottPlotUC1.plt.data.AddHorizLine(0, lineColor: Color.Black);
                    scottPlotUC1.plt.data.AddHorizLine(curveToCopy.Max(), lineColor: Color.Black);

                    scottPlotUC1.plt.settings.axisLabelY = "Delta G (%)";
                }
                else
                {
                    curveToCopy = lsFolder.GetFilteredYs(lsFolder.curveG);
                    scottPlotUC1.plt.data.AddScatter(lsFolder.timesMsec, lsFolder.curveG, lineWidth: 0, markerColor: Color.LightGreen, markerSize: 2);
                    scottPlotUC1.plt.data.AddScatter(lsFolder.GetFilteredXs(), curveToCopy, markerSize: 0, lineColor: Color.Green);
                    scottPlotUC1.plt.data.AddHorizLine(curveToCopy.Max(), lineColor: Color.Black);
                    scottPlotUC1.plt.settings.axisLabelY = "G (AFU)";
                }

            }
            scottPlotUC1.plt.settings.AxisFit(.05, .1);

            // update styling before the render
            scottPlotUC1.plt.settings.figureBgColor = SystemColors.Control;
            scottPlotUC1.plt.settings.title = "";
            scottPlotUC1.plt.settings.axisLabelX = "Time (milliseconds)";
            scottPlotUC1.plt.settings.SetDataPadding(60, 5, 45, 5);
            scottPlotUC1.plt.settings.drawAxes = true;
            scottPlotUC1.Render();

            // update peak label
            if (curveToCopy == null)
                lblPeak.Text = $"";
            else
                lblPeak.Text = $"{Math.Round(curveToCopy.Max(), 2)}%";

            Application.DoEvents();
            busyUpdating = false;
        }

        #region debug log
        private void DebugLogHide()
        {
            this.Height = 656;
            tbLog.Visible = false;
        }
        private void DebugLogShow()
        {
            this.Height = 800;
            tbLog.Visible = true;
        }
        #endregion

        #region drop-down menus

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new FormAbout();
            frm.ShowDialog();
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
            // use the save dialog to select the folder because it's better than the folder browser dialog
            SaveFileDialog sf = new SaveFileDialog();
            sf.FileName = "Load this folder";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string pathFolder = System.IO.Path.GetDirectoryName(sf.FileName);
                SetFolder(pathFolder);
            }
        }

        private void debugLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (debugLogToolStripMenuItem.Checked)
                DebugLogShow();
            else
                DebugLogHide();
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
                lsFolder.structure1 = tbStructure1.Value;
            UpdateGuiFromLinescan();
        }

        private void tbStructure2_Scroll(object sender, EventArgs e)
        {
            if (!ignoreGuiUpdates)
                lsFolder.structure2 = tbStructure2.Value;
            UpdateGuiFromLinescan();
        }

        private void tbBaseline1_Scroll(object sender, EventArgs e)
        {
            if (!ignoreGuiUpdates)
                lsFolder.baseline1 = tbBaseline1.Maximum - tbBaseline1.Value;
            UpdateGuiFromLinescan();
        }

        private void tbBaseline2_Scroll(object sender, EventArgs e)
        {
            if (!ignoreGuiUpdates)
                lsFolder.baseline2 = tbBaseline2.Maximum - tbBaseline2.Value;
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
                lsFolder.baseline1 = (int)nudBaseline1.Value;
            UpdateGuiFromLinescan();
        }

        private void nudBaseline2_ValueChanged(object sender, EventArgs e)
        {
            if (!ignoreGuiUpdates)
                lsFolder.baseline2 = (int)nudBaseline2.Value;
            UpdateGuiFromLinescan();
        }

        private void nudStructure1_ValueChanged(object sender, EventArgs e)
        {
            if (!ignoreGuiUpdates)
                lsFolder.structure1 = (int)nudStructure1.Value;
            UpdateGuiFromLinescan();
        }

        private void nudStructure2_ValueChanged(object sender, EventArgs e)
        {
            if (!ignoreGuiUpdates)
                lsFolder.structure2 = (int)nudStructure2.Value;
            UpdateGuiFromLinescan();
        }

        private void nudFilter_ValueChanged(object sender, EventArgs e)
        {
            if (!ignoreGuiUpdates)
                lsFolder.filterPx = (int)nudFilter.Value;
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

        private void btnCurveCopy_Click(object sender, EventArgs e)
        {
            if (curveToCopy == null)
                return;
            string txt = "";
            foreach (var val in curveToCopy)
                txt += $"{Math.Round(val, 3)}\n";
            Clipboard.SetText(txt);
        }

        private void btnCopyAllData_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lsFolder.GetCsvAllData());
        }

        private void btnSaveAllData_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = $"{lsFolder.folderName}.png";
            savefile.Filter = "CSV Files (*.png)|*.png|All files (*.*)|*.*";
            if (savefile.ShowDialog() == DialogResult.OK)
                System.IO.File.WriteAllText(savefile.FileName, lsFolder.GetCsvAllData());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            lsFolder.SaveSettingsINI();
            UpdateGuiFromLinescan();
            SaveNeeded(false);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            lsFolder.LoadSettingsINI();
            UpdateGuiFromLinescan();
            SaveNeeded(false);
        }
        #endregion

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

    }
}
