using ScanAGator.Analysis;
using ScanAGator.Imaging;
using System;
using System.Linq;
using System.Windows.Forms;
using ScanAGator.Controls;

namespace ScanAGator.Forms
{
    public partial class FormMainV4 : Form
    {
        Timer Timer = new() { Interval = 20, Enabled = true };
        Analysis.AnalysisSettings? SettingsToAnalyze = null;
        readonly CurveCompareForm CompareForm = new();

        public FormMainV4()
        {
            InitializeComponent();
            Text = Version.VersionString;
            folderSelector1.FolderSelected = OnFolderSelected;
            folderSelector1.AutoAnalyze += OnAutoAnalyze;
            folderSelector1.PlotCurves += OnPlotCurves;
            analysisSettingsControl.Recalculate += OnRecalculate;
            analysisSettingsControl.StructureChanged += (s, e) => imagesControl1.StructureRange = e;
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
            HideLinescanControls();

            if (folderPath is null)
            {
                return;
            }

            bool isLinescanFolder = (folderPath is not null) && FolderSelectControl.IsLinescanFolder(folderPath);

            if (isLinescanFolder)
            {
                try
                {
                    analysisSettingsControl.SetLinescanFolder(folderPath);
                    imagesControl1.SetLinescanFolder(folderPath);
                    ShowLinescanControls();
                }
                catch (Exception ex)
                {
                    new CrashForm(folderPath!, ex.ToString()).ShowDialog();
                }
            }
        }

        public void HideLinescanControls() => SetLinescanControlVisibility(false);

        public void ShowLinescanControls() => SetLinescanControlVisibility(true);

        public void SetLinescanControlVisibility(bool visible)
        {
            imagesControl1.Visible = visible;
            analysisSettingsControl.Visible = visible;
            analysisResultsControl.Visible = visible;
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
