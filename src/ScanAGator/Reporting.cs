using System;
using System.IO;
using System.Linq;

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

        if (linescans.Length > 1)
        {
            SaveAverageCurve(linescans, lsFolder, settings);
        }
    }

    private static void SaveAverageCurve(RatiometricLinescan[] linescans, LineScan.LineScanFolder2 lsFolder, LineScanSettings settings)
    {
        double sampleRate = 1000.0 / linescans.First().DGR.MsPerPixel;
        int filterSizePx = linescans.First().FilterSizePixels;

        // save average CSV
        (double[] xs, double[] avg, double[] err) = AverageDeltaGreenOverRed(linescans);

        DataExport.CsvBuilder csv = new();
        csv.Add("Time", "sec", "sec", xs);
        for (int i = 0; i < linescans.Length; i++)
        {
            csv.Add("ΔG/R", "%", $"Frame {i + 1}", linescans[i].DGR.Values);
        }

        string analysisFolder = Path.Combine(lsFolder.FolderPath, "ScanAGator");
        string csvFilePath = Path.Combine(analysisFolder, $"Frame-average.csv");
        Console.WriteLine(csvFilePath);
        csv.SaveAs(csvFilePath);
        lsFolder.SaveJsonMetadata(csvFilePath + ".json", settings);

        // create plot showing average image analysis
        Plot.PlotDGoRAverage(avg, err, sampleRate, filterSizePx, Path.GetFileName(lsFolder.FolderPath), Path.Combine(analysisFolder, $"Frames-dff-average.png"));
    }

    private static (double[] xs, double[] avg, double[] err) AverageDeltaGreenOverRed(RatiometricLinescan[] linescans, bool stdErr = false)
    {
        int pointCount = linescans.First().DGR.Values.Length;

        double[] avgCurve = new double[pointCount];
        double[] errCurve = new double[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            double[] sequence = new double[linescans.Length];
            for (int j = 0; j < sequence.Length; j++)
                sequence[j] = linescans[j].DGR.Values[i];

            double avg = sequence.Average();
            double sumVarianceSquared = sequence.Sum(d => Math.Pow(d - avg, 2));
            double stDev = Math.Sqrt(sumVarianceSquared / sequence.Length);

            avgCurve[i] = avg;
            errCurve[i] = stdErr ? Math.Sqrt(linescans.Length) : stDev;
        }

        double samplePeriod = linescans.First().G.MsPerPixel;
        double[] xs = Enumerable.Range(0, pointCount).Select(x => x * samplePeriod).ToArray();

        xs = TrimUnfilteredEdges(xs, linescans.First().FilterSizePixels);
        avgCurve = TrimUnfilteredEdges(avgCurve, linescans.First().FilterSizePixels);
        errCurve = TrimUnfilteredEdges(errCurve, linescans.First().FilterSizePixels);

        return (xs, avgCurve, errCurve);
    }

    private static double[] TrimUnfilteredEdges(double[] values, int filterSizePx)
    {
        int subIndex1 = filterSizePx * 2 + 1;
        int subIndex2 = values.Length - 1 - subIndex1;
        int subLength = subIndex2 - subIndex1;

        double[] trimmed = new double[subLength];
        Array.Copy(values, subIndex1, trimmed, 0, subLength);
        return trimmed;
    }
}
