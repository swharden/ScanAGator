using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScanAGator.Forms
{
    public partial class CrashForm : Form
    {
        public CrashForm(string folder, string message)
        {
            InitializeComponent();
            textBox1.Text = folder;
            richTextBox1.Text = message;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/swharden/ScanAGator");
        }
    }
}
