using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanAGator.CSV;

/// <summary>
/// This class generates CSV files that can be read by OriginLab
/// </summary>
public class CsvBuilder
{
    private readonly List<Column> Columns = new();

    public CsvBuilder()
    {
    }

    public void Add(string title, string units, string comments, double[] data)
    {
        Columns.Add(new Column(title, units, comments, data));
    }

    public void SaveAs(string filePath)
    {
        StringBuilder sb = new();

        sb.AppendLine(string.Join(", ", Columns.Select(x => x.Title)));
        sb.AppendLine(string.Join(", ", Columns.Select(x => x.Units)));
        sb.AppendLine(string.Join(", ", Columns.Select(x => x.Comments)));

        int maxDataLength = Columns.Select(x => x.Data.Length).Max();
        for (int i = 0; i < maxDataLength; i++)
        {
            for (int j = 0; j < Columns.Count; j++)
            {
                if (i < Columns[j].Data.Length)
                {
                    sb.Append(Columns[j].Data[i].ToString());
                }

                if (j < Columns.Count - 1)
                {
                    sb.Append(", ");
                }
                else
                {
                    sb.AppendLine();
                }
            }
        }

        System.IO.File.WriteAllText(filePath, sb.ToString());
    }
}
