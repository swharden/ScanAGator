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
        pictureBox1.MouseMove += (s, e) => { if (e.Button == MouseButtons.Left) { Corner2 = e.Location; AnalyzeROI(); } };
        pictureBox1.MouseUp += (s, e) => { Corner2 = e.Location; AnalyzeROI(); };
        btnCopyImage.Click += (s, e) => Clipboard.SetImage(pictureBox1.Image);
    }

    private Rectangle GetSelectionRect()
    {
        int expandX = Corner1.X == Corner2.X ? 1 : 0;
        int expandY = Corner1.Y == Corner2.Y ? 1 : 0;
        int xMin = Math.Min(Corner1.X, Corner2.X);
        int xMax = Math.Max(Corner1.X, Corner2.X) + expandX;
        int yMin = Math.Min(Corner1.Y, Corner2.Y);
        int yMax = Math.Max(Corner1.Y, Corner2.Y) + expandY;

        // TODO: don't allow the rectangle to get larger than the image
        return new Rectangle(xMin, yMin, xMax - xMin, yMax - yMin);
    }

    private void btnSelectImage_Click(object sender, EventArgs e)
    {
        OpenFileDialog diag = new()
        {
            Filter = "TIF files (*.tif)|*.tif",
            Title = "Select a 2-channel TIF file",
        };

        if (diag.ShowDialog() == DialogResult.OK)
            LoadImage(diag.FileName);
    }

    private void LoadImage(string ratiometricImagePath)
    {
        Text = Path.GetFileName(ratiometricImagePath);

        SciTIF.TifFile tif = new(ratiometricImagePath);
        if (tif.Frames != 1 || tif.Slices != 1 || tif.Channels != 2)
            throw new ArgumentException("Image must be a 1-frame, 1-slice, 2-channel TIF");

        RedImage = tif.GetImage(0, 0, 0);
        GreenImage = tif.GetImage(0, 0, 1);
        ReferenceImage = ImageOperations.MakeDisplayImage(RedImage, GreenImage);
        pictureBox1.Width = ReferenceImage.Width;
        pictureBox1.Height = ReferenceImage.Height;
        AnalyzeROI();
    }

    private void AnalyzeROI()
    {
        if (ReferenceImage is null || RedImage is null || GreenImage is null)
            return;

        RoiAnalysis roi = new(RedImage, GreenImage, GetSelectionRect());

        GraphOperations.PlotIntensities(formsPlot1, roi);
        GraphOperations.PlotRatios(formsPlot2, roi);

        Image? originalImage = pictureBox1.Image;
        pictureBox1.Image = ImageOperations.Annotate(ReferenceImage, roi);
        pictureBox1.Refresh();
        originalImage?.Dispose();
    }
}