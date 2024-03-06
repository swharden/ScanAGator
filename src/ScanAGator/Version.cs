namespace ScanAGator;

public static class Version
{
    public static int Major = 4; // bump for changes that break the analysis data or save file format

    public static int Minor = 6; // bump for the addition of new features

    public static string VersionString => $"Scan-A-Gator v{Major}.{Minor}";
}