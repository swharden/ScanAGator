using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanAGatorTests
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathDemoLinescans = System.IO.Path.GetFullPath("../../../../data/linescans/");
            string lineScanOld = System.IO.Path.Combine(pathDemoLinescans, "LineScan-09212014-1554-750");
            string lineScanNew = System.IO.Path.Combine(pathDemoLinescans, "LineScan-02052019-1234-2683");

            ScanAGator.PrairieLS linescan;
            linescan = new ScanAGator.PrairieLS(lineScanOld);
            linescan = new ScanAGator.PrairieLS(lineScanNew);

            Console.WriteLine("DONE");
        }
    }
}
