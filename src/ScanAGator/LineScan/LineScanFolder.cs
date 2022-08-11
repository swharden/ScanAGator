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
        public LineScanFolder(string pathFolder, bool analyzeFirstFrameImmediately = true)
        {
            this.pathFolder = System.IO.Path.GetFullPath(pathFolder);
            if (!System.IO.Directory.Exists(this.pathFolder))
                throw new ArgumentException($"folder does not exist: {this.pathFolder}");

            try
            {
                ScanForFiles(pathFolder);
                ReadValuesFromXML(pathXml);
                isValid = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                isValid = false;
            }

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
                if (analyzeFirstFrameImmediately)
                    GenerateAnalysisCurves();
            }
        }

        /// <summary>
        /// This is the primary analysis method for delta green and delta green over red calculations
        /// </summary>
        public void GenerateAnalysisCurves()
        {
            // determine mean pixel intensity over time between the structure markers
            (int s1, int s2) = Operations.GetValidStructure(structure1, structure2);
            PixelRange structure = new(s1, s2, micronsPerPx);

            curveG = ImageDataTools.GetAverageTopdown(imgG, structure);
            curveR = ImageDataTools.GetAverageTopdown(imgR, structure);

            // create a dG channel by baseline subtracting just the green channel
            (int b1, int b2) = Operations.GetValidBaseline(baseline1, baseline2);
            PixelRange baseline = new(b1, b2, scanLinePeriod);
            curveDeltaG = Operations.SubtractBaseline(curveG, baseline);

            // create a d(G/R) curve by finding the G/R ratio then baseline subtracting that
            if (isRatiometric)
            {
                curveGoR = Operations.CreateRatioCurve(curveG, curveR);
                curveDeltaGoR = Operations.SubtractBaseline(curveGoR, baseline);
            }
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
            PixelRange structure = StructureDetection.GetBrightestStructure(imgG);
            structure1 = structure.FirstPixel;
            structure2 = structure.LastPixel;
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

        private void ScanForFiles(string folderPath)
        {
            Prairie.FolderContents scanner = new(folderPath);
            pathsRef = scanner.ReferenceTifPaths;
            pathsDataG = scanner.ImageFilesG;
            pathsDataR = scanner.ImageFilesR;
            pathXml = scanner.XmlFilePath;
        }

        public void ReadValuesFromXML(string xmlFilePath)
        {
            Prairie.ParirieXmlFile x = new(xmlFilePath);
            acquisitionDate = x.AcquisitionDate;
            scanLinePeriod = x.MsecPerPixel;
            micronsPerPx = x.MicronsPerPixel;
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