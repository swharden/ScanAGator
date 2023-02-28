namespace ImageRatioTool;

public static class SampleData
{
    public static string RatiometricImage => GetSampleDataFile("2ch-baseline.tif");

    public static string GetSampleDataFile(string filename)
    {
        string localFolder = Path.GetFullPath("./");
        string localPath = Path.Combine(localFolder, filename);
        if (File.Exists(localPath))
            return localPath;

        string sampleDataFolder = Path.Join(
            path1: Application.StartupPath,
            path2: "../../../../../data/single");
        string sampleDataFolderPath = Path.Combine(sampleDataFolder, filename);
        if (File.Exists(sampleDataFolderPath))
            return sampleDataFolderPath;

        string networkFolder = Path.GetFullPath("X:\\zTemp\\2p sample data");
        string networkFolderPath = Path.Combine(networkFolder, filename);
        if (File.Exists(networkFolderPath))
            return networkFolderPath;

        throw new InvalidOperationException("sample data file not found");
    }
}
