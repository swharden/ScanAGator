using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanAGator;

public class IntensityCurve
{
    public readonly double[] Values;
    public readonly double MsPerPixel;

    public IntensityCurve(double[] values)
    {
        Values = values;
        MsPerPixel = double.NaN;
    }

    public IntensityCurve(double[] values, double msPerPixel)
    {
        Values = values;
        MsPerPixel = msPerPixel;
    }

    public IntensityCurve(ImageData img, PixelRange structure)
    {
        Values = img.AverageByRow(structure.Min, structure.Max);
        MsPerPixel = double.NaN;
    }

    public double[] GetTimes()
    {
        double[] times = new double[Values.Length];
        double period = double.IsNaN(MsPerPixel) ? 1 : MsPerPixel;
        for (int i = 0; i < times.Length; i++)
        {
            times[i] = i * period;
        }
        return times;
    }

    public double GetMean(PixelRange baseline)
    {
        return GetMean(baseline.FirstPixel, baseline.LastPixel);
    }

    /// <summary>
    /// Return the mean of the range (inclusive)
    /// </summary>
    public double GetMean(int i1, int i2)
    {
        double sum = 0;
        for (int i = i1; i <= i2; i++)
        {
            sum += Values[i];
        }
        return sum / (i2 - i1 + 1);
    }

    public IntensityCurve LowPassFiltered(int filterSizePixels)
    {
        double[] smooth = ImageDataTools.GaussianFilter1d(Values, filterSizePixels);
        int padPoints = filterSizePixels * 2 + 1;

        for (int i = 0; i < padPoints; i++)
        {
            smooth[i] = smooth[padPoints];
            smooth[smooth.Length - 1 - i] = smooth[smooth.Length - 1 - padPoints];
        }

        if (Values.Length != smooth.Length)
            throw new InvalidOperationException("Array size changed after filtering");
        return new IntensityCurve(smooth, MsPerPixel);
    }

    public static IntensityCurve operator +(IntensityCurve a, IntensityCurve b)
    {
        if (a.Values.Length != b.Values.Length)
            throw new InvalidOperationException("curves must have the same length");

        double[] values = new double[a.Values.Length];
        for (int i = 0; i < values.Length; i++)
            values[i] = a.Values[i] + b.Values[i];

        return new IntensityCurve(values);
    }

    public static IntensityCurve operator -(IntensityCurve a, IntensityCurve b)
    {
        if (a.Values.Length != b.Values.Length)
            throw new InvalidOperationException("curves must have the same length");

        double[] values = new double[a.Values.Length];
        for (int i = 0; i < values.Length; i++)
            values[i] = a.Values[i] - b.Values[i];

        return new IntensityCurve(values);
    }

    public static IntensityCurve operator *(IntensityCurve a, IntensityCurve b)
    {
        if (a.Values.Length != b.Values.Length)
            throw new InvalidOperationException("curves must have the same length");

        double[] values = new double[a.Values.Length];
        for (int i = 0; i < values.Length; i++)
            values[i] = a.Values[i] * b.Values[i];

        return new IntensityCurve(values);
    }

    public static IntensityCurve operator /(IntensityCurve a, IntensityCurve b)
    {
        if (a.Values.Length != b.Values.Length)
            throw new InvalidOperationException("curves must have the same length");

        double[] values = new double[a.Values.Length];
        for (int i = 0; i < values.Length; i++)
            values[i] = a.Values[i] / b.Values[i];

        return new IntensityCurve(values);
    }

    public static IntensityCurve operator +(IntensityCurve a, double b)
    {
        double[] values = new double[a.Values.Length];
        for (int i = 0; i < values.Length; i++)
            values[i] = a.Values[i] + b;

        return new IntensityCurve(values);
    }

    public static IntensityCurve operator -(IntensityCurve a, double b)
    {
        double[] values = new double[a.Values.Length];
        for (int i = 0; i < values.Length; i++)
            values[i] = a.Values[i] - b;

        return new IntensityCurve(values);
    }

    public static IntensityCurve operator *(IntensityCurve a, double b)
    {
        double[] values = new double[a.Values.Length];
        for (int i = 0; i < values.Length; i++)
            values[i] = a.Values[i] * b;

        return new IntensityCurve(values);
    }

    public static IntensityCurve operator /(IntensityCurve a, double b)
    {
        double[] values = new double[a.Values.Length];
        for (int i = 0; i < values.Length; i++)
            values[i] = a.Values[i] / b;

        return new IntensityCurve(values);
    }
}
