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
        return sum / (i2 - i1 + 1);
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
            newValues[i] = Values[i] / otherCurve.Values[i] * multiplier;
        }
        return new IntensityCurve(newValues, MsPerPixel);
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
}
