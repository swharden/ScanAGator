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

        public FormMainV4()
        {
            InitializeComponent();
            Text = Analysis.AnalysisResult.VersionString;
            folderSelector1.FolderSelected = OnFolderSelected;
            folderSelector1.AutoAnalyze += OnAutoAnalyze;
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

            ColumnStyle menuColumn = tableLayoutPanel1.ColumnStyles[0];
            ColumnStyle settingsColumn = tableLayoutPanel1.ColumnStyles[1];
            ColumnStyle resultsColumn = tableLayoutPanel1.ColumnStyles[2];
            ColumnStyle textColumn = tableLayoutPanel1.ColumnStyles[3];

            if (isLinescanFolder)
            {
                analysisSettingsControl.SetLinescanFolder(folderPath);
                imagesControl1.SetLinescanFolder(folderPath);
                imagesControl1.Visible = true;
                settingsColumn.Width = 373;
                resultsColumn.Width = 100;
                textColumn.Width = 0;
            }
            else
            {
                notesControl1.SetFolder(folderPath);
                imagesControl1.Visible = false;
                settingsColumn.Width = 0;
                resultsColumn.Width = 0;
                textColumn.Width = 100;
            }
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
    }
}
