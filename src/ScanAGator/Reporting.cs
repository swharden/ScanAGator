using System.IO;

namespace ScanAGator;

public static class Reporting
{
    public static void AnalyzeLinescanFolder(LineScan.LineScanFolder2 lsFolder, LineScanSettings settings)
    {
        string analysisFolder = Path.Combine(lsFolder.FolderPath, "ScanAGator");
        if (!Directory.Exists(analysisFolder))
            Directory.CreateDirectory(analysisFolder);

        // TODO: save linescan images with lines drawn on them

        // save CSVs for each individual frame
        RatiometricLinescan[] linescans = lsFolder.GetRatiometricLinescanFrames(settings);
        for (int i = 0; i < lsFolder.FrameCount; i++)
        {
            string csvFilePath = Path.Combine(analysisFolder, $"Frame-{i + 1}.csv");
            linescans[i].SaveCsv(csvFilePath);
            lsFolder.SaveJsonMetadata(csvFilePath + ".json", settings);
        }

        // create plots showing each frame
        Plot.PlotRaw(linescans, Path.GetFileName(lsFolder.FolderPath), Path.Combine(analysisFolder, $"Frames-raw.png"));
        Plot.PlotDGoR(linescans, Path.GetFileName(lsFolder.FolderPath), Path.Combine(analysisFolder, $"Frames-dff.png"));

        if (lsFolder.FrameCount > 1)
        {
            // save average CSV
            RatiometricLinescan averageLinescan = lsFolder.GetRatiometricLinescanAverage(settings);
            string csvFilePath = Path.Combine(analysisFolder, $"Frame-average.csv");
            averageLinescan.SaveCsv(csvFilePath);
            lsFolder.SaveJsonMetadata(csvFilePath + ".json", settings);

            // create plot showing average image analysis
            Plot.PlotDGoRAverage(averageLinescan, Path.GetFileName(lsFolder.FolderPath), Path.Combine(analysisFolder, $"Frames-dff-average.png"));
        }
    }
}
