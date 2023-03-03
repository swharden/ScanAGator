namespace ImageRatioTool;

public class RoiRectangle
{
    public int X1;
    public int Y1;
    public int X2;
    public int Y2;

    public int XMin => Math.Min(X1, X2);
    public int YMin => Math.Min(Y1, Y2); // TOP of the image
    public int XCenter => (X1 + X2) / 2;
    public int XMax => Math.Max(X1, X2);
    public int YMax => Math.Max(Y1, Y2); // BOTTOM of the image
    public int YCenter => (Y1 + Y2) / 2;
    public int Width => XMax - XMin;
    public int Height => YMax - YMin;

    public Rectangle Rect => new(XMin, YMin, Width, Height);

    public Point[] Corners => new Point[]
    {
        new Point(X1, Y1),
        new Point(X1, Y2),
        new Point(X2, Y1),
        new Point(X2, Y2),
    };

    public Point[] Centers => new Point[]
    {
        new Point(XCenter, Y1),
        new Point(XCenter, Y2),
        new Point(X1, YCenter),
        new Point(X2, YCenter),
    };

    public void Update(Point p1, Point p2)
    {
        X1 = p1.X;
        Y1 = p1.Y;
        X2 = p2.X;
        Y2 = p2.Y;
    }

    public void Update(int x1, int y1, int x2, int y2)
    {
        X1 = x1;
        Y1 = y1;
        X2 = x2;
        Y2 = y2;
    }
}
