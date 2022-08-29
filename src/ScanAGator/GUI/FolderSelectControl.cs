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
using System.Diagnostics;

namespace ScanAGator.GUI
{
    public partial class FolderSelectControl : UserControl
    {
        public Action<string?>? FolderSelected;
        public Action<string>? AutoAnalyze;

        public FolderSelectControl()
        {
            InitializeComponent();
            FullSizeFirstColumn(lvFolders);

            string initialFolderPath = @"X:\Data\zProjects\OT-Tom NMDA signaling\2P bpAP NMDA\2022-08-22 1mM Mg\cell3";
            SetFolder(initialFolderPath);

            lvFolders.MouseDoubleClick += LvFolders_MouseDoubleClick;
            lvFolders.SelectedIndexChanged += LvFolders_SelectedIndexChanged;
            lvFolders.MouseClick += LvFolders_MouseClick;

            AllowDrop = true;
            DragEnter += FolderSelectControl_DragEnter;
            DragDrop += FolderSelectControl_DragDrop;
        }

        private void LvFolders_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu context = new();

                string s = lvFolders.SelectedItems.Count > 1 ? "s" : "";


                MenuItem copyFolderPathItem = new() { Text = $"Copy Folder Path{s}" };
                copyFolderPathItem.Click += (s, e) => CopySelectedFolders();
                context.MenuItems.Add(copyFolderPathItem);

                MenuItem openLinescanFolderItem = new() { Text = $"Open Folder{s}" };
                openLinescanFolderItem.Click += (s, e) => LaunchSelectedFolders();
                context.MenuItems.Add(openLinescanFolderItem);

                MenuItem openAnalysisFolderItem = new() { Text = $"Open ScanAGator Folder{s}" };
                openAnalysisFolderItem.Click += (s, e) => LaunchSelectedFolders(true);
                context.MenuItems.Add(openAnalysisFolderItem);

                MenuItem analyzeItem = new() { Text = $"Auto-Analyze Folder{s}" };
                analyzeItem.Click += (s, e) => AnalyzeSelectedFolders();
                context.MenuItems.Add(analyzeItem);

                MenuItem analyzeItem2 = new() { Text = $"Delete ScanAGator Folder{s}" };
                analyzeItem2.Click += (s, e) => ClearSelectedFolders();
                context.MenuItems.Add(analyzeItem2);

                context.Show(this, e.Location);
            }
        }

        private void LvFolders_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem clickedItem = lvFolders.SelectedItems[0];
            string clickedItemPath = clickedItem.ImageKey;
            if (IsLinescanFolder(clickedItemPath))
            {
                Process.Start("explorer.exe", clickedItemPath);
            }
            else
            {
                SetFolder(clickedItemPath);
            }
        }

        private void CopySelectedFolders()
        {
            string[] selectedPaths = Enumerable.Range(0, lvFolders.SelectedItems.Count)
                .Select(x => lvFolders.SelectedItems[x].ImageKey)
                .ToArray();

            Clipboard.SetText(string.Join(Environment.NewLine, selectedPaths));
        }

        private void LaunchSelectedFolders(bool analysisFolder = false)
        {
            Enumerable.Range(0, lvFolders.SelectedItems.Count)
                .Select(x => lvFolders.SelectedItems[x].ImageKey)
                .Select(x => analysisFolder ? Path.Combine(x, "ScanAGator") : x)
                .Where(x => Directory.Exists(x))
                .ToList()
                .ForEach(x => Process.Start("explorer.exe", x));
        }

        private void ClearSelectedFolders()
        {
            DialogResult dialogResult = MessageBox.Show(
                text: "Are you sure you want to delete ScanAGator analysis folders?",
                caption: "Clear Old Results",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (dialogResult != DialogResult.Yes)
                return;

            int count = 0;

            Enumerable.Range(0, lvFolders.SelectedItems.Count)
                .Select(x => lvFolders.SelectedItems[x].ImageKey)
                .Where(x => IsLinescanFolder(x))
                .ToList()
                .ForEach(x =>
                {
                    string analysisFolderPath = Path.Combine(x, "ScanAGator");
                    if (Directory.Exists(analysisFolderPath))
                    {
                        Directory.Delete(analysisFolderPath, true);
                        count += 1;
                    }
                });

            MessageBox.Show(
                text: $"Deleted {count} ScanAGator folders",
                caption: "Clear Old Results",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void AnalyzeSelectedFolders()
        {
            int count = 0;

            Enumerable.Range(0, lvFolders.SelectedItems.Count)
                .Select(x => lvFolders.SelectedItems[x].ImageKey)
                .Where(x => IsLinescanFolder(x))
                .ToList()
                .ForEach(x =>
                {
                    AutoAnalyze?.Invoke(x);
                    count += 1;
                });

            MessageBox.Show(
                text: $"Automatically analyzed data for {count} folders",
                caption: "Auto-Analysis",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void FolderSelectControl_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void FolderSelectControl_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((string[])e.Data.GetData(DataFormats.FileDrop)).First();
            if (!File.GetAttributes(path).HasFlag(FileAttributes.Directory))
                path = Path.GetDirectoryName(path);

            if (IsLinescanFolder(path))
            {
                SetFolder(Path.GetDirectoryName(path));
                int i = Enumerable.Range(0, lvFolders.Items.Count)
                    .Where(x => lvFolders.Items[x].ImageKey == path)
                    .Single();
                lvFolders.Items[i].Selected = true;
                lvFolders.Select();
            }
            else
            {
                SetFolder(path);
            }
        }

        private void LvFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvFolders.SelectedItems.Count != 1)
            {
                if (lvFolders.SelectedItems.Count > 1)
                    FolderSelected?.Invoke(null);
                return;
            }

            ListViewItem clickedItem = lvFolders.SelectedItems[0];
            FolderSelected?.Invoke(clickedItem.ImageKey);
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
            if (!Directory.Exists(folderPath))
                return;

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

        public static bool IsLinescanFolder(string folderPath)
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
