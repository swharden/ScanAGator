using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace ScanAGator
{
    public class LineScanFolder
    {
        public bool isValid { get; private set; } = true;
        public bool isRatiometric => pathsDataG?.Length > 0 && pathsDataR?.Length > 0;

        public readonly string pathFolder;
        public string folderName => System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(pathFolder));
        public string pathXml;
        public string[] pathsRef;
        public string[] pathsDataR;
        public string[] pathsDataG;

        public Bitmap bmpRef;
        public Bitmap bmpDataG;
        public Bitmap bmpDataR;
        public Bitmap bmpDataM;

        public ImageData imgG;
        public ImageData imgR;
        public ImageData imgM;

        public double scanLinePeriod;
        public double[] timesMsec;
        public double micronsPerPx;
        public DateTime acquisitionDate;
        public double pixelsPerMicron { get { return 1.0 / micronsPerPx; } }

        public int baseline1;
        public int baseline2;
        public int structure1;
        public int structure2;
        public int filterPx;

        public double defaultBaselineEndFrac = 0.10;
        public double defaultFilterTimeMs = 0;

        public double[] curveG;
        public double[] curveDeltaG;
        public double[] curveR;
        public double[] curveGoR;
        public double[] curveDeltaGoR;

        private Version ver => typeof(LineScanFolder).Assembly.GetName().Version;
        public string version => $"Scan-A-Gator v{ver.Major}.{ver.Minor}";
        public string pathIniFile => System.IO.Path.Combine(pathFolder, "ScanAGator/LineScanSettings.ini");
        public string pathSaveFolder => System.IO.Path.GetDirectoryName(pathIniFile);
        public string pathProgramSettings => System.IO.Path.GetFullPath("Defaults.ini");

        private readonly StringBuilder logSB = new StringBuilder();
        public string log => logSB.ToString();

        /// <summary>
        /// A linescan folder holds data for an experiment at a single position.
        /// Repeated sequences (multiple frames)
        /// </summary>
        public LineScanFolder(string pathFolder, bool analyzeImmediately = true)
        {
            this.pathFolder = System.IO.Path.GetFullPath(pathFolder);
            if (!System.IO.Directory.Exists(this.pathFolder))
                throw new ArgumentException($"folder does not exist: {this.pathFolder}");

            ScanForFiles();
            ReadValuesFromXML();

            if (isValid)
            {
                bmpRef = GetRefImage();
                SetFrame(0);
                CreateTimePoints();
                ConfigFile.LoadDefaultSettings(this);
                AutoBaseline();
                AutoStructure();
                AutoFilter();
                LoadSettingsINI();
                if (analyzeImmediately)
                    GenerateAnalysisCurves();
            }
        }

        /// <summary>
        /// This is the primary analysis method for delta green and delta green over red calculations
        /// </summary>
        public void GenerateAnalysisCurves()
        {
            // determine mean pixel intensity over time between the structure markers
            var (structureIndex1, structureIndex2) = GetValidStructure(structure1, structure2);
            curveG = ImageDataTools.GetAverageTopdown(imgG, structureIndex1, structureIndex2);
            curveR = ImageDataTools.GetAverageTopdown(imgR, structureIndex1, structureIndex2);

            // create a dG channel by baseline subtracting just the green channel
            var (baselineIndex1, baselineIndex2) = GetValidBaseline(baseline1, baseline2);
            curveDeltaG = CreateBaselineSubtractedCurve(curveG, baselineIndex1, baselineIndex2);

            // create a d(G/R) curve by finding the G/R ratio then baseline subtracting that
            if (isRatiometric)
            {
                curveGoR = CreateRatioCurve(curveG, curveR);
                curveDeltaGoR = CreateBaselineSubtractedCurve(curveGoR, baselineIndex1, baselineIndex2);
            }
        }

        /// <summary>
        /// Return structure indexes in the proper order and separated by at least 1px
        /// </summary>
        private static (int s1, int s2) GetValidStructure(int structure1, int structure2)
        {
            int s1 = structure1;
            int s2 = structure2;

            if (s1 > s2)
            {
                int tmp = s1;
                s1 = s2;
                s2 = tmp;
            }

            if (s1 == s2)
            {
                if (s1 == 0)
                    s2 += 1;
                else
                    s1 -= 1;
            }

            return (s1, s2);
        }

        /// <summary>
        /// Return baseline indexes in the proper order and separated by at least 1px
        /// </summary>
        private static (int s1, int s2) GetValidBaseline(int baseline1, int baseline2)
        {
            int b1 = baseline1;
            int b2 = baseline2;

            if (b1 > b2)
            {
                int tmp = b1;
                b1 = b2;
                b2 = tmp;
            }

            if (b1 == b2)
            {
                if (b1 == 0)
                    b2 += 1;
                else
                    b1 -= 1;
            }

            return (b1, b2);
        }

        /// <summary>
        /// Return a new array representing numerator / denomenator in % units
        /// </summary>
        private static double[] CreateRatioCurve(double[] numerator, double[] denomenator)
        {
            if (numerator.Length != denomenator.Length)
                throw new ArgumentException("numerator and denomenator must have the same length");

            return Enumerable.Range(0, numerator.Length)
                             .Select(i => numerator[i] / denomenator[i] * 100)
                             .ToArray();
        }

        /// <summary>
        /// Return a copy of the source array where every point was subtracted by the mean value between the baseline indexes
        /// </summary>
        private static double[] CreateBaselineSubtractedCurve(double[] source, int baselineIndex1, int baselineIndex2)
        {
            double baselineSum = 0;
            for (int i = baselineIndex1; i < baselineIndex2; i++)
                baselineSum += source[i];
            double baselineG = baselineSum / (baselineIndex2 - baselineIndex1);

            double[] delta = new double[source.Length];
            for (int i = 0; i < delta.Length; i++)
                delta[i] = source[i] - baselineG;

            return delta;
        }

        /// <summary>
        /// Set the baseline to encompass the default amount around the first fraction of the data (defined by defaultBaselineEndFrac)
        /// </summary>
        public void AutoBaseline()
        {
            if (!isValid)
                return;

            baseline1 = 0;
            baseline2 = (int)(imgG.height * defaultBaselineEndFrac);
            Log($"Automatic baseline: {baseline1}px - {baseline1}px ({baseline1 * scanLinePeriod}ms = {baseline1 * scanLinePeriod}ms)");
        }

        /// <summary>
        /// Set the structure bounds around the brightest structure in the image.
        /// The 1D projected image of the green channel is used.
        /// Boundaries are placed where intensity drops to half the distance between the peak and the noise floor.
        /// Noise floor is defined as the 20-percentile of the 1D projected image.
        /// </summary>
        public void AutoStructure()
        {
            // determine the brightest column
            double[] columnIntensities = ImageDataTools.GetAverageLeftright(imgG);
            double brightestValue = 0;
            int brightestIndex = 0;
            for (int i = 0; i < columnIntensities.Length; i++)
            {
                if (columnIntensities[i] > brightestValue)
                {
                    brightestValue = columnIntensities[i];
                    brightestIndex = i;
                }
            }
            Log($"Brightest structure (G): {brightestValue} AFU at {brightestIndex}px");

            // determine the 20-percentile brightness (background fluorescence)
            double[] sortedIntensities = new double[columnIntensities.Length];
            Array.Copy(columnIntensities, sortedIntensities, columnIntensities.Length);
            Array.Sort(sortedIntensities);
            double noiseFloor = sortedIntensities[(int)(sortedIntensities.Length * .2)];
            Log($"Noise floor (G): {noiseFloor} AFU");

            // intensity cut-off is half-way to the noise floor
            double peakAboveNoise = brightestValue - noiseFloor;
            double cutOff = (peakAboveNoise * .5) + noiseFloor;
            Log($"Cut-off (G): {cutOff} AFU");

            // start both structures at the brighest point, then walk away
            structure1 = brightestIndex;
            structure2 = brightestIndex;
            while (columnIntensities[structure1] > cutOff && structure1 > 0)
                structure1--;
            while (columnIntensities[structure2] > cutOff && structure2 < columnIntensities.Length - 1)
                structure2++;

            Log($"Automatic structure: {structure1}px - {structure2}px");
        }

        /// <summary>
        /// Determine the ideal low-pass filter size (pixels) based on the filter time (ms)
        /// </summary>
        public void AutoFilter()
        {
            if (!isValid)
                return;

            double filterTimeMs = defaultFilterTimeMs;
            if (defaultFilterTimeMs == 0)
            {
                double scanTimeMs = scanLinePeriod * imgG.height;
                if (scanTimeMs > 1000)
                    filterTimeMs = 100;
                else
                    filterTimeMs = 10;
            }

            filterPx = (int)(filterTimeMs / scanLinePeriod);

            filterPx = Math.Min(filterPx, imgG.height); // limit max filter size to 1/5 of the duration

            Log($"Automatic filter: {filterPx}px ({filterPx * scanLinePeriod}ms)");
        }

        private void ScanForFiles()
        {

            // ensure a reference folder exists
            string pathRefFolder = System.IO.Path.Combine(pathFolder, "References");
            if (!System.IO.Directory.Exists(pathRefFolder))
            {
                Error("no 'References' sub-folder");
                return;
            }

            // scan for reference images
            pathsRef = System.IO.Directory.GetFiles(pathRefFolder, "*.tif");
            if (pathsRef.Length == 0)
            {
                Error("References sub-folder contains no TIF images");
                return;
            }
            Log($"Reference image count: {pathsRef.Length}");

            // scan for data image files
            string[] pathsTif = System.IO.Directory.GetFiles(pathFolder, "LineScan*.tif");
            Array.Sort(pathsTif);
            List<string> dataImagesR = new List<string>();
            List<string> dataImagesG = new List<string>();
            foreach (string filePath in pathsTif)
            {
                string fileName = System.IO.Path.GetFileName(filePath);
                if (fileName.Contains("Source.tif"))
                    continue;
                if (fileName.Contains("Ch1"))
                    dataImagesR.Add(filePath);
                if (fileName.Contains("Ch2"))
                    dataImagesG.Add(filePath);
            }
            pathsDataG = dataImagesG.ToArray();
            pathsDataR = dataImagesR.ToArray();
            if (pathsDataG.Length == 0 && pathsDataG.Length == 0)
            {
                Error("No data images found");
                return;
            }
            if (pathsDataG.Length > 0 && pathsDataR.Length > 0 && pathsDataG.Length != pathsDataR.Length)
            {
                Error("A different number of red and green images were found");
                return;
            }
            Log($"Red image count: {pathsDataR.Length}");
            Log($"Green image count: {pathsDataG.Length}");
            Log($"Ratiometric: {isRatiometric}");

            // scan for the XML configuration file
            string[] pathsXml = System.IO.Directory.GetFiles(pathFolder, "LineScan*.xml");
            if (pathsXml.Length == 0)
            {
                Error("Linescan XML file could not be found");
                return;
            }
            pathXml = pathsXml[0];
            Log($"XML File: {System.IO.Path.GetFileName(pathXml)}");
        }

        public void ReadValuesFromXML()
        {
            // error checking
            if (!isValid)
                return;

            // WARNING: PrarieView has version-specific XML structures, so discrete string parsing is simplest
            string[] xmlLines = System.IO.File.ReadAllLines(pathXml);

            // date
            if (xmlLines[1].Contains("date="))
            {
                string dateString = xmlLines[1].Split('\"')[3];
                acquisitionDate = DateTime.Parse(dateString);
            }

            // scan line period
            // WARNING: XML files can have multiple scan line periods. Take the last one.
            scanLinePeriod = -1;
            foreach (string line in xmlLines)
            {
                if ((line.Contains("scanLinePeriod") || line.Contains("scanlinePeriod")) && line.Contains("value="))
                {
                    string split1 = "value=\"";
                    string split2 = "\"";
                    string valStr = line.Substring(line.IndexOf(split1) + split1.Length);
                    valStr = valStr.Substring(0, valStr.IndexOf(split2));
                    scanLinePeriod = double.Parse(valStr) * 1000;
                    Log($"Scan line period: {scanLinePeriod} ms");
                }
            }
            if (scanLinePeriod == -1)
                Error($"Scan line period could not be found in: {pathXml}");

            // microns per pixel
            micronsPerPx = double.NaN;
            for (int i = 0; i < xmlLines.Length - 1; i++)
            {
                string line = xmlLines[i];
                if (line.Contains("micronsPerPixel_XAxis"))
                {
                    Error("unsupported (old) version of PrairieView");
                    return;
                }

                if (line.Contains("micronsPerPixel"))
                {
                    string value = xmlLines[i + 1];
                    string[] values = value.Split('"');
                    micronsPerPx = double.Parse(values[3]);
                    Log($"Microns per pixel: {micronsPerPx} µm");
                    break;
                }
            }
            if (micronsPerPx == double.NaN)
                Error($"Microns per pixel could not be found in: {pathXml}");
        }

        /// <summary>
        /// Populate timesMsec based on scanLinePeriod
        /// </summary>
        public void CreateTimePoints()
        {
            // error checking
            if (!isValid)
                return;

            timesMsec = new double[imgG.height];
            for (int i = 0; i < timesMsec.Length; i++)
            {
                if (scanLinePeriod > 0)
                    timesMsec[i] = i * scanLinePeriod;
                else
                    timesMsec[i] = i;
            }
        }

        /// <summary>
        /// Multi-scan linescans have multiple frames. 
        /// Call this to load data for a specific frame.
        /// </summary>
        public void SetFrame(int frameNumber)
        {
            // error checking
            if (!isValid)
                return;
            int frameCount = Math.Max(pathsDataG.Length, pathsDataR.Length);
            if (frameNumber >= frameCount)
            {
                Log($"frame {frameNumber} cannot be called (max frame {frameCount - 1})");
                return;
            }

            // load data bitmaps and create ratio if needed
            if (pathsDataG.Length > 0)
            {
                imgG = new ImageData(pathsDataG[frameNumber]);
                bmpDataG = imgG.GetBmpDisplay();
            }

            if (pathsDataR.Length > 0)
            {
                imgR = new ImageData(pathsDataR[frameNumber]);
                bmpDataR = imgR.GetBmpDisplay();
            }

            if (bmpDataG != null && bmpDataR != null)
            {
                bmpDataM = ImageDataTools.Merge(imgR, imgG, imgR);

                double[] dataM = new double[imgG.data.Length];
                for (int i = 0; i < dataM.Length; i++)
                    dataM[i] = imgG.data[i] / imgR.data[i];
                imgM = new ImageData(dataM, imgG.width, imgG.height);
            }
        }

        /// <summary>
        /// Mark the linescan invalid and provide a reason
        /// </summary>
        private void Error(string message)
        {
            isValid = false;
            logSB.AppendLine("CRITICAL ERROR: " + message);
        }

        /// <summary>
        /// Record details about the linescan analysis procedure
        /// </summary>
        private void Log(string message)
        {
            logSB.AppendLine(message);
            Debug.WriteLine(message);
        }

        /// <summary>
        /// Return a copy of the reference image
        /// </summary>
        public Bitmap GetRefImage(int number = 0)
        {
            if (number >= pathsRef.Length)
                return null;
            ImageData imgRef = new ImageData(pathsRef[number]);
            return imgRef.GetBmpDisplay();
        }

        /// <summary>
        /// Return a copy of a linescan image, brightness/contrast-enhanced, with baseline and structures drawn on it
        /// </summary>
        public Bitmap MarkLinescan(Bitmap bmpOriginal)
        {
            Bitmap bmp = new Bitmap(bmpOriginal);
            using (Graphics gfx = Graphics.FromImage(bmp))
            using (Pen pen = new Pen(new SolidBrush(Color.Yellow)))
            {
                gfx.DrawLine(pen, new Point(0, baseline1), new Point(bmp.Width, baseline1));
                gfx.DrawLine(pen, new Point(0, baseline2), new Point(bmp.Width, baseline2));
                gfx.DrawLine(pen, new Point(structure1, 0), new Point(structure1, bmp.Height));
                gfx.DrawLine(pen, new Point(structure2, 0), new Point(structure2, bmp.Height));
            }
            return bmp;
        }

        /// <summary>
        /// Return copy of the curve filtered according to the filter settings (shorter by double the filter size)
        /// </summary>
        public double[] GetFilteredYs(double[] curve)
        {
            if (curve == null)
                return null;
            double[] filteredValues = ImageDataTools.GaussianFilter1d(curve, filterPx);
            int padPoints = filterPx * 2 + 1;
            double[] filteredYs = new double[curve.Length - 2 * padPoints];
            Array.Copy(filteredValues, padPoints, filteredYs, 0, filteredYs.Length);
            return filteredYs;
        }

        /// <summary>
        /// Return copy of the Xs to go with the filtered Ys
        /// </summary>
        public double[] GetFilteredXs()
        {
            if (curveG == null)
                return null;
            int padPoints = filterPx * 2 + 1;
            double[] filteredXs = new double[curveG.Length - 2 * padPoints];
            Array.Copy(timesMsec, padPoints, filteredXs, 0, filteredXs.Length);
            return filteredXs;
        }

        public string GetCsvAllData() => DataExport.GetCSV(this);

        public void LoadSettingsINI() => ConfigFile.LoadINI(this);

        public void SaveSettingsINI() => ConfigFile.SaveINI(this);

        public string GetMetadataJson() => DataExport.GetMetadataJson(this);
    }
}