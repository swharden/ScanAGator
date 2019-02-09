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
        private PrairieLS linescan;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

            // prepare the tree browser
            TreeBrowserLoad();
            string path = @"C:\Users\scott\Documents\GitHub\Scan-A-Gator\data\linescans\LineScan-09212014-1554-750";
            LoadLinescanFolder(path);

            // configure scottPlot settings
            scottPlotUC1.plt.settings.figureBgColor = SystemColors.Control;
            scottPlotUC1.plt.settings.axisLabelX = "time (milliseconds)";
            scottPlotUC1.Render();
        }

        #region actions

        private void SaveNeeded(bool needed = false)
        {
            if (needed)
            {
                btnSave.BackColor = Color.Red;
                btnSave.ForeColor = Color.White;
            }
            else
            {
                btnSave.UseVisualStyleBackColor = true;
                btnSave.ForeColor = Color.Black;
            }                
        }

        private void LoadLinescanFolder(string path)
        {
            if (treeView1.SelectedNode == null)
                TreeBrowserSelectPath(path);

            linescan = new PrairieLS(path);
            pbRef.BackgroundImage = (linescan.validLinescanFolder) ? linescan.GetBmpReference() : null;
            UpdateGuiFromLinescan();
            AnalyzeData();

            if (linescan.validLinescanFolder && !System.IO.File.Exists(linescan.pathIniFile))
                SaveNeeded(true);
            else
                SaveNeeded(false);
        }

        private void UpdateLinescanFromGui()
        {
            linescan.baseline1 = tbBaseline1.Value;
            linescan.baseline2 = tbBaseline2.Value;
            linescan.structure1 = tbStructure1.Value;
            linescan.structure2 = tbStructure2.Value;
            linescan.filterPx = (int)nudFilter.Value;
            linescan.frame = (int)nudFrame.Value - 1;
            SaveNeeded(true);

            UpdateGuiFromLinescan(); // to update labels
        }

        private void UpdateGuiFromLinescan()
        {
            if (!linescan.validLinescanFolder)
                return;

            tbBaseline1.Maximum = linescan.dataImage.Height;
            tbBaseline1.Value = linescan.baseline1;
            tbBaseline2.Maximum = linescan.dataImage.Height;
            tbBaseline2.Value = linescan.baseline2;

            tbStructure1.Maximum = linescan.dataImage.Width;
            tbStructure1.Value = linescan.structure1;
            tbStructure2.Maximum = linescan.dataImage.Width;
            tbStructure2.Value = linescan.structure2;

            nudFilter.Maximum = linescan.dataImage.Height / 5;
            nudFilter.Value = linescan.filterPx;
            lblFilterMs.Text = string.Format("{0:0.00} ms", linescan.filterMillisec);

            nudFrame.Maximum = linescan.pathsDataG.Length;
            nudFrame.Minimum = 1;
            nudFrame.Value = linescan.frame + 1;
            gbFrame.Text = $"Frame (of {nudFrame.Maximum})";
            if (linescan.pathsDataG.Length == 1)
                gbFrame.Enabled = false;
            else
                gbFrame.Enabled = true;

            gbBaseline.Text = $"Baseline ({tbBaseline1.Value} px to {tbBaseline2.Value} px)";
            gbStructure.Text = $"Structure ({tbStructure1.Value} px to {tbStructure2.Value} px)";
        }

        private void UpdateImages()
        {
            if (linescan.validLinescanFolder)
            {
            }
            else
            {
                pbRef.BackgroundImage = null;
            }
        }

        private void AnalyzeData(bool loadNewFrame = false)
        {

            if (!linescan.validLinescanFolder)
            {
                pbData.BackgroundImage = null;
                lblPeak.Text = "";
                scottPlotUC1.plt.data.Clear();
                scottPlotUC1.Render();
                return;
            }

            if (loadNewFrame)
                linescan.LoadFrame();

            linescan.Analyze();
            
            if (radioImageG.Checked)
                pbData.BackgroundImage = linescan.GetBmpMarkedG();
            else
                pbData.BackgroundImage = linescan.GetBmpMarkedR();

            lblPeak.Text = string.Format("{0:0.00}%", Math.Round(linescan.dataDeltaGoRsmoothedPeak, 2));

            scottPlotUC1.plt.data.Clear();

            if (radioDeltaGoR.Checked)
            {
                scottPlotUC1.plt.data.AddScatter(linescan.dataTimeMsec, linescan.dataDeltaGoR, markerColor: Color.LightBlue, lineWidth: 0);
                scottPlotUC1.plt.data.AddScatter(linescan.dataDeltaGoRsmoothedChoppedXs, linescan.dataDeltaGoRsmoothedChoppedYs, markerSize: 0, lineColor: Color.Blue);
                scottPlotUC1.plt.data.AddHorizLine(0, 2, Color.Black);
                scottPlotUC1.plt.data.AddHorizLine(linescan.dataDeltaGoRsmoothedPeak, 2, Color.Red);
                scottPlotUC1.plt.settings.AxisFit(0, .1);
                scottPlotUC1.plt.settings.title = "Delta G/R";
                scottPlotUC1.plt.settings.axisLabelY = "Delta G/R (%)";
            }
            else if (radioGoR.Checked)
            {
                scottPlotUC1.plt.data.AddScatter(linescan.dataTimeMsec, linescan.dataGoR, markerColor: Color.Blue, lineWidth: 0);
                scottPlotUC1.plt.settings.AxisFit(0, .1);
                scottPlotUC1.plt.settings.title = "Raw G/R";
                scottPlotUC1.plt.settings.axisLabelY = "G/R (%)";
            }
            else if (radioPMT.Checked)
            {
                scottPlotUC1.plt.data.AddScatter(linescan.dataTimeMsec, linescan.dataR, lineColor: Color.Red, markerSize: 0);
                scottPlotUC1.plt.data.AddScatter(linescan.dataTimeMsec, linescan.dataG, lineColor: Color.Green, markerSize: 0);
                scottPlotUC1.plt.settings.AxisFit(0, .1);
                scottPlotUC1.plt.settings.axisY.Set(0, null);
                scottPlotUC1.plt.settings.title = "Raw G and R";
                scottPlotUC1.plt.settings.axisLabelY = "PMT Value (AFU)";
            }

            scottPlotUC1.plt.data.AddVertLine(linescan.dataTimeMsec[linescan.baseline1], 2, Color.Gray);
            scottPlotUC1.plt.data.AddVertLine(linescan.dataTimeMsec[linescan.baseline2], 2, Color.Gray);

            scottPlotUC1.Render();
        }

        #endregion

        #region directory browser

        private void TreeBrowserSelectPath(string path)
        {
            path = System.IO.Path.GetFullPath(path);
            List<string> folderNames = new List<string>(path.Split(System.IO.Path.DirectorySeparatorChar));
            TreeBrowserExpandChildren(treeView1.Nodes[0], folderNames);
        }

        private void TreeBrowserExpandChildren(TreeNode node, List<string> children)
        {
            children.RemoveAt(0);
            node.Expand();
            if (children.Count == 0)
                return;
            foreach (TreeNode mynode in node.Nodes)
                if (mynode.Text == children[0])
                {
                    treeView1.SelectedNode = mynode;
                    TreeBrowserExpandChildren(mynode, children);
                    break;
                }
        }

        private void TreeBrowserLoad()
        {
            string[] drives = Environment.GetLogicalDrives();
            foreach (string drive in drives)
            {
                System.IO.DriveInfo di = new System.IO.DriveInfo(drive);
                int driveImage;

                // define icon based on drive type
                switch (di.DriveType)
                {
                    case System.IO.DriveType.CDRom:
                        driveImage = 3;
                        break;
                    case System.IO.DriveType.Network:
                        driveImage = 6;
                        break;
                    case System.IO.DriveType.NoRootDirectory:
                        driveImage = 8;
                        break;
                    case System.IO.DriveType.Unknown:
                        driveImage = 8;
                        break;
                    default:
                        driveImage = 2;
                        break;
                }

                TreeNode node = new TreeNode(drive.Substring(0, 1), driveImage, driveImage);
                node.Tag = drive;
                if (di.IsReady == true)
                    node.Nodes.Add("...");
                treeView1.Nodes.Add(node);
            }
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                if (e.Node.Nodes[0].Text == "..." && e.Node.Nodes[0].Tag == null)
                {
                    e.Node.Nodes.Clear();

                    //get the list of sub direcotires
                    string[] dirs = System.IO.Directory.GetDirectories(e.Node.Tag.ToString());

                    foreach (string dir in dirs)
                    {
                        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(dir);
                        TreeNode node = new TreeNode(di.Name, 0, 1);

                        try
                        {
                            //keep the directory's full path in the tag for use later
                            node.Tag = dir;

                            //if the directory has sub directories add the place holder
                            if (di.GetDirectories().Count() > 0)
                                node.Nodes.Add(null, "...", 0, 0);
                        }
                        catch (UnauthorizedAccessException)
                        {
                            //display a locked folder icon
                            node.ImageIndex = 12;
                            node.SelectedImageIndex = 12;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "DirectoryLister",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            e.Node.Nodes.Add(node);
                        }
                    }
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string path = "";
            var mynode = e.Node;
            while (mynode != null)
            {
                path = mynode.Text + "/" + path;
                mynode = mynode.Parent;
            }
            path = path.Insert(1, ":");
            path = System.IO.Path.GetFullPath(path);
            LoadLinescanFolder(path);
        }

        #endregion


        private void tbBaseline1_Scroll(object sender, EventArgs e)
        {
            UpdateLinescanFromGui();
            AnalyzeData();
        }

        private void tbBaseline2_Scroll(object sender, EventArgs e)
        {
            UpdateLinescanFromGui();
            AnalyzeData();
        }

        private void tbStructure1_Scroll(object sender, EventArgs e)
        {
            UpdateLinescanFromGui();
            AnalyzeData();
        }

        private void tbStructure2_Scroll(object sender, EventArgs e)
        {
            UpdateLinescanFromGui();
            AnalyzeData();
        }

        private void radioDeltaGoR_CheckedChanged(object sender, EventArgs e)
        {
            AnalyzeData();
        }

        private void radioGoR_CheckedChanged(object sender, EventArgs e)
        {
            AnalyzeData();
        }

        private void radioPMT_CheckedChanged(object sender, EventArgs e)
        {
            AnalyzeData();
        }

        private void nudFilter_ValueChanged(object sender, EventArgs e)
        {
            UpdateLinescanFromGui();
            AnalyzeData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", linescan.pathLinescanFolder);
        }

        private void radioImageG_CheckedChanged(object sender, EventArgs e)
        {
            AnalyzeData();
        }

        private void radioImageR_CheckedChanged(object sender, EventArgs e)
        {
            AnalyzeData();
        }

        private void nudFrame_ValueChanged(object sender, EventArgs e)
        {
            UpdateLinescanFromGui();
            AnalyzeData(true);
        }

        private void cbFrameAverage_CheckedChanged(object sender, EventArgs e)
        {
            // TODO: support analysis from the average of all frames
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/swharden/Scan-A-Gator");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadLinescanFolder(linescan.pathLinescanFolder);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            linescan.SaveSettingsINI();
            SaveNeeded(false);
        }
    }
}
