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
        }

        public void OnLinescanFolderSelected(string folderPath)
        {
            if (folderPath is null)
            {
                imageRangeSelector1.SetFolder();
            }
            else
            {
                Prairie.FolderContents pvFolder = new(folderPath);
                Prairie.ParirieXmlFile xml = new(pvFolder.XmlFilePath);
                Imaging.RatiometricImages images = new(pvFolder);

                imageRangeSelector1.SetFolder(xml, images);
            }
        }
    }
}
