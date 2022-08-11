using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace ScanAGator.LineScan;

public class LineScanFolder2
{
    public readonly string FolderPath;

    private readonly Prairie.FolderContents FolderContents;

    private readonly Prairie.ParirieXmlFile XmlFile;

    public readonly ImageData[] GreenImages;

    public readonly ImageData[] RedImages;

    public readonly ImageData AverageGreenImage;

    public readonly ImageData AveageRedImage;

    public int FrameCount => GreenImages.Count();
    public int LineScanImageWidth => GreenImages[0].width;
    public int LineScanImageHeight => GreenImages[0].height;

    public LineScanFolder2(string folderPath)
    {
        FolderPath = Path.GetFullPath(folderPath);
        if (!Directory.Exists(FolderPath))
            throw new DirectoryNotFoundException(folderPath);

        FolderContents = new Prairie.FolderContents(FolderPath);
        XmlFile = new Prairie.ParirieXmlFile(FolderContents.XmlFilePath);

        GreenImages = FolderContents.ImageFilesG.Select(x => new ImageData(x)).ToArray();
        RedImages = FolderContents.ImageFilesR.Select(x => new ImageData(x)).ToArray();

        bool IsRatiometric = GreenImages.Length == RedImages.Length;
        if (!IsRatiometric)
            throw new InvalidOperationException("not ratiometric");
    }

    public RatiometricLinescan[] GetRatiometricLinescanFrames(LineScanSettings settings)
    {
        RatiometricLinescan[] linescans = new RatiometricLinescan[FrameCount];

        for (int i = 0; i < FrameCount; i++)
        {
            linescans[i] = new RatiometricLinescan(
                green: GreenImages[i],
                red: RedImages[i],
                msPerPx: XmlFile.MsecPerPixel,
                settings: settings);
        }

        return linescans;
    }

    public RatiometricLinescan GetRatiometricLinescanAverage(LineScanSettings settings)
    {
        return new RatiometricLinescan(
                green: ImageOperations.Average(GreenImages),
                red: ImageOperations.Average(RedImages),
                msPerPx: XmlFile.MsecPerPixel,
                settings: settings);
    }

    public void SaveJsonMetadata(string saveAs, LineScanSettings settings)
    {
        using MemoryStream stream = new();
        JsonWriterOptions options = new() { Indented = true };
        using Utf8JsonWriter writer = new(stream, options);

        writer.WriteStartObject();
        writer.WriteString("version", Versioning.GetVersionString());
        writer.WriteString("acquisitionDate", XmlFile.AcquisitionDate.ToString("s"));
        writer.WriteString("analysisDate", DateTime.Now.ToString("s"));
        writer.WriteString("folderPV", FolderPath);
        writer.WriteNumber("scanLinePeriod", XmlFile.MsecPerPixel);
        writer.WriteNumber("micronsPerPixel", XmlFile.MicronsPerPixel);
        writer.WriteNumber("baselinePixel1", settings.Baseline.FirstPixel);
        writer.WriteNumber("baselinePixel2", settings.Baseline.LastPixel);
        writer.WriteNumber("structurePixel1", settings.Structure.FirstPixel);
        writer.WriteNumber("structurePixel2", settings.Structure.LastPixel);
        writer.WriteNumber("filterPixels", settings.FilterSizePixels);

        writer.WriteEndObject();

        writer.Flush();
        string json = Encoding.UTF8.GetString(stream.ToArray());

        File.WriteAllText(saveAs, json);
    }
}
