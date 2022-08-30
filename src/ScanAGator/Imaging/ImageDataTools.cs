using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using BitMiracle.LibTiff.Classic;

namespace ScanAGator.Imaging;

/// <summary>
/// This class contains static methods which act on images
/// </summary>
public static class ImageDataTools
{
    public static Bitmap Merge(ImageData imgR, ImageData imgG, ImageData imgB)
    {
        byte[] bytesR = GetDisplayBytes(imgR.Values);
        byte[] bytesG = GetDisplayBytes(imgG.Values);
        byte[] bytesB = GetDisplayBytes(imgB.Values);

        // create a merged image
        Bitmap bmpM = new Bitmap(imgR.Width, imgR.Height, PixelFormat.Format32bppArgb);

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
            bmpBytes[i * 4 + 2] = bytesB[i];
            bmpBytes[i * 4 + 3] = 255;
        }

        // copy the byte array back into the image
        Marshal.Copy(bmpBytes, 0, bmpDataM.Scan0, bmpBytes.Length);
        bmpM.UnlockBits(bmpDataM);

        return bmpM;
    }

    private static byte[] GetDisplayBytes(double[] Values, bool autoBrightness = true, bool autoBrightnessIgnoreExtremes = true)
    {
        //apply auto-contrast 
        double brightestPixelValue = 1;
        if (autoBrightness)
        {
            if (autoBrightnessIgnoreExtremes)
            {
                // set the maximum brightest to the brightest 0.1% of pixels
                double[] orderedValues = new double[Values.Length];
                Array.Copy(Values, orderedValues, Values.Length);
                Array.Sort(orderedValues);
                brightestPixelValue = orderedValues[(int)(Values.Length * .999)];
            }
            else
            {
                // just adjust contrast to the brightest pixel
                brightestPixelValue = Values.Max();
            }
        }

        byte[] pixelsOutput = new byte[Values.Length];
        for (int i = 0; i < Values.Length; i++)
        {
            // optionally transform the image
            double pixelValue;
            if (autoBrightness)
                pixelValue = Values[i] / brightestPixelValue * 256;
            else
                pixelValue = Values[i];

            // apply clip limits
            if (pixelValue < 0)
                pixelValue = 0;
            else if (pixelValue > 255)
                pixelValue = 255;

            // set the pixel value
            pixelsOutput[i] = (byte)pixelValue;

        }

        return pixelsOutput;
    }

    public static ImageData Average(IEnumerable<ImageData> images)
    {
        int length = images.First().Values.Length;
        int width = images.First().Width;
        int height = images.First().Height;
        foreach (ImageData img in images)
        {
            if (img.Values.Length != length || img.Width != width || img.Height != height)
                throw new InvalidOperationException("all images must be same size");
        }

        double[] output = new double[length];

        foreach (ImageData img in images)
        {
            for (int i = 0; i < output.Length; i++)
            {
                output[i] += img.Values[i];
            }
        }

        for (int i = 0; i < output.Length; i++)
        {
            output[i] /= images.Count();
        }

        return new ImageData(output, width, height);
    }

    /// <summary>
    /// Given a 2D image return the mean pixel value of each row (between left and right columns) from top to bottom
    /// </summary>
    public static double[] GetAverageTopdown(ImageData img, StructureRange structure)
    {
        // Return an array with length the same as the image height.
        double[] avgByRow = new double[img.Height];

        // perform the averages
        for (int row = 0; row < img.Height; row++)
        {
            double rowSum = 0;
            for (int col = structure.Min; col < structure.Max; col++)
            {
                rowSum += img.Values[row * img.Width + col];
            }
            avgByRow[row] = rowSum / structure.Size;
        }
        return avgByRow;
    }

    /// <summary>
    /// Given a 2D image return the mean pixel value of each column from left to right
    /// </summary>
    public static double[] GetAverageLeftright(ImageData img)
    {
        // Return an array with length the same as image width.
        double[] avgByCol = new double[img.Width];

        for (int col = 0; col < img.Width; col++)
        {
            double colSum = 0;
            for (int row = 0; row < img.Height; row++)
            {
                colSum += img.Values[row * img.Width + col];
            }
            avgByCol[col] = colSum / img.Height;
        }
        return avgByCol;
    }

    public static ImageData ReadTif(string filePath)
    {
        using Tiff image = Tiff.Open(filePath, "r");

        int width = image.GetField(TiffTag.IMAGEWIDTH)[0].ToInt();
        int height = image.GetField(TiffTag.IMAGELENGTH)[0].ToInt();
        int numberOfStrips = image.NumberOfStrips();

        byte[] bytes = new byte[numberOfStrips * image.StripSize()];
        for (int i = 0; i < numberOfStrips; ++i)
            image.ReadRawStrip(i, bytes, i * image.StripSize(), image.StripSize());

        double[] data = new double[bytes.Length / 2];
        for (int i = 0; i < bytes.Length; i += 2)
            data[i / 2] = bytes[i] + (bytes[i + 1] << 8);

        return new ImageData(data, width, height);
    }

    public static Bitmap GetBitmapIndexed(ImageData imageData)
    {
        // create and fill a pixel array for the 8-bit final image

        byte[] pixelsOutput = GetDisplayBytes(imageData.Values);

        // trim-off extra bytes if width is not a multiple of 4 bytes
        int strideByteMultiple = 4;
        int strideOverhang = imageData.Width % strideByteMultiple;
        if (strideOverhang > 0)
        {
            int strideBytesNeededPerRow = strideByteMultiple - strideOverhang;
            byte[] pixelsOutputOriginal = new byte[pixelsOutput.Length];
            Array.Copy(pixelsOutput, pixelsOutputOriginal, pixelsOutput.Length);
            pixelsOutput = new byte[pixelsOutput.Length + strideBytesNeededPerRow * imageData.Height];
            int newStrideWidth = imageData.Width + strideBytesNeededPerRow;
            for (int row = 0; row < imageData.Height; row++)
                for (int col = 0; col < imageData.Width; col++)
                    pixelsOutput[row * newStrideWidth + col] = pixelsOutputOriginal[row * imageData.Width + col];
        }

        // create the output bitmap (8-bit indexed color)
        PixelFormat formatOutput = PixelFormat.Format8bppIndexed;
        Bitmap bmp = new(imageData.Width, imageData.Height, formatOutput);

        // Create a grayscale palette, although other colors and LUTs could go here
        ColorPalette pal = bmp.Palette;
        for (int i = 0; i < 256; i++)
            pal.Entries[i] = Color.FromArgb(255, i, i, i);
        bmp.Palette = pal;

        // copy the new pixel data into the data of our output bitmap
        var rect = new Rectangle(0, 0, imageData.Width, imageData.Height);
        BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, formatOutput);
        Marshal.Copy(pixelsOutput, 0, bmpData.Scan0, pixelsOutput.Length);
        bmp.UnlockBits(bmpData);

        return bmp;
    }

    public static Bitmap GetBmpDisplay(ImageData imageData)
    {
        Bitmap bmpSource = GetBitmapIndexed(imageData);
        var format = PixelFormat.Format32bppRgb;
        Bitmap bmp = new Bitmap(bmpSource.Width, bmpSource.Height, format);
        Graphics gr = Graphics.FromImage(bmp);
        gr.DrawImage(bmpSource, 0, 0);
        gr.Dispose();
        return bmp;
    }

    public static ImageData Average(ImageData[] images)
    {
        ImageData firstImage = images.First();
        double[] dataSum = new double[firstImage.Values.Length];

        foreach (ImageData image in images)
        {
            if (image.Values.Length != firstImage.Values.Length)
                throw new InvalidOperationException("all images must be the same size");

            if (image.Width != firstImage.Width)
                throw new InvalidOperationException("all images must be the same size");

            if (image.Height != firstImage.Height)
                throw new InvalidOperationException("all images must be the same size");

            for (int i = 0; i < dataSum.Length; i++)
            {
                dataSum[i] += image.Values[i];
            }
        }

        double[] averageData = dataSum.Select(x => x / images.Length).ToArray();

        return new ImageData(averageData, images.First().Width, images.First().Height);
    }

    public static Bitmap ReadTif_ST(string imagePath) // WARNING: not supporting indexed tifs
    {
        SciTIF.TifFile tifFile = new(imagePath); 
        SciTIF.Image tifImage = tifFile.GetImage();
        return tifImage.ToBitmap();
    }

    public static Bitmap ReadTif_SD(string imagePath)
    {
        using Bitmap bmp = new(imagePath);
        Bitmap bmp2 = new(bmp.Width, bmp.Height);
        using Graphics gfx = Graphics.FromImage(bmp2);
        gfx.DrawImage(bmp, 0, 0);
        return bmp2;
    }

    public static Bitmap ToBitmap(this SciTIF.Image image)
    {
        byte[] bmpBytes = image.GetBitmapBytes();
        using var ms = new MemoryStream(bmpBytes);
        Bitmap bmp = new(ms);
        return bmp;
    }

    public static Bitmap ToBitmap(this SciTIF.ImageRGB image)
    {
        byte[] bmpBytes = image.GetBitmapBytes();
        using var ms = new MemoryStream(bmpBytes);
        Bitmap bmp = new(ms);
        return bmp;
    }
}
