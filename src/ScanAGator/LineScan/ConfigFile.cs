using System;
using System.Collections.Generic;
using System.Text;

namespace ScanAGator
{
    public static class ConfigFile
    {
        /// <summary>
        /// Load baseline, structure, and filter settings from LineScanSettings.ini in the linescan folder
        /// </summary>
        public static void LoadINI(LineScanFolder ls)
        {
            if (!ls.IsValid)
                return;

            if (!System.IO.File.Exists(ls.IniFilePath))
                return;

            foreach (string rawLine in System.IO.File.ReadAllLines(ls.IniFilePath))
            {
                string line = rawLine.Trim();
                if (line.StartsWith(";"))
                    continue;
                if (!line.Contains("="))
                    continue;
                string[] lineParts = line.Split('=');
                string var = lineParts[0];
                string valStr = lineParts[1];

                if (var == "baseline1")
                    ls.BaselineIndex1 = int.Parse(valStr);
                else if (var == "baseline2")
                    ls.BaselineIndex2 = int.Parse(valStr);
                else if (var == "structure1")
                    ls.StructureIndex1 = int.Parse(valStr);
                else if (var == "structure2")
                    ls.StructureIndex2 = int.Parse(valStr);
                else if (var == "filterPx")
                    ls.FilterSizePixels = int.Parse(valStr);
            }
        }

        /// <summary>
        /// Save baseline, structure, and filter settings to LineScanSettings.ini in the linescan folder
        /// </summary>
        public static void SaveINI(LineScanFolder ls)
        {
            if (!ls.IsValid)
                return;

            if (!System.IO.Directory.Exists(ls.SaveFolderPath))
                System.IO.Directory.CreateDirectory(ls.SaveFolderPath);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("; Scan-A-Gator Linescan Settings");
            sb.AppendLine($"version={Versioning.GetVersionString()}");
            sb.AppendLine($"baseline1={ls.BaselineIndex1}");
            sb.AppendLine($"baseline2={ls.BaselineIndex2}");
            sb.AppendLine($"structure1={ls.StructureIndex1}");
            sb.AppendLine($"structure2={ls.StructureIndex2}");
            sb.AppendLine($"filterPx={ls.FilterSizePixels}");

            System.IO.File.WriteAllText(ls.IniFilePath, sb.Replace("\n", "\r\n").ToString().Trim());
        }

        /// <summary>
        /// Default filter time and baseline duration for unanalyzed linescans is stored in an INI next to this EXE.
        /// </summary>
        public static void LoadDefaultSettings(LineScanFolder ls)
        {
            if (!ls.IsValid)
                return;

            if (!System.IO.File.Exists(ls.ProgramSettingsPath))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("; ScanAGator default settings");
                sb.AppendLine("baselineEndFrac = 0.10");
                sb.AppendLine(";filterTimeMs = 50.0");
                System.IO.File.WriteAllText(ls.ProgramSettingsPath, sb.ToString());
            }

            string raw = System.IO.File.ReadAllText(ls.ProgramSettingsPath);
            string[] lines = raw.Split('\n');
            foreach (string thisLine in lines)
            {
                string line = thisLine.Trim();
                if (line.StartsWith(";") || !line.Contains("="))
                    continue;
                string var = line.Split('=')[0].Trim();
                string val = line.Split('=')[1].Trim();

                if (var == "baselineEndFrac")
                    ls.DefaultBaselineFraction2 = double.Parse(val);

                if (var == "filterTimeMs")
                    ls.DefaultFilterTimeMsec = double.Parse(val);
            }
        }
    }
}
