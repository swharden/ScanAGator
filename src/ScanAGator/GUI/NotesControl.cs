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
            SetContent(string.Empty);
        }

        public static bool IsNotesFolder(string folderPath)
        {
            string[] textFilePaths = Directory.GetFiles(folderPath, "*.txt").ToArray();
            return textFilePaths.Any();
        }

        public void SetFolder(string folderPath)
        {
            StringBuilder sb = new();

            string[] textFilePaths = Directory.GetFiles(folderPath, "*.txt").ToArray();

            foreach (string path in textFilePaths)
            {
                string title = Path.GetFileName(path);
                string text = File.ReadAllText(path);

                sb.AppendLine($"<h1>{title}</h1>");
                sb.AppendLine($"<div><pre>{text}</pre></div>");
            }

            SetContent(sb.ToString());
        }

        private void SetContent(string body)
        {
            string bgColor = "#"
                + SystemColors.Control.R.ToString("X2")
                + SystemColors.Control.G.ToString("X2")
                + SystemColors.Control.B.ToString("X2");

            webBrowser1.DocumentText = $"<html><body style=\"background: {bgColor};\">{body}</body></html>";
        }
    }
}
