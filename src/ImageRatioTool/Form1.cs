namespace ImageRatioTool;

public partial class Form1 : Form
{
    readonly List<Point> Corners = new();
    Bitmap? ReferenceImage = null;

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
        // use this 13-bit data for math
        SciTIF.TifFile red = new(imagePathRed);
        SciTIF.TifFile green = new(imagePathGreen);

        // create 8-bit images for viewing
        int divisionFactor = 1 << (13 - 8);

        SciTIF.Image redImage = red.GetImage();
        redImage /= divisionFactor;

        SciTIF.Image greenImage = green.GetImage();
        greenImage /= divisionFactor;

        SciTIF.ImageRGB merge = new(redImage, greenImage, redImage);

        ReferenceImage = merge.GetBitmap();
        pictureBox1.Width = merge.Width;
        pictureBox1.Height = merge.Height;
        DrawOutline();
    }

    private void DrawOutline()
    {
        if (ReferenceImage is null)
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
            Rectangle rect = GetSelectionRect();
            gfx.DrawRectangle(Pens.Yellow, rect);
        }

        pictureBox1.Image = bmp;

        originalImage?.Dispose();
    }
}
