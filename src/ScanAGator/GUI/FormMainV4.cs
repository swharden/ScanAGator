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
        public FormMainV4()
        {
            InitializeComponent();
            folderSelector1.LinescanFolderSelected = OnLinescanFolderSelected;
            analysisSettingsControl.Recalculate += OnNewCalculation;
            OnLinescanFolderSelected(null);
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

        public void OnNewCalculation(Analysis.AnalysisSettings settings)
        {
            Analysis.AnalysisResult results = new(settings);
            //analysisResultsControl.Visible = results.IsValid;
            //if (results.IsValid)
                analysisResultsControl.ShowResult(results);
        }
    }
}
