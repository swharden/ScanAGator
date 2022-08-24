namespace ScanAGator.CSV;

public struct Column
{
    public readonly string Title;
    public readonly string Units;
    public readonly string Comments;
    public readonly double[] Data;

    public Column(string title, string units, string comments, double[] data)
    {
        Title = title;
        Units = units;
        Comments = comments;
        Data = data;
    }
}
