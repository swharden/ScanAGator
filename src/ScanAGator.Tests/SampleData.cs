using NUnit.Framework;
using System.IO;

namespace ScanAGator.Tests
{
    class SampleData
    {
        public static string Folder
        {
            get
            {
                string folderPath = Path.Combine(
                    TestContext.CurrentContext.TestDirectory,
                    "../../../../../data/linescans/");

                return Path.GetFullPath(folderPath);
            }
        }
            
        public static string MultiFrameRatiometric => Path.Combine(Folder, "LineScan-08092022-1225-528");
    }
}
