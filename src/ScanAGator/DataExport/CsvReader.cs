using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanAGator.DataExport;

public class CsvReader
{
    public readonly string FilePath;
    public readonly string LinescanFolderName;
    public readonly double[] Times;
    public readonly double[] AvgGreen;
    public readonly double[] AvgRed;
    public readonly double[] AvgDeltaGreenOverRed;

    public CsvReader(string csvFilePath, int skipLines = 3)
    {
        FilePath = Path.GetFullPath(csvFilePath);
        LinescanFolderName = Path.GetFileName(Path.GetDirectoryName(Path.GetDirectoryName(FilePath)));

        string[] lines = File.ReadAllLines(csvFilePath);
        string[][] cells = new string[lines.Length][];

        for (int i = skipLines; i < lines.Length; i++)
        {
            cells[i] = lines[i].Split(',');
        }

        Times = GetColumnValues(cells, 0);
        AvgGreen = GetColumnValues(cells, 1);
        AvgRed = GetColumnValues(cells, 2);
        AvgDeltaGreenOverRed = GetColumnValues(cells, 3);
    }

    public static double[] GetColumnValues(string[][] cells, int columnIndex)
    {
        double[] values = new double[cells.Length];

        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i] is not null && cells[i].Length > columnIndex)
            {
                if (!double.TryParse(cells[i][columnIndex], out values[i]))
                {
                    values[i] = double.NaN;
                }
            }
        }

        return values.ToArray();
    }
}
