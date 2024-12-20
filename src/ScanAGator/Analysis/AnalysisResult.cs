﻿using ScanAGator.DataExport;
using ScanAGator.Imaging;

namespace ScanAGator.Analysis;

public class AnalysisResult
{
    public readonly AnalysisSettings Settings;
    public readonly IntensityCurve GreenCurve;
    public readonly IntensityCurve RedCurve;
    public readonly IntensityCurve SmoothGreenCurve;
    public readonly IntensityCurve SmoothRedCurve;
    public readonly IntensityCurve SmoothDeltaGreenCurve;
    public readonly IntensityCurve SmoothDeltaGreenOverRedCurve;

    /// <summary>
    /// This class analyzes a linescan image according to the given settings.
    /// The constructor contains the core linescan analysis routine for ScanAGator.
    /// </summary>
    public AnalysisResult(RatiometricImage img, AnalysisSettings settings)
    {
        Settings = settings;

        // get the intensity curves for the structure of interest
        GreenCurve = new IntensityCurve(img.GreenData, settings.Xml.MsecPerPixel, settings.Structure);
        RedCurve = new IntensityCurve(img.RedData, settings.Xml.MsecPerPixel, settings.Structure);

        // calculate smooth curves
        SmoothGreenCurve = GreenCurve.LowPassFiltered(settings.FilterPx);
        SmoothRedCurve = RedCurve.LowPassFiltered(settings.FilterPx);

        // calculate ratios from smoothed curves
        double greenCurveBaseline = GreenCurve.GetMean(settings.Baseline);
        SmoothDeltaGreenCurve = SmoothGreenCurve - greenCurveBaseline;
        SmoothDeltaGreenOverRedCurve = SmoothDeltaGreenCurve / SmoothRedCurve * 100;
    }

    /// <summary>
    /// Save these results as a CSV file (containing curves) and JSON file (containing scan settings)
    /// </summary>
    /// <returns>Path to the CSV file created</returns>
    public string Save()
    {
        string analysisFilePath = AnalysisReport.Generate(this);
        ScottPlot.Tools.LaunchBrowser(analysisFilePath);

        string csvFilePath = AnalysisResultFile.SaveCsv(this);
        return csvFilePath;
    }
}
