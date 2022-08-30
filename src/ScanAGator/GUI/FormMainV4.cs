using ScanAGator.Analysis;
using ScanAGator.Imaging;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScanAGator.GUI
{
    public partial class FormMainV4 : Form
    {
        Timer Timer = new() { Interval = 20, Enabled = true };
        Analysis.AnalysisSettings? SettingsToAnalyze = null;
        readonly CurveCompareForm CompareForm = new();

        public FormMainV4()
        {
            InitializeComponent();
            Text = Analysis.AnalysisResult.VersionString;
            folderSelector1.FolderSelected = OnFolderSelected;
            folderSelector1.AutoAnalyze += OnAutoAnalyze;
            folderSelector1.PlotCurves += OnPlotCurves;
            analysisSettingsControl.Recalculate += OnRecalculate;
            OnFolderSelected(null);
            Timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (SettingsToAnalyze is not null)
            {
                Analysis.AnalysisSettings settings = SettingsToAnalyze;
                SettingsToAnalyze = null;

                RatiometricImage? img = analysisSettingsControl.GetRatiometricImage();
                if (img is null)
                    return;

                AnalysisResult results = new(img, settings);
                analysisResultsControl.ShowResult(results);
            }
        }

        public void OnFolderSelected(string? folderPath)
        {
            bool isLinescanFolder = (folderPath is not null) && FolderSelectControl.IsLinescanFolder(folderPath);
            bool isZStackFolder = (folderPath is not null) && ZStackControl.IsZStackFolder(folderPath);
            bool isNotesFolder = (folderPath is not null) && NotesControl.IsNotesFolder(folderPath);

            ColumnStyle menuColumn = tableLayoutPanel1.ColumnStyles[0];
            ColumnStyle settingsColumn = tableLayoutPanel1.ColumnStyles[1];
            ColumnStyle resultsColumn = tableLayoutPanel1.ColumnStyles[2];
            ColumnStyle notesColumn = tableLayoutPanel1.ColumnStyles[3];
            ColumnStyle stackColumn = tableLayoutPanel1.ColumnStyles[4];

            tableLayoutPanel1.SuspendLayout();

            // hide everything
            Enumerable.Range(1, tableLayoutPanel1.ColumnCount - 1)
                .Select(i => tableLayoutPanel1.ColumnStyles[i])
                .ToList()
                .ForEach(x => x.Width = 0);

            imagesControl1.Visible = false;

            if (folderPath is null)
            {
                // use invisible notes as a placeholder to take up space
                notesControl1.Visible = false;
                notesColumn.Width = 100;
            }
            else if (isLinescanFolder)
            {
                try
                {
                    analysisSettingsControl.SetLinescanFolder(folderPath);
                    imagesControl1.SetLinescanFolder(folderPath);
                    imagesControl1.Visible = true;
                    settingsColumn.Width = 373;
                    resultsColumn.Width = 100;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Linescan folder contains invalid data.\n\n{ex}",
                        "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (isZStackFolder)
            {
                zStackControl1.SetFolder(folderPath);
                stackColumn.Width = 100;
            }
            else if (isNotesFolder)
            {
                notesControl1.SetFolder(folderPath);
                notesControl1.Visible = true;
                notesColumn.Width = 100;
            }
            else
            {
                // use invisible notes as a placeholder to take up space
                notesControl1.Visible = false;
                notesColumn.Width = 100;
            }

            tableLayoutPanel1.ResumeLayout();
        }

        public void OnRecalculate(Analysis.AnalysisSettings settings)
        {
            // We got new settings, but don't analyze them immediately.
            // Instead store them and we will analyze them when the GUI thread is free.
            SettingsToAnalyze = settings;
        }

        /// <summary>
        /// Fully automated analysis starting with just a path and ending with saved CSV files
        /// </summary>
        public void OnAutoAnalyze(string folderPath)
        {
            // ideal baseline and structure are selected automatically by default
            analysisSettingsControl.SetLinescanFolder(folderPath);
            analysisSettingsControl.Visible = true;
            analysisResultsControl.Visible = true;

            Application.DoEvents(); // force UI update

            AnalysisSettings settings = analysisSettingsControl.RecalculateNow()
                ?? throw new NullReferenceException("auto-calculated settings");

            RatiometricImage img = analysisSettingsControl.GetRatiometricImage()
                ?? throw new NullReferenceException("ratiometric image");

            Analysis.AnalysisResult results = new(img, settings);
            results.Save();
            Application.DoEvents(); // force UI update
        }

        public void OnPlotCurves(string[] folderPaths)
        {
            folderPaths.Where(x => FolderSelectControl.IsLinescanFolder(x))
                .ToList()
                .ForEach(x => CompareForm.AddLinescanFolder(x));

            CompareForm.Visible = true;
        }
    }
}
