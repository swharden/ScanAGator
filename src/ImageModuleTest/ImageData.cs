using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
// MODULE MUST ADD REFS TO "PRESENTATION CORE"

namespace ImageModuleTest
{
    public class ImageData
    {
        public double[] data;
        public int width;
        public int height;
        public int pixelCount { get { return width * height; } }

        public ImageData(string filePath)
        {
            LoadDataFromFile(filePath);
        }

        public ImageData(double[] data, int width, int height)
        {
            this.data = data;
            this.width = width;
            this.height = height;
        }

        public Bitmap GetBitmapIndexed(bool autoBrightness = true, bool autoBrightnessIgnoreExtremes = true)
        {
            // create and fill a pixel array for the 8-bit final image
            Console.WriteLine($"Creating BMP from data with extremes: {data.Min()} {data.Max()}");

            //apply auto-contrast 
            double brightestPixelValue = 1;
            if (autoBrightness)
            {
                if (autoBrightnessIgnoreExtremes)
                {
                    // set the maximum brightest to the brightest 0.1% of pixels
                    double[] orderedValues = new double[data.Length];
                    Array.Copy(data, orderedValues, data.Length);
                    Array.Sort(orderedValues);
                    brightestPixelValue = orderedValues[(int)(data.Length * .999)];
                }
                else
                {
                    // just adjust contrast to the brightest pixel
                    brightestPixelValue = data.Max();
                }
            }

            byte[] pixelsOutput = new byte[pixelCount];
            for (int i = 0; i < pixelCount; i++)
            {
                // optionally transform the image
                double pixelValue;
                if (autoBrightness)
                    pixelValue = data[i] / brightestPixelValue * 256;
                else
                    pixelValue = data[i];

                // apply clip limits
                if (pixelValue < 0)
                    pixelValue = 0;
                else if (pixelValue > 255)
                    pixelValue = 255;

                // set the pixel value
                pixelsOutput[i] = (byte)pixelValue;

            }

            // trim-off extra bytes if width is not a multiple of 4 bytes
            int strideByteMultiple = 4;
            int strideOverhang = width % strideByteMultiple;
            if (strideOverhang > 0)
            {
                int strideBytesNeededPerRow = strideByteMultiple - (strideOverhang);
                byte[] pixelsOutputOriginal = new byte[pixelCount];
                Array.Copy(pixelsOutput, pixelsOutputOriginal, pixelCount);
                pixelsOutput = new byte[pixelCount + strideBytesNeededPerRow * height];
                int newStrideWidth = width + strideBytesNeededPerRow;
                for (int row = 0; row < height; row++)
                    for (int col = 0; col < width; col++)
                        pixelsOutput[row * newStrideWidth + col] = pixelsOutputOriginal[row * width + col];
            }

            // create the output bitmap (8-bit indexed color)
            var formatOutput = System.Drawing.Imaging.PixelFormat.Format8bppIndexed;
            Bitmap bmp = new Bitmap(width, height, formatOutput);

            // Create a grayscale palette, although other colors and LUTs could go here
            System.Drawing.Imaging.ColorPalette pal = bmp.Palette;
            for (int i = 0; i < 256; i++)
                pal.Entries[i] = System.Drawing.Color.FromArgb(255, i, i, i);
            bmp.Palette = pal;

            // copy the new pixel data into the data of our output bitmap
            var rect = new Rectangle(0, 0, width, height);
            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, formatOutput);
            System.Runtime.InteropServices.Marshal.Copy(pixelsOutput, 0, bmpData.Scan0, pixelsOutput.Length);
            bmp.UnlockBits(bmpData);

            return bmp;
        }

        public Bitmap GetBmpDisplay()
        {
            Bitmap bmpSource = GetBitmapIndexed();
            var format = System.Drawing.Imaging.PixelFormat.Format32bppRgb;
            Bitmap bmp = new Bitmap(bmpSource.Width, bmpSource.Height, format);
            Graphics gr = Graphics.FromImage(bmp);
            gr.DrawImage(bmpSource, 0, 0);
            gr.Dispose();
            return bmp;
        }

        private void LoadDataFromFile(string filePath, int frameNumber = 0)
        {

            // open a file stream and keep it open until we're done reading the file
            System.IO.Stream stream = new System.IO.FileStream(filePath, System.IO.FileMode.Open,
                System.IO.FileAccess.Read, System.IO.FileShare.Read);

            // carefully open the file to see if it will decode
            TiffBitmapDecoder decoder;
            try
            {
                decoder = new TiffBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            }
            catch
            {
                Console.WriteLine("TiffBitmapDecoder crashed");
                stream.Dispose();
                data = null;
                return;
            }

            // access information about the image
            int imageFrames = decoder.Frames.Count;
            BitmapSource bitmapSource = decoder.Frames[frameNumber];
            int sourceImageDepth = bitmapSource.Format.BitsPerPixel;
            int bytesPerPixel = sourceImageDepth / 8;
            Size imageSize = new Size(bitmapSource.PixelWidth, bitmapSource.PixelHeight);
            width = imageSize.Width;
            height = imageSize.Height;
            Console.WriteLine($"Image size ({width}, {height}), bytesPerPx {bytesPerPixel} ({sourceImageDepth}-bit), frames: {imageFrames}, format: {bitmapSource.Format}");

            // fill a byte array with source data bytes from the file
            int imageByteCount = pixelCount * bytesPerPixel;
            byte[] bytesSource = new byte[imageByteCount];
            bitmapSource.CopyPixels(bytesSource, imageSize.Width * bytesPerPixel, 0);

            // close the original file
            stream.Dispose();

            // convert the byte array to an array of values
            data = new double[pixelCount];
            if (bitmapSource.Format == PixelFormats.Gray8)
            {
                for (int i = 0; i < data.Length; i++)
                    data[i] += bytesSource[i];
            }
            else if (bitmapSource.Format == PixelFormats.Gray16)
            {
                for (int i = 0; i < data.Length; i++)
                    data[i] = bytesSource[i * 2] + (bytesSource[i * 2 + 1] << 8);
            }
            else if (bitmapSource.Format == PixelFormats.Bgra32)
            {
                for (int i = 0; i < data.Length; i++)
                    data[i] += bytesSource[i * 4] + bytesSource[i * 4 + 1] + bytesSource[i * 4 + 2];
            }
            else
            {
                Console.WriteLine($"Unsupported TIF pixel format: {bitmapSource.Format}");
            }
        }
    }
}