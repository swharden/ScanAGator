using NUnit.Framework;
using System;

namespace ScanAGator.Tests
{
    internal class MetadataTests
    {
        [Test]
        public void Test_Metadata_Export()
        {
            string LinescanFolder = TestContext.CurrentContext.TestDirectory + "/../../../../../data/linescans/";
            string lsFolderPath = System.IO.Path.Combine(LinescanFolder, "LineScan-03272018-1330-2145");
            var lsFolder = new ScanAGator.LineScanFolder(lsFolderPath);

            string metadata = lsFolder.GetMetadataJson();
            Assert.That(metadata, Is.Not.Null);
            Assert.That(metadata, Is.Not.Empty);
            Console.WriteLine(metadata);
        }
    }
}
