using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanAGator.Tests
{
    internal class NewLinescanTests
    {
        [Test]
        public void Test()
        {
            string folderPath = SampleData.MultipleGreenOverRed().FolderPath;
            LineScan.LineScanFolder2 lsFolder = new(folderPath);
            Plot.RatiometricCurves(lsFolder);
        }
    }
}
