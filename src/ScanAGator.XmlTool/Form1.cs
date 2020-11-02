using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScanAGator.XmlTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateGuiValues(new ExperimentXml(null));

            string startupFilePath = "../../../../data/tseries/TSeries-10232020-1129-1771.xml";
            if (File.Exists(startupFilePath))
                LoadXmlFile(startupFilePath);
        }

        private void LoadXmlFile(string filePath)
        {
            filePath = Path.GetFullPath(filePath);
            XmlFilePathLabel.Text = Path.GetDirectoryName(filePath);
            XmlFileNameLabel.Text = Path.GetFileName(filePath);

            var experiment = new ExperimentXml(filePath);
            UpdateGuiValues(experiment);
        }

        private void UpdateGuiValues(ExperimentXml experiment)
        {
            dataGridView1.Visible = experiment.IsValid;
            LaserPowerGroupBox.Visible = experiment.IsValid;
            ImageGroupBox.Visible = experiment.IsValid;
            PmtGainGroupBox.Visible = experiment.IsValid;
            SettingsGroupBox.Visible = experiment.IsValid;

            if (experiment.IsValid)
            {
                dataGridView1.DataSource = experiment.GetFrameDataTable();
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
                dataGridView1.AutoResizeColumns();

                MiraPowerLabel.Text = experiment.PowerMira.ToString();
                X3FixedPowerLabel.Text = experiment.PowerX3Fixed.ToString();
                X3TunablePowerLabel.Text = experiment.PowerX3Tunable.ToString();
                ImageWidthLabel.Text = experiment.PixelsPerLine.ToString();
                ImageHeightLabel.Text = experiment.LinesPerFrame.ToString();
                ImageScaleLabel.Text = Math.Round(experiment.MicronsPerPixel, 4).ToString();
                PmtCh1Label.Text = experiment.PmtGainCh1.ToString();
                PmtCh2Label.Text = experiment.PmtGainCh2.ToString();
                DwellLabel.Text = experiment.DwellTime.ToString();
                ZoomLabel.Text = experiment.OpticalZoom.ToString();
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            dataGridView1.Visible = false;
            string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (Directory.Exists(paths[0]))
            {
                string[] xmlFilesInFolder = Directory.GetFiles(paths[0], "*.xml");
                if (xmlFilesInFolder.Length > 0)
                    LoadXmlFile(xmlFilesInFolder[0]);
            }
            else
            {
                LoadXmlFile(paths[0]);
            }
        }
    }
}
