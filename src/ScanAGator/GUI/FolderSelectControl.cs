using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ScanAGator.GUI
{
    public partial class FolderSelectControl : UserControl
    {
        public Action<string> LinescanFolderSelected;

        public FolderSelectControl()
        {
            InitializeComponent();

            FullSizeFirstColumn(lvFolders);

            string initialFolderPath = @"X:\Data\zProjects\Oxytocin Biosensor\experiments\2P bpAP NMDA\2022-08-09\2p";
            SetFolder(initialFolderPath);

            lvFolders.DoubleClick += LvFolders_DoubleClick;
            lvFolders.SelectedIndexChanged += LvFolders_SelectedIndexChanged;
        }

        private void LvFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvFolders.SelectedItems.Count == 0)
                return;

            ListViewItem clickedItem = lvFolders.SelectedItems[0];
            string clickedItemPath = clickedItem.ImageKey;
            if (IsLinescanFolder(clickedItemPath))
            {
                LinescanFolderSelected.Invoke(clickedItemPath);
            }
            else
            {
                LinescanFolderSelected.Invoke(null);
            }
        }

        private void LvFolders_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem clickedItem = lvFolders.SelectedItems[0];
            string clickedItemPath = clickedItem.ImageKey;
            SetFolder(clickedItemPath);
        }

        private void FullSizeFirstColumn(ListView lv)
        {
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            foreach (ColumnHeader column in lv.Columns)
                column.Width -= 20;
        }

        public void SetFolder(string folderPath)
        {
            lvFolders.Items.Clear();
            string indentation = "";
            foreach (string path in GetParentPaths(folderPath))
            {
                string name = indentation + Path.GetFileName(path) + "/";
                if (name == "/")
                    name = folderPath.Split(':')[0] + ":/";
                lvFolders.Items.Add(name, path);
                indentation += " ";
            }

            foreach (string path in Directory.GetDirectories(folderPath))
            {
                string name = indentation + Path.GetFileName(path) + "/";
                ListViewItem item = new(name, path)
                {
                    ForeColor = IsLinescanFolder(path) ? Color.Blue : Color.Black
                };
                lvFolders.Items.Add(item);
            }
        }

        private static bool IsLinescanFolder(string folderPath)
        {
            string folderName = Path.GetFileName(folderPath);
            if (!folderName.StartsWith("LineScan-"))
                return false;
            return Directory.GetFiles(folderPath, "LineScan-*.xml").Any();
        }

        private static string[] GetParentPaths(string folderPath)
        {
            List<string> names = new();

            while (!string.IsNullOrWhiteSpace(folderPath))
            {
                names.Add(folderPath);
                folderPath = Path.GetDirectoryName(folderPath);
            }

            return names.Where(x => !string.IsNullOrWhiteSpace(x)).Reverse().ToArray();
        }
    }
}
