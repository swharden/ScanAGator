using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace ScanAGator
{
    public static class DataExport
    {
        public static string GetCSV(LineScanFolder ls)
        {
            // name, unit, comment, data...
            int dataPoints = ls.imgG.height;
            string[] csvLines = new string[dataPoints + 3];

            // times (ms)
            csvLines[0] = "Time, ";
            csvLines[1] = "ms, ";
            csvLines[2] = ls.folderName + ", ";
            for (int i = 0; i < dataPoints; i++)
                csvLines[i + 3] = Math.Round(ls.timesMsec[i], 3).ToString() + ", ";

            // raw PMT values (R)
            if (ls.curveR != null)
            {
                csvLines[0] += "R, ";
                csvLines[1] += "AFU, ";
                csvLines[2] += ", ";
                for (int i = 0; i < dataPoints; i++)
                    csvLines[i + 3] += Math.Round(ls.curveR[i], 3).ToString() + ", ";
            }

            // raw PMT values (G)
            if (ls.curveG != null)
            {
                csvLines[0] += "G, ";
                csvLines[1] += "AFU, ";
                csvLines[2] += ", ";
                for (int i = 0; i < dataPoints; i++)
                    csvLines[i + 3] += Math.Round(ls.curveG[i], 3).ToString() + ", ";
            }

            // delta raw PMT values (G)
            if (ls.curveDeltaG != null)
            {
                csvLines[0] += "dG, ";
                csvLines[1] += "AFU, ";
                csvLines[2] += ", ";
                for (int i = 0; i < dataPoints; i++)
                    csvLines[i + 3] += Math.Round(ls.curveDeltaG[i], 3).ToString() + ", ";

                csvLines[0] += "f(dG), ";
                csvLines[1] += "AFU, ";
                csvLines[2] += "filtered, ";
                double[] filteredChopped = ls.GetFilteredYs(ls.curveDeltaG);
                double[] filtered = new double[dataPoints];
                for (int i = 0; i < dataPoints; i++)
                    filtered[i] = 0;
                Array.Copy(filteredChopped, 0, filtered, ls.filterPx * 2, filteredChopped.Length);
                for (int i = 0; i < dataPoints; i++)
                    if (i < ls.filterPx * 2 || i > (dataPoints - ls.filterPx * 2 * 2))
                        csvLines[i + 3] += ", ";
                    else
                        csvLines[i + 3] += Math.Round(filtered[i], 3).ToString() + ", ";
            }

            // Green over Red
            if (ls.curveGoR != null)
            {
                csvLines[0] += "G/R, ";
                csvLines[1] += "%, ";
                csvLines[2] += ", ";
                for (int i = 0; i < dataPoints; i++)
                    csvLines[i + 3] += Math.Round(ls.curveGoR[i], 3).ToString() + ", ";
            }

            // Delta Green over Red
            if (ls.curveDeltaGoR != null)
            {
                csvLines[0] += "dG/R, ";
                csvLines[1] += "%, ";
                csvLines[2] += ", ";
                for (int i = 0; i < dataPoints; i++)
                    csvLines[i + 3] += Math.Round(ls.curveDeltaGoR[i], 3).ToString() + ", ";

                csvLines[0] += "f(dG/R), ";
                csvLines[1] += "AFU, ";
                csvLines[2] += "filtered, ";
                double[] filteredChopped = ls.GetFilteredYs(ls.curveDeltaGoR);
                double[] filtered = new double[dataPoints];
                for (int i = 0; i < dataPoints; i++)
                    filtered[i] = 0;
                Array.Copy(filteredChopped, 0, filtered, ls.filterPx * 2, filteredChopped.Length);
                for (int i = 0; i < dataPoints; i++)
                    if (i < ls.filterPx * 2 || i > (dataPoints - ls.filterPx * 2 * 2))
                        csvLines[i + 3] += ", ";
                    else
                        csvLines[i + 3] += Math.Round(filtered[i], 3).ToString() + ", ";
            }

            // convert to CSV
            string csv = "";
            foreach (string line in csvLines)
                csv += line + "\n";
            return csv;
        }

        public static string GetMetadataJson(LineScanFolder ls)
        {
            using MemoryStream stream = new();
            JsonWriterOptions options = new() { Indented = true };
            using Utf8JsonWriter writer = new(stream, options);

            writer.WriteStartObject();
            writer.WriteString("version", ls.version);
            writer.WriteString("acquisitionDate", ls.acquisitionDate.ToString("s"));
            writer.WriteString("analysisDate", DateTime.Now.ToString("s"));
            writer.WriteString("folderPV", ls.pathFolder);
            writer.WriteString("folderSAG", ls.pathSaveFolder);
            writer.WriteNumber("scanLinePeriod", ls.scanLinePeriod);
            writer.WriteNumber("micronsPerPixel", ls.micronsPerPx);
            writer.WriteNumber("baselinePixel1", ls.baseline1);
            writer.WriteNumber("baselinePixel2", ls.baseline2);
            writer.WriteNumber("structurePixel1", ls.structure1);
            writer.WriteNumber("structurePixel2", ls.structure2);
            writer.WriteNumber("filterPixels", ls.filterPx);

            writer.WriteEndObject();

            writer.Flush();
            string json = Encoding.UTF8.GetString(stream.ToArray());

            return json;
        }
    }
}
