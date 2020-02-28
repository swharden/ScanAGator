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
            Version ver = typeof(LineScanFolder).Assembly.GetName().Version;
            lblVersion.Text = $"version{ver.Major}.{ver.Minor}";
        }

        private void label2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/swharden/Scan-A-Gator");
        }

        private void label4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.SWHarden.com");
        }

        private void label3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.SWHarden.com");
        }

        private void label7_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.SWHarden.com");
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {

        }
    }
}
