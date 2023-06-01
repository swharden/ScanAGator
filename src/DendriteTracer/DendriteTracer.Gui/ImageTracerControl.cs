using DendriteTracer.Core;

namespace DendriteTracer.Gui;

public class ImageTracerControl : UserControl
{
    private bool RenderNeeded = false;

    private readonly System.Windows.Forms.Timer RenderTimer = new()
    {
        Interval = 20,
        Enabled = true
    };

    private readonly PictureBox PictureBox1 = new()
    {
        Dock = DockStyle.Fill,
        BackColor = SystemColors.ControlDark,
        Cursor = Cursors.Cross,
    };

    public readonly DendritePath DendritePath = new(0, 0);

    private System.Drawing.Bitmap? SourceImage;

    private readonly int ControlPointRadius = 5;

    private int? DraggingPointIndex = null;

    public event EventHandler PointsChanged = delegate { };

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
                DendritePath.Add(e.Location.X, e.Location.Y);
                PointsChanged.Invoke(this, EventArgs.Empty);
                AnnotateImage();
                return;
            }

            if (e.Button == MouseButtons.Right)
            {
                DendritePath.Clear();
                PointsChanged.Invoke(this, EventArgs.Empty);
                AnnotateImage();
                return;
            }
        };

        PictureBox1.MouseMove += (s, e) =>
        {
            if (DraggingPointIndex is not null)
            {
                DendritePath.Points[DraggingPointIndex.Value] = new(e.Location.X, e.Location.Y);
                PointsChanged.Invoke(this, EventArgs.Empty);
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

        RenderTimer.Tick += (s, e) =>
        {
            if (RenderNeeded)
            {
                AnnotateImageNow();
                RenderNeeded = false;
            }
        };
    }

    private int? GetPointUnderMouse(PointF mouse)
    {
        for (int i = 0; i < DendritePath.Count; i++)
        {
            float dx = Math.Abs(DendritePath.Points[i].X - mouse.X);
            float dy = Math.Abs(DendritePath.Points[i].Y - mouse.Y);
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
        using Image img = System.Drawing.Bitmap.FromStream(ms);

        SourceImage?.Dispose();
        SourceImage = new(Width, Height);
        using Graphics gfx = Graphics.FromImage(SourceImage);
        gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        gfx.DrawImage(img, 0, 0, Width, Height);

        DendritePath.Resize(Width, Height);
        AnnotateImage();
    }

    private void AnnotateImage()
    {
        RenderNeeded = true;
    }

    private void AnnotateImageNow()
    {
        if (SourceImage is null)
            return;

        System.Drawing.Bitmap bmp = new(SourceImage);
        using Graphics gfx = Graphics.FromImage(bmp);
        gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        if (DendritePath.Count > 1)
        {
            PointF[] points = DendritePath.Points.Select(pt => new PointF(pt.X, pt.Y)).ToArray();
            gfx.DrawLines(Pens.White, points);
        }

        foreach (Pixel px in DendritePath.Points)
        {
            RectangleF rect = new(
                x: px.X - ControlPointRadius,
                y: px.Y - ControlPointRadius,
                width: ControlPointRadius * 2,
                height: ControlPointRadius * 2);

            gfx.DrawEllipse(Pens.Yellow, rect);
        }

        Image old = PictureBox1.Image;
        PictureBox1.Image = bmp;
        old?.Dispose();
    }
}
