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

            Text = $"Scan-A-Gator v{Properties.Resources.ResourceManager.GetString("version")}";

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            SetFolder(@"C:\Users\scott\Documents\GitHub\Scan-A-Gator\data\linescans\LineScan-02132019-1317-2778");
        }

        public LineScanFolder lsFolder;
        private void SetFolder(string path, bool updateTree = true)
        {
            if (updateTree)
                // if you update the tree, an event will re-call this function
                treeViewDirUC1.SelectPath(path);
            else
                lsFolder = new LineScanFolder(path);
        }

        #region developer

        #endregion

        #region drop-down menu

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
            treeViewDirUC1.SelectPath(@"C:\Users\scott\Documents\GitHub\Scan-A-Gator\data\linescans");
        }

        #endregion

        #region navigation

        private void treeViewDirUC1_PathSelected(object sender, EventArgs e)
        {
            SetFolder(treeViewDirUC1.selectedPath, false);
        }

        #endregion

    }
}
