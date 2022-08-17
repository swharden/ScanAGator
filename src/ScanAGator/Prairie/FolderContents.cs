using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScanAGator.Prairie;

/// <summary>
/// This class encapsulates logic related to scanning a PrairieView folder for useful content
/// </summary>
public class FolderContents
{
    public readonly string FolderPath;
    public string ReferenceFolderPath => Path.Combine(FolderPath, "References");
    public readonly string[] ReferenceTifPaths;
    public readonly string[] ImageFilesG;
    public readonly string[] ImageFilesR;
    public readonly string XmlFilePath;
    public int Frames => ImageFilesG.Length;

    public FolderContents(string folderPath)
    {
        FolderPath = Path.GetFullPath(folderPath);

        if (!Directory.Exists(ReferenceFolderPath))
            throw new DirectoryNotFoundException(ReferenceFolderPath);

        // scan for reference images
        ReferenceTifPaths = Directory.GetFiles(ReferenceFolderPath, "*.tif");
        if (!ReferenceTifPaths.Any())
            throw new InvalidOperationException("References sub-folder contains no TIF images");

        // scan for data image files
        string[] pathsTif = Directory.GetFiles(FolderPath, "LineScan*.tif");
        Array.Sort(pathsTif);
        List<string> dataImagesR = new();
        List<string> dataImagesG = new();
        foreach (string filePath in pathsTif)
        {
            string fileName = System.IO.Path.GetFileName(filePath);
            if (fileName.Contains("Source.tif"))
                continue;
            if (fileName.Contains("Ch1"))
                dataImagesR.Add(filePath);
            if (fileName.Contains("Ch2"))
                dataImagesG.Add(filePath);
        }
        ImageFilesG = dataImagesG.ToArray();
        ImageFilesR = dataImagesR.ToArray();

        bool isRatiometric = ImageFilesG.Any() && ImageFilesR.Any();
        if (isRatiometric && (ImageFilesR.Length != ImageFilesG.Length))
            throw new InvalidOperationException("A different number of red and green images were found");

        // scan for the XML configuration file
        string[] pathsXml = System.IO.Directory.GetFiles(FolderPath, "LineScan*.xml");
        if (!pathsXml.Any())
            throw new InvalidOperationException("Linescan XML file could not be found");
        XmlFilePath = pathsXml.First();
    }
}
