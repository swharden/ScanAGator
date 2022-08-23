using ScanAGator.DataExport;
using ScanAGator.Imaging;
using System;
using System.IO;

namespace ScanAGator.Analysis;

/// <summary>
/// This class contains logic for calculating ΔG/R from analysis settings containing a single ratiometric image
/// </summary>
public class AnalysisResult
{
    public readonly AnalysisSettings Settings;
    public readonly AnalysisResultCurves Curves;

    public static Version Version => new(4, 1); // EDIT THIS MANUALLY

    public static string VersionString => $"Scan-A-Gator v{Version.Major}.{Version.Minor}";

    public AnalysisResult(AnalysisSettings settings)
    {
        Settings = settings;
        Curves = new(settings.PrimaryImage, settings.Baseline, settings.Structure, settings.FilterPx, settings.Xml.MsecPerPixel);
    }

    public string Save()
    {
        string outputFolder = Path.Combine(Settings.Xml.FolderPath, "ScanAGator");
        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        CsvBuilder csv = new();
        csv.Add("Time", "ms", "", Curves.SmoothDeltaGreenOverRedCurve.Times);
        csv.Add("Green", "AFU", "average", Curves.SmoothGreenCurve.Values);
        csv.Add("Red", "AFU", "average", Curves.SmoothRedCurve.Values);
        csv.Add("ΔG/R", "%", "average", Curves.SmoothDeltaGreenOverRedCurve.Values);

        for (int i = 0; i < Settings.SecondaryImages.Length; i++)
        {
            RatiometricImage img = Settings.SecondaryImages[i];
            AnalysisResultCurves curves = new(img, Settings.Baseline, Settings.Structure, Settings.FilterPx, Settings.Xml.MsecPerPixel);

            csv.Add("Green", "AFU", $"frame {i + 1}", curves.SmoothGreenCurve.Values);
            csv.Add("Red", "AFU", $"frame {i + 1}", curves.SmoothRedCurve.Values);
            csv.Add("ΔG/R", "AFU", $"frame {i + 1}", curves.SmoothDeltaGreenOverRedCurve.Values);
        }

        string csvFilePath = Path.Combine(outputFolder, "curves.csv");
        csv.SaveAs(csvFilePath);

        Metadata.SaveJsonMetadata(csvFilePath + ".json", Settings);

        return csvFilePath;
    }
}
