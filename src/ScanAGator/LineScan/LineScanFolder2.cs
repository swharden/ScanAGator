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
    public int LineScanImageWidth => GreenImages[0].Width;
    public int LineScanImageHeight => GreenImages[0].Height;

    public LineScanFolder2(string folderPath)
    {
        FolderPath = Path.GetFullPath(folderPath);
        if (!Directory.Exists(FolderPath))
            throw new DirectoryNotFoundException(folderPath);

        FolderContents = new Prairie.FolderContents(FolderPath);
        XmlFile = new Prairie.ParirieXmlFile(FolderContents.XmlFilePath);

        GreenImages = FolderContents.ImageFilesG.Select(x => ImageDataTools.ReadTif(x)).ToArray();
        RedImages = FolderContents.ImageFilesR.Select(x => ImageDataTools.ReadTif(x)).ToArray();

        bool IsRatiometric = GreenImages.Length == RedImages.Length;
        if (!IsRatiometric)
            throw new InvalidOperationException("not ratiometric");
    }

    public RatiometricLinescan GetRatiometricLinescanFrame(int frame, LineScanSettings settings)
    {
        return new RatiometricLinescan(
            green: GreenImages[frame],
            red: RedImages[frame],
            msPerPx: XmlFile.MsecPerPixel,
            settings: settings);
    }

    public RatiometricLinescan[] GetRatiometricLinescanFrames(LineScanSettings settings)
    {
        return Enumerable.Range(0, FrameCount)
            .Select(x => GetRatiometricLinescanFrame(x, settings))
            .ToArray();
    }

    /// <summary>
    /// This function creates average images across all frames, then measures G and R from those images to calculate dG/R.
    /// This method is not recommended because movement results in a sub-ideal average image.
    /// Instead, calculate independent dG/R curves, then average those.
    /// </summary>
    public RatiometricLinescan GetRatiometricLinescanFromAverageImage(LineScanSettings settings)
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
