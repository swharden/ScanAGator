namespace ImageRatioTool;

public partial class TSeriesRoiSelector : UserControl
{
    private SciTIF.Image[] RedImages = Array.Empty<SciTIF.Image>();

    private SciTIF.Image[] GreenImages = Array.Empty<SciTIF.Image>();
    private Bitmap[] DisplayImages = Array.Empty<Bitmap>();
    private string ImagePath = string.Empty;

    public readonly RoiRectangle Roi = new();

    public event EventHandler<RoiAnalysis> AnalysisUpdated = delegate { };

    public TSeriesRoiSelector()
    {
        InitializeComponent();

        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        pictureBox1.MouseWheel += PictureBox1_MouseWheel;
        pictureBox1.MouseDown += PictureBox1_MouseDown;
        pictureBox1.MouseUp += PictureBox1_MouseUp;
        pictureBox1.MouseMove += PictureBox1_MouseMove;

        hScrollBar1.ValueChanged += (s, e) => SetFrame(hScrollBar1.Value);
    }

    private void PictureBox1_MouseDown(object? sender, MouseEventArgs e)
    {
    }

    private void PictureBox1_MouseMove(object? sender, MouseEventArgs e)
    {
    }

    private void PictureBox1_MouseUp(object? sender, MouseEventArgs e)
    {
    }

    private void PictureBox1_MouseWheel(object? sender, MouseEventArgs e)
    {
        int newFrame = e.Delta < 0
            ? Math.Min(hScrollBar1.Maximum, hScrollBar1.Value + 1)
            : hScrollBar1.Value = Math.Max(hScrollBar1.Minimum, hScrollBar1.Value - 1);

        SetFrame(newFrame);
    }

    private void SetFrame(int index)
    {
        hScrollBar1.Value = index;
        groupBox1.Text = $"{Path.GetFileName(ImagePath)} ({hScrollBar1.Value}/{hScrollBar1.Maximum})";
        Analyze();
    }

    public void Analyze()
    {
        Bitmap bmp = new(DisplayImages[hScrollBar1.Value]);
        using Graphics gfx = Graphics.FromImage(bmp);
        ImageOperations.DrawRoiRectangle(gfx, Roi);

        var oldBmp = pictureBox1.Image;
        pictureBox1.Image = bmp;
        oldBmp?.Dispose();

        SciTIF.Image red = RedImages[hScrollBar1.Value];
        SciTIF.Image green = GreenImages[hScrollBar1.Value];
        RoiAnalysis analysis = new(red, green, Roi.Rect);

        AnalysisUpdated.Invoke(this, analysis);
    }

    public void LoadFile(string path)
    {
        ImagePath = Path.GetFullPath(path);
        SciTIF.TifFile tif = new(path);

        if (tif.Frames < 2)
            throw new ArgumentException($"TIF must have multiple frames: {path}");

        if (tif.Channels != 2)
            throw new ArgumentException($"TIF must have 2 channels: {path}");

        if (tif.Slices != 1)
            throw new ArgumentException($"TIF must have 1 slice: {path}");

        RedImages = new SciTIF.Image[tif.Frames];
        GreenImages = new SciTIF.Image[tif.Frames];
        DisplayImages = new Bitmap[tif.Frames];

        for (int i = 0; i < tif.Frames; i++)
        {
            RedImages[i] = tif.GetImage(i, 0, 0);
            GreenImages[i] = tif.GetImage(i, 0, 1);
            DisplayImages[i] = ImageOperations.MakeDisplayImage(RedImages[i], GreenImages[i]);
        }

        hScrollBar1.Value = 0;
        hScrollBar1.Maximum = tif.Frames - 1;

        Roi.Update(
            x1: (int)(tif.Width * .6),
            y1: (int)(tif.Height * .3),
            x2: (int)(tif.Width * .9),
            y2: (int)(tif.Height * .7));

        SetFrame(0);
    }
}
