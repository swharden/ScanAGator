using System.Drawing;

namespace DendriteTracer.Core;

public class DendritePath
{
    public int Width { get; private set; }
    public int Height { get; private set; }

    public readonly List<Pixel> Points = new();
    public int Count => Points.Count;

    public DendritePath(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public void Resize(int width, int height)
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

    public string GetPointsString()
    {
        return string.Join(" ", Points.Select(x => $"{x.X},{x.Y};"));
    }

    public void LoadFromString(string text)
    {
        Points.Clear();
        foreach (string line in text.Split(";"))
        {
            string[] parts = line.Split(",");
            if (parts.Length != 2)
                continue;
            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);
            Pixel px = new(x, y);
            Points.Add(px);
        }
    }
}