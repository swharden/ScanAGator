using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanAGator
{
    public class LineScanFolder
    {
        string PathFolderData;
        string PathFolderRefs;
        string FolderName { get { return System.IO.Path.GetFileName(PathFolderData); } }
        string[] DataImagePaths;
        string[] RefImagePaths;
        public bool IsValidLinescan;

        public LineScanFolder(string pathFolder)
        {
            PathFolderData = System.IO.Path.GetFullPath(pathFolder);
            PathFolderRefs = System.IO.Path.Combine(PathFolderData, "References");
            IsValidLinescan = ScanFiles();
            // TODO: XML parsing to get linescan time
        }

        private bool ScanFiles()
        {
            if (!System.IO.Directory.Exists(PathFolderData) || !System.IO.Directory.Exists(PathFolderRefs))
                return false;

            RefImagePaths = System.IO.Directory.GetFiles(PathFolderRefs, "*.tif");
            DataImagePaths = System.IO.Directory.GetFiles(PathFolderData, "*.tif");

            if (DataImagePaths.Length == 0 || RefImagePaths.Length == 0)
                return false;

            return true;
        }

        public Bitmap GetRepresentativeBitmap()
        {
            // start with the first image in the list
            string repFilePath = System.IO.Path.Combine(PathFolderData, RefImagePaths[0]);

            // update it to the first 8-bit image if one is available
            foreach (string imagePath in RefImagePaths)
                if (imagePath.Contains("8bit"))
                    repFilePath = imagePath;

            Bitmap bmp = new Bitmap(repFilePath);
            return bmp;
        }
    }
}
