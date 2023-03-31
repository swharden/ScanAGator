using System.Text.Json;
using System.Text;

namespace ImageRatioTool;

public class SquareRoiCollection
{
    public string Source { get; }
    public int Width { get; }
    public int Height { get; }
    public int Count => Points.Count;

    public int Radius { get; } = 5;

    public List<FractionalPoint> Points { get; } = new();

    public SquareRoiCollection(string source, int width, int height)
    {
        Source = source;
        Width = width;
        Height = height;
    }

    public void Save(string csvFile, List<double[]> afusByFrame, double[] frameTimes)
    {
        string saveAsCSV = Path.GetFullPath(csvFile);
        File.WriteAllText(saveAsCSV, GetCSV(afusByFrame, frameTimes));
        Console.WriteLine(saveAsCSV);

        string saveAsJSON = saveAsCSV + ".json";
        File.WriteAllText(saveAsJSON, GetJson());
        Console.WriteLine(saveAsJSON);
    }

    public string GetCSV(List<double[]> afusByFrame, double[] frameTimes)
    {
        if (frameTimes.Length != afusByFrame.Count)
            throw new ArgumentException("length of times must equal the number of AFU curves");

        int frameCount = afusByFrame.Count;
        int roiCount = afusByFrame.First().Length;

        StringBuilder sb = new();

        List<string> columnNames = new() { "Time" };
        for (int i = 0; i < frameCount; i++)
        {
            columnNames.Add($"ROI {i + 1}");
        }
        sb.AppendLine(string.Join(", ", columnNames));

        for (int frameIndex = 0; frameIndex < frameCount; frameIndex++)
        {
            string[] values = afusByFrame[frameIndex].Select(x => x.ToString()).ToArray();
            string line = frameTimes[frameIndex].ToString() + ", " + string.Join(", ", values);
            sb.AppendLine(line);
        }

        return sb.ToString();
    }

    public string GetJson()
    {
        using MemoryStream stream = new();
        JsonWriterOptions options = new() { Indented = true };
        using Utf8JsonWriter writer = new(stream, options);

        writer.WriteStartObject();
        writer.WriteString("type", "Square ROI Collection");
        writer.WriteString("DateTime", DateTime.Now);
        writer.WriteString(nameof(Source), Source);
        writer.WriteNumber(nameof(Width), Width);
        writer.WriteNumber(nameof(Height), Height);
        writer.WriteStartArray("ROIs");
        foreach (FractionalPoint point in Points)
        {
            writer.WriteStartObject();
            writer.WriteNumber("X", point.X);
            writer.WriteNumber("Y", point.Y);
            writer.WriteNumber("R", Radius);
            writer.WriteEndObject();
        }
        writer.WriteEndArray();
        writer.WriteEndObject();

        writer.Flush();
        string json = Encoding.UTF8.GetString(stream.ToArray());

        return json;
    }
}
