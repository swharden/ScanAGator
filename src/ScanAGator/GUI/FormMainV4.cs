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
            folderSelector1.LinescanFolderSelected = OnLinescanFolderSelected;
            folderSelector1.AutoAnalyze += OnAutoAnalyze;
            analysisSettingsControl.Recalculate += OnRecalculate;
            OnLinescanFolderSelected(null);
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

        public void OnLinescanFolderSelected(string? folderPath)
        {
            analysisSettingsControl.SetLinescan(folderPath);
            analysisSettingsControl.Visible = folderPath is not null;
            analysisResultsControl.Visible = folderPath is not null;
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
            analysisSettingsControl.SetLinescan(folderPath);
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
