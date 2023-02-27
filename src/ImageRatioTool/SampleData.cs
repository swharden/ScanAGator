namespace ImageRatioTool;

public static class SampleData
{
    public static string Folder = GetSampleDataFolder();

    public static string RedImage => Path.Combine(Folder, "C1-TSeries-2062-4D-1.tif");

    public static string GreenImage => Path.Combine(Folder, "C2-TSeries-2062-4D-1.tif");

    private static string GetSampleDataFolder()
    {
        string path = Path.Join(
            path1: Application.StartupPath,
            path2: "../../../../../data/single");

        path = Path.GetFullPath(path);

        if (!Directory.Exists(path))
            throw new DirectoryNotFoundException(path);

        return path;
    }
}
