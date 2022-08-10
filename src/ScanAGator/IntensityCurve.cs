using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanAGator;

public class IntensityCurve
{
    readonly double[] Values;
    readonly double MsPerPixel;

    public IntensityCurve(double[] values, double msPerPixel)
    {
        Values = values;
        MsPerPixel = msPerPixel;
    }

    public double BaselineMean(PixelRange baseline)
    {
        return BaselineMean(baseline.FirstPixel, baseline.LastPixel);
    }

    /// <summary>
    /// Return the mean of the range (inclusive)
    /// </summary>
    public double BaselineMean(int i1, int i2)
    {
        double sum = 0;
        for (int i = i1; i <= i2; i++)
        {
            sum += Values[i];
        }
        return sum / (i2 - i1);
    }

    public IntensityCurve SubtractedBy(double value)
    {
        double[] newValues = new double[Values.Length];
        for (int i = 0; i < Values.Length; i++)
            newValues[i] = Values[i] - value;
        return new IntensityCurve(newValues, MsPerPixel);
    }

    public IntensityCurve DividedBy(IntensityCurve otherCurve, bool percent = true)
    {
        double multiplier = percent ? 100 : 1;
        double[] newValues = new double[Values.Length];
        for (int i = 0; i < Values.Length; i++)
        {
            newValues[i] = Values[i] * multiplier;
        }
        return new IntensityCurve(newValues, MsPerPixel);
    }
}
