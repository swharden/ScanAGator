﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace ScanAGator.Prairie;

/// <summary>
/// This class encapsulates logic required to read Prairie XML files.
/// We don't need to read many items so raw string manipulation was chosen here. 
/// This is important because XML trees aren't stable across PrairieView versions.
/// </summary>
public class ParirieXmlFile
{
    public readonly string FilePath;
    public string Filename => Path.GetFileName(FilePath);
    public readonly DateTime AcquisitionDate;
    public readonly double MsecPerPixel;
    public readonly double MicronsPerPixel;
    public readonly Vector3 Position;
    public readonly LinescanMode Mode;
    public readonly Vector2[] Points;

    public string FolderPath => Path.GetDirectoryName(FilePath);

    public ParirieXmlFile(string xmlFilePath)
    {
        FilePath = Path.GetFullPath(xmlFilePath);
        if (!File.Exists(FilePath))
            throw new FileNotFoundException(FilePath);

        string[] xmlLines = File.ReadAllLines(xmlFilePath);

        AcquisitionDate = ReadAcquisitionDate(xmlLines);
        MsecPerPixel = ReadMsecPerPixel(xmlLines);
        MicronsPerPixel = ReadMicronsPerPixel(xmlLines);
        Position = ReadPosition(xmlLines);
        (Mode, Points) = ReadLinescanType(xmlLines);
    }

    private static DateTime ReadAcquisitionDate(string[] xmlLines)
    {
        if (xmlLines[1].Contains("date="))
        {
            string dateString = xmlLines[1].Split('\"')[3];
            return DateTime.Parse(dateString);
        }

        throw new InvalidOperationException($"could not read acquisition date from XML file");
    }

    private static double ReadMsecPerPixel(string[] xmlLines)
    {
        // WARNING: XML files can have multiple scan line periods. Take the LAST one.
        double msPerPx = double.NaN;

        foreach (string line in xmlLines)
        {
            bool isPeriodLine = line.Contains("scanLinePeriod") || line.Contains("scanlinePeriod");
            bool lineHasValue = line.Contains("value=");
            if (isPeriodLine && lineHasValue)
            {
                string split1 = "value=\"";
                string split2 = "\"";
                string valStr = line.Substring(line.IndexOf(split1) + split1.Length);
                valStr = valStr.Substring(0, valStr.IndexOf(split2));
                msPerPx = double.Parse(valStr) * 1000;
            }
        }

        if (double.IsNaN(msPerPx))
            throw new InvalidOperationException($"milliseconds per pixel could not be read from XML file");

        return msPerPx;
    }

    private static double ReadMicronsPerPixel(string[] xmlLines)
    {
        for (int i = 0; i < xmlLines.Length - 1; i++)
        {
            string line = xmlLines[i];
            if (line.Contains("micronsPerPixel_XAxis"))
            {
                throw new InvalidOperationException($"old (unsupported) XML file version");
            }

            if (line.Contains("micronsPerPixel"))
            {
                return ReadDoubleValue(xmlLines[i + 1]);
            }
        }

        throw new InvalidOperationException($"could not locate microns per pixel");
    }

    private static Vector3 ReadPosition(string[] xmlLines)
    {
        double x = double.NaN;
        double y = double.NaN;
        double z = double.NaN;

        for (int i = 0; i < xmlLines.Length - 1; i++)
        {
            if (xmlLines[i].Contains("<SubindexedValues index=\"XAxis\">"))
            {
                x = ReadDoubleValue(xmlLines[i + 1]);
            }
            else if (xmlLines[i].Contains("<SubindexedValues index=\"YAxis\">"))
            {
                y = ReadDoubleValue(xmlLines[i + 1]);
            }
            else if (xmlLines[i].Contains("<SubindexedValues index=\"ZAxis\">"))
            {
                z = ReadDoubleValue(xmlLines[i + 1]);
            }
        }

        return new Vector3((float)x, (float)y, (float)z);
    }

    private static double ReadDoubleValue(string line) => double.Parse(line.Split('"')[3]);

    public static (LinescanMode, Vector2[] points) ReadLinescanType(string[] xmlLines)
    {
        foreach (string line in xmlLines)
        {
            if (line.Contains("mode=\"straightLine\""))
                return (LinescanMode.StraightLine, ReadStraightLinePoints(xmlLines));

            if (line.Contains("mode=\"freeHand\""))
                return (LinescanMode.FreeHand, ReadFreehandPoints(xmlLines));
        }

        throw new InvalidOperationException("unable to determine mode");
    }

    public static Vector2[] ReadStraightLinePoints(string[] xmlLines)
    {
        foreach (string line in xmlLines.Where(x => x.Contains("<PVLine startPixelY")))
        {
            string[] parts = line.Trim().Split('"');
            float y = float.Parse(parts[1]);
            float x1 = float.Parse(parts[3]);
            float x2 = float.Parse(parts[5]);
            return [new Vector2(x1, y), new Vector2(x2, y)];
        }

        throw new InvalidOperationException("unable to read line positions");
    }

    public static Vector2[] ReadFreehandPoints(string[] xmlLines)
    {
        List<Vector2> points = [];

        foreach (string line in xmlLines.Where(x => x.Contains("PVFreehand")))
        {
            string[] parts = line.Trim().Split('"');
            float x = float.Parse(parts[1]);
            float y = float.Parse(parts[3]);
            points.Add(new Vector2(x, y));
        }

        if (points.Count == 0)
            throw new InvalidOperationException("unable to read line positions");

        return [.. points];
    }
}