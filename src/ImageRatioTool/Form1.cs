namespace ImageRatioTool;

public partial class Form1 : Form
{
    Point Corner1;
    Point Corner2;
    Bitmap? ReferenceImage = null;
    SciTIF.Image? RedImage = null;
    SciTIF.Image? GreenImage = null;

    public Form1()
    {
        InitializeComponent();
        LoadImage(SampleData.RatiometricImage);

        pictureBox1.MouseDown += (s, e) => { Corner1 = e.Location; };
        pictureBox1.MouseMove += (s, e) => { if (e.Button == MouseButtons.Left) { Corner2 = e.Location; DrawSelectionRectangle(); } };
        pictureBox1.MouseUp += (s, e) => { Corner2 = e.Location; DrawSelectionRectangle(); };

        label1.Text = string.Empty;
        label2.Text = string.Empty;
        label3.Text = string.Empty;
    }

    private Rectangle GetSelectionRect()
    {
        int expandX = Corner1.X == Corner2.X ? 1 : 0;
        int expandY = Corner1.Y == Corner2.Y ? 1 : 0;
        int xMin = Math.Min(Corner1.X, Corner2.X);
        int xMax = Math.Max(Corner1.X, Corner2.X) + expandX;
        int yMin = Math.Min(Corner1.Y, Corner2.Y);
        int yMax = Math.Max(Corner1.Y, Corner2.Y) + expandY;
        return new Rectangle(xMin, yMin, xMax - xMin, yMax - yMin);
    }

    private void btnSelectImage_Click(object sender, EventArgs e)
    {
        OpenFileDialog diag = new() { Filter = "TIF files (*.tif)|*.tif", Title = "Select a 2-channel TIF file", };
        if (diag.ShowDialog() == DialogResult.OK)
            LoadImage(diag.FileName);
    }

    private void LoadImage(string ratiometricImagePath)
    {
        SciTIF.TifFile tif = new(ratiometricImagePath);
        if (tif.Frames != 1 || tif.Slices != 1 || tif.Channels != 2)
            throw new ArgumentException("Image must be a 1-frame, 1-slice, 2-channel TIF");

        RedImage = tif.GetImage(0, 0, 0);
        GreenImage = tif.GetImage(0, 0, 1);

        ReferenceImage = MakeDisplayImage(RedImage, GreenImage);
        pictureBox1.Width = ReferenceImage.Width;
        pictureBox1.Height = ReferenceImage.Height;
        DrawSelectionRectangle();
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

    private void DrawSelectionRectangle()
    {
        if (ReferenceImage is null || RedImage is null || GreenImage is null)
            return;

        Image? originalImage = pictureBox1.Image;

        Bitmap bmp = new(ReferenceImage);
        using Graphics gfx = Graphics.FromImage(bmp);

        // outline the rectangle
        Rectangle rect = GetSelectionRect();
        label1.Text = $"X: [{rect.Left}, {rect.Right}]";
        label2.Text = $"Y: [{rect.Top}, {rect.Bottom}]";

        gfx.DrawRectangle(Pens.Yellow, rect);

        // analyze source data
        SciTIF.Image croppedRed = RedImage.Crop(rect.Left, rect.Right, rect.Top, rect.Bottom);
        SciTIF.Image croppedGreen = GreenImage.Crop(rect.Left, rect.Right, rect.Top, rect.Bottom);
        double threshold = CalculateThreshold(croppedRed);
        MeasureGreenOverRedRatio(croppedRed, croppedGreen, threshold);

        // show source data preview image
        Image? originalImage2 = pictureBox2.Image;
        pictureBox2.Image = MakeDisplayImage(croppedRed, croppedGreen);
        pictureBox2.Refresh();
        originalImage2?.Dispose();
        pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

        pictureBox1.Image = bmp;
        pictureBox1.Refresh();
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
        formsPlot1.Plot.Title($"Pixel Intensitites (n={redValues.Length:N0})");
        formsPlot1.Plot.YLabel("Intensity (AFU)");
        formsPlot1.Plot.AddSignal(redValues);
        formsPlot1.Plot.AddVerticalLine(noiseFloorIndex, Color.Black, 1, ScottPlot.LineStyle.Dot, "floor");
        formsPlot1.Plot.AddHorizontalLine(noiseFloor, Color.Black, 1, ScottPlot.LineStyle.Dot);
        formsPlot1.Plot.AddVerticalLine(thresholdIndex, Color.Black, 1, ScottPlot.LineStyle.Dash, "threshold");
        formsPlot1.Plot.AddHorizontalLine(signalThreshold, Color.Black, 1, ScottPlot.LineStyle.Dash);
        formsPlot1.Plot.Legend(true, ScottPlot.Alignment.UpperLeft);
        formsPlot1.Refresh();

        return signalThreshold;
    }

    private void MeasureGreenOverRedRatio(SciTIF.Image red, SciTIF.Image green, double threshold)
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
                double ratio = greenValue / redValue;
                ratios.Add(ratio);
            }
        }

        if (ratios.Count == 0)
        {
            label3.Text = "No pixels were suffeciently above the noise floor";
            formsPlot2.Plot.Clear();
            formsPlot2.Plot.Title(string.Empty);
            formsPlot2.Refresh();
            return;
        }

        double percent = 100.0 * ratios.Count / (red.Width * red.Height);
        label3.Text = $"n={ratios.Count} ({percent:#.##}%)";

        double[] sortedRatios = ratios.OrderBy(x => x).ToArray();
        int medianIndex = sortedRatios.Length / 2;
        double medianRatio = sortedRatios[medianIndex];

        formsPlot2.Plot.Clear();
        formsPlot2.Plot.YLabel("G/R Ratio");
        formsPlot2.Plot.Title($"Median G/R Ratio: {medianRatio:#.###}");
        formsPlot2.Plot.AddSignal(sortedRatios);
        formsPlot2.Plot.AddVerticalLine(medianIndex, Color.Black, 1, ScottPlot.LineStyle.Dot);
        var hline = formsPlot2.Plot.AddHorizontalLine(medianRatio, Color.Black, 1, ScottPlot.LineStyle.Dash);
        hline.PositionLabel = true;
        hline.PositionLabelOppositeAxis = true;
        formsPlot2.Plot.Layout(right: 50);
        formsPlot2.Refresh();
    }
}