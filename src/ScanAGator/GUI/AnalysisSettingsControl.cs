using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScanAGator.Analysis;

namespace ScanAGator.GUI
{
    public partial class AnalysisSettingsControl : UserControl
    {
        public Action<Analysis.AnalysisSettings>? Recalculate;

        private Imaging.RatiometricImages? Images;
        private Bitmap? DisplayBitmap;
        private Prairie.ParirieXmlFile? PVXml;

        public AnalysisSettingsControl()
        {
            InitializeComponent();
            cbDisplay.SelectedIndex = 0;

            tbFrame.ValueChanged += TbFrame_ValueChanged;
            cbAverage.CheckedChanged += CbAverage_CheckedChanged;
            cbDisplay.SelectedIndexChanged += CbDisplay_SelectedIndexChanged;

            EnableDoubleBuffering(panel1);
            panel1.Paint += Panel1_Paint;
        }

        public void SetLinescan(Prairie.ParirieXmlFile xml, Imaging.RatiometricImages images)
        {
            SetMaxValues();

            PVXml = xml;
            Images = images;

            tbFrame.Value = 0;
            tbFrame.Maximum = Images.FrameCount - 1;
            OnLinescanImageChanged();
            SetMaxValues();
            AutoBaseline();
            AutoStructure();
        }

        private void EnableDoubleBuffering(Panel target)
        {
            BindingFlags flags = BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic;
            typeof(Panel).InvokeMember("DoubleBuffered", flags, null, target, new object[] { true });
        }

        private void CbDisplay_SelectedIndexChanged(object sender, EventArgs e) => OnLinescanImageChanged();
        private void CbAverage_CheckedChanged(object sender, EventArgs e) => OnLinescanImageChanged();
        private void TbFrame_ValueChanged(object sender, EventArgs e) => OnLinescanImageChanged();
        private void TrackBar_ValueChanged(object sender, EventArgs e) => OnTrackbarChanged();

        private void btnAutoBaseline_Click(object sender, EventArgs e) => AutoBaseline();

        private void btnAutoStructure_Click(object sender, EventArgs e) => AutoStructure();

        private void AutoBaseline(double b1Frac = .02, double b2Frac = .08)
        {
            Imaging.RatiometricImage? img = GetRatiometricImage();
            if (img is null)
                return;

            tbBaseline1.Value = tbBaseline1.Maximum - (int)(img.Green.Height * b1Frac);
            tbBaseline2.Value = tbBaseline2.Maximum - (int)(img.Green.Height * b2Frac);
            OnTrackbarChanged();
        }

        private void AutoStructure()
        {
            Imaging.RatiometricImage? img = GetRatiometricImage();
            if (img is null)
                return;

            StructureRange structure = StructureDetection.GetBrightestStructure(img.GreenData);
            tbStructure1.Value = structure.Min;
            tbStructure2.Value = structure.Max;
            OnTrackbarChanged();
        }

        private Imaging.RatiometricImage? GetRatiometricImage()
        {
            if (Images is null)
                return null;
            return cbAverage.Checked ? Images.Average : Images.Frames[tbFrame.Value];
        }

        private void OnLinescanImageChanged()
        {
            Imaging.RatiometricImage? img = GetRatiometricImage();

            if (img is null)
                return;

            DisplayBitmap = cbDisplay.Text switch
            {
                "Merge" => img.Merge,
                "Green" => img.Green,
                "Red" => img.Red,
                _ => throw new NotImplementedException(),
            };

            panel1.Invalidate();

            if (cbAverage.Checked)
            {
                lblFrame.Visible = false;
                tbFrame.Visible = false;
            }
            else
            {
                lblFrame.Text = $"Frame: {tbFrame.Value + 1}/{tbFrame.Maximum + 1}";
                lblFrame.Visible = true;
                tbFrame.Visible = true;
            }

            Recalculte();
        }

        private void Nud_ValueChanged(object sender, EventArgs e)
        {
            OnNudChanged();
            panel1.Invalidate();
        }

        private void SetMaxValues()
        {
            int maxBaseline = DisplayBitmap is null ? 999999 : DisplayBitmap.Height;
            int maxStructure = DisplayBitmap is null ? 999999 : DisplayBitmap.Width;

            // un-wire event handlers
            tbBaseline1.ValueChanged -= TrackBar_ValueChanged;
            tbBaseline2.ValueChanged -= TrackBar_ValueChanged;
            tbStructure1.ValueChanged -= TrackBar_ValueChanged;
            tbStructure2.ValueChanged -= TrackBar_ValueChanged;

            nudBaseline1.ValueChanged -= Nud_ValueChanged;
            nudBaseline2.ValueChanged -= Nud_ValueChanged;
            nudStructure1.ValueChanged -= Nud_ValueChanged;
            nudStructure2.ValueChanged -= Nud_ValueChanged;

            // reset the values conservatively
            nudBaseline1.Value = 0;
            nudBaseline2.Value = 0;
            nudStructure1.Value = 0;
            nudStructure2.Value = 0;

            tbBaseline1.Value = 0;
            tbBaseline2.Value = 0;
            tbStructure1.Value = 0;
            tbStructure2.Value = 0;

            // set the max upper bounds
            nudBaseline1.Maximum = maxBaseline;
            nudBaseline2.Maximum = maxBaseline;
            nudStructure1.Maximum = maxStructure;
            nudStructure2.Maximum = maxStructure;

            tbBaseline1.Maximum = maxBaseline;
            tbBaseline2.Maximum = maxBaseline;
            tbStructure1.Maximum = maxStructure;
            tbStructure2.Maximum = maxStructure;

            // wire-up the event handlers
            tbBaseline1.ValueChanged += TrackBar_ValueChanged;
            tbBaseline2.ValueChanged += TrackBar_ValueChanged;
            tbStructure1.ValueChanged += TrackBar_ValueChanged;
            tbStructure2.ValueChanged += TrackBar_ValueChanged;

            nudBaseline1.ValueChanged += Nud_ValueChanged;
            nudBaseline2.ValueChanged += Nud_ValueChanged;
            nudStructure1.ValueChanged += Nud_ValueChanged;
            nudStructure2.ValueChanged += Nud_ValueChanged;
        }

        private void OnTrackbarChanged()
        {
            nudBaseline1.Value = tbBaseline1.Maximum - tbBaseline1.Value;
            nudBaseline2.Value = tbBaseline2.Maximum - tbBaseline2.Value;
            nudStructure1.Value = tbStructure1.Value;
            nudStructure2.Value = tbStructure2.Value;
            Recalculte();
        }

        private void OnNudChanged()
        {
            tbBaseline1.Value = tbBaseline1.Maximum - (int)nudBaseline1.Value;
            tbBaseline2.Value = tbBaseline2.Maximum - (int)nudBaseline2.Value;
            tbStructure1.Value = (int)nudStructure1.Value;
            tbStructure2.Value = (int)nudStructure2.Value;
            Recalculte();
        }

        /// <summary>
        /// Call this to force a redraw of the linescan image and regeneration of all plots
        /// </summary>
        private void Recalculte()
        {
            Imaging.RatiometricImage? ratioImage = GetRatiometricImage();

            if (ratioImage is null || PVXml is null)
                return;

            BaselineRange baseline = new((int)nudBaseline1.Value, (int)nudBaseline2.Value);
            StructureRange structure = new((int)nudStructure1.Value, (int)nudStructure2.Value);

            AnalysisSettings settings = new(ratioImage, baseline, structure, (int)nudFilterPx.Value, PVXml);

            ScottPlot.Plot plt = new(pbGraph.Width, pbGraph.Height);
            plt.Frameless();
            plt.AddSignal(ratioImage.GreenData.AverageByColumn(), 1, Color.Green);
            plt.AddSignal(ratioImage.RedData.AverageByColumn(), 1, Color.Red);
            plt.AddHorizontalSpan(structure.Min, structure.Max, Color.FromArgb(20, Color.Blue));
            plt.AxisAutoX(0);
            pbGraph.Image?.Dispose();
            pbGraph.Image = plt.GetBitmap();

            Recalculate?.Invoke(settings);
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            if (DisplayBitmap is null)
                return;

            Graphics gfx = e.Graphics;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            int b1 = (DisplayBitmap.Height - tbBaseline1.Value) * panel1.Height / DisplayBitmap.Height;
            int b2 = (DisplayBitmap.Height - tbBaseline2.Value) * panel1.Height / DisplayBitmap.Height;
            int s1 = tbStructure1.Value * panel1.Width / DisplayBitmap.Width;
            int s2 = tbStructure2.Value * panel1.Width / DisplayBitmap.Width;

            gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            gfx.DrawImage(DisplayBitmap, 0, 0, panel1.Width, panel1.Height);

            gfx.DrawLine(Pens.Yellow, 0, b1, panel1.Width, b1);
            gfx.DrawLine(Pens.Yellow, 0, b2, panel1.Width, b2);
            gfx.DrawLine(Pens.Yellow, s1, 0, s1, panel1.Height);
            gfx.DrawLine(Pens.Yellow, s2, 0, s2, panel1.Height);
        }
    }
}
