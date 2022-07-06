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
            if (!ls.isValid)
                return;

            if (!System.IO.File.Exists(ls.pathIniFile))
                return;

            foreach (string rawLine in System.IO.File.ReadAllLines(ls.pathIniFile))
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
                    ls.baseline1 = int.Parse(valStr);
                else if (var == "baseline2")
                    ls.baseline2 = int.Parse(valStr);
                else if (var == "structure1")
                    ls.structure1 = int.Parse(valStr);
                else if (var == "structure2")
                    ls.structure2 = int.Parse(valStr);
                else if (var == "filterPx")
                    ls.filterPx = int.Parse(valStr);
            }
        }

        /// <summary>
        /// Save baseline, structure, and filter settings to LineScanSettings.ini in the linescan folder
        /// </summary>
        public static void SaveINI(LineScanFolder ls)
        {
            if (!ls.isValid)
                return;

            if (!System.IO.Directory.Exists(ls.pathSaveFolder))
                System.IO.Directory.CreateDirectory(ls.pathSaveFolder);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("; Scan-A-Gator Linescan Settings");
            sb.AppendLine($"version={ls.version}");
            sb.AppendLine($"baseline1={ls.baseline1}");
            sb.AppendLine($"baseline2={ls.baseline2}");
            sb.AppendLine($"structure1={ls.structure1}");
            sb.AppendLine($"structure2={ls.structure2}");
            sb.AppendLine($"filterPx={ls.filterPx}");

            System.IO.File.WriteAllText(ls.pathIniFile, sb.Replace("\n", "\r\n").ToString().Trim());
        }

        /// <summary>
        /// Default filter time and baseline duration for unanalyzed linescans is stored in an INI next to this EXE.
        /// </summary>
        public static void LoadDefaultSettings(LineScanFolder ls)
        {
            if (!ls.isValid)
                return;

            if (!System.IO.File.Exists(ls.pathProgramSettings))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("; ScanAGator default settings");
                sb.AppendLine("baselineEndFrac = 0.10");
                sb.AppendLine(";filterTimeMs = 50.0");
                System.IO.File.WriteAllText(ls.pathProgramSettings, sb.ToString());
            }

            string raw = System.IO.File.ReadAllText(ls.pathProgramSettings);
            string[] lines = raw.Split('\n');
            foreach (string thisLine in lines)
            {
                string line = thisLine.Trim();
                if (line.StartsWith(";") || !line.Contains("="))
                    continue;
                string var = line.Split('=')[0].Trim();
                string val = line.Split('=')[1].Trim();

                if (var == "baselineEndFrac")
                    ls.defaultBaselineEndFrac = double.Parse(val);

                if (var == "filterTimeMs")
                    ls.defaultFilterTimeMs = double.Parse(val);
            }
        }
    }
}
