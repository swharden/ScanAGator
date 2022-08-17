using ScanAGator.Imaging;

namespace ScanAGator.Analysis;

/// <summary>
/// This class holds all settings and data needed to calculate ΔG/R for a single ratiometric image
/// </summary>
public class AnalysisSettings
{
    public readonly RatiometricImage Image;
    public readonly BaselineRange Baseline;
    public readonly StructureRange Structure;
    public readonly int FilterPx;
    public readonly Prairie.ParirieXmlFile Xml;

    public AnalysisSettings(RatiometricImage image, BaselineRange baseline, StructureRange structure, int filterPx, Prairie.ParirieXmlFile xml)
    {
        Image = image;
        Baseline = baseline;
        Structure = structure;
        FilterPx = filterPx;
        Xml = xml;
    }
}
