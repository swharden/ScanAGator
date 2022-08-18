using System.IO;
using System;

namespace ScanAGator.Tests;

internal static class TestTools
{
    public static void SaveFig(ScottPlot.Plot plt, string fileName)
    {
        string outputFolder = Path.GetFullPath("test_output");
        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);
        string saveAs = Path.Combine(outputFolder, fileName);
        Console.WriteLine(saveAs);
        plt.SaveFig(saveAs);
    }
}
