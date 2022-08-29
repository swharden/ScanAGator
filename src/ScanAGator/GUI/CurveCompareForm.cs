using ScanAGator.DataExport;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScanAGator.GUI
{
    public partial class CurveCompareForm : Form
    {
        private readonly List<CsvReader> CsvFiles = new();

        public CurveCompareForm()
        {
            InitializeComponent();

            ContextMenu cm = new();

            MenuItem item1 = new("remove");
            item1.Click += (s, e) => RemoveSelected();
            cm.MenuItems.Add(item1);

            MenuItem item2 = new("clear");
            item2.Click += (s, e) => listBox1.Items.Clear();
            cm.MenuItems.Add(item2);

            listBox1.ContextMenu = cm;
            listBox1.SelectedIndexChanged += (s, e) => Replot();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void RemoveSelected()
        {
            Enumerable.Range(0, listBox1.SelectedItems.Count)
                .Select(x => listBox1.SelectedItems[x])
                .ToList()
                .ForEach(x => listBox1.Items.Remove(x));
        }

        public void AddLinescanFolderOfFolders(string folderPath)
        {
            Directory.GetDirectories(folderPath)
                .Where(x => FolderSelectControl.IsLinescanFolder(x))
                .ToList()
                .ForEach(x => AddLinescanFolder(x));
        }

        public void AddLinescanFolder(string folderPath)
        {
            string csvFilePath = Path.Combine(folderPath, "ScanAGator/curves.csv");
            CsvReader reader = new(csvFilePath);
            CsvFiles.Add(reader);

            listBox1.Items.Add(Path.GetFileName(folderPath));

            Replot();
        }

        public void Replot()
        {
            formsPlot1.Plot.Clear();
            formsPlot2.Plot.Clear();

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                CsvReader reader = CsvFiles[i];

                float thickness = listBox1.SelectedIndices.Contains(i) ? 3 : 1;
                double xOffset = 0;

                var spGreen = formsPlot1.Plot.AddScatterLines(reader.Times, reader.AvgGreen, Color.Green);
                spGreen.OffsetX = xOffset;
                spGreen.LineWidth = thickness;
                spGreen.OnNaN = ScottPlot.Plottable.ScatterPlot.NanBehavior.Gap;

                var spRed = formsPlot1.Plot.AddScatterLines(reader.Times, reader.AvgRed, Color.Red);
                spRed.OffsetX = xOffset;
                spRed.LineWidth = thickness;
                spRed.OnNaN = ScottPlot.Plottable.ScatterPlot.NanBehavior.Gap;

                string title = reader.LinescanFolderName.Split(new char[] { '-' }, 4).Last();
                var spDFF = formsPlot2.Plot.AddScatterLines(reader.Times, reader.AvgDeltaGreenOverRed, label: title);
                spRed.OffsetX = xOffset;
                spDFF.LineWidth = thickness;
                spDFF.OnNaN = ScottPlot.Plottable.ScatterPlot.NanBehavior.Gap;

                formsPlot2.Plot.AddHorizontalLine(0, Color.Black, 1, LineStyle.Dash);

                formsPlot2.Plot.Legend(true, Alignment.UpperRight);
            }

            formsPlot1.Refresh();
            formsPlot2.Refresh();
        }

        public void ClearLinescanPaths()
        {
            CsvFiles.Clear();
            listBox1.Items.Clear();
        }
    }
}
