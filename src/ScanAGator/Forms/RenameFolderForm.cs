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
    public partial class RenameFolderForm : Form
    {
        public string FolderName { get; set; }

        public RenameFolderForm(string initialFolderName)
        {
            InitializeComponent();
            FolderName = initialFolderName;
            textBox1.Text = initialFolderName;
            textBox1.TextChanged += (s, e) => FolderName = textBox1.Text;
            btnOK.Click += (s, e) => OK();
            btnCancel.Click += (s, e) => Cancel();
            this.KeyPreview = true;
            this.KeyDown += RenameFolderForm_KeyDown;
        }

        private void RenameFolderForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OK();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Cancel();
            }
        }

        private void OK()
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel()
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
