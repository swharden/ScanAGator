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

        public int baseline1 { get; set; }
        public int baseline2 { get; set; }
        public int structure1 { get; set; }
        public int structure2 { get; set; }
        public int filterPoints { get; set; }

        public string pathLinescanFolder { get; private set; }
        public bool validLinescanFolder { get; private set; }
        public string pathLinescanXML { get; private set; }
        public double scanLinePeriod { get; private set; }
        public string[] pathsRefImages { get; private set; }
        public string[] pathsDataR;
        public string[] pathsDataG;

        public double[] dataG { get; private set; }
        public double[] dataR { get; private set; }
        public double[] dataGoR { get; private set; }
        public double[] dataDeltaGoR { get; private set; }
        public Size dataImage { get; private set; }

        #region loading of linescan files and data

        public PrairieLS(string path)
        {
            validLinescanFolder = true;

            pathLinescanFolder = System.IO.Path.GetFullPath(path);
            Log($"Loading linescan folder: {this.pathLinescanFolder}");

            ScanFolder();
            LimitAuto();
        }

        /// <summary>
        /// Call this once to load all information about the linescan into this class.
        /// This function does everything short of loading TIFF data.
        /// </summary>
        private void ScanFolder()
        {

            if (!System.IO.Directory.Exists(pathLinescanFolder))
            {
                Error("Linescan folder not found.");
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

            // error check data image counts
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
                Console.WriteLine($"data image dimensions: {dataImage.Width}px (position) by {dataImage.Height}px (time)");
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
        private List<string> log;
        private void Log(string message)
        {
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

        public void LimitAuto()
        {
            // default limits

            // determine baseline as first 5-15%
            baseline1 = (int)(.05 * dataImage.Height);
            baseline2 = (int)(.15 * dataImage.Height);
            Log($"Baseline defaulted to {baseline1}px to {baseline2}px");

            // determine center then go a few pixels on each side
            // todo: replace this with an auto-detected structure centered on brightest point
            int center = dataImage.Width / 2;
            int stripWidth = 5;
            structure1 = center - stripWidth;
            structure2 = center + stripWidth;
            Log($"Structure defaulted to {structure1}px to {structure2}px");
        }

        public void LimitErrorChecking()
        {
            // fix things like inverted or out-of-bounds structures and baselines

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

            baseline1 = (baseline1 < 0) ? 0 : baseline1;
            baseline2 = (baseline2 < 0) ? 0 : baseline2;
            structure1 = (structure1 < 0) ? 0 : structure1;
            structure2 = (structure2 < 0) ? 0 : structure2;

            baseline1 = (baseline1 > dataImage.Height - 1) ? 0 : dataImage.Height - 1;
            baseline2 = (baseline2 > dataImage.Height) ? 0 : dataImage.Height;
            structure1 = (structure1 > dataImage.Width - 1) ? 0 : dataImage.Width - 1;
            structure2 = (structure2 > dataImage.Width) ? 0 : dataImage.Width;

        }

        public void Analyze(int frame = 0)
        {
            LimitErrorChecking();

            // load the G and R images and hold them as BMPs

            // load G and R based on limits

            // apply lowpass filter if necessary

            // calculate the GoR and dGoR arrays
        }

        #endregion

        #region bitmap handling

        private Bitmap GetBmpReference(int imageNumber = 0)
        {
            return new Bitmap(1, 1);
        }

        private Bitmap MarkLimits(Bitmap bmp)
        {
            // draw the lines
            return bmp;
        }

        private Bitmap GetBmpG(bool markLimits = true)
        {
            return new Bitmap(1, 1);
        }

        private Bitmap GetBmpR(bool markLimits = true)
        {
            return new Bitmap(1, 1);
        }

        #endregion

        #region saving and loading

        public void SaveCSV(string saveFilePath)
        {

        }

        public void SaveSettings()
        {
            // save current settings (frame, structure, baseline) as XML.
        }

        #endregion
    }
}
