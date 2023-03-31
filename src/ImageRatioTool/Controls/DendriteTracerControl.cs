using System.Data;
using System.Globalization;
using System.Text;

namespace ImageRatioTool.Controls;

public partial class DendriteTracerControl : UserControl
{
    private SciTIF.Image[] RedImages = Array.Empty<SciTIF.Image>();
    private SciTIF.Image[] GreenImages = Array.Empty<SciTIF.Image>();
    private Bitmap[] DisplayImages = Array.Empty<Bitmap>();
    private double ImageMicronsPerPixel = 1;
    double MicronsPerRoi => ResultRoiSpacing * ImageMicronsPerPixel * ScaleX;
    double MicronsRoiRadius => tbRoiSize.Value * ImageMicronsPerPixel * ScaleX;

    private readonly List<double[]> ResultCurves = new();
    private double ResultRoiSpacing;
    private string TifFilePath = string.Empty;

    readonly List<FractionalPoint> FPoints = new();
    float ScaleX => RedImages.Any() ? (float)RedImages.First().Width / pictureBox1.Width : 1;
    float ScaleY => RedImages.Any() ? (float)RedImages.First().Height / pictureBox1.Height : 1;

    public DendriteTracerControl()
    {
        InitializeComponent();

        AllowDrop = true;
        DragEnter += OnDragEnter;
        DragDrop += OnDragDrop;
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

        AnalyzeSingleFrame();
    }

    private void OnDragDrop(object? sender, DragEventArgs e)
    {
        string[] paths = (string[])e.Data!.GetData(DataFormats.FileDrop)!;
        SetData(paths.First());
    }

    private void OnDragEnter(object? sender, DragEventArgs e)
    {
        if (e.Data!.GetDataPresent(DataFormats.FileDrop))
            e.Effect = DragDropEffects.Copy;
    }

    private void PictureBox1_MouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {
            FPoints.Clear();
            AnalyzeSingleFrame();
            formsPlot1.Plot.Clear();
            formsPlot1.Refresh();
            return;
        }

        double fracX = (double)e.X / pictureBox1.Width;
        double fracY = (double)e.Y / pictureBox1.Height;
        FractionalPoint pt = new(fracX, fracY);
        FPoints.Add(pt);
        AnalyzeSingleFrame();
    }

    public void SetData(string tifFilePath)
    {
        TifFilePath = Path.GetFullPath(tifFilePath);
        ImageMicronsPerPixel = TifFileOperations.GetMicronsPerPixel(tifFilePath);
        var oldImages = DisplayImages.ToList();
        (RedImages, GreenImages, DisplayImages) = ImageOperations.GetMultiFrameRatiometricImages(tifFilePath);
        oldImages.ForEach(x => x.Dispose());
        hScrollBar1.Value = 0;
        hScrollBar1.Maximum = RedImages.Length - 1;
        SetSlice(0);
    }

    public void SetSlice(int index, bool analyze = true)
    {
        hScrollBar1.Value = index;
        if (analyze)
            AnalyzeSingleFrame();
    }

    public void AnalyzeSingleFrame(bool plotSingleFrame = true)
    {
        if (!RedImages.Any())
            return;

        SciTIF.Image red = RedImages[hScrollBar1.Value];
        SciTIF.Image green = GreenImages[hScrollBar1.Value];
        Bitmap referenceImage = DisplayImages[hScrollBar1.Value];

        double roiSpacing = tbRoiSpacing.Value;
        double roiRadius = tbRoiSize.Value;

        Bitmap bmp = new(pictureBox1.Width, pictureBox1.Height);
        using Graphics gfx = Graphics.FromImage(bmp);
        gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        Rectangle srcRect = new(0, 0, referenceImage.Width, referenceImage.Height);
        Rectangle destRect = new(0, 0, pictureBox1.Width, pictureBox1.Height);
        gfx.DrawImage(referenceImage, destRect, srcRect, GraphicsUnit.Pixel);

        Point[] linePoints = FPoints.Select(x => x.ToPoint(bmp.Width, bmp.Height)).ToArray();
        PointF[] roiPoints = LineOperations.GetSubPoints(linePoints, roiSpacing);

        foreach (Point pt in linePoints)
            ImageOperations.DrawCircle(gfx, pt, Pens.Yellow, 3);

        if (linePoints.Length >= 2)
            gfx.DrawLines(Pens.Yellow, linePoints);

        foreach (PointF roiPoint in roiPoints)
        {
            RectangleF rectF = ImageOperations.GetRectangle(roiPoint, (int)roiRadius);
            Rectangle rect = Rectangle.Round(rectF);
            gfx.DrawRectangle(Pens.White, rect);
        }

        var oldImage = pictureBox1.Image;
        pictureBox1.Image = bmp;
        pictureBox1.Refresh();
        oldImage?.Dispose();

        // perform analysis
        List<double> ratios = new();
        foreach (PointF roiPoint in roiPoints)
        {
            // raw coordinates for visual representation on screen
            RectangleF rectF = ImageOperations.GetRectangle(roiPoint, (int)roiRadius);
            Rectangle rect = Rectangle.Round(rectF);

            // scaled coordinates for analzying data
            PointF scaledPoint = new(roiPoint.X * ScaleX, roiPoint.Y * ScaleY);
            RectangleF scaledRectF = ImageOperations.GetRectangle(scaledPoint, (int)roiRadius);
            Rectangle scaledRect = Rectangle.Round(scaledRectF);
            RoiAnalysis analysis = new(red, green, scaledRect);
            if (analysis.PixelsAboveThreshold > 0)
                ratios.Add(analysis.MedianRatio * 100);
        }

        ResultRoiSpacing = roiSpacing;

        if (plotSingleFrame)
        {
            ResultCurves.Clear();
            ResultCurves.Add(ratios.ToArray());
            PlotResults();
        }
        else
        {
            ResultCurves.Add(ratios.ToArray());
        }
    }

    private void PlotResults()
    {
        lblRoiSpacing.Text = $"ROI Spacing: {MicronsPerRoi:N2} µm";
        lblRoiSpacing.Refresh();

        lblRoiSize.Text = $"ROI Size: {MicronsRoiRadius:N2} µm";
        lblRoiSize.Refresh();

        formsPlot1.Plot.Clear();

        double maxValue = 0;

        for (int i = 0; i < ResultCurves.Count; i++)
        {
            if (ResultCurves[i].Length < 2)
                continue;

            maxValue = Math.Max(maxValue, ResultCurves[i].Max());

            Color color = ResultCurves.Count > 1
               ? ScottPlot.Drawing.Colormap.Turbo.GetColor((double)i / ResultCurves.Count)
               : ScottPlot.Drawing.Colormap.Turbo.GetColor((double)hScrollBar1.Value / hScrollBar1.Maximum);

            var sig = formsPlot1.Plot.AddSignal(ResultCurves[i], 1.0 / MicronsPerRoi, color);

            if (cbDistributeHorizontally.Checked)
            {
                sig.OffsetX = ResultRoiSpacing * ResultCurves[i].Length * i;
                sig.LineWidth = 3;
            }
        }

        formsPlot1.Plot.YLabel("G/R (%)");
        formsPlot1.Plot.XLabel("Distance");
        if (maxValue > 0)
            formsPlot1.Plot.SetAxisLimits(yMin: 0, yMax: maxValue * 1.1);
        formsPlot1.Refresh();
    }

    private void tbRoiSpacing_Scroll(object sender, EventArgs e) => AnalyzeSingleFrame();

    private void tbRoiSize_Scroll(object sender, EventArgs e) => AnalyzeSingleFrame();

    private void hScrollBar1_Scroll(object sender, ScrollEventArgs e) => SetSlice(hScrollBar1.Value);

    private void btnAnalyzeAllFrames_Click(object sender, EventArgs e) => AnalyzeAllFrames();

    private void AnalyzeAllFrames()
    {
        ResultCurves.Clear();
        for (int i = 0; i < RedImages.Length; i++)
        {
            SetSlice(i, analyze: false);
            AnalyzeSingleFrame(plotSingleFrame: false);
        }
        PlotResults();
    }

    private void cbDistributeHorizontally_CheckedChanged(object sender, EventArgs e)
    {
        PlotResults();
    }

    /// <summary>
    /// Copy values of the first visible curve to the clipboard
    /// </summary>
    private void btnCopyData_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new();
        for (int row = 0; row < ResultCurves.First().Length; row++)
        {
            double x = MicronsPerRoi * row;
            sb.Append(x.ToString());
            for (int col = 0; col < ResultCurves.Count(); col++)
            {
                sb.Append(" " + ResultCurves[col][row].ToString());
            }
            sb.AppendLine();
        }
        Clipboard.SetText(sb.ToString());
    }

    private void btnSaveData_Click(object sender, EventArgs e)
    {
        AnalyzeAllFrames();

        SquareRoiCollection rois = new(TifFilePath, GreenImages.First().Width, GreenImages.First().Height);
        
        // TODO: read times from XML somehow
        //double[] times = XmlFileOperations.GetSequenceTimes(TifFilePath);
        double[] times = Enumerable.Range(0, ResultCurves.Count).Select(x => (double)x).ToArray();

        string saveAs = TifFilePath + ".csv";
        rois.Save(saveAs, ResultCurves, times);
        System.Diagnostics.Process.Start("explorer.exe", $"/select, \"{saveAs}\"");
    }
}
