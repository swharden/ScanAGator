using NUnit.Framework;
using System.IO;

namespace ScanAGator.Tests
{
    class SampleData
    {
        public static string FolderPath
        {
            get
            {
                string folderPath = System.IO.Path.Combine(
                    TestContext.CurrentContext.TestDirectory,
                    "../../../../../data/linescans/");

                return System.IO.Path.GetFullPath(folderPath);
            }
        }

        public static string MultiFrameRatiometricFolderPath => Path.Combine(FolderPath, "LineScan-08092022-1225-528");
        public static string MultiFrameRatiometricFolderWithMarkPointsPath => Path.Combine(FolderPath, "LineScan-11182022-1250-763");
        public static string GreenLinescanImagePath => Path.Combine(FolderPath, "LineScan-08092022-1225-528/LineScan-08092022-1225-528_Cycle00001_Ch2_000001.ome.tif");
        public static string RedLinescanImagePath => Path.Combine(FolderPath, "LineScan-08092022-1225-528/LineScan-08092022-1225-528_Cycle00001_Ch1_000001.ome.tif");
    }
}
