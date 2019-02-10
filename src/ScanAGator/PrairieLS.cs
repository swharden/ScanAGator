using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanAGator
{
    /// <summary>
    /// Represents a PrarieView linescan experiment (a single folder)
    /// </summary>
    public class PrairieLS
    {

        #region class variables and properties

        public int baseline1 { get; set; }
        public int baseline2 { get; set; }
        public int structure1 { get; set; }
        public int structure2 { get; set; }
        public int filterPx { get; set; }
        public int frame { get; set; }
        public double filterMillisec { get { return filterPx * scanLinePeriod * 1000.0; } }
        string version { get { return Properties.Resources.ResourceManager.GetString("version"); } }
        public string pathLinescanFolder { get; private set; }
        public bool validLinescanFolder { get; private set; }
        public string pathLinescanXML { get; private set; }
        public double scanLinePeriod { get; private set; }
        public readonly string pathSaveFolder;
        public readonly string pathIniFile;
        public string[] pathsRefImages { get; private set; }
        public string[] pathsDataR;
        public string[] pathsDataG;
        public Size dataImage { get; private set; }
        public double[] dataG { get; private set; }
        public double[] dataR { get; private set; }
        public double[] dataGoR { get; private set; }
        public double[] dataTimeMsec { get; private set; }
        public double[] dataDeltaGoR { get; private set; }
        public double[] dataDeltaGoRsmoothed { get; private set; }
        public double dataDeltaGoRsmoothedPeak { get; private set; }
        public double[] dataDeltaGoRsmoothedChoppedYs
        {
            get
            {
                // return just the valid smoothed data values (not edges with incomplete smoothing)
                int skipEdgePoints = filterPx * 2;
                double[] chopped = new double[dataDeltaGoRsmoothed.Length - skipEdgePoints * 2];
                for (int i = 0; i < chopped.Length; i++)
                    chopped[i] = dataDeltaGoRsmoothed[i + skipEdgePoints];
                return chopped;
            }
        }
        public double[] dataDeltaGoRsmoothedChoppedXs
        {
            get
            {
                // return just the valid smoothed data value times
                int skipEdgePoints = filterPx * 2;
                double[] chopped = new double[dataDeltaGoRsmoothed.Length - skipEdgePoints * 2];
                for (int i = 0; i < chopped.Length; i++)
                    chopped[i] = dataTimeMsec[i + skipEdgePoints];
                return chopped;
            }
        }

        #endregion

        #region loading of linescan files and data

        public PrairieLS(string path)
        {
            validLinescanFolder = true;
            pathLinescanFolder = System.IO.Path.GetFullPath(path);
            pathSaveFolder = System.IO.Path.Combine(pathLinescanFolder, "ScanAGator");
            pathIniFile = System.IO.Path.Combine(pathSaveFolder, "LineScanSettings.ini");

            Log($"Loading linescan folder: {this.pathLinescanFolder}");
            ScanFolder();
            CreateSaveFolder();
            LoadDefaultSettings();
            LoadSettingsINI();
            Analyze();
            keepLogging = false; // stop logging after instantiation
        }

        /// <summary>
        /// Call this once to load all information about the linescan into this class.
        /// This function does everything short of pulling values out of TIFF data.
        /// </summary>
        private void ScanFolder()
        {

            if (!System.IO.Directory.Exists(pathLinescanFolder))
            {
                Error("Linescan folder not found.");
                return;
            }

            try
            {
                System.IO.Directory.GetFiles(pathLinescanFolder, "*.*");
            }
            catch (UnauthorizedAccessException)
            {
                Error("You do not have permissions to access this folder.");
                return;
            }

            // Populate list of reference images
            string pathReferences = System.IO.Path.Combine(pathLinescanFolder, "References");
            if (System.IO.Directory.Exists(pathReferences))
            {
                pathsRefImages = System.IO.Directory.GetFiles(pathReferences, "*.tif");
                Array.Sort(pathsRefImages);
            }
            if (pathsRefImages == null || pathsRefImages.Length == 0)
                Log("WARNING: no reference images were found");
            else
                Log($"Found {pathsRefImages.Length} reference images");

            // Prepare list of G and R data images
            List<string> filePathsR = new List<string>();
            List<string> filePathsG = new List<string>();
            string[] dataImagePaths = System.IO.Directory.GetFiles(pathLinescanFolder, "*.tif");
            foreach (string filePath in dataImagePaths)
            {
                string fileName = System.IO.Path.GetFileName(filePath);
                if (fileName.Contains("Source"))
                    continue;
                if (fileName.Contains("Ch1"))
                    filePathsR.Add(filePath);
                if (fileName.Contains("Ch2"))
                    filePathsG.Add(filePath);
            }
            pathsDataR = filePathsR.ToArray();
            pathsDataG = filePathsG.ToArray();
            Log($"Found {pathsDataR.Length} red and {pathsDataG.Length} green data images");
            if (pathsDataR.Length == 0)
                Error("No data images found!");
            if (pathsDataR.Length != pathsDataG.Length)
                Error("Number of red and green data images must be the same");

            // determine where the linescan configuration XML file is
            string[] linescanXmlFiles = System.IO.Directory.GetFiles(pathLinescanFolder, "LineScan*.xml");
            if (linescanXmlFiles.Length > 0)
            {
                pathLinescanXML = linescanXmlFiles[0];
                Log("XML file: " + System.IO.Path.GetFileName(pathLinescanXML));
                scanLinePeriod = ParseXML(pathLinescanXML);
                if (scanLinePeriod < 0)
                    Log("WARNING: Scan line period could not be found in XML file");
            }
            else
            {
                Error("XML file does not exist");
            }

            // determine the dimensins of the linescan images from the first red channel
            if (pathsDataR.Length > 0)
            {
                Bitmap bmp = new Bitmap(pathsDataR[0]);
                dataImage = new Size(bmp.Width, bmp.Height);
                Log($"data image dimensions: {dataImage.Width}px (position) by {dataImage.Height}px (time)");
            }

            // create an array to hold data times based on scane line period
            dataTimeMsec = new double[dataImage.Height];
            for (int i = 0; i < dataTimeMsec.Length; i++)
            {
                if (scanLinePeriod < 0)
                    dataTimeMsec[i] = i;
                else
                    dataTimeMsec[i] = i * scanLinePeriod * 1000.0;
            }
        }

        /// <summary>
        /// Parse a PV LineScan XML file (v4 or v5) and return the line scan period
        /// </summary>
        private double ParseXML(string pathXmlFile)
        {
            // PV v4 and PV v5 have very different XML. 
            // Use string parsing (not XML objects) because of this.
            // We just need a single number (scane line period) so this is easy.
            // If more items are desired, consider subclassing for different XML versions...
            string[] xmlLines = System.IO.File.ReadAllLines(pathXmlFile);
            foreach (string line in xmlLines)
            {
                if ((line.Contains("scanLinePeriod") || line.Contains("scanlinePeriod")) && line.Contains("value="))
                {
                    string split1 = "value=\"";
                    string split2 = "\"";
                    string valStr = line.Substring(line.IndexOf(split1) + split1.Length);
                    valStr = valStr.Substring(0, valStr.IndexOf(split2));
                    double period = double.Parse(valStr);
                    Log($"Scan line: {period} ms");
                    return period;
                }
            }
            return -1;
        }
        #endregion

        #region logging

        private bool keepLogging = true;
        private List<string> log;

        private void Log(string message)
        {
            if (keepLogging == false)
                return;
            if (log == null)
                log = new List<string>();
            log.Add(message);
            Console.WriteLine(message);
        }

        private void Error(string message)
        {
            Log("ERROR: " + message);
            validLinescanFolder = false;
        }

        public string GetLogForTextbox()
        {
            string logText = "";
            foreach (string line in log)
                logText += $"{line}\r\n";
            return logText;
        }
        #endregion

        #region analysis and calculations

        /// <summary>
        /// Determine sane staring values for baseline, structure, and filter
        /// </summary>
        public void LoadDefaultSettings()
        {
            if (!validLinescanFolder)
                return;

            // filtering
            filterPx = 20;
            Log($"Default gaussian filter size: {filterPx} px ({filterPx} ms)");

            // image selection
            frame = 0;
            Log($"Default frame: {frame + 1} (of {pathsDataG.Length})");
            LoadFrame();

            // set baseline to first 5-10%
            baseline1 = (int)(.05 * dataImage.Height);
            baseline2 = (int)(.10 * dataImage.Height);
            Log($"Baseline defaulted to {baseline1}px to {baseline2}px");

            // set the structure positions
            StructureAutoDetect();            
        }

        public void StructureAutoDetect()
        {
            // determine the brightest point and select a few pixels on each side of it
            double[] brightness = imG.AverageVertically();
            double brightestValue = 0;
            int brightestIndex = 0;
            for (int i = 0; i < brightness.Length; i++)
            {
                if (brightness[i] > brightestValue)
                {
                    brightestValue = brightness[i];
                    brightestIndex = i;
                }
            }
            int structureWidthFromCenter = 3;
            structure1 = brightestIndex - structureWidthFromCenter;
            structure2 = brightestIndex + structureWidthFromCenter;
            Log($"Selected {structureWidthFromCenter} px on each side of the brightest structure (at {brightestIndex} px)");
        }

        /// <summary>
        /// Fix potential errors with the baseline or structure values
        /// </summary>
        public void FixLimits()
        {

            // swap order if necessary
            int original1;
            if (baseline2 < baseline1)
            {
                original1 = baseline1;
                baseline1 = baseline2;
                baseline2 = original1;
            }

            if (structure2 < structure1)
            {
                original1 = structure1;
                structure1 = structure2;
                structure2 = original1;
            }

            // ensure things are at least 1px wide
            if (structure1 == structure2)
            {
                if (structure1 > 0)
                    structure1 -= 1;
                else
                    structure2 += 1;
            }

            if (baseline1 == baseline2)
            {
                if (baseline1 > 0)
                    baseline1 -= 1;
                else
                    baseline2 += 1;
            }
        }

        /// <summary>
        /// Return a filtered version of the input array (with the same number of points).
        /// </summary>
        public double[] GaussianFilter1d(double[] data, int degree = 5)
        {
            if (degree < 2)
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

        private ImageData imR;
        private ImageData imG;

        /// <summary>
        /// Read data values from the red and green TIF of the class-level frame
        /// </summary>
        public void LoadFrame()
        {
            if (!validLinescanFolder)
                return;
            imR = new ImageData(pathsDataR[frame]);
            imG = new ImageData(pathsDataG[frame]);
        }

        public void Analyze()
        {
            if (!validLinescanFolder)
                return;

            FixLimits();

            // load G and R data based on structure limits
            dataR = imR.AverageHorizontally(structure1, structure2);
            dataG = imG.AverageHorizontally(structure1, structure2);

            // calculate GoR
            dataGoR = new double[dataG.Length];
            for (int i = 0; i < dataG.Length; i++)
                dataGoR[i] = dataG[i] / dataR[i] * 100.0;
            Log($"peak G/R: {Math.Round(dataGoR.Max(), 2)}%");

            // calculate baseline GoR (%)
            double baselineValue = 0;
            for (int i = baseline1; i < baseline2; i++)
                baselineValue += dataGoR[i];
            baselineValue = baselineValue / (baseline2 - baseline1);
            Log($"baseline G/R: {Math.Round(baselineValue, 2)}%");

            // calculate dGoR
            dataDeltaGoR = new double[dataG.Length];
            for (int i = 0; i < dataG.Length; i++)
                dataDeltaGoR[i] = dataGoR[i] - baselineValue;

            // create the smoothed version
            Log($"applying gaussian filter size: {filterPx} px ({filterPx} ms)");
            dataDeltaGoRsmoothed = GaussianFilter1d(dataDeltaGoR, filterPx);
            dataDeltaGoRsmoothedPeak = dataDeltaGoRsmoothed.Max();
            Log($"peak smoothed d[G/R]: {Math.Round(dataDeltaGoRsmoothedPeak, 2)}%");
        }

        #endregion

        #region bitmap handling

        /// <summary>
        /// Return a brightness/contrast-enhanced TIF as a RGB bitmap
        /// </summary>
        public Bitmap GetBmpReference(int imageNumber = 0)
        {
            string refPath = pathsRefImages[imageNumber];
            foreach (string altRef in pathsRefImages)
            {
                if (altRef.Contains("8bit"))
                {
                    refPath = altRef;
                    break;
                }
            }
            ImageData imRef = new ImageData(refPath);
            imRef.AutoContrast();
            return imRef.GetBitmapRGB();
        }

        /// <summary>
        /// Return a data TIF, brightness/contrast-enhanced, with baseline and structures drawn on it
        /// </summary>
        private Bitmap GetBmpMarked(string imagePath)
        {
            ImageData img = new ImageData(imagePath);
            img.AutoContrast();
            Bitmap bmp = img.GetBitmapRGB();
            Pen pen = new Pen(new SolidBrush(Color.Yellow));
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.DrawLine(pen, new Point(0, baseline1), new Point(bmp.Width, baseline1));
            gfx.DrawLine(pen, new Point(0, baseline2), new Point(bmp.Width, baseline2));
            gfx.DrawLine(pen, new Point(structure1, 0), new Point(structure1, bmp.Height));
            gfx.DrawLine(pen, new Point(structure2, 0), new Point(structure2, bmp.Height));
            gfx.Dispose();
            return bmp;
        }

        public Bitmap GetBmpMarkedG()
        {
            return GetBmpMarked(pathsDataG[frame]);
        }

        public Bitmap GetBmpMarkedR()
        {
            return GetBmpMarked(pathsDataR[frame]);
        }

        #endregion

        #region saving and loading

        public void CreateSaveFolder()
        {
            if (!validLinescanFolder)
                return;
            if (!System.IO.Directory.Exists(pathSaveFolder))
            {
                System.IO.Directory.CreateDirectory(pathSaveFolder);
                Log("created folder: " + pathSaveFolder);
            }
        }

        public void LoadSettingsINI()
        {
            if (!validLinescanFolder)
                return;

            if (!System.IO.File.Exists(pathIniFile))
            {
                Log("INI file not found.");
                return;
            }

            Log("Loading data from INI file...");
            foreach (string rawLine in System.IO.File.ReadAllLines(pathIniFile))
            {
                string line = rawLine.Trim();
                if (line.StartsWith(";"))
                    continue;
                if (!line.Contains("="))
                    continue;
                string[] lineParts = line.Split('=');
                string var = lineParts[0];
                string valStr = lineParts[1];

                if (var == "version" && valStr != version)
                    Log($"WARNING! INI version ({valStr}) does not match this software version ({version})");
                else if (var == "baseline1")
                    baseline1 = int.Parse(valStr);
                else if (var == "baseline2")
                    baseline2 = int.Parse(valStr);
                else if (var == "structure1")
                    structure1 = int.Parse(valStr);
                else if (var == "structure2")
                    structure2 = int.Parse(valStr);
                else if (var == "filterPx")
                    filterPx = int.Parse(valStr);
            }
        }

        public void SaveSettingsINI()
        {
            if (!validLinescanFolder)
                return;
            string text = "; Scan-A-Gator Linescan Settings\n";
            text += $"version={version}\n";
            text += $"baseline1={baseline1}\n";
            text += $"baseline2={baseline2}\n";
            text += $"structure1={structure1}\n";
            text += $"structure2={structure2}\n";
            text += $"filterPx={filterPx}\n";
            text = text.Replace("\n", "\r\n").Trim();
            System.IO.File.WriteAllText(pathIniFile, text);
            Log($"Saved settings in: {pathIniFile}");
        }

        #endregion

        #region data export

        public string GetCsvCurve()
        {
            string csv = "dG/R\n";
            for (int i = 0; i < dataDeltaGoRsmoothed.Length; i++)
                csv += dataDeltaGoRsmoothed[i].ToString() + "\n";
            return csv;
        }

        public string GetCsvAllData(string delimiter = "\t")
        {
            string csv = "Time (ms), R (PMT), G (PMT), G/R (%), dG/R (%), filtered dG/R (%)\n";
            csv = csv.Replace(", ", delimiter);
            for (int i = 0; i < dataDeltaGoRsmoothed.Length; i++)
            {
                csv += dataTimeMsec[i].ToString() + delimiter;
                csv += dataR[i].ToString() + delimiter;
                csv += dataG[i].ToString() + delimiter;
                csv += dataGoR[i].ToString() + delimiter;
                csv += dataDeltaGoR[i].ToString() + delimiter;
                csv += dataDeltaGoRsmoothed[i].ToString() + delimiter;
                csv += "\n";
            }

            return csv;
        }

        #endregion

    }
}
