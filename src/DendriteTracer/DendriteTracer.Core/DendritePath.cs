namespace DendriteTracer.Core;

public class DendritePath
{
    public readonly int Width;
    public readonly int Height;

    readonly List<Pixel> Points = new();

    public DendritePath(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public void Add(Pixel px)
    {
        Add(px.X, px.Y);
    }

    public void Add(int x, int y)
    {
        if (x < 0 || x >= Width)
            throw new InvalidOperationException($"X ({x}) outside image width ({Width})");

        if (y < 0 || y >= Height)
            throw new InvalidOperationException($"X ({y}) outside image width ({Height})");

        Points.Add(new Pixel(x, y));
    }

    public void Clear()
    {
        Points.Clear();
    }

    public Pixel[] GetEvenlySpacedPoints(double spacing)
    {
        return PathOperations.GetSubPoints(Points, spacing);
    }

    public void Draw(Bitmap bmp)
    {
        bmp.DrawLines(Points, Colors.White);

        foreach (Pixel px in Points)
        {
            Rectangle rect = new(px, 2);
            bmp.DrawRect(rect, Colors.Yellow);
        }
    }
}