using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanAGator
{
    public class LineScanFolder
    {
        public readonly bool isValid = false;
        public readonly string pathFolder;
        public readonly string pathXml;
        public readonly string[] pathsRef;
        public readonly string[] pathsDataR;
        public readonly string[] pathsDataG;

        public LineScanFolder(string pathFolder)
        {
            this.pathFolder = System.IO.Path.GetFullPath(pathFolder);


            // scan for reference images
            string pathRefFolder = System.IO.Path.Combine(pathFolder, "References");
            if (System.IO.Directory.Exists(pathRefFolder))
            {
                pathsRef = System.IO.Directory.GetFiles(pathRefFolder, "*.tif");
            }
            else
            {
                Console.WriteLine("INVALID LINESCAN FOLDER - no reference sub-folder");
                return;
            }

            // scan for data images and XML configuration file
            string[] filePaths = System.IO.Directory.GetFiles(pathFolder, "LineScan*.*");
            Array.Sort(filePaths);
            List<string> dataImagesR = new List<string>();
            List<string> dataImagesG = new List<string>();
            foreach (string filePath in filePaths)
            {
                string fileName = System.IO.Path.GetFileName(filePath);
                if (fileName.EndsWith(".tif") && !fileName.Contains("Source.tif"))
                {
                    if (fileName.Contains("Ch1"))
                        dataImagesR.Add(filePath);
                    if (fileName.Contains("Ch2"))
                        dataImagesG.Add(filePath);
                }
                else if (fileName.EndsWith(".xml"))
                {
                    pathXml = filePath;
                }
            }
            pathsDataG = dataImagesG.ToArray();
            pathsDataR = dataImagesR.ToArray();


            Console.WriteLine($"Reference image count: {pathsRef.Length}");
            Console.WriteLine($"Red image count: {pathsDataG.Length}");
            Console.WriteLine($"Green image count: {pathsDataR.Length}");
            Console.WriteLine($"XML File: {System.IO.Path.GetFileName(pathXml)}");


            isValid = true;
        }

        private void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
