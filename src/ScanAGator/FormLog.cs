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
    public partial class FormLog : Form
    {
        public FormLog(string text)
        {
            InitializeComponent();
            textBox1.Text = text;
            textBox1.Select(0, 0);
        }
    }
}
