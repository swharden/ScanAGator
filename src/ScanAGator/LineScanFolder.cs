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
            if (IsValidLinescan) LoadImages();
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
        public double[] dataDeltaGoR;
        public Bitmap bmpDataR;
        public Bitmap bmpDataG;
        public int baseline1 = 1;
        public int baseline2 = 2;
        public int structure1 = 1;
        public int structure2 = 2;

        public void CalculateCurve()
        {
            // invert if opposite
            if (baseline1 > baseline2)
            {
                int orig = baseline1;
                baseline1 = baseline2;
                baseline2 = orig;
            }
            if (structure1 > structure2)
            {
                int orig = structure1;
                structure1 = structure2;
                structure2 = orig;
            }

            // correct if same
            if (baseline1 == baseline2)
                baseline2 += 1;
            if (structure1 == structure2)
                structure2 += 1;

            // create data trace
            dataR = imR.AverageHorizontally(structure1, structure2);
            dataG = imG.AverageHorizontally(structure1, structure2);

            // calculate GoR
            dataGoR = new double[dataG.Length];
            for (int i = 0; i < dataG.Length; i++)
                dataGoR[i] = dataG[i] / dataR[i] * 100.0;

            // calculate baseline GoR (%)
            double baselineValue = 0;
            for (int i = baseline1; i < baseline2; i++)
                baselineValue += dataGoR[i];
            baselineValue = baselineValue / (baseline2 - baseline1);

            // calculate dGoR
            dataDeltaGoR = new double[dataG.Length];
            for (int i = 0; i < dataG.Length; i++)
                dataDeltaGoR[i] = dataGoR[i] - baselineValue;
        }

        ImageData imR;
        ImageData imG;
        public void LoadImages()
        {

            // TODO: put image averaging here
            int imageFrameNumber = 0;
            imR = new ImageData(DataImagePathsR[imageFrameNumber]);
            imG = new ImageData(DataImagePathsG[imageFrameNumber]);

            if (imR.data.Length != imG.data.Length)
            {
                Error("red and green images are different sizes");
                return;
            }

            // TODO: this is destructive
            imR.AutoBrightness();
            bmpDataR = imR.GetBitmapRGB();
            imG.AutoBrightness();
            bmpDataG = imG.GetBitmapRGB();

            CalculateCurve();
        }
    }
}
