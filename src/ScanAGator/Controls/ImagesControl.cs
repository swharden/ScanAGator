using ScanAGator.Imaging;
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
    public partial class ImagesControl : UserControl
    {
        List<string> ImagePaths = new();

        public ImagesControl()
        {
            InitializeComponent();
            hScrollBar1.ValueChanged += (s, e) => LoadSelectedImage();
            pictureBox1.MouseClick += PictureBox1_MouseClick;
        }

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu context = new();

                MenuItem mi1 = new() { Text = $"Show in Explorer" };
                mi1.Click += (s, e) =>
                {
                    System.Diagnostics.Process.Start("explorer.exe", $"/select, \"{ImagePaths[hScrollBar1.Value]}\"");
                };
                context.MenuItems.Add(mi1);

                MenuItem mi2 = new() { Text = $"Copy Image" };
                mi2.Click += (s, e) =>
                {
                    Clipboard.SetImage(pictureBox1.Image);
                };
                context.MenuItems.Add(mi2);

                context.Show(this, e.Location);
            }
        }

        public void LoadSelectedImage()
        {
            string imagePath = ImagePaths[hScrollBar1.Value];
            Bitmap bmp = ImageDataTools.ReadTif_SD(imagePath);
            Image oldImage = pictureBox1.Image;
            pictureBox1.Image = bmp;
            oldImage?.Dispose();

            string title = Path.GetFileName(imagePath);
            if (title.StartsWith("LineScan-") && title.Contains("-Cycle"))
                title = "Cycle" + title.Split(new string[] { "Cycle" }, StringSplitOptions.None)[1];
            label1.Text = title;
        }

        public void SelelctBestImage()
        {
            for (int i = 0; i < ImagePaths.Count; i++)
            {
                string path = ImagePaths[i];
                string filename = Path.GetFileName(path);
                if (filename.Contains("-8bit-") && filename.Contains("-Ch1-"))
                {
                    SelectImage(i);
                    return;
                }
            }

            SelectImage(0);
        }

        private void SelectImage(int newValue)
        {
            int oldValue = hScrollBar1.Value;
            hScrollBar1.Value = newValue;
            if (newValue == oldValue)
                LoadSelectedImage();
        }

        public void SetLinescanFolder(string? linescanFolder)
        {
            if (linescanFolder is null)
                return;

            ImagePaths.Clear();

            string referenceFolder = Path.Combine(linescanFolder, "References");
            if (Directory.Exists(referenceFolder))
            {
                ImagePaths.AddRange(Directory.GetFiles(referenceFolder, "*.tif"));
                ImagePaths.AddRange(Directory.GetFiles(referenceFolder, "*.jpg"));
                ImagePaths.AddRange(Directory.GetFiles(referenceFolder, "*.bmp"));
                ImagePaths.AddRange(Directory.GetFiles(referenceFolder, "*.png"));
            }

            hScrollBar1.Value = Math.Min(hScrollBar1.Value, ImagePaths.Count - 1);
            hScrollBar1.Maximum = ImagePaths.Count - 1;
            SelelctBestImage();
        }
    }
}
