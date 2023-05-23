namespace DendriteTracer.Gui;

public class ImageTracerControl : UserControl
{
    private readonly PictureBox PictureBox1 = new()
    {
        Dock = DockStyle.Fill,
        BackColor = SystemColors.ControlDark,
        Cursor = Cursors.Cross,
    };

    private readonly List<PointF> ClickedPoints = new();

    private Bitmap? SourceImage;

    private readonly int ControlPointRadius = 5;

    private int? DraggingPointIndex = null;

    public ImageTracerControl()
    {
        Controls.Add(PictureBox1);

        PictureBox1.MouseClick += (s, e) =>
        {
            if (DraggingPointIndex is not null)
            {
                AnnotateImage();
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                ClickedPoints.Add(e.Location);
                AnnotateImage();
                return;
            }

            if (e.Button == MouseButtons.Right)
            {
                ClickedPoints.Clear();
                AnnotateImage();
                return;
            }
        };

        PictureBox1.MouseMove += (s, e) =>
        {
            if (DraggingPointIndex is not null)
            {
                ClickedPoints[DraggingPointIndex.Value] = e.Location;
                AnnotateImage();
                return;
            }

            int? index = GetPointUnderMouse(e.Location);
            Cursor = index is null ? Cursors.Cross : Cursors.Hand;
        };

        PictureBox1.MouseDown += (s, e) =>
        {
            DraggingPointIndex = GetPointUnderMouse(e.Location);
        };

        PictureBox1.MouseUp += (s, e) =>
        {
            DraggingPointIndex = null;
        };
    }

    private int? GetPointUnderMouse(PointF mouse)
    {
        for (int i = 0; i < ClickedPoints.Count; i++)
        {
            PointF pt = ClickedPoints[i];
            float dx = Math.Abs(pt.X - mouse.X);
            float dy = Math.Abs(pt.Y - mouse.Y);
            if (dx <= ControlPointRadius && dy <= ControlPointRadius)
            {
                return i;
            }
        }

        return null;
    }

    public void SetImage(Core.Bitmap bmp)
    {
        byte[] bytes = bmp.GetBitmapBytes();
        using MemoryStream ms = new(bytes);
        using Image img = Bitmap.FromStream(ms);

        SourceImage?.Dispose();
        SourceImage = new(Width, Height);
        using Graphics gfx = Graphics.FromImage(SourceImage);
        gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        gfx.DrawImage(img, 0, 0, Width, Height);

        AnnotateImage();
    }

    private void AnnotateImage()
    {
        if (SourceImage is null)
            return;

        Bitmap bmp = new(SourceImage);
        using Graphics gfx = Graphics.FromImage(bmp);
        gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        if (ClickedPoints.Count > 1)
            gfx.DrawLines(Pens.White, ClickedPoints.ToArray());

        foreach (PointF pt in ClickedPoints)
        {
            RectangleF rect = new(
                x: pt.X - ControlPointRadius,
                y: pt.Y - ControlPointRadius,
                width: ControlPointRadius * 2,
                height: ControlPointRadius * 2);

            gfx.DrawEllipse(Pens.Yellow, rect);
        }

        Image old = PictureBox1.Image;
        PictureBox1.Image = bmp;
        old?.Dispose();
    }
}
