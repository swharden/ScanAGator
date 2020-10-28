using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ScanAGator.XmlTool.PrairieXml
{
    public class TSeries : ExperimentXml
    {
        public TSeries(string path) : base(path) { }

        public int frameCount;
        public bool IsValid { get => frameCount > 0; }
        public int[] Indexes;
        public double[] TimesSec;
        public double[] TimesMin;
        public string[] FileNames;

        public override void ParseXml()
        {

            try
            {
                XDocument doc = XDocument.Parse(XmlString);
                var frames = doc.Root.Element("Sequence").Elements("Frame");

                frameCount = frames.Count();
                Indexes = Enumerable.Range(0, frameCount).ToArray();
                TimesSec = frames.Select(x => double.Parse(x.Attribute("relativeTime").Value)).Select(x => Math.Round(x, 3)).ToArray();
                TimesMin = TimesSec.Select(x => Math.Round(x / 60, 5)).ToArray();
                FileNames = frames.Select(x => x.Element("File").Attribute("filename").Value).ToArray();
                Debug.WriteLine($"TSeries: parsed {frameCount} frames");
            }
            catch
            {
                frameCount = 0;
                Debug.WriteLine($"Error parsing XML file: {XmlFilePath}");
            }
        }

        public DataTable GetDataTable()
        {
            if (!IsValid)
                return null;

            DataTable table = new DataTable();
            table.Columns.Add("Index", typeof(string));
            table.Columns.Add("Seconds", typeof(double));
            table.Columns.Add("Minutes", typeof(double));
            table.Columns.Add("Filename", typeof(string));

            for (int i = 0; i < frameCount; i++)
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
