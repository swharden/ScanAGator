using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanAGator;

public static class Filtering
{
    /// <summary>
    /// Return a copy of the input array lowpass filtered using a Gaussian-weighted moving average.
    /// The size of the moving window (N) is defined by N = degree * 2 - 1.
    /// The Gaussian curve half-width is approximately equal to 20% of the window size (40% of degree).
    /// The first and last N points will not be smoothed (their value will be 0).
    /// </summary>
    /// <param name="data">raw data to be filtered</param>
    /// <param name="degree">Amount of smoothing. The window will have N points where N = degree * 2 - 1</param>
    /// <param name="forward">If enabled, smoothing will only occur forward in time, preserving abrupt changes</param>
    /// <returns></returns>
    public static double[] GaussianFilter1d(double[] data, int degree = 5, bool forward = true)
    {
        if (degree < 2)
            return data;

        int windowSize = degree * 2 - 1;

        if (forward)
            return GetGaussianFilter1dForward(windowSize, data);
        else
            return GetFullGaussianSmooth(windowSize, data);
    }

    /// <summary>
    /// Return the forward-only Gaussian-weighted moving window average.
    /// This method is best for applying smoothing while preserving abrupt changes in the data.
    /// Each point is the mean of itself and the N points after it using a half-Gaussian weighted window.
    /// The size of the window (N) is defined by N = degree * 2 - 1.
    /// </summary>
    private static double[] GetGaussianFilter1dForward(int windowSize, double[] data)
    {
        double[] window = GetGaussianCurve(windowSize);
        double[] smooth = new double[data.Length];

        // apply the window over the period of data where enough data exists to make a weighted average
        int firstSmoothIndex = window.Length;
        int lastSmoothIndex = smooth.Length - window.Length;
        for (int i = firstSmoothIndex; i < lastSmoothIndex; i++)
        {
            // this point will be the weighted average of itself and the next N points
            for (int j = 0; j < window.Length; j++)
            {
                smooth[i] += window[j] * data[i + j];
            }
        }

        return smooth;
    }

    /// <summary>
    /// Return a bidirectional Gaussian-weighted moving window average.
    /// This method applies equal weighting in both directions so abrupt changes will be greatly smoothed.
    /// Each point is the mean of itself and the N/2 points on either side using a Gaussian weighted window.
    /// The size of the window (N) is defined by N = degree * 2 - 1.
    /// </summary>
    private static double[] GetFullGaussianSmooth(int windowSize, double[] data)
    {
        double[] window = GetGaussianCurve(windowSize, half: false);
        double[] smooth = new double[data.Length];

        // apply the window over the period of data where enough data exists to make a weighted average
        int firstSmoothIndex = window.Length;
        int lastSmoothIndex = smooth.Length - window.Length;
        for (int i = firstSmoothIndex; i < lastSmoothIndex; i++)
        {
            // this point will be the weighted average of itself and the next N points
            for (int j = 0; j < window.Length; j++)
            {
                smooth[i] += window[j] * data[i + j - window.Length / 2];
            }
        }

        return smooth;
    }

    /// <summary>
    /// Return an array of values forming a Gaussian curve
    /// </summary>
    /// <param name="pointCount">Total number of points in the array</param>
    /// <param name="normalize">Scale the curve so the sum of its points is 1. Otherwise the peak is 1.</param>
    /// <param name="half">If true, the first value will be the largest and subsequent values will approach zero.</param>
    /// <returns></returns>
    public static double[] GetGaussianCurve(int pointCount, bool normalize = true, bool half = true)
    {
        double xMultiplier = half ? 4 : 8; // chosen so the curve reaches near zero at the edge
        double xOffset = half ? 0 : -.5;
        double[] kernel = new double[pointCount];
        for (int i = 0; i < pointCount; i++)
        {
            double xFraction = i / (double)pointCount + xOffset;
            double gauss = 1.0 / Math.Exp(Math.Pow(xMultiplier * xFraction, 2));
            kernel[i] = gauss * pointCount;
        }

        if (normalize)
            Normalize(kernel);

        return kernel;
    }

    /// <summary>
    /// Normalize an array in-place so its sum is 1.0
    /// </summary>
    /// <param name="data"></param>
    public static void Normalize(double[] data)
    {
        double sum = data.Sum();
        for (int i = 0; i < data.Length; i++)
            data[i] = data[i] / sum;
    }
}
