using System.Data;

namespace ImageRatioTool.Controls;

public partial class DendriteTracerControl : UserControl
{
    Bitmap ReferenceImage = new(256, 256);
    readonly List<FractionalPoint> FPoints = new();

    public DendriteTracerControl()
    {
        InitializeComponent();

        pictureBox1.MouseDown += PictureBox1_MouseDown;

        // initial data
        FPoints.Add(new(0.4570, 0.4824));
        FPoints.Add(new(0.5840, 0.4922));
        FPoints.Add(new(0.6211, 0.4277));
        FPoints.Add(new(0.7148, 0.4297));
        FPoints.Add(new(0.8867, 0.5039));
        FPoints.Add(new(0.9258, 0.6191));
        FPoints.Add(new(0.9395, 0.6855));
        FPoints.Add(new(0.9004, 0.7207));

        UpdateDrawing();
    }

    private void PictureBox1_MouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {
            FPoints.Clear();
            UpdateDrawing();
            return;
        }

        double fracX = (double)e.X / pictureBox1.Width;
        double fracY = (double)e.Y / pictureBox1.Height;
        FractionalPoint pt = new(fracX, fracY);
        FPoints.Add(pt);
        UpdateDrawing();
    }

    public void SetReferenceImage(Bitmap bmp)
    {
        ReferenceImage?.Dispose();
        ReferenceImage = bmp;
        UpdateDrawing();
    }

    public void UpdateDrawing()
    {
        Bitmap bmp = new(pictureBox1.Width, pictureBox1.Height);
        using Graphics gfx = Graphics.FromImage(bmp);
        gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        Rectangle srcRect = new(0, 0, ReferenceImage.Width, ReferenceImage.Height);
        Rectangle destRect = new(0, 0, pictureBox1.Width, pictureBox1.Height);
        gfx.DrawImage(ReferenceImage, destRect, srcRect, GraphicsUnit.Pixel);

        Point[] linePoints = FPoints.Select(x => x.ToPoint(bmp.Width, bmp.Height)).ToArray();
        PointF[] roiPoints = LineOperations.GetSubPoints(linePoints, tbRoiSpacing.Value);

        if (linePoints.Length >= 2)
            gfx.DrawLines(Pens.Yellow, linePoints);

        foreach (PointF roiPoint in roiPoints)
            ImageOperations.DrawRectangle(gfx, roiPoint, Pens.White, tbRoiSize.Value);

        pictureBox1.Image?.Dispose();
        pictureBox1.Image = bmp;
        pictureBox1.Refresh();
    }

    private void tbRoiSpacing_Scroll(object sender, EventArgs e) { UpdateDrawing(); }

    private void tbRoiSize_Scroll(object sender, EventArgs e) => UpdateDrawing();
}
