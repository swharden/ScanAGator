using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScanAGator.GUI
{
    public partial class NotesControl : UserControl
    {
        public NotesControl()
        {
            InitializeComponent();
        }

        public void SetFolder(string? folderPath)
        {
            tabControl1.TabPages.Clear();

            if (folderPath is null)
            {
                tabControl1.Visible = false;
                return;
            }

            string[] textFilePaths = Directory.GetFiles(folderPath, "*.txt").ToArray();

            if (!textFilePaths.Any())
            {
                tabControl1.Visible = false;
                return;
            }

            foreach (string path in textFilePaths)
            {
                TabPage tp = new()
                {
                    ImageKey = path,
                    Text = Path.GetFileName(path),
                };

                RichTextBox rtb = new()
                {
                    Dock = DockStyle.Fill,
                    Text = File.ReadAllText(path),
                };

                tp.Controls.Add(rtb);

                tabControl1.TabPages.Add(tp);
            }

            tabControl1.Visible = true;
        }
    }
}
