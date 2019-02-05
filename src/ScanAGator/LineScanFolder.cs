using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ScanAGator
{
    public class LineScanFolder
    {
        string PathFolderData;
        string PathFolderRefs;
        string FolderName { get { return System.IO.Path.GetFileName(PathFolderData); } }


        string[] RefImagePaths;
        string RefPrimaryPath;
        string[] DataImagePaths;

        string[] DataImagePathsR;
        string[] DataImagePathsG;
        double[] DataRatio;

        string FilenameTagR = "Ch1";
        string FilenameTagG = "Ch2";

        public bool IsValidLinescan { get; private set; }
        public string log { get; private set; }
        public Bitmap BmpReference { get; private set; }

        public LineScanFolder(string pathFolder)
        {
            IsValidLinescan = true;

            PathFolderData = System.IO.Path.GetFullPath(pathFolder);
            Log($"Loading folder: {PathFolderData}");
            if (!System.IO.Directory.Exists(PathFolderData))
                Error($"Folder does not exist: {PathFolderData}");

            PathFolderRefs = System.IO.Path.Combine(PathFolderData, "References");
            if (!System.IO.Directory.Exists(PathFolderRefs))
                Error($"Folder does not exist: {PathFolderRefs}");

            if (IsValidLinescan) ScanFiles();
            if (IsValidLinescan) ReadXML();
            if (IsValidLinescan) LoadData();
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
            log += $"{message}\n";
        }

        private void Error(string message)
        {
            Log($"ERROR: {message}");
            IsValidLinescan = false;
        }

        /// Determine the paths to meaningful files and folders in the linescan folder
        private void ScanFiles()
        {
            // scan reference images
            RefImagePaths = System.IO.Directory.GetFiles(PathFolderRefs, "*.tif");
            if (RefImagePaths.Length == 0)
                Error("No reference images");
            else
                Log($"Found {RefImagePaths.Length} reference images");

            // identify a representative reference image and load it as a bitmap
            RefPrimaryPath = System.IO.Path.Combine(PathFolderData, RefImagePaths[0]);
            foreach (string imagePath in RefImagePaths)
                if (imagePath.Contains("8bit"))
                    RefPrimaryPath = imagePath;
            BmpReference = new Bitmap(RefPrimaryPath);
            Log($"Primary refrence image: {RefPrimaryPath}");

            // scan data images
            DataImagePaths = System.IO.Directory.GetFiles(PathFolderData, "*.tif");
            if (DataImagePaths.Length == 0)
                Error("No data images");
            else
                Log($"Found {DataImagePaths.Length} reference images");

            // scan for red and green channel images
            List<string> filePathsR = new List<string>();
            List<string> filePathsG = new List<string>();
            foreach (string filePath in DataImagePaths)
            {
                string fileName = System.IO.Path.GetFileName(filePath);

                // skip all source images
                if (fileName.Contains("Source"))
                    continue;

                if (fileName.Contains(FilenameTagR))
                    filePathsR.Add(filePath);
                if (fileName.Contains(FilenameTagG))
                    filePathsG.Add(filePath);
            }
            DataImagePathsR = filePathsR.ToArray();
            DataImagePathsG = filePathsG.ToArray();
            Log($"Found {DataImagePathsR.Length} red and {DataImagePathsG.Length} green data images");
            if (DataImagePathsR.Length != DataImagePathsG.Length)
            {
                Error("Number of red and green data images must be the same");
                return;
            }
        }

        public void ReadXML()
        {
            // TODO: XML parsing to get linescan time
        }

        private int[] LoadImageTiff(string path)
        {

            // open a file stream and keep it open until we're done reading the file
            System.IO.Stream stream = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);

            TiffBitmapDecoder decoder = new TiffBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);

            // access information about the image
            int imageFrames = decoder.Frames.Count;
            BitmapSource bitmapSource = decoder.Frames[0];
            int sourceImageDepth = bitmapSource.Format.BitsPerPixel;
            int bytesPerPixel = sourceImageDepth / 8;
            Size imageSize = new Size(bitmapSource.PixelWidth, bitmapSource.PixelHeight);
            int pixelCount = imageSize.Width * imageSize.Height;

            // fill a byte array with source data bytes from the file
            int imageByteCount = pixelCount * bytesPerPixel;
            byte[] bytesSource = new byte[imageByteCount];
            bitmapSource.CopyPixels(bytesSource, imageSize.Width * bytesPerPixel, 0);

            // we can now close the original file
            stream.Dispose();

            // now convert the byte array to an int array (with 1 int per pixel)
            int[] valuesSource = new int[pixelCount];
            for (int i = 0; i < valuesSource.Length; i++)
            {
                // this loop is great because it works on any number of bytes per pixel
                int bytePosition = i * bytesPerPixel;
                for (int byteNumber = 0; byteNumber < bytesPerPixel; byteNumber++)
                {
                    valuesSource[i] += bytesSource[bytePosition + byteNumber] << (byteNumber * 8);
                }
            }

            // TODO: input bytes are padded such that stride is a multiple of 4 bytes, so trim that off

            return valuesSource;
        }

        double[] CalculateGoR(int[] dataR, int[] dataG)
        {
            double[] dataGoR = new double[dataR.Length];
            for (int i = 0; i < dataGoR.Length; i++)
                dataGoR[i] = (double)dataG[i] / (double)dataR[i];
            return dataGoR;
        }

        public void LoadData()
        {
            // TODO: image averaging

            int[] dataR = LoadImageTiff(DataImagePathsR[0]);
            int[] dataG = LoadImageTiff(DataImagePathsG[0]);
            if (dataR.Length != dataG.Length)
            {
                Error("red and green images are different sizes");
                return;
            }
            Log($"Red channel min={dataR.Min()}, max={dataR.Max()}");

            // TODO: background subtration of original images

            double[] dataGoR = CalculateGoR(dataR, dataG);
            Log($"GoR min={Math.Round(dataGoR.Min(), 2)}, max={Math.Round(dataGoR.Max(), 2)}");
        }
    }
}
