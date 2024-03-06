using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScanAGator.Tests;

static class SampleData
{
    private static string FolderPath
    {
        get
        {
            string folderPath = System.IO.Path.Combine(
                TestContext.CurrentContext.TestDirectory,
                "../../../../../data/linescans/");

            return System.IO.Path.GetFullPath(folderPath);
        }
    }

    public static string MultiFrameRatiometricFolderPath => Path.Combine(FolderPath, "LineScan-08092022-1225-528");
    public static string GreenLinescanImagePath => Path.Combine(FolderPath, "LineScan-08092022-1225-528/LineScan-08092022-1225-528_Cycle00001_Ch2_000001.ome.tif");
    public static string[] LinescanXmlFiles => Directory
        .GetDirectories(FolderPath)
        .SelectMany(x => Directory.GetFiles(x, "*.xml"))
        .Where(x => !x.EndsWith("_MarkPoints.xml"))
        .Where(x => !x.Contains("2014")) // no old verions
        .ToArray();
}
