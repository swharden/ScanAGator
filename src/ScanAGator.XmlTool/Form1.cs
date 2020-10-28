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
            string startupFilePath = "../../../../data/tseries/TSeries-10232020-1129-1771.xml";
            if (File.Exists(startupFilePath))
                LoadXmlFile(startupFilePath);
        }

        private void LoadXmlFile(string filePath)
        {
            filePath = Path.GetFullPath(filePath);
            XmlFilePathLabel.Text = Path.GetDirectoryName(filePath);
            XmlFileNameLabel.Text = Path.GetFileName(filePath);

            var tseries = new PrairieXml.TSeries(filePath);
            dataGridView1.Visible = tseries.IsValid;
            if (!tseries.IsValid)
                return;
            
            dataGridView1.DataSource = tseries.GetDataTable();

            foreach (DataGridViewColumn col in dataGridView1.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
            dataGridView1.AutoResizeColumns();
            
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
