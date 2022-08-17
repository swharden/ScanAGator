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
            folderSelector1.LinescanFolderSelected = OnLinescanFolderSelected;
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
                Analysis.AnalysisResult results = new(settings);
                analysisResultsControl.ShowResult(results);
            }
        }

        public void OnLinescanFolderSelected(string? folderPath)
        {
            if (folderPath is not null)
            {
                Prairie.FolderContents pvFolder = new(folderPath);
                Prairie.ParirieXmlFile xml = new(pvFolder.XmlFilePath);
                Imaging.RatiometricImages images = new(pvFolder);
                analysisSettingsControl.SetLinescan(xml, images);
            }

            analysisSettingsControl.Visible = folderPath is not null;
            analysisResultsControl.Visible = folderPath is not null;
        }

        public void OnRecalculate(Analysis.AnalysisSettings settings)
        {
            // We got new settings, but don't analyze them immediately.
            // Instead store them and we will analyze them when the GUI thread is free.
            SettingsToAnalyze = settings;
        }
    }
}
