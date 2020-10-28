using System;

namespace ScanAGator.XmlTool.PrairieXml
{
    public abstract class ExperimentXml
    {
        public readonly string XmlFilePath;
        public readonly string XmlString;

        public ExperimentXml(string path)
        {
            path = System.IO.Path.GetFullPath(path);
            if (!System.IO.File.Exists(path))
                throw new ArgumentException($"file does not exist {path}");

            XmlFilePath = path;
            XmlString = System.IO.File.ReadAllText(path);

            ParseXml();
        }

        public abstract void ParseXml();
    }
}
