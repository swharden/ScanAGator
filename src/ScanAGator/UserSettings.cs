using System.IO;

namespace ScanAGator;

public static class UserSettings
{
    private static readonly string SettingsFilePath = Path.GetFullPath("settings-path.txt");

    public static void SavePath(string? path)
    {
        if (!Directory.Exists(path))
            return;

        path = Path.GetFullPath(path);
        File.WriteAllText(SettingsFilePath, path);
    }

    public static string? LoadPath()
    {
        if (!File.Exists(SettingsFilePath))
            return null;

        string path = File.ReadAllText(SettingsFilePath);
        if (!Directory.Exists(path))
            return null;

        return path;
    }
}
