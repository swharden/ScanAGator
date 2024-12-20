﻿using System;
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

namespace ScanAGator.Controls
{
    public partial class FolderSelectControl : UserControl
    {
        public Action<string?>? FolderSelected;
        public Action<string[]>? PlotCurves;
        public Action<string>? AutoAnalyze;

        public FolderSelectControl()
        {
            InitializeComponent();
            FullSizeFirstColumn(lvFolders);

            string initialFolderPath = @"X:\Data\zProjects\OT-Tom NMDA signaling\2P bpAP NMDA\2022-08-22 1mM Mg\cell3";
            UpdateControlsForFolder(initialFolderPath);

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

                string[] selectedPaths = Enumerable.Range(0, lvFolders.SelectedItems.Count)
                    .Select(x => lvFolders.SelectedItems[x].ImageKey)
                    .ToArray();

                string s = selectedPaths.Length > 1 ? "s" : "";

                if (selectedPaths.Length == 1)
                {
                    MenuItem item0a = new() { Text = $"Rename Folder" };
                    item0a.Click += (s, e) => RenameFolder();
                    context.MenuItems.Add(item0a);

                    MenuItem item0c = new() { Text = $"Refresh Folder" };
                    item0c.Click += (s, e) => UpdateControlsForFolder(lvFolders.SelectedItems[0].ImageKey);
                    context.MenuItems.Add(item0c);
                }

                MenuItem item0b = new() { Text = $"Copy Folder Path{s}" };
                item0b.Click += (s, e) => CopySelectedFolders();
                context.MenuItems.Add(item0b);

                MenuItem item1 = new() { Text = $"Open Folder{s}" };
                item1.Click += (s, e) => LaunchSelectedFolders();
                context.MenuItems.Add(item1);

                context.MenuItems.Add("-");

                MenuItem item3 = new() { Text = $"Auto-Analyze Folder{s}" };
                item3.Click += (s, e) => AnalyzeSelectedFolders();
                context.MenuItems.Add(item3);

                MenuItem item4 = new() { Text = $"Delete ScanAGator Folder{s}" };
                item4.Click += (s, e) => ClearSelectedFolders();
                context.MenuItems.Add(item4);

                context.MenuItems.Add("-");

                MenuItem item5 = new() { Text = $"Plot Curve{s}" };
                item5.Click += (s, e) => PlotCurves?.Invoke(selectedPaths);
                context.MenuItems.Add(item5);

                MenuItem item6 = new() { Text = $"Show Plotted Curves" };
                item6.Click += (s, e) => PlotCurves?.Invoke(Array.Empty<string>());
                context.MenuItems.Add(item6);

                context.MenuItems.Add("-");

                MenuItem item7 = new() { Text = $"Open ZSeries Image{s}" };
                item7.Click += (s, e) => OpenSelectedZSeries();
                context.MenuItems.Add(item7);

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
                UpdateControlsForFolder(clickedItemPath);
            }
        }

        private void RenameFolder()
        {
            string originalPath = lvFolders.SelectedItems[0].ImageKey;
            string originalFolderName = Path.GetFileName(originalPath);
            Forms.RenameFolderForm frm = new(originalFolderName);
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                string containingFolder = Path.GetDirectoryName(originalPath);
                string newFodlerName = frm.FolderName;
                string newPath = Path.Combine(containingFolder, newFodlerName);
                Directory.Move(originalPath, newPath);

                int selectedIndex = lvFolders.SelectedIndices[0];
                UpdateControlsForFolder(containingFolder); // rescan to update folder names
                lvFolders.SelectedIndices.Add(selectedIndex);
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
            StringBuilder commands = new();

            Enumerable.Range(0, lvFolders.SelectedItems.Count)
                .Select(x => lvFolders.SelectedItems[x].ImageKey)
                .Where(x => IsLinescanFolder(x))
                .ToList()
                .ForEach(x =>
                {
                    AutoAnalyze?.Invoke(x);
                    count += 1;
                    string csvPath = Path.GetFullPath(Path.Combine(x, "ScanAGator/curves.csv"));
                    string folderName = Path.GetFileName(x);
                    string sheetName = folderName.Split(new char[] { '-' }, 4).Last();
                    commands.AppendLine($"LoadLinescanCSV \"{csvPath}\" \"LineScans.BookName\" \"{sheetName}\";");
                });

            Clipboard.SetText(commands.ToString());

            MessageBox.Show(
                text: $"Automatically analyzed data for {count} folders. " +
                    Environment.NewLine + "Added OriginLab commands to clipboard.",
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
            SetFolder(path);
        }

        public void SetFolder(string path)
        {
            if (IsLinescanFolder(path))
            {
                UpdateControlsForFolder(Path.GetDirectoryName(path));
                int i = Enumerable.Range(0, lvFolders.Items.Count)
                    .Where(x => lvFolders.Items[x].ImageKey == path)
                    .Single();
                lvFolders.Items[i].Selected = true;
                lvFolders.Select();
            }
            else
            {
                UpdateControlsForFolder(path);
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

        private void UpdateControlsForFolder(string folderPath)
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
                    ForeColor = IsLinescanFolder(path) ? Color.Blue : Color.Black,
                };
                lvFolders.Items.Add(item);
            }

            lvFolders.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        public static bool IsLinescanFolder(string folderPath)
        {
            string folderName = Path.GetFileName(folderPath);
            if (!folderName.StartsWith("LineScan-"))
                return false;
            return Directory.GetFiles(folderPath, "LineScan-*.xml").Any();
        }

        public static string[] GetZSeriesTifPaths(string folderPath)
        {
            return Directory.GetFiles(folderPath, "ZSeries-*_Ch1_*.tif");
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

        private void OpenSelectedZSeries()
        {
            Enumerable.Range(0, lvFolders.SelectedItems.Count)
                .Select(x => lvFolders.SelectedItems[x].ImageKey)
                .Select(x => GetZSeriesTifPaths(x))
                .Where(x => x.Any())
                .ToList()
                .ForEach(x => { new Forms.ZSeriesForm(x).Show(); });
        }
    }
}
