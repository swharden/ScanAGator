using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    class SampleData
    {
        public static ScanAGator.LineScanFolder GreenOverRed() =>
             new ScanAGator.LineScanFolder(TestContext.CurrentContext.TestDirectory +
                 "/../../../../../data/linescans/LineScan-02052019-1234-2683");
        public static ScanAGator.LineScanFolder GreenOnly() =>
             new ScanAGator.LineScanFolder(TestContext.CurrentContext.TestDirectory +
                 "/../../../../../data/linescans/LineScan-02132019-1317-2775");
    }
}
