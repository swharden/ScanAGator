namespace Resampler;

internal static class Parsing
{
    public static double[,]? GetValues(string txt, string sep = "\t")
    {
        if (string.IsNullOrWhiteSpace(txt))
            return null;

        string[] lines = txt.Split("\n");
        int rowCount = lines.Length;
        int colCount = lines.First().Split(sep).Length;
        double[,] values = new double[rowCount, colCount];

        for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            string[] lineValues = lines[rowIndex].Split(sep).ToArray();
            for (int colIndex = 0; colIndex < colCount; colIndex++)
            {
                if (lineValues.Length >= colIndex)
                {
                    values[rowIndex, colIndex] = double.NaN;
                    continue;
                }

                if (string.IsNullOrWhiteSpace(lineValues[colIndex]))
                {
                    values[rowIndex, colIndex] = double.NaN;
                    continue;
                }

                if (!double.TryParse(lineValues[colIndex], out double parsedValue))
                {
                    values[rowIndex, colIndex] = double.NaN;
                    continue;
                }

                values[rowIndex, colIndex] = parsedValue;
            }
        }

        return values;
    }
}
