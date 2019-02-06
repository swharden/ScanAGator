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

        string FilenameTagR = "Ch1";
        string FilenameTagG = "Ch2";

        public bool IsValidLinescan { get; private set; }
        public string log { get; private set; }
        public Bitmap BmpReference { get; private set; }
        public Bitmap BmpR, BmpG;

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
            Array.Sort(RefImagePaths);
            if (RefImagePaths.Length == 0)
                Error("No reference images");
            else
                Log($"Found {RefImagePaths.Length} reference images");

            // identify a representative reference image and load it as a bitmap
            RefPrimaryPath = System.IO.Path.Combine(PathFolderData, RefImagePaths[0]);
            foreach (string imagePath in RefImagePaths)
            {
                if (imagePath.Contains("8bit"))
                {
                    RefPrimaryPath = imagePath;
                    break;
                }
            }
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

        public double[] dataG;
        public double[] dataR;
        public double[] dataGoR;

        public void LoadData()
        {
            // TODO: image averaging

            ImageData imR = new ImageData(DataImagePathsR[0]);
            ImageData imG = new ImageData(DataImagePathsG[0]);

            if (imR.data.Length != imG.data.Length)
            {
                Error("red and green images are different sizes");
                return;
            }

            imR.AutoBrightness();
            dataR = imR.AverageHorizontally();
            BmpR = imR.GetBitmap();

            imG.AutoBrightness();
            dataG = imG.AverageHorizontally();
            BmpG = imG.GetBitmap();

            dataGoR = new double[dataG.Length];
            for (int i = 0; i < dataG.Length; i++)
                dataGoR[i] = dataG[i] / dataR[i];

            //Log($"GoR min={Math.Round(dataGoR.Min(), 2)}, max={Math.Round(dataGoR.Max(), 2)}");
        }
    }
}
