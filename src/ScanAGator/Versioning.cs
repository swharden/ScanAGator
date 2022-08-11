using System;

namespace ScanAGator;

public static class Versioning
{
    public static string GetVersionString()
    {
        Version v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        return $"Scan-A-Gator v{v.Major}.{v.Minor}";
    }
}
