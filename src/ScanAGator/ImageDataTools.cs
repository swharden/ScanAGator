using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ScanAGator
{
    public class ImageDataTools
    {

        /// <summary>
        /// Return a copy of the input array lowpass filtered using a Gaussian-weighted moving average.
        /// The first and last N points will not be smoothed (their value will be 0)
        /// </summary>
        /// <param name="data">raw data to be filtered</param>
        /// <param name="degree">Amount of smoothing. The window will have N points where N = degree * 2 - 1</param>
        /// <returns></returns>
        public static double[] GaussianFilter1d(double[] data, int degree = 5)
        {
            // input arrays with 1 point or fewer will get returned without modification
            if (degree < 2 || data == null)
                return data;

            // create a copy of the original data to work with
            double[] smooth = new double[data.Length];

            // create a gaussian windowing function
            int windowSize = degree * 2 - 1;
            double[] kernel = GetHalfGaussian(windowSize);

            // apply the window
            int firstSmoothIndex = kernel.Length;
            int lastSmoothIndex = smooth.Length - kernel.Length;
            for (int i = firstSmoothIndex; i < lastSmoothIndex; i++)
            {
                for (int j = 0; j < kernel.Length; j++)
                {
                    smooth[i] += kernel[j] * data[i + j];
                }
            }

            return smooth;
        }

        /// <summary>
        /// Return an array starting at a large number and falling toward zero to form half a Gaussian curve.
        /// The Gaussian curve's half-width is approximately 1/4 the point count.
        /// If normalized, the curve will be scaled so all points equal one.
        /// If not normalized, the peak of the curve will be 1.
        /// </summary>
        public static double[] GetHalfGaussian(int pointCount, bool normalize = true)
        {
            const double xMultiplier = 4; // chosen so the end of the curve is zero

            double[] kernel = new double[pointCount];
            for (int i = 0; i < pointCount; i++)
            {
                double xFraction = i / (double)pointCount;
                double gauss = 1.0 / Math.Exp(Math.Pow(xMultiplier * xFraction, 2));
                kernel[i] = gauss * pointCount;
            }

            if (normalize)
            {
                double weightSum = kernel.Sum();
                for (int i = 0; i < pointCount; i++)
                    kernel[i] = kernel[i] / weightSum;
            }

            return kernel;
        }

        public static Bitmap Merge(ImageData imgR, ImageData imgG, ImageData imgB)
        {

            byte[] bytesR = imgR.GetDisplayBytes();
            byte[] bytesG = imgG.GetDisplayBytes();
            byte[] bytesB = imgB.GetDisplayBytes();

            // create a merged image
            Bitmap bmpM = new Bitmap(imgR.width, imgR.height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            // copy the image into a byte array
            int bytesPerPixel = 4;
            int pixelCount = bmpM.Width * bmpM.Height;
            byte[] bmpBytes = new byte[pixelCount * bytesPerPixel];
            Rectangle rect = new Rectangle(0, 0, bmpM.Width, bmpM.Height);
            BitmapData bmpDataM = bmpM.LockBits(rect, ImageLockMode.ReadWrite, bmpM.PixelFormat);
            Marshal.Copy(bmpDataM.Scan0, bmpBytes, 0, bmpBytes.Length);

            // load channel data into the byte array
            for (int i = 0; i < pixelCount; i++)
            {
                bmpBytes[i * 4 + 0] = bytesR[i];
                bmpBytes[i * 4 + 1] = bytesG[i];
                bmpBytes[i * 4 + 2] = bytesR[i];
                bmpBytes[i * 4 + 3] = 255;
            }

            // copy the byte array back into the image
            Marshal.Copy(bmpBytes, 0, bmpDataM.Scan0, bmpBytes.Length);
            bmpM.UnlockBits(bmpDataM);

            return bmpM;
        }

        /// <summary>
        /// Given a 2D image return the mean pixel value of each row (between left and right columns) from top to bottom
        /// </summary>
        public static double[] GetAverageTopdown(ImageData img, int leftPx = -1, int rightPx = -1)
        {
            if (img == null)
                return null;

            // Return an array with length the same as the image height.
            double[] avgByRow = new double[img.height];

            // if pixels aren't given, default to full size
            if (leftPx < 0)
                leftPx = 0;
            if (rightPx < 0)
                rightPx = img.width;

            // perform the averages
            for (int row = 0; row < img.height; row++)
            {
                double rowSum = 0;
                for (int col = leftPx; col < rightPx; col++)
                {
                    rowSum += img.data[row * img.width + col];
                }
                avgByRow[row] = rowSum / (rightPx - leftPx);
            }
            return avgByRow;
        }

        /// <summary>
        /// Given a 2D image return the mean pixel value of each column from left to right
        /// </summary>
        public static double[] GetAverageLeftright(ImageData img)
        {
            if (img == null)
                return null;

            // Return an array with length the same as image width.
            double[] avgByCol = new double[img.width];

            for (int col = 0; col < img.width; col++)
            {
                double colSum = 0;
                for (int row = 0; row < img.height; row++)
                {
                    colSum += img.data[row * img.width + col];
                }
                avgByCol[col] = colSum / img.height;
            }
            return avgByCol;
        }

    }
}
