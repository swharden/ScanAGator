using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScanAGator
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/swharden/Scan-A-Gator");
        }
    }
}
