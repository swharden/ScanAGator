using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Runtime.Intrinsics.X86;
using System.Windows.Forms;

namespace ImageRatioTool;

public partial class Form1 : Form
{
    readonly List<Point> Corners = new();
    Bitmap? ReferenceImage = null;
    SciTIF.Image? RedImage = null;
    SciTIF.Image? GreenImage = null;

    public Form1()
    {
        InitializeComponent();
        SetImages(SampleData.RedImage, SampleData.GreenImage);

        pictureBox1.MouseDown += PictureBox1_MouseDown;
    }

    private void PictureBox1_MouseDown(object? sender, MouseEventArgs e)
    {
        Corners.Add(e.Location);

        if (Corners.Count == 3)
            Corners.Clear();

        DrawOutline();
    }

    private Rectangle GetSelectionRect()
    {
        if (Corners.Count != 2)
            throw new InvalidOperationException();

        Point corner1 = Corners[0];
        Point corner2 = Corners[1];

        if (corner1.X == corner2.X)
            corner2 = new Point(corner1.X + 1, corner2.Y);

        if (corner1.Y == corner2.Y)
            corner2 = new Point(corner2.X, corner1.Y + 1);

        int xMin = Math.Min(corner1.X, corner2.X);
        int xMax = Math.Max(corner1.X, corner2.X);
        int yMin = Math.Min(corner1.Y, corner2.Y);
        int yMax = Math.Max(corner1.Y, corner2.Y);
        return new Rectangle(xMin, yMin, xMax - xMin, yMax - yMin);
    }

    private void btnSetImages_Click(object sender, EventArgs e)
    {
        SetImages(SampleData.RedImage, SampleData.GreenImage);
    }

    private void SetImages(string imagePathRed, string imagePathGreen)
    {
        // use the original high bit depth data for math
        SciTIF.TifFile red = new(imagePathRed);
        RedImage = red.GetImage();
        SciTIF.TifFile green = new(imagePathGreen);
        GreenImage = green.GetImage();

        if ((red.Width != green.Width) || (red.Height != green.Height))
            throw new InvalidOperationException("images have different dimensions");

        ReferenceImage = MakeDisplayImage(RedImage, GreenImage);
        pictureBox1.Width = ReferenceImage.Width;
        pictureBox1.Height = ReferenceImage.Height;
        DrawOutline();
    }

    private Bitmap MakeDisplayImage(SciTIF.Image red, SciTIF.Image green)
    {
        int divisionFactor = 1 << (13 - 8); // 13-bit to 8-bit
        var displayRed = red / divisionFactor;
        var displayGreen = green / divisionFactor;
        SciTIF.ImageRGB displayMerge = new(displayRed, displayGreen, displayRed);
        Bitmap bmp = displayMerge.GetBitmap();
        return bmp;
    }

    private void DrawOutline()
    {
        if (ReferenceImage is null || RedImage is null || GreenImage is null)
            return;

        Image? originalImage = pictureBox1.Image;

        Bitmap bmp = new(ReferenceImage);
        using Graphics gfx = Graphics.FromImage(bmp);

        foreach (Point pt in Corners)
        {
            int r = 2;
            Rectangle rect = new(pt.X - r, pt.Y - r, r * 2, r * 2);
            gfx.FillEllipse(Brushes.Yellow, rect);
        }

        if (Corners.Count == 2)
        {
            // outline the rectangle
            Rectangle rect = GetSelectionRect();
            if (rect.Width < 1 || rect.Height < 1)
                return;

            gfx.DrawRectangle(Pens.Yellow, rect);

            // analyze source data
            SciTIF.Image croppedRed = RedImage.Crop(rect.Left, rect.Right, rect.Top, rect.Bottom);
            SciTIF.Image croppedGreen = GreenImage.Crop(rect.Left, rect.Right, rect.Top, rect.Bottom);
            double threshold = CalculateThreshold(croppedRed);
            AnalyzeRatio(croppedRed, croppedGreen, threshold);

            // show source data preview image
            Image? originalImage2 = pictureBox2.Image;
            pictureBox2.Image = MakeDisplayImage(croppedRed, croppedGreen);
            originalImage2?.Dispose();
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }

        pictureBox1.Image = bmp;

        originalImage?.Dispose();
    }

    private double CalculateThreshold(SciTIF.Image red, double noiseFloorFraction = 0.2, double signalThresholdNoiseFloorMultiple = 5)
    {
        double[] redValues = red.Values.OrderBy(x => x).ToArray();
        int noiseFloorIndex = (int)(redValues.Length * noiseFloorFraction);
        double noiseFloor = redValues[noiseFloorIndex];

        double signalThreshold = noiseFloor * signalThresholdNoiseFloorMultiple;
        int thresholdIndex;
        for (thresholdIndex = 0; thresholdIndex < redValues.Length; thresholdIndex++)
        {
            if (redValues[thresholdIndex] >= signalThreshold)
                break;
        }

        formsPlot1.Plot.Clear();
        formsPlot1.Plot.YLabel("Intensity (AFU)");
        formsPlot1.Plot.AddSignal(redValues);

        // floor
        formsPlot1.Plot.AddVerticalLine(noiseFloorIndex, Color.Black, 1, ScottPlot.LineStyle.Dot, "floor");
        formsPlot1.Plot.AddHorizontalLine(noiseFloor, Color.Black, 1, ScottPlot.LineStyle.Dot);

        // signal
        formsPlot1.Plot.AddVerticalLine(thresholdIndex, Color.Black, 1, ScottPlot.LineStyle.Dash, "threshold");
        formsPlot1.Plot.AddHorizontalLine(signalThreshold, Color.Black, 1, ScottPlot.LineStyle.Dash);

        formsPlot1.Plot.Legend(true, ScottPlot.Alignment.UpperLeft);
        formsPlot1.Refresh();

        return signalThreshold;
    }

    private void AnalyzeRatio(SciTIF.Image red, SciTIF.Image green, double threshold)
    {
        List<double> ratios = new();

        for (int x = 0; x < red.Width; x++)
        {
            for (int y = 0; y < red.Height; y++)
            {
                double redValue = red.GetPixel(x, y);
                if (redValue < threshold)
                    continue;

                double greenValue = green.GetPixel(x, y);
                double ratio = redValue / greenValue;
                ratios.Add(ratio);
            }
        }

        double[] sortedRatios = ratios.OrderBy(x => x).ToArray();
        double medianRatio = sortedRatios[sortedRatios.Length / 2];

        formsPlot2.Plot.Clear();
        formsPlot2.Plot.YLabel("G/R Ratio");
        formsPlot2.Plot.Title($"Median G/R Ratio: {medianRatio:#.###}");
        formsPlot2.Plot.AddSignal(sortedRatios);
        formsPlot2.Refresh();
    }
}
