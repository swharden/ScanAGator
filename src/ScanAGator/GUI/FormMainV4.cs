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
            imageRangeSelector1.SetFolder(folderPath);
        }
    }
}
