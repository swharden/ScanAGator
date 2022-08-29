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
    public partial class ZStackControl : UserControl
    {
        public ZStackControl()
        {
            InitializeComponent();
            pictureBox1.BackColor = SystemColors.Control;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        public void SetFolder(string folderPath)
        {
            string[] pathsCh1 = Directory.GetFiles(folderPath, "*_Ch1_*.tif");

            Image? original = pictureBox1.Image;

            if (pathsCh1.Length > 1)
            {
                string analysisFolder = Path.Combine(folderPath, "ScanAGator");
                string projectionImageFile = Path.Combine(analysisFolder, "projection-max.png");

                if (File.Exists(projectionImageFile))
                {
                    pictureBox1.Image = new Bitmap(projectionImageFile);
                }
                else
                {
                    SciTIF.Image[] slices = pathsCh1.Select(x => new SciTIF.TifFile(x)).Select(x => x.GetImage()).ToArray();
                    SciTIF.ImageStack stack = new(slices);
                    SciTIF.Image projection = stack.ProjectMax();
                    projection.AutoScale();
                    byte[] bmpBytes = projection.GetBitmapBytes();
                    using MemoryStream ms = new(bmpBytes);
                    Bitmap bmp = new(ms);
                    pictureBox1.Image = bmp;

                    if (!Directory.Exists(analysisFolder))
                        Directory.CreateDirectory(analysisFolder);
                    bmp.Save(projectionImageFile);
                }
            }
            else
            {
                pictureBox1.Image = null;
            }

            original?.Dispose();
        }

        public static bool IsZStackFolder(string folderPath)
        {
            string[] pathsCh1 = Directory.GetFiles(folderPath, "*_Ch1_*.tif");
            return pathsCh1.Length > 1;
        }
    }
}
