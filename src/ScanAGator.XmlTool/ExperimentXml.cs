using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace ScanAGator.XmlTool
{
    public class ExperimentXml
    {
        public readonly string XmlFilePath;
        public readonly string XmlString;
        public int FrameCount { get; private set; } = 0;
        public bool IsValid { get => FrameCount > 0; }
        public int[] Indexes { get; private set; }
        public double[] TimesSec { get; private set; }
        public double[] TimesMin { get; private set; }
        public string[] FileNames { get; private set; }
        public double DwellTime { get; private set; } = 0;
        public int LinesPerFrame { get; private set; } = 0;
        public int PixelsPerLine { get; private set; } = 0;
        public double MicronsPerPixel { get; private set; } = 0;
        public double OpticalZoom { get; private set; } = 0;
        public int PmtGainCh1 { get; private set; } = 0;
        public int PmtGainCh2 { get; private set; } = 0;
        public int PowerMira { get; private set; } = 0;
        public int PowerX3Tunable { get; private set; } = 0;
        public int PowerX3Fixed { get; private set; } = 0;

        public ExperimentXml(string path)
        {
            if (!System.IO.File.Exists(path))
                return;

            path = System.IO.Path.GetFullPath(path);
            XmlFilePath = path;
            XmlString = System.IO.File.ReadAllText(path);
            Parse();
        }

        private void Parse()
        {
            try
            {
                XDocument doc = XDocument.Parse(XmlString);
                var frames = doc.Root.Element("Sequence").Elements("Frame");

                FrameCount = frames.Count();
                Indexes = Enumerable.Range(0, FrameCount).ToArray();
                TimesSec = frames.Select(x => double.Parse(x.Attribute("relativeTime").Value)).Select(x => Math.Round(x, 3)).ToArray();
                TimesMin = TimesSec.Select(x => Math.Round(x / 60, 5)).ToArray();
                FileNames = frames.Select(x => x.Element("File").Attribute("filename").Value).ToArray();
                Debug.WriteLine($"Basic parsing found {FrameCount} frames");

                foreach (var stateValue in doc.Root.Element("PVStateShard").Elements("PVStateValue"))
                {
                    string key = stateValue.Attribute("key")?.Value;
                    string value = stateValue.Attribute("value")?.Value;
                    bool isKeyOnly = (key != null) && (value == null);
                    bool isKeyValuePair = (key != null) && (value != null);

                    if (isKeyValuePair)
                    {
                        Debug.WriteLine($"{key}: {value}");
                        if (key == "dwellTime")
                            DwellTime = double.Parse(value);
                        else if (key == "linesPerFrame")
                            LinesPerFrame = int.Parse(value);
                        else if (key == "pixelsPerLine")
                            PixelsPerLine = int.Parse(value);
                        else if (key == "opticalZoom")
                            OpticalZoom = int.Parse(value);
                    }
                    else if (isKeyOnly)
                    {
                        Debug.WriteLine($"{key} [key only]");
                        var values = stateValue.Elements("IndexedValue").ToArray();
                        if (key == "micronsPerPixel")
                        {
                            MicronsPerPixel = double.Parse(values[0].Attribute("value").Value);
                        }
                        else if (key == "pmtGain")
                        {
                            PmtGainCh1 = int.Parse(values[0].Attribute("value").Value);
                            PmtGainCh2 = int.Parse(values[1].Attribute("value").Value);
                        }
                        else if (key == "laserPower")
                        {
                            foreach (var val in values)
                            {
                                string description = val.Attribute("description").Value;
                                int power = int.Parse(val.Attribute("value").Value);

                                if (description.Contains("Mira"))
                                    PowerMira = power;
                                else if (description.Contains("1040"))
                                    PowerX3Fixed = power;
                                else if (description.Contains("X3"))
                                    PowerX3Tunable = power;
                            }
                        }
                    }
                }
            }
            catch
            {
                FrameCount = 0;
                Debug.WriteLine($"Basic XML parsing failed: {XmlFilePath}");
            }
        }

        public DataTable GetFrameDataTable()
        {
            if (!IsValid)
                return null;

            DataTable table = new DataTable();
            table.Columns.Add("Index", typeof(string));
            table.Columns.Add("Seconds", typeof(double));
            table.Columns.Add("Minutes", typeof(double));
            table.Columns.Add("Filename", typeof(string));

            for (int i = 0; i < FrameCount; i++)
            {
                DataRow row = table.NewRow();
                row.SetField(0, Indexes[i]);
                row.SetField(1, TimesSec[i]);
                row.SetField(2, TimesMin[i]);
                row.SetField(3, FileNames[i]);
                table.Rows.Add(row);
            }

            return table;
        }
    }
}
