using ScanAGator.Analysis;
using ScanAGator.CSV;
using ScanAGator.Imaging;
using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace ScanAGator.DataExport;

public static class AnalysisResultFile
{
    public static string SaveCsv(AnalysisResult result, bool saveMetadataToo = true)
    {
        CsvBuilder csv = new();
        csv.Add("Time", "ms", "", result.SmoothDeltaGreenOverRedCurve.Times);
        csv.Add("Green", "AFU", "average", result.SmoothGreenCurve.Values);
        csv.Add("Red", "AFU", "average", result.SmoothRedCurve.Values);
        csv.Add("ΔG/R", "%", "average", result.SmoothDeltaGreenOverRedCurve.Values);

        for (int i = 0; i < result.Settings.SecondaryImages.Length; i++)
        {
            RatiometricImage img = result.Settings.SecondaryImages[i];
            AnalysisResult curve = new(img, result.Settings);
            csv.Add("Green", "AFU", $"frame {i + 1}", curve.SmoothGreenCurve.Values);
            csv.Add("Red", "AFU", $"frame {i + 1}", curve.SmoothRedCurve.Values);
            csv.Add("ΔG/R", "AFU", $"frame {i + 1}", curve.SmoothDeltaGreenOverRedCurve.Values);
        }

        string outputFolder = result.Settings.GetOutputFolder();
        string csvFilePath = Path.Combine(outputFolder, "curves.csv");
        csv.SaveAs(csvFilePath);

        if (saveMetadataToo)
            SaveJson(result);

        return csvFilePath;
    }

    public static string SaveJson(AnalysisResult result)
    {
        AnalysisSettings settings = result.Settings;

        using MemoryStream stream = new();
        JsonWriterOptions options = new() { Indented = true };
        using Utf8JsonWriter writer = new(stream, options);

        writer.WriteStartObject();
        writer.WriteString("version", AnalysisResult.VersionString);
        writer.WriteString("acquisitionDate", settings.Xml.AcquisitionDate.ToString("s"));
        writer.WriteString("analysisDate", DateTime.Now.ToString("s"));
        writer.WriteString("folderPV", settings.Xml.FolderPath);
        writer.WriteNumber("scanLinePeriod", settings.Xml.MsecPerPixel);
        writer.WriteNumber("micronsPerPixel", settings.Xml.MicronsPerPixel);
        writer.WriteNumber("baselinePixel1", settings.Baseline.Min);
        writer.WriteNumber("baselinePixel2", settings.Baseline.Max);
        writer.WriteNumber("structurePixel1", settings.Structure.Min);
        writer.WriteNumber("structurePixel2", settings.Structure.Max);
        writer.WriteNumber("filterPixels", settings.FilterPx);
        writer.WriteNumber("imageFloorPercentile", settings.FloorPercentile);

        writer.WriteEndObject();

        writer.Flush();
        string json = Encoding.UTF8.GetString(stream.ToArray());

        string outputFolder = result.Settings.GetOutputFolder();
        string csvFilePath = Path.Combine(outputFolder, "curves.csv.json");
        File.WriteAllText(csvFilePath, json);

        return csvFilePath;
    }
}
