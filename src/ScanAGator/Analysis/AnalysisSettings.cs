using ScanAGator.Imaging;
using System.IO;

namespace ScanAGator.Analysis;

/// <summary>
/// This class holds all settings and data needed to calculate ΔG/R for a single ratiometric image
/// </summary>
public class AnalysisSettings
{
    public readonly RatiometricImage PrimaryImage;
    public readonly RatiometricImage[] SecondaryImages;
    public readonly BaselineRange Baseline;
    public readonly StructureRange Structure;
    public readonly int FilterPx;
    public readonly double FloorPercentile;
    public readonly Prairie.ParirieXmlFile Xml;

    public AnalysisSettings(RatiometricImage img,
        RatiometricImage[] img2,
        BaselineRange baseline,
        StructureRange structure,
        int filterPx,
        double floorPercentile,
        Prairie.ParirieXmlFile xml)
    {
        PrimaryImage = img;
        SecondaryImages = img2;
        Baseline = baseline;
        Structure = structure;
        FilterPx = filterPx;
        FloorPercentile = floorPercentile;
        Xml = xml;
    }

    public string GetOutputFolder(bool create = true)
    {
        string outputFolder = Path.Combine(Xml.FolderPath, "ScanAGator");
        if (create && !Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);
        return outputFolder;
    }
}
