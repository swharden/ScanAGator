namespace DendriteTracer.Core;

internal class DendritePath
{
    public readonly int Width;
    public readonly int Height;

    readonly List<Pixel> Points = new();

    public DendritePath(int width, int height)
    {
        Width = width;
        Height = height;
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
}