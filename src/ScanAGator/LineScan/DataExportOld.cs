using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace ScanAGator
{
    public static class DataExportOld
    {
        public static string GetCSV(LineScanFolder ls)
        {
            // name, unit, comment, data...
            int dataPoints = ls.ImgG.height;
            string[] csvLines = new string[dataPoints + 3];

            // times (ms)
            csvLines[0] = "Time, ";
            csvLines[1] = "ms, ";
            csvLines[2] = ls.FolderName + ", ";
            for (int i = 0; i < dataPoints; i++)
                csvLines[i + 3] = Math.Round(ls.timesMsec[i], 3).ToString() + ", ";

            // raw PMT values (R)
            if (ls.CurveR != null)
            {
                csvLines[0] += "R, ";
                csvLines[1] += "AFU, ";
                csvLines[2] += ", ";
                for (int i = 0; i < dataPoints; i++)
                    csvLines[i + 3] += Math.Round(ls.CurveR[i], 3).ToString() + ", ";
            }

            // raw PMT values (G)
            if (ls.CurveG != null)
            {
                csvLines[0] += "G, ";
                csvLines[1] += "AFU, ";
                csvLines[2] += ", ";
                for (int i = 0; i < dataPoints; i++)
                    csvLines[i + 3] += Math.Round(ls.CurveG[i], 3).ToString() + ", ";
            }

            // delta raw PMT values (G)
            if (ls.CurveDeltaG != null)
            {
                csvLines[0] += "dG, ";
                csvLines[1] += "AFU, ";
                csvLines[2] += ", ";
                for (int i = 0; i < dataPoints; i++)
                    csvLines[i + 3] += Math.Round(ls.CurveDeltaG[i], 3).ToString() + ", ";

                csvLines[0] += "f(dG), ";
                csvLines[1] += "AFU, ";
                csvLines[2] += "filtered, ";
                double[] filteredChopped = ls.GetFilteredYs(ls.CurveDeltaG);
                double[] filtered = new double[dataPoints];
                for (int i = 0; i < dataPoints; i++)
                    filtered[i] = 0;
                Array.Copy(filteredChopped, 0, filtered, ls.FilterSizePixels * 2, filteredChopped.Length);
                for (int i = 0; i < dataPoints; i++)
                    if (i < ls.FilterSizePixels * 2 || i > (dataPoints - ls.FilterSizePixels * 2 * 2))
                        csvLines[i + 3] += ", ";
                    else
                        csvLines[i + 3] += Math.Round(filtered[i], 3).ToString() + ", ";
            }

            // Green over Red
            if (ls.CurveGoR != null)
            {
                csvLines[0] += "G/R, ";
                csvLines[1] += "%, ";
                csvLines[2] += ", ";
                for (int i = 0; i < dataPoints; i++)
                    csvLines[i + 3] += Math.Round(ls.CurveGoR[i], 3).ToString() + ", ";
            }

            // Delta Green over Red
            if (ls.CurveDeltaGoR != null)
            {
                csvLines[0] += "dG/R, ";
                csvLines[1] += "%, ";
                csvLines[2] += ", ";
                for (int i = 0; i < dataPoints; i++)
                    csvLines[i + 3] += Math.Round(ls.CurveDeltaGoR[i], 3).ToString() + ", ";

                csvLines[0] += "f(dG/R), ";
                csvLines[1] += "AFU, ";
                csvLines[2] += "filtered, ";
                double[] filteredChopped = ls.GetFilteredYs(ls.CurveDeltaGoR);
                double[] filtered = new double[dataPoints];
                for (int i = 0; i < dataPoints; i++)
                    filtered[i] = 0;
                Array.Copy(filteredChopped, 0, filtered, ls.FilterSizePixels * 2, filteredChopped.Length);
                for (int i = 0; i < dataPoints; i++)
                    if (i < ls.FilterSizePixels * 2 || i > (dataPoints - ls.FilterSizePixels * 2 * 2))
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
            writer.WriteString("version", Versioning.GetVersionString());
            writer.WriteString("acquisitionDate", ls.AcquisitionDate.ToString("s"));
            writer.WriteString("analysisDate", DateTime.Now.ToString("s"));
            writer.WriteString("folderPV", ls.FolderPath);
            writer.WriteString("folderSAG", ls.SaveFolderPath);
            writer.WriteNumber("scanLinePeriod", ls.ScanLinePeriodMsec);
            writer.WriteNumber("micronsPerPixel", ls.MicronsPerPixel);
            writer.WriteNumber("baselinePixel1", ls.BaselineIndex1);
            writer.WriteNumber("baselinePixel2", ls.BaselineIndex2);
            writer.WriteNumber("structurePixel1", ls.StructureIndex1);
            writer.WriteNumber("structurePixel2", ls.StructureIndex2);
            writer.WriteNumber("filterPixels", ls.FilterSizePixels);

            writer.WriteEndObject();

            writer.Flush();
            string json = Encoding.UTF8.GetString(stream.ToArray());

            return json;
        }
    }
}
