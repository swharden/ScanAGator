namespace ImageRatioTool;

public partial class TSeriesRoiSelector : UserControl
{
    private SciTIF.Image[] RedImages = Array.Empty<SciTIF.Image>();

    private SciTIF.Image[] GreenImages = Array.Empty<SciTIF.Image>();
    private Bitmap[] DisplayImages = Array.Empty<Bitmap>();
    private string ImagePath;

    public int FrameCount => RedImages.Length;

    public bool IsImageLoaded => FrameCount > 0;

    public TSeriesRoiSelector()
    {
        InitializeComponent();
        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        pictureBox1.MouseWheel += PictureBox1_MouseWheel;
        hScrollBar1.ValueChanged += (s, e) => SetFrame(hScrollBar1.Value);
    }

    private void PictureBox1_MouseWheel(object? sender, MouseEventArgs e)
    {
        int newFrame = e.Delta > 0
            ? Math.Min(hScrollBar1.Maximum, hScrollBar1.Value + 1)
            : hScrollBar1.Value = Math.Max(hScrollBar1.Minimum, hScrollBar1.Value - 1);

        SetFrame(newFrame);
    }

    private void SetFrame(int index)
    {
        hScrollBar1.Value = index;
        pictureBox1.Image = DisplayImages[hScrollBar1.Value];
        groupBox1.Text = $"{Path.GetFileName(ImagePath)} ({hScrollBar1.Value}/{hScrollBar1.Maximum})";
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

        SetFrame(0);
    }
}
