using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace ScanAGator.DataExport;

public static class Metadata
{
    public static void SaveJsonMetadata(string saveAs, Analysis.AnalysisSettings settings)
    {
        using MemoryStream stream = new();
        JsonWriterOptions options = new() { Indented = true };
        using Utf8JsonWriter writer = new(stream, options);

        writer.WriteStartObject();
        writer.WriteString("version", Analysis.AnalysisResult.VersionString);
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

        writer.WriteEndObject();

        writer.Flush();
        string json = Encoding.UTF8.GetString(stream.ToArray());

        File.WriteAllText(saveAs, json);
    }
}
