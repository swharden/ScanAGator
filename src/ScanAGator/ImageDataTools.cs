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


        public static double[] GaussianFilter1d(double[] data, int degree = 5)
        {
            if (degree < 2 || data == null)
                return data;

            double[] smooth = new double[data.Length];

            // create a gaussian windowing function
            int windowSize = degree * 2 - 1;
            double[] kernel = new double[windowSize];
            for (int i = 0; i < windowSize; i++)
            {
                int pos = i - degree + 1;
                double frac = i / (double)windowSize;
                double gauss = 1.0 / Math.Exp(Math.Pow(4 * frac, 2)); // TODO: why 4?
                kernel[i] = gauss * windowSize;
            }

            // normalize the kernel (so area is 1)
            double weightSum = kernel.Sum();
            for (int i = 0; i < windowSize; i++)
                kernel[i] = kernel[i] / weightSum;

            // apply the window
            for (int i = 0; i < smooth.Length; i++)
            {
                if (i > kernel.Length && i < smooth.Length - kernel.Length)
                {
                    double smoothedValue = 0;
                    for (int j = 0; j < kernel.Length; j++)
                    {
                        smoothedValue += kernel[j] * data[i + j];
                    }
                    smooth[i] = smoothedValue;
                }
                else
                {
                    smooth[i] = data[i];
                }
            }

            // The edges are only partially smoothed, so they should be "NaN", but extending
            // the first and last point out seems to be good enough.
            int firstValidPoint = kernel.Length;
            int lastValidPoint = smooth.Length - kernel.Length;
            for (int i = 0; i < firstValidPoint; i++)
                smooth[i] = smooth[firstValidPoint];
            for (int i = lastValidPoint; i < smooth.Length; i++)
                smooth[i] = smooth[lastValidPoint];

            return smooth;
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
