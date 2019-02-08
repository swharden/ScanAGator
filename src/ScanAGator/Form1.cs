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
    public partial class Form1 : Form
    {
        string VERSION = Properties.Resources.ResourceManager.GetString("VERSION");

        public Form1()
        {
            InitializeComponent();
            lblStatus.Text = $"Scan-A-Gator version {VERSION}";
        }

        // ACTIONS /////////////////////////////////////////////////////////////////

        public void SetStatus(string message)
        {
            Console.WriteLine(message);
            lblStatus.Text = message;
        }

        public string PathFolder;
        public void SelectFolder(string pathFolder = null)
        {

            // ask for the path with a dialog if one isn't given
            if (pathFolder == null)
            {
                var diag = new FolderBrowserDialog();
                if (diag.ShowDialog() == DialogResult.OK)
                    pathFolder = diag.SelectedPath;
                else
                    return;
            }

            // ensure the path is valid
            pathFolder = System.IO.Path.GetFullPath(pathFolder);
            if (!System.IO.Directory.Exists(pathFolder))
            {
                SetStatus($"ERROR - experiment folder does not exist: {pathFolder}");
                return;
            }
            PathFolder = pathFolder;
            lsFolder = null;

            // populate the list of subfolders
            string[] folders = System.IO.Directory.GetDirectories(pathFolder);
            for (int i = 0; i < folders.Length; i++)
                folders[i] = System.IO.Path.GetFileName(folders[i]);
            lbFolders.Items.Clear();
            lbFolders.Items.AddRange(folders);
            SetStatus($"{folders.Length} sub-folders found in {PathFolder}");
        }

        public LineScanFolder lsFolder;
        public void LoadLinescan(string pathLinescan)
        {
            ClearLinescan(); // clear the old data so we can cleanly exit at any time

            pathLinescan = System.IO.Path.GetFullPath(pathLinescan);
            if (!System.IO.Directory.Exists(pathLinescan))
            {
                Console.WriteLine($"ERROR - linescan folder does not exist: {pathLinescan}");
                return;
            }
            string folderName = System.IO.Path.GetFileName(pathLinescan);
            SetStatus($"Loading {folderName} ...");

            // all the parsing and data loading is handled here
            lsFolder = new LineScanFolder(pathLinescan);
            tbInformation.Text = lsFolder.log.Replace("\n", "\r\n");
            if (!lsFolder.IsValidLinescan)
            {
                SetStatus($"Error - Invalid linescan folder: {folderName}");
                return;
            }

            // by this point everything is valid, so just populate the GUI based on it.
            SetStatus($"Successfully loaded linescan folder: {folderName}");
            Text = $"Scan-A-Gator - {folderName}";
            pbRef.BackgroundImage = lsFolder.BmpReference;
            UpdateGuiFromLinescan();
        }


        public Bitmap BitmapAddLineVert(Bitmap bmp, int xPixel)
        {
            Pen pen = new Pen(new SolidBrush(Color.Yellow));
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.DrawLine(pen, new Point(xPixel, 0), new Point(xPixel, bmp.Height));
            return bmp;
        }

        public Bitmap BitmapAddLineHoriz(Bitmap bmp, int yPixel)
        {
            Pen pen = new Pen(new SolidBrush(Color.Yellow));
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.DrawLine(pen, new Point(0, yPixel), new Point(bmp.Width, yPixel));
            return bmp;
        }

        public void UpdateLinescanFromGui()
        {
            lsFolder.baselinePixel1 = (int)nudBaseline1.Value;
            lsFolder.baselinePixel2 = (int)nudBaseline2.Value;
            lsFolder.baselineStructure1 = (int)nudStructure1.Value;
            lsFolder.baselineStructure2 = (int)nudStructure2.Value;

            UpdateGuiFromLinescan();
        }

        public void UpdateGuiFromLinescan()
        {
            if (lsFolder == null)
                return;

            // TODO: use XY not signal
            double sampleRate = 1;

            // update ScottPlot
            scottPlotUC1.plt.data.Clear();
            Bitmap bmpData = null;
            if (calcDeltaGoR.Checked)
            {
                scottPlotUC1.plt.data.AddSignal(lsFolder.dataDeltaGoR, sampleRate);
                scottPlotUC1.plt.data.AddHorizLine(0, 2, Color.Black);
                scottPlotUC1.plt.settings.axisLabelY = "Delta G/R (%)";
                bmpData = new Bitmap(lsFolder.bmpDataG);
                gbLinescan.Text = "Green Image (brightness-adjusted)";
            }
            else if (calcGoR.Checked)
            {
                scottPlotUC1.plt.data.AddSignal(lsFolder.dataGoR, sampleRate);
                scottPlotUC1.plt.settings.axisLabelY = "Raw G/R (%)";
                bmpData = new Bitmap(lsFolder.bmpDataG);
                gbLinescan.Text = "Green Image (brightness-adjusted)";
            }
            else if (calcG.Checked)
            {
                scottPlotUC1.plt.data.AddSignal(lsFolder.dataG, sampleRate, lineColor: Color.Green);
                scottPlotUC1.plt.settings.axisLabelY = "Green (AFU)";
                bmpData = new Bitmap(lsFolder.bmpDataG);
                gbLinescan.Text = "Green Image (brightness-adjusted)";
            }
            else if (calcR.Checked)
            {
                scottPlotUC1.plt.data.AddSignal(lsFolder.dataR, sampleRate, lineColor: Color.Red);
                scottPlotUC1.plt.settings.axisLabelY = "Red (AFU)";
                bmpData = new Bitmap(lsFolder.bmpDataR);
                gbLinescan.Text = "Red Image (brightness-adjusted)";
            }
            else if (calcGR.Checked)
            {
                scottPlotUC1.plt.data.AddSignal(lsFolder.dataR, sampleRate, lineColor: Color.Red);
                scottPlotUC1.plt.data.AddSignal(lsFolder.dataG, sampleRate, lineColor: Color.Green);
                scottPlotUC1.plt.settings.axisLabelY = "Red and Green (AFU)";
                bmpData = bmpData = new Bitmap(lsFolder.bmpDataG);
                gbLinescan.Text = "Red Image (brightness-adjusted)";
            }
            scottPlotUC1.plt.settings.AxisFit(0, .1);
            scottPlotUC1.Render();

            // add baseline and structure lines
            bmpData = BitmapAddLineHoriz(bmpData, lsFolder.baselinePixel1);
            bmpData = BitmapAddLineHoriz(bmpData, lsFolder.baselinePixel2);
            bmpData = BitmapAddLineVert(bmpData, lsFolder.baselineStructure1);
            bmpData = BitmapAddLineVert(bmpData, lsFolder.baselineStructure2);
            pbLinescan.BackgroundImage = bmpData;

        }

        public void ClearLinescan()
        {
            pbRef.BackgroundImage = null;
            pbLinescan.BackgroundImage = null;
            Text = $"Scan-A-Gator";
            lsFolder = null;
        }

        // GUI BINDINGS /////////////////////////////////////////////////////////////////

        private void Form1_Load(object sender, EventArgs e)
        {
            SelectFolder("../../../../data/linescans/");

            scottPlotUC1.plt.settings.title = "";
            scottPlotUC1.plt.settings.axisLabelY = "";
            scottPlotUC1.plt.settings.axisLabelX = "Frame Number";
            scottPlotUC1.plt.settings.figureBgColor = SystemColors.Control;
            //scottPlotUC1.plt.settings.SetDataPadding(40, 17, 40, 10);
            scottPlotUC1.Render();

        }

        private void documentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/swharden/Scan-A-Gator");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form formAbout = new FormAbout();
            formAbout.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lbFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbFolders.SelectedIndex < 0)
                return;
            string pathLinescan = System.IO.Path.Combine(PathFolder, lbFolders.SelectedItem.ToString());
            LoadLinescan(pathLinescan);
        }

        private void btnFolderSelect_Click(object sender, EventArgs e)
        {
            SelectFolder();
        }

        private void btnFolderRefresh_Click(object sender, EventArgs e)
        {
            SelectFolder(PathFolder);
        }

        private void lbFolders_DoubleClick(object sender, EventArgs e)
        {
            if (lbFolders.SelectedIndex < 0)
                return;
            string pathLinescan = System.IO.Path.Combine(PathFolder, lbFolders.SelectedItem.ToString());
            System.Diagnostics.Process.Start("explorer.exe", pathLinescan);
        }

        private void calcDeltaGoR_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGuiFromLinescan();
        }

        private void calcG_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGuiFromLinescan();
        }

        private void calcGoR_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGuiFromLinescan();
        }

        private void calcR_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGuiFromLinescan();
        }

        private void calcGR_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGuiFromLinescan();
        }

        private void nudBaseline1_ValueChanged(object sender, EventArgs e)
        {
            UpdateLinescanFromGui();
        }

        private void nudBaseline2_ValueChanged(object sender, EventArgs e)
        {
            UpdateLinescanFromGui();
        }

        private void nudStructure1_ValueChanged(object sender, EventArgs e)
        {
            UpdateLinescanFromGui();
        }

        private void nudStructure2_ValueChanged(object sender, EventArgs e)
        {
            UpdateLinescanFromGui();
        }
    }
}
