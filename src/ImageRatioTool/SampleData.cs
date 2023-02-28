namespace ImageRatioTool;

public static class SampleData
{
    public static string RedImage => GetSampleDataFile("C1-TSeries-2062-4D-1.tif");

    public static string GreenImage => GetSampleDataFile("C2-TSeries-2062-4D-1.tif");

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
