using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor

namespace ScanAGator
{
    /* This class provides the original linescan analysis used by ScanAGator.
     * It is included for backup purposes, but the newer version should be used wherever possible.
     * This was designed to be manipulated by a GUI and requires lots of lifetime state management.
     * Newer designs use a simpler functional workflow with immutabile state.
     */
    public class LineScanFolder
    {
        public bool IsValid { get; private set; } = true;

        public readonly string FolderPath;

        private readonly Prairie.FolderContents FolderContents;

        private readonly Prairie.ParirieXmlFile XmlFile;

        // Bitmaps for displaying
        public Bitmap? BmpReference;
        public Bitmap? BmpDataG;
        public Bitmap? BmpDataR;
        public Bitmap? BmpDataMerge;

        // Bitmap data for analysis
        public ImageData ImgG;
        public ImageData ImgR;
        public ImageData ImgM;

        public readonly double[] timesMsec;

        public int BaselineIndex1;
        public int BaselineIndex2;
        public int StructureIndex1;
        public int StructureIndex2;
        public int FilterSizePixels;

        public double DefaultBaselineFraction2 = 0.10;
        public double DefaultFilterTimeMsec = 0;

        public double[] CurveG;
        public double[] CurveDeltaG;
        public double[] CurveR;
        public double[] CurveGoR;
        public double[] CurveDeltaGoR;

        public string IniFilePath => Path.Combine(FolderPath, "ScanAGator/LineScanSettings.ini");
        public string SaveFolderPath => Path.GetDirectoryName(IniFilePath);
        public string ProgramSettingsPath => Path.GetFullPath("Defaults.ini");
        public bool IsRatiometric => IsValid && (DataImagePathsG.Any() && DataImagePathsR.Any());
        public string FolderName => Path.GetFileName(Path.GetDirectoryName(FolderPath));
        public string XmlFilePath => FolderContents.XmlFilePath;
        public string[] ReferenceImagePaths => FolderContents.ReferenceTifPaths;
        public string[] DataImagePathsR => FolderContents.ImageFilesR;
        public string[] DataImagePathsG => FolderContents.ImageFilesG;
        public double ScanLinePeriodMsec => XmlFile.MsecPerPixel;
        public double MicronsPerPixel => XmlFile.MicronsPerPixel;
        public DateTime AcquisitionDate => XmlFile.AcquisitionDate;
        public double PixelsPerMicron => 1.0 / MicronsPerPixel;

        /// <summary>
        /// A linescan folder holds data for an experiment at a single position.
        /// Repeated sequences (multiple frames)
        /// </summary>
        public LineScanFolder(string folderPath, bool analyzeFirstFrameImmediately = true)
        {
            FolderPath = Path.GetFullPath(folderPath);
            if (!Directory.Exists(FolderPath))
                throw new DirectoryNotFoundException(folderPath);

            try
            {
                FolderContents = new Prairie.FolderContents(FolderPath);
                XmlFile = new Prairie.ParirieXmlFile(FolderContents.XmlFilePath);
                IsValid = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                IsValid = false;
            }

            if (IsValid && ImgG is not null)
            {
                BmpReference = GetRefImage();
                SetFrame(0);
                timesMsec = Enumerable.Range(0, ImgG.Height).Select(x => x * ScanLinePeriodMsec).ToArray();
                ConfigFile.LoadDefaultSettings(this);
                AutoBaseline();
                AutoStructure();
                LoadDefaultFilterSettings();
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
            (int s1, int s2) = Operations.GetValidStructure(StructureIndex1, StructureIndex2);
            PixelRange structure = new(s1, s2);

            CurveG = ImageDataTools.GetAverageTopdown(ImgG, structure);
            CurveR = ImageDataTools.GetAverageTopdown(ImgR, structure);

            // create a dG channel by baseline subtracting just the green channel
            (int b1, int b2) = Operations.GetValidBaseline(BaselineIndex1, BaselineIndex2);
            PixelRange baseline = new(b1, b2);
            CurveDeltaG = Operations.SubtractBaseline(CurveG, baseline);

            // create a d(G/R) curve by finding the G/R ratio then baseline subtracting that
            if (IsRatiometric)
            {
                CurveGoR = Operations.CreateRatioCurve(CurveG, CurveR);
                CurveDeltaGoR = Operations.SubtractBaseline(CurveGoR, baseline);
            }
        }

        /// <summary>
        /// Set the baseline to encompass the default amount around the first fraction of the data (defined by defaultBaselineEndFrac)
        /// </summary>
        public void AutoBaseline()
        {
            if (!IsValid)
                return;

            BaselineIndex1 = 0;
            BaselineIndex2 = (int)(ImgG.Height * DefaultBaselineFraction2);
        }

        /// <summary>
        /// Set the structure bounds around the brightest structure in the image.
        /// The 1D projected image of the green channel is used.
        /// Boundaries are placed where intensity drops to half the distance between the peak and the noise floor.
        /// Noise floor is defined as the 20-percentile of the 1D projected image.
        /// </summary>
        public void AutoStructure()
        {
            PixelRange structure = StructureDetection.GetBrightestStructure(ImgG);
            StructureIndex1 = structure.FirstPixel;
            StructureIndex2 = structure.LastPixel;
        }

        /// <summary>
        /// Determine the ideal low-pass filter size (pixels) based on the filter time (ms)
        /// </summary>
        public void LoadDefaultFilterSettings()
        {
            if (!IsValid)
                return;

            double filterTimeMs = DefaultFilterTimeMsec;
            if (DefaultFilterTimeMsec == 0)
            {
                double scanTimeMs = ScanLinePeriodMsec * ImgG.Height;
                if (scanTimeMs > 1000)
                    filterTimeMs = 100;
                else
                    filterTimeMs = 10;
            }

            FilterSizePixels = (int)(filterTimeMs / ScanLinePeriodMsec);

            FilterSizePixels = Math.Min(FilterSizePixels, ImgG.Height); // limit max filter size to 1/5 of the duration
        }

        /// <summary>
        /// Multi-scan linescans have multiple frames. 
        /// Call this to load data for a specific frame.
        /// </summary>
        public void SetFrame(int frameNumber)
        {
            // error checking
            if (!IsValid)
                return;
            int frameCount = Math.Max(DataImagePathsG.Length, DataImagePathsR.Length);
            if (frameNumber >= frameCount)
            {
                return;
            }

            // load data bitmaps and create ratio if needed
            if (DataImagePathsG.Length > 0)
            {
                ImgG = ImageDataTools.ReadTif(DataImagePathsG[frameNumber]);
                BmpDataG = ImageDataTools.GetBmpDisplay(ImgG);
            }

            if (DataImagePathsR.Length > 0)
            {
                ImgR = ImageDataTools.ReadTif(DataImagePathsR[frameNumber]);
                BmpDataR = ImageDataTools.GetBmpDisplay(ImgR);
            }

            if (BmpDataG != null && BmpDataR != null)
            {
                BmpDataMerge = ImageDataTools.Merge(ImgR, ImgG, ImgR);

                double[] dataM = new double[ImgG.Values.Length];
                for (int i = 0; i < dataM.Length; i++)
                    dataM[i] = ImgG.Values[i] / ImgR.Values[i];
                ImgM = new ImageData(dataM, ImgG.Width, ImgG.Height);
            }
        }

        /// <summary>
        /// Return a copy of the reference image
        /// </summary>
        public Bitmap? GetRefImage(int number = 0)
        {
            if (number >= ReferenceImagePaths.Length)
                return null;
            ImageData imgRef = ImageDataTools.ReadTif(ReferenceImagePaths[number]);
            return ImageDataTools.GetBmpDisplay(imgRef);
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
                gfx.DrawLine(pen, new Point(0, BaselineIndex1), new Point(bmp.Width, BaselineIndex1));
                gfx.DrawLine(pen, new Point(0, BaselineIndex2), new Point(bmp.Width, BaselineIndex2));
                gfx.DrawLine(pen, new Point(StructureIndex1, 0), new Point(StructureIndex1, bmp.Height));
                gfx.DrawLine(pen, new Point(StructureIndex2, 0), new Point(StructureIndex2, bmp.Height));
            }
            return bmp;
        }

        /// <summary>
        /// Return copy of the curve filtered according to the filter settings (shorter by double the filter size)
        /// </summary>
        public double[] GetFilteredYs(double[] curve)
        {
            double[] filteredValues = ImageDataTools.GaussianFilter1d(curve, FilterSizePixels);
            int padPoints = FilterSizePixels * 2 + 1;
            double[] filteredYs = new double[curve.Length - 2 * padPoints];
            Array.Copy(filteredValues, padPoints, filteredYs, 0, filteredYs.Length);
            return filteredYs;
        }

        /// <summary>
        /// Return copy of the Xs to go with the filtered Ys
        /// </summary>
        public double[] GetFilteredXs()
        {
            int padPoints = FilterSizePixels * 2 + 1;
            double[] filteredXs = new double[CurveG.Length - 2 * padPoints];
            Array.Copy(timesMsec, padPoints, filteredXs, 0, filteredXs.Length);
            return filteredXs;
        }

        public string GetCsvAllData() => DataExportOld.GetCSV(this);

        public void LoadSettingsINI() => ConfigFile.LoadINI(this);

        public void SaveSettingsINI() => ConfigFile.SaveINI(this);

        public string GetMetadataJson() => DataExportOld.GetMetadataJson(this);
    }
}