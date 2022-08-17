using System;
using System.Linq;

namespace ScanAGator;

/// <summary>
/// This object holds data values for a single-channel 2D image.
/// Values are stored as floating point data and contents may exceed magnitude and precision of 8-bit images.
/// </summary>
public class ImageData
{
    public readonly double[] Values;
    public readonly int Width;
    public readonly int Height;

    public ImageData(double[] data, int width, int height)
    {
        Values = data;
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Return the value of a pixel at a specific position
    /// </summary>
    public double GetValue(int x, int y)
    {
        if (x < 0 || x >= Width)
            throw new IndexOutOfRangeException(nameof(x));
        if (y < 0 || y >= Height)
            throw new IndexOutOfRangeException(nameof(y));

        return Values[y * Width + x];
    }

    /// <summary>
    /// Return the mean intensity of all pixels
    /// </summary>
    public double Average()
    {
        return Average(0, Width - 1, 0, Height - 1);
    }


    /// <summary>
    /// Return the mean intensity of all pixels between the given ranges (inclusive)
    /// </summary>
    public double Average(int x1, int x2, int y1, int y2)
    {
        if (x1 < 0 || x1 >= Width)
            throw new IndexOutOfRangeException(nameof(x1));
        if (x2 < 0 || x2 >= Width)
            throw new IndexOutOfRangeException(nameof(x2));
        if (x1 > x2)
            throw new ArgumentException($"{nameof(x1)} must be <= {nameof(x2)}");

        if (y1 < 0 || y1 >= Height)
            throw new IndexOutOfRangeException(nameof(y1));
        if (y2 < 0 || y2 >= Height)
            throw new IndexOutOfRangeException(nameof(y2));
        if (y1 > y2)
            throw new ArgumentException($"{nameof(y1)} must be <= {nameof(y2)}");

        double sum = 0;
        int count = (x2 - x1 + 1) * (y2 - y1 + 1);

        for (int x = x1; x <= x2; x++)
        {
            for (int y = y1; y <= y2; y++)
            {
                sum += Values[Width * y + x];
            }
        }

        return sum / count;
    }

    /// <summary>
    /// Return an array of length <see cref="Height"/> where each value is the mean row value
    /// </summary>
    public double[] AverageByRow()
    {
        return AverageByRow(0, Width - 1);
    }

    /// <summary>
    /// Return an array of length <see cref="Height"/> where each value is the mean row value 
    /// between X pixel <paramref name="x1"/> and <paramref name="x2"/> (inclusive)
    /// </summary>
    public double[] AverageByRow(int x1, int x2)
    {
        if (x1 < 0 || x1 >= Width)
            throw new IndexOutOfRangeException(nameof(x1));
        if (x2 < 0 || x2 >= Width)
            throw new IndexOutOfRangeException(nameof(x2));
        if (x1 > x2)
            throw new ArgumentException($"{nameof(x1)} must be <= {nameof(x2)}");

        double[] avg = new double[Height];

        for (int y = 0; y < Height; y++)
        {
            for (int x = x1; x <= x2; x++)
                avg[y] += Values[Width * y + x];
            avg[y] = avg[y] / (x2 - x1 + 1);
        }

        return avg;
    }

    /// <summary>
    /// Return an array of length <see cref="Width"/> where each value is the mean column value
    /// </summary>
    public double[] AverageByColumn()
    {
        double[] avg = new double[Width];

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
                avg[x] += Values[Width * y + x];
            avg[x] = avg[x] / Height;
        }

        return avg;
    }

    /// <summary>
    /// Return an array of length <see cref="Width"/> where each value is the mean column value
    /// between Y pixel <paramref name="y1"/> and <paramref name="y2"/> (inclusive)
    /// </summary>
    public double[] AverageByColumn(int y1, int y2)
    {
        if (y1 < 0 || y1 >= Height)
            throw new IndexOutOfRangeException(nameof(y1));
        if (y2 < 0 || y2 >= Height)
            throw new IndexOutOfRangeException(nameof(y2));
        if (y1 > y2)
            throw new ArgumentException($"{nameof(y1)} must be <= {nameof(y2)}");

        double[] avg = new double[Width];

        for (int x = 0; x < Width; x++)
        {
            for (int y = y1; y <= y2; y++)
                avg[x] += Values[Width * y + x];
            avg[x] = avg[x] / (y2 - y1 + 1);
        }

        return avg;
    }

    public static ImageData operator -(ImageData a, ImageData b)
    {
        if (a.Width != b.Width || a.Height != b.Height)
            throw new InvalidOperationException("images must have the same dimensions");

        double[] values = new double[a.Values.Length];
        for (int i = 0; i < values.Length; i++)
            values[i] = a.Values[i] - b.Values[i];

        return new ImageData(values, a.Width, a.Height);
    }

    public static ImageData operator +(ImageData a, ImageData b)
    {
        if (a.Width != b.Width || a.Height != b.Height)
            throw new InvalidOperationException("images must have the same dimensions");

        double[] values = new double[a.Values.Length];
        for (int i = 0; i < values.Length; i++)
            values[i] = a.Values[i] + b.Values[i];

        return new ImageData(values, a.Width, a.Height);
    }

    public static ImageData operator *(ImageData a, ImageData b)
    {
        if (a.Width != b.Width || a.Height != b.Height)
            throw new InvalidOperationException("images must have the same dimensions");

        double[] values = new double[a.Values.Length];
        for (int i = 0; i < values.Length; i++)
            values[i] = a.Values[i] * b.Values[i];

        return new ImageData(values, a.Width, a.Height);
    }

    public static ImageData operator /(ImageData a, ImageData b)
    {
        if (a.Width != b.Width || a.Height != b.Height)
            throw new InvalidOperationException("images must have the same dimensions");

        double[] values = new double[a.Values.Length];
        for (int i = 0; i < values.Length; i++)
            values[i] = a.Values[i] / b.Values[i];

        return new ImageData(values, a.Width, a.Height);
    }

    public static ImageData operator -(ImageData a, double b)
    {
        double[] values = new double[a.Values.Length];
        for (int i = 0; i < values.Length; i++)
            values[i] = a.Values[i] - b;

        return new ImageData(values, a.Width, a.Height);
    }

    public static ImageData operator +(ImageData a, double b)
    {
        double[] values = new double[a.Values.Length];
        for (int i = 0; i < values.Length; i++)
            values[i] = a.Values[i] + b;

        return new ImageData(values, a.Width, a.Height);
    }

    public static ImageData operator *(ImageData a, double b)
    {
        double[] values = new double[a.Values.Length];
        for (int i = 0; i < values.Length; i++)
            values[i] = a.Values[i] * b;

        return new ImageData(values, a.Width, a.Height);
    }

    public static ImageData operator /(ImageData a, double b)
    {
        double[] values = new double[a.Values.Length];
        for (int i = 0; i < values.Length; i++)
            values[i] = a.Values[i] / b;

        return new ImageData(values, a.Width, a.Height);
    }
}
