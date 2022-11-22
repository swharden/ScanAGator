using ScanAGator.Imaging;
using SciTIF;
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

namespace ScanAGator.Forms
{
    public partial class ZSeriesForm : Form
    {
        readonly string[] TifPaths;
        ImageStack? Stack = null;
        Bitmap? ProjectionMax = null;
        Bitmap? ProjectionRainbow = null;
        Bitmap[] Slices;

        Timer Timer = new() { Interval = 100, Enabled = true };

        public ZSeriesForm(string[] tifPaths)
        {
            InitializeComponent();
            TifPaths = tifPaths;
            Slices = new Bitmap[tifPaths.Length];

            hScrollBar1.LargeChange = 1;
            hScrollBar1.Maximum = tifPaths.Length - 1;
            hScrollBar1.Scroll += (s, e) => UpdateImage();

            cbProject.CheckedChanged += (s, e) => UpdateImage();
            cbRainbow.CheckedChanged += (s, e) => UpdateImage();

            Timer.Tick += Timer_Tick;

            this.Text = "Loading...";
        }

        private void UpdateImage()
        {
            if (cbProject.Checked)
            {
                pictureBox1.Image = cbRainbow.Checked ? ProjectionRainbow : ProjectionMax;
                hScrollBar1.Visible = false;
            }
            else
            {
                pictureBox1.Image = Slices[hScrollBar1.Value];
                hScrollBar1.Visible = true;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Timer.Enabled = false;

            SciTIF.Image[] images = new SciTIF.Image[TifPaths.Length];

            for (int i = 0; i < TifPaths.Length; i++)
            {
                this.Text = $"Loading {i + 1} of {TifPaths.Length}...";
                Application.DoEvents();
                images[i] = new TifFile(TifPaths[i]).GetImage();
            }

            Stack = new ImageStack(images);

            this.Text = $"AutoScaling...";
            Application.DoEvents();
            Stack.AutoScale();


            for (int i = 0; i < TifPaths.Length; i++)
            {
                this.Text = $"Creating Bitmap {i + 1} of {TifPaths.Length}...";
                Application.DoEvents();
                Slices[i] = images[i].ToBitmap();
            }

            this.Text = $"Projecting Grayscale...";
            Application.DoEvents();
            ProjectionMax = Stack.ProjectMax().ToBitmap();

            this.Text = $"Projecting Rainbow...";
            Application.DoEvents();
            ProjectionRainbow = Stack.Project(new SciTIF.LUTs.Jet()).ToBitmap();

            Text = Path.GetFileName(Path.GetFileName(Path.GetDirectoryName(TifPaths[0])));
            pictureBox1.Image = ProjectionMax;
        }
    }
}
