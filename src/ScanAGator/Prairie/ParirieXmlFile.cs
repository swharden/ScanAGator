using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanAGator.Prairie;

/// <summary>
/// This class encapsulates logic required to read Prairie XML files.
/// We don't need to read many items so raw string manipulation was chosen here. 
/// This is important because XML trees aren't stable across PrairieView versions.
/// </summary>
public class ParirieXmlFile
{
    public readonly string FilePath;
    public readonly DateTime AcquisitionDate;
    public readonly double MsecPerPixel;
    public readonly double MicronsPerPixel;

    public string FolderPath => System.IO.Path.GetDirectoryName(FilePath);

    public ParirieXmlFile(string xmlFilePath)
    {
        FilePath = System.IO.Path.GetFullPath(xmlFilePath);

        string[] xmlLines = System.IO.File.ReadAllLines(xmlFilePath);

        AcquisitionDate = ReadAcquisitionDate(xmlLines);
        MsecPerPixel = ReadMsecPerPixel(xmlLines);
        MicronsPerPixel = ReadMicronsPerPixel(xmlLines);
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
                string value = xmlLines[i + 1];
                string[] values = value.Split('"');
                return double.Parse(values[3]);
            }
        }

        throw new InvalidOperationException($"could not locate microns per pixel");
    }
}
