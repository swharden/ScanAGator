using NUnit.Framework;

namespace ScanAGator.Tests
{
    class SampleData
    {
        public static ScanAGator.LineScanFolder GreenOverRed() =>
             new ScanAGator.LineScanFolder(TestContext.CurrentContext.TestDirectory +
                 "/../../../../../data/linescans/LineScan-02052019-1234-2683");
        public static ScanAGator.LineScanFolder GreenOnly() =>
             new ScanAGator.LineScanFolder(TestContext.CurrentContext.TestDirectory +
                 "/../../../../../data/linescans/LineScan-02132019-1317-2775");
        public static ScanAGator.LineScanFolder MultipleGreenOverRed() =>
             new ScanAGator.LineScanFolder(TestContext.CurrentContext.TestDirectory +
                 "/../../../../../data/linescans/LineScan-08092022-1225-528");
    }
}
