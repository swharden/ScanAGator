using ScanAGator.Imaging;

namespace ScanAGator.Analysis
{
    /// <summary>
    /// Holds all information needed to analyze a single linescan
    /// </summary>
    public class AnalysisInput
    {
        public readonly RatiometricImage Image;
        public readonly PixelRange Baseline;
        public readonly PixelRange Structure;
        public readonly int FilterPx;
        public readonly Prairie.ParirieXmlFile Xml;

        public AnalysisInput(RatiometricImage image, PixelRange baseline, PixelRange structure, int filterPx, Prairie.ParirieXmlFile xml)
        {
            Image = image;
            Baseline = baseline;
            Structure = structure;
            FilterPx = filterPx;
            Xml = xml;
        }
    }
}
