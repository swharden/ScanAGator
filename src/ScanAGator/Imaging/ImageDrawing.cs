using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Xml.XPath;

namespace ScanAGator.Imaging;

public static class ImageDrawing
{
    public static void DrawLinescan(Bitmap bmp, Prairie.ParirieXmlFile? pv, StructureRange structure)
    {
        if (pv is null)
            return;

        if (pv.Points.Length < 2)
            return;

        PointF[] points = pv.Points
            .Select(x => new PointF(bmp.Width * x.X, bmp.Height * x.Y))
            .ToArray();

        using Graphics gfx = Graphics.FromImage(bmp);
        gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        using Pen pen1 = new(Color.FromArgb(100, 255, 255, 0), width: 2);
        gfx.DrawLines(pen1, points);

        if (points.Length > 2)
        {
            MarkFreehandEdges(gfx, points, structure);
        }
        else
        {
            MarkStraightEdges(gfx, points, structure);
        }
    }

    private static void MarkStraightEdges(Graphics gfx, PointF[] points, StructureRange structure)
    {
        float x1 = points[0].X;
        float xSpan = points[1].X - points[0].X;
        float y = points[0].Y;

        PointF point1 = new(x1 + structure.MinFraction * xSpan, y);
        PointF point2 = new(x1 + structure.MaxFraction * xSpan, y);

        if (double.IsNaN(point1.X))
            return;

        using Pen pen2 = new(Color.FromArgb(255, 255, 255, 0), width: 3);
        MarkEdge(gfx, pen2, point1);
        MarkEdge(gfx, pen2, point2);
    }

    private static void MarkFreehandEdges(Graphics gfx, PointF[] points, StructureRange structure)
    {
        int index1 = (int)(points.Length * structure.MinFraction) + 1;
        int index2 = (int)(points.Length * structure.MaxFraction) + 1;

        using Pen pen2 = new(Color.FromArgb(255, 255, 255, 0), width: 3);
        MarkEdge(gfx, pen2, points, index1);
        MarkEdge(gfx, pen2, points, index2);
    }

    private static void MarkEdge(Graphics gfx, Pen pen, PointF[] points, int index, float r = 5)
    {
        index = Math.Max(index, 0);
        index = Math.Min(index, points.Length - 1);
        MarkEdge(gfx, pen, points[index]);
    }

    private static void MarkEdge(Graphics gfx, Pen pen, PointF point, float r = 5)
    {
        gfx.DrawRectangle(pen, point.X - r, point.Y - r, r * 2, r * 2);
    }
}
