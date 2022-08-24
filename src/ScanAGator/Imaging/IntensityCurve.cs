using ScottPlot.Drawing.Colormaps;
using System;
using System.Linq;

namespace ScanAGator.Imaging;

public class IntensityCurve
{
    public readonly double[] Values;
    public readonly double SamplePeriod;
    public readonly double[] Times;

    public IntensityCurve(double[] values, double period)
    {
        Values = values;
        SamplePeriod = period;
        Times = Enumerable.Range(0, values.Length).Select(x => x * period).ToArray();
    }

    public IntensityCurve(ImageData img, double period, StructureRange structure)
    {
        Values = img.AverageByRow(structure.Min, structure.Max);
        SamplePeriod = period;
        Times = Enumerable.Range(0, Values.Length).Select(x => x * period).ToArray();
    }

    public double GetMean(BaselineRange baseline)
    {
        return GetMean(baseline.Min, baseline.Max);
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

    public double GetPeak()
    {
        return Values.Max();
    }

    public IntensityCurve LowPassFiltered(int filterSizePixels)
    {
        double[] smooth = Filtering.GaussianFilter1d(Values, filterSizePixels);

        int padPoints = filterSizePixels * 2 + 1;

        for (int i = 0; i < padPoints; i++)
        {
            smooth[i] = double.NaN;
            smooth[smooth.Length - 1 - i] = double.NaN;
        }

        if (Values.Length != smooth.Length)
            throw new InvalidOperationException("Array size changed after filtering");
        return new IntensityCurve(smooth, SamplePeriod);
    }

    public static IntensityCurve operator +(IntensityCurve a, IntensityCurve b)
    {
        if (a.Values.Length != b.Values.Length)
            throw new InvalidOperationException("curves must have the same length");

        double[] values = new double[a.Values.Length];
        for (int i = 0; i < values.Length; i++)
            values[i] = a.Values[i] + b.Values[i];

        return new IntensityCurve(values, a.SamplePeriod);
    }

    public static IntensityCurve operator -(IntensityCurve a, IntensityCurve b)
    {
        if (a.Values.Length != b.Values.Length)
            throw new InvalidOperationException("curves must have the same length");

        double[] values = new double[a.Values.Length];
        for (int i = 0; i < values.Length; i++)
            values[i] = a.Values[i] - b.Values[i];

        return new IntensityCurve(values, a.SamplePeriod);
    }

    public static IntensityCurve operator *(IntensityCurve a, IntensityCurve b)
    {
        if (a.Values.Length != b.Values.Length)
            throw new InvalidOperationException("curves must have the same length");

        double[] values = new double[a.Values.Length];
        for (int i = 0; i < values.Length; i++)
            values[i] = a.Values[i] * b.Values[i];

        return new IntensityCurve(values, a.SamplePeriod);
    }

    public static IntensityCurve operator /(IntensityCurve a, IntensityCurve b)
    {
        if (a.Values.Length != b.Values.Length)
            throw new InvalidOperationException("curves must have the same length");

        double[] values = new double[a.Values.Length];
        for (int i = 0; i < values.Length; i++)
            values[i] = a.Values[i] / b.Values[i];

        return new IntensityCurve(values, a.SamplePeriod);
    }

    public static IntensityCurve operator +(IntensityCurve a, double b)
    {
        double[] values = new double[a.Values.Length];
        for (int i = 0; i < values.Length; i++)
            values[i] = a.Values[i] + b;

        return new IntensityCurve(values, a.SamplePeriod);
    }

    public static IntensityCurve operator -(IntensityCurve a, double b)
    {
        double[] values = new double[a.Values.Length];
        for (int i = 0; i < values.Length; i++)
            values[i] = a.Values[i] - b;

        return new IntensityCurve(values, a.SamplePeriod);
    }

    public static IntensityCurve operator *(IntensityCurve a, double b)
    {
        double[] values = new double[a.Values.Length];
        for (int i = 0; i < values.Length; i++)
            values[i] = a.Values[i] * b;

        return new IntensityCurve(values, a.SamplePeriod);
    }

    public static IntensityCurve operator /(IntensityCurve a, double b)
    {
        double[] values = new double[a.Values.Length];
        for (int i = 0; i < values.Length; i++)
            values[i] = a.Values[i] / b;

        return new IntensityCurve(values, a.SamplePeriod);
    }
}
