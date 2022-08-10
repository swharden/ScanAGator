using System;
using System.Linq;

namespace ScanAGator;

public static class Operations
{
    /// <summary>
    /// Return a copy of the source array where every point was subtracted by the mean value between the baseline indexes
    /// </summary>
    public static double[] SubtractBaseline(double[] values, PixelRange baseline)
    {
        double baselineSum = 0;
        for (int i = baseline.FirstPixel; i < baseline.LastPixel; i++)
            baselineSum += values[i];
        double baselineMean = baselineSum / baseline.SpanPixels;

        double[] output = new double[values.Length];
        for (int i = 0; i < output.Length; i++)
            output[i] = values[i] - baselineMean;

        return output;
    }

    /// <summary>
    /// Return a new array representing numerator / denomenator (in % units)
    /// </summary>
    public static double[] CreateRatioCurve(double[] numerator, double[] denomenator)
    {
        if (numerator.Length != denomenator.Length)
            throw new ArgumentException("numerator and denomenator must have the same length");

        return Enumerable.Range(0, numerator.Length)
                         .Select(i => numerator[i] / denomenator[i] * 100)
                         .ToArray();
    }

    /// <summary>
    /// Return structure indexes in the proper order and separated by at least 1px
    /// </summary>
    public static (int s1, int s2) GetValidStructure(int structure1, int structure2)
    {
        int s1 = structure1;
        int s2 = structure2;

        if (s1 > s2)
        {
            int tmp = s1;
            s1 = s2;
            s2 = tmp;
        }

        if (s1 == s2)
        {
            if (s1 == 0)
                s2 += 1;
            else
                s1 -= 1;
        }

        return (s1, s2);
    }

    /// <summary>
    /// Return baseline indexes in the proper order and separated by at least 1px
    /// </summary>
    public static (int s1, int s2) GetValidBaseline(int baseline1, int baseline2)
    {
        int b1 = baseline1;
        int b2 = baseline2;

        if (b1 > b2)
        {
            int tmp = b1;
            b1 = b2;
            b2 = tmp;
        }

        if (b1 == b2)
        {
            if (b1 == 0)
                b2 += 1;
            else
                b1 -= 1;
        }

        return (b1, b2);
    }
}
