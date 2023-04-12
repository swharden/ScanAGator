namespace Resampler;

internal static class Parsing
{
    public static (double[] xs, double[] ys)? GetXY(string txt, string sep = "\t")
    {
        if (string.IsNullOrWhiteSpace(txt))
            return null;

        List<double> xs = new();
        List<double> ys = new();

        foreach (string line in txt.Split("\n"))
        {
            string[] parts = line.Split(sep);

            if (parts.Length != 2)
                continue;

            if (!double.TryParse(parts[0], out double x))
                continue;

            if (!double.TryParse(parts[1], out double y))
                continue;

            xs.Add(x);
            ys.Add(y);
        }

        if (xs.Count() < 3)
            return null;

        if (xs.Count() != ys.Count())
            return null;

        return (xs.ToArray(), ys.ToArray());
    }
}
