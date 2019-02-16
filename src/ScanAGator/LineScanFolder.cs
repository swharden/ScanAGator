using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ScanAGator
{
    public class LineScanFolder
    {
        public bool isValid { get; private set; }
        public bool isRatiometric { get; private set; }

        public readonly string pathFolder;
        public readonly string folderName;
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

        public int baseline1;
        public int baseline2;
        public int structure1;
        public int structure2;
        public int filterPx;

        public double[] curveG;
        public double[] curveDeltaG;
        public double[] curveR;
        public double[] curveGoR;
        public double[] curveDeltaGoR;

        public string version { get { return Properties.Resources.ResourceManager.GetString("version"); } }
        public string pathIniFile { get { return System.IO.Path.Combine(pathFolder, "ScanAGator/LineScanSettings.ini"); } }
        public string pathSaveFolder { get { return System.IO.Path.GetDirectoryName(pathIniFile); } }
        public string pathProgramSettings { get { return System.IO.Path.GetFullPath("Defaults.ini"); } }

        public string log { get; private set; }


        public LineScanFolder(string pathFolder)
        {
            isValid = true;

            this.pathFolder = System.IO.Path.GetFullPath(pathFolder);
            folderName = System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(this.pathFolder));
            Log($"Loading linescan folder: {this.pathFolder}");

            ScanForFiles();
            LoadRefBmp();
            ReadScanLinePeriod();
            SetFrame(0);
            CreateTimePoints();
            LoadDefaultSettings();
            AutoBaseline();
            AutoStructure();
            AutoFilter();
            LoadSettingsINI();
        }


        #region loading of defaults from INI
        public void LoadDefaultSettings()
        {
            if (!isValid)
                return;

            if (!System.IO.File.Exists(pathProgramSettings))
            {
                Log($"Creating settings file: {pathProgramSettings}");
                string txt = "; ScanAGator default settings\n";
                txt += "baselineEndFrac = 0.10\n";
                txt += ";filterTimeMs = 50.0\n";
                System.IO.File.WriteAllText(pathProgramSettings, txt);
            }

            Log($"Reading default values from: {pathProgramSettings}");
            string raw = System.IO.File.ReadAllText(pathProgramSettings);
            string[] lines = raw.Split('\n');
            foreach (string thisLine in lines)
            {
                string line = thisLine.Trim();
                if (line.StartsWith(";") || !line.Contains("="))
                    continue;
                string var = line.Split('=')[0].Trim();
                string val = line.Split('=')[1].Trim();
                if (var == "baselineEndFrac")
                    defaultBaselineEndFrac = double.Parse(val);
                if (var == "filterTimeMs")
                    defaultFilterTimeMs = double.Parse(val);
            }
        }
        #endregion

        #region analysis

        public void GenerateAnalysisCurves()
        {

            // quick and dirty error checking for structure
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

            // quick and dirty error checking for baseline
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

            // perform the analysis of R and G curves
            curveG = ImageDataTools.GetAverageTopdown(imgG, s1, s2);
            curveR = ImageDataTools.GetAverageTopdown(imgR, s1, s2);
            if (curveG != null && curveR != null)
            {
                curveGoR = new double[curveG.Length];
                for (int i = 0; i < curveG.Length; i++)
                    curveGoR[i] = curveG[i] / curveR[i] * 100.0;
            }

            // create delta G curve
            double baselineGsum = 0;
            for (int i = b1; i < b2; i++)
                baselineGsum += curveG[i];
            double baselineG = baselineGsum / (b2 - b1);
            curveDeltaG = new double[curveG.Length];
            for (int i = 0; i < curveDeltaG.Length; i++)
                curveDeltaG[i] = curveG[i] - baselineG;

            // create delta G over R curve
            if (curveGoR != null)
            {
                double baselineGoRsum = 0;
                for (int i = b1; i < b2; i++)
                    baselineGoRsum += curveGoR[i];
                double baselineGoR = baselineGoRsum / (b2 - b1);
                curveDeltaGoR = new double[curveGoR.Length];
                for (int i = 0; i < curveDeltaGoR.Length; i++)
                    curveDeltaGoR[i] = curveGoR[i] - baselineGoR;
            }
        }

        #endregion

        #region automatic detection and default values

        private double defaultBaselineEndFrac = 0.10;
        public void AutoBaseline()
        {
            if (!isValid)
                return;

            baseline1 = 0;
            baseline2 = (int)(imgG.height * defaultBaselineEndFrac);
            Log($"Automatic baseline: {baseline1}px - {baseline1}px ({baseline1 * scanLinePeriod}ms = {baseline1 * scanLinePeriod}ms)");
        }

        public void AutoStructure()
        {
            if (!isValid)
                return;

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

        private double defaultFilterTimeMs;
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
            // try to set the filter to 100 ms
            filterPx = (int)(filterTimeMs / scanLinePeriod);

            // of this linescan is super short, just use 1/5 of it
            if (filterPx > imgG.height / 5)
                filterPx = imgG.height / 5;
            Log($"Automatic filter: {filterPx}px ({filterPx * scanLinePeriod}ms)");
        }

        #endregion

        #region initialization and frame selection
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
            if (pathsDataG.Length > 0 && pathsDataR.Length > 0)
                isRatiometric = true;
            else
                isRatiometric = false;
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

        public void ReadScanLinePeriod()
        {
            // error checking
            if (!isValid)
                return;

            // PrarieView has version-specific XML structures, so discrete string parsing is simplest
            string[] xmlLines = System.IO.File.ReadAllLines(pathXml);
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
                    return;
                }
            }
            Error($"Scan line period could not be found in: {pathXml}");
        }

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

        #endregion

        #region logging

        private void Error(string message)
        {
            isValid = false;
            Log("CRITICAL ERROR: " + message);
        }

        private void Log(string message)
        {
            if (log is null)
                log = "";
            log = log + message + "\n";
            Console.WriteLine(message);
        }

        #endregion

        #region Bitmap handling

        // pre-load the reference bitmap to make it quick to recall
        public void LoadRefBmp()
        {
            // error checking
            if (!isValid)
                return;

            // try to load an 8-bit image if it's available
            string pathRefImage = null;
            foreach (string path in pathsRef)
            {
                if (path.Contains("8bit"))
                {
                    pathRefImage = path;
                    break;
                }
            }

            // fall back on the first image
            if (pathRefImage == null)
                pathRefImage = pathsRef[0];

            //bmpRef = new Bitmap(pathRefImage);
            ImageData imgRef = new ImageData(pathRefImage);
            bmpRef = imgRef.GetBmpDisplay();
        }

        public Bitmap MarkLinescan(Bitmap bmpOriginal)
        {
            // Return a data TIF, brightness/contrast-enhanced, with baseline and structures drawn on it
            Bitmap bmp = new Bitmap(bmpOriginal);
            Pen pen = new Pen(new SolidBrush(Color.Yellow));
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.DrawLine(pen, new Point(0, baseline1), new Point(bmp.Width, baseline1));
            gfx.DrawLine(pen, new Point(0, baseline2), new Point(bmp.Width, baseline2));
            gfx.DrawLine(pen, new Point(structure1, 0), new Point(structure1, bmp.Height));
            gfx.DrawLine(pen, new Point(structure2, 0), new Point(structure2, bmp.Height));
            gfx.Dispose();
            return bmp;
        }

        #endregion

        #region filtering
        public double[] GetFilteredYs(double[] data)
        {
            double[] filteredValues = ImageDataTools.GaussianFilter1d(data, filterPx);
            int padPoints = filterPx * 2 + 1;
            double[] filteredYs = new double[data.Length - 2 * padPoints];
            Array.Copy(filteredValues, padPoints, filteredYs, 0, filteredYs.Length);
            return filteredYs;
        }

        public double[] GetFilteredXs()
        {
            int padPoints = filterPx * 2 + 1;
            double[] filteredXs = new double[curveG.Length - 2 * padPoints];
            Array.Copy(timesMsec, padPoints, filteredXs, 0, filteredXs.Length);
            return filteredXs;
        }
        #endregion

        #region CSV

        public string GetCsvAllData()
        {
            // name, unit, comment, data...
            int dataPoints = imgG.height;
            string[] csvLines = new string[dataPoints + 3];

            // times (ms)
            csvLines[0] = "Time, ";
            csvLines[1] = "ms, ";
            csvLines[2] = folderName + ", ";
            for (int i = 0; i < dataPoints; i++)
                csvLines[i + 3] = Math.Round(timesMsec[i], 3).ToString() + ", ";

            // raw PMT values (R)
            if (curveR != null)
            {
                csvLines[0] += "R, ";
                csvLines[1] += "AFU, ";
                csvLines[2] += ", ";
                for (int i = 0; i < dataPoints; i++)
                    csvLines[i + 3] += Math.Round(curveR[i], 3).ToString() + ", ";
            }

            // raw PMT values (G)
            if (curveG != null)
            {
                csvLines[0] += "G, ";
                csvLines[1] += "AFU, ";
                csvLines[2] += ", ";
                for (int i = 0; i < dataPoints; i++)
                    csvLines[i + 3] += Math.Round(curveG[i], 3).ToString() + ", ";
            }

            // delta raw PMT values (G)
            if (curveDeltaG != null)
            {
                csvLines[0] += "dG, ";
                csvLines[1] += "AFU, ";
                csvLines[2] += ", ";
                for (int i = 0; i < dataPoints; i++)
                    csvLines[i + 3] += Math.Round(curveDeltaG[i], 3).ToString() + ", ";

                csvLines[0] += "f(dG), ";
                csvLines[1] += "AFU, ";
                csvLines[2] += "filtered, ";
                double[] filteredChopped = GetFilteredYs(curveDeltaG);
                double[] filtered = new double[dataPoints];
                for (int i = 0; i < dataPoints; i++)
                    filtered[i] = 0;
                Array.Copy(filteredChopped, 0, filtered, filterPx * 2, filteredChopped.Length);
                for (int i = 0; i < dataPoints; i++)
                    if (i < filterPx * 2 || i > (dataPoints - filterPx * 2 * 2))
                        csvLines[i + 3] += ", ";
                    else
                        csvLines[i + 3] += Math.Round(filtered[i], 3).ToString() + ", ";
            }

            // Green over Red
            if (curveGoR != null)
            {
                csvLines[0] += "G/R, ";
                csvLines[1] += "%, ";
                csvLines[2] += ", ";
                for (int i = 0; i < dataPoints; i++)
                    csvLines[i + 3] += Math.Round(curveGoR[i], 3).ToString() + ", ";
            }

            // Delta Green over Red
            if (curveDeltaGoR != null)
            {
                csvLines[0] += "dG/R, ";
                csvLines[1] += "%, ";
                csvLines[2] += ", ";
                for (int i = 0; i < dataPoints; i++)
                    csvLines[i + 3] += Math.Round(curveDeltaGoR[i], 3).ToString() + ", ";

                csvLines[0] += "f(dG/R), ";
                csvLines[1] += "AFU, ";
                csvLines[2] += "filtered, ";
                double[] filteredChopped = GetFilteredYs(curveDeltaGoR);
                double[] filtered = new double[dataPoints];
                for (int i = 0; i < dataPoints; i++)
                    filtered[i] = 0;
                Array.Copy(filteredChopped, 0, filtered, filterPx * 2, filteredChopped.Length);
                for (int i = 0; i < dataPoints; i++)
                    if (i < filterPx * 2 || i > (dataPoints - filterPx * 2 * 2))
                        csvLines[i + 3] += ", ";
                    else
                        csvLines[i + 3] += Math.Round(filtered[i], 3).ToString() + ", ";
            }

            // convert to CSV
            string csv = "";
            foreach (string line in csvLines)
                csv += line + "\n";
            return csv;
        }

        #endregion

        #region save and load settings

        public void LoadSettingsINI()
        {
            if (!isValid)
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
                    Log($"Note that INI version ({valStr}) differs from this software version ({version})");
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
            if (!isValid)
                return;

            if (!System.IO.Directory.Exists(pathSaveFolder))
            {
                System.IO.Directory.CreateDirectory(pathSaveFolder);
                Log("created folder: " + pathSaveFolder);
            }

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
    }
}
