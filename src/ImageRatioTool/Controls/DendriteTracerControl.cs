using ScottPlot;
using System.Data;
using System.Diagnostics;

namespace ImageRatioTool.Controls;

public partial class DendriteTracerControl : UserControl
{
    Bitmap? ReferenceImage = null;
    SciTIF.Image? Red;
    SciTIF.Image? Green;

    readonly List<FractionalPoint> FPoints = new();
    float ScaleX => Red is null ? 1 : (float)Red.Width / pictureBox1.Width;
    float ScaleY => Red is null ? 1 : (float)Red.Height / pictureBox1.Height;

    public DendriteTracerControl()
    {
        InitializeComponent();

        pictureBox1.MouseDown += PictureBox1_MouseDown;

        // initial data
        FPoints.Add(new(0.4925, 0.4912));
        FPoints.Add(new(0.5840, 0.4922));
        FPoints.Add(new(0.6211, 0.4277));
        FPoints.Add(new(0.7148, 0.4297));
        FPoints.Add(new(0.8867, 0.5039));
        FPoints.Add(new(0.9258, 0.6191));
        FPoints.Add(new(0.9395, 0.6855));
        FPoints.Add(new(0.9004, 0.7207));

        Analyze();
    }

    private void PictureBox1_MouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {
            FPoints.Clear();
            Analyze();
            formsPlot1.Plot.Clear();
            formsPlot1.Refresh();
            return;
        }

        double fracX = (double)e.X / pictureBox1.Width;
        double fracY = (double)e.Y / pictureBox1.Height;
        FractionalPoint pt = new(fracX, fracY);
        FPoints.Add(pt);
        Debug.WriteLine($"Adding fractional point: {pt}");
        Analyze();
    }

    public void SetData(SciTIF.Image red, SciTIF.Image green)
    {
        Red = red;
        Green = green;
        ReferenceImage?.Dispose();
        ReferenceImage = ImageOperations.MakeDisplayImage(red, green);
        Analyze();
    }

    public void Analyze()
    {
        if (ReferenceImage is null || Red is null || Green is null)
            return;

        double roiSpacing = tbRoiSpacing.Value;
        double roiRadius = tbRoiSize.Value;

        Bitmap bmp = new(pictureBox1.Width, pictureBox1.Height);
        using Graphics gfx = Graphics.FromImage(bmp);
        gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        Rectangle srcRect = new(0, 0, ReferenceImage.Width, ReferenceImage.Height);
        Rectangle destRect = new(0, 0, pictureBox1.Width, pictureBox1.Height);
        gfx.DrawImage(ReferenceImage, destRect, srcRect, GraphicsUnit.Pixel);

        Point[] linePoints = FPoints.Select(x => x.ToPoint(bmp.Width, bmp.Height)).ToArray();
        PointF[] roiPoints = LineOperations.GetSubPoints(linePoints, roiSpacing);

        foreach (Point pt in linePoints)
            ImageOperations.DrawCircle(gfx, pt, Pens.Yellow, 3);

        if (linePoints.Length >= 2)
        {
            gfx.DrawLines(Pens.Yellow, linePoints);
        }

        List<double> ratios = new();
        foreach (PointF roiPoint in roiPoints)
        {
            // raw coordinates for visual representation on screen
            RectangleF rectF = ImageOperations.GetRectangle(roiPoint, (int)roiRadius);
            Rectangle rect = Rectangle.Round(rectF);
            gfx.DrawRectangle(Pens.White, rect);

            // scaled coordinates for analzying data
            PointF scaledPoint = new(roiPoint.X * ScaleX, roiPoint.Y * ScaleY);
            RectangleF scaledRectF = ImageOperations.GetRectangle(scaledPoint, (int)roiRadius);
            Rectangle scaledRect = Rectangle.Round(scaledRectF);
            RoiAnalysis analysis = new(Red, Green, scaledRect);
            if (analysis.PixelsAboveThreshold > 0)
                ratios.Add(analysis.MedianRatio * 100);
        }

        pictureBox1.Image?.Dispose();
        pictureBox1.Image = bmp;
        pictureBox1.Refresh();

        formsPlot1.Plot.Clear();

        if (ratios.Count > 1)
        {
            formsPlot1.Plot.AddSignal(ratios.ToArray(), roiRadius);
            formsPlot1.Plot.YLabel("G/R (%)");
            formsPlot1.Plot.XLabel("Distance");
            formsPlot1.Plot.SetAxisLimits(yMin: 0, yMax: ratios.Max() * 1.1);
            formsPlot1.Refresh();
        }
    }

    private void tbRoiSpacing_Scroll(object sender, EventArgs e) { Analyze(); }

    private void tbRoiSize_Scroll(object sender, EventArgs e) => Analyze();
}
