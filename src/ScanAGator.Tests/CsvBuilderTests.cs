using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanAGator.Tests
{
    internal class CsvBuilderTests
    {
        [Test]
        public void Test_CsvBuilder_SampleData()
        {
            DataExport.CsvBuilder csv = new();
            csv.Add("Time", "sec", "fileid", ScottPlot.DataGen.Consecutive(50));
            csv.Add("title 2", "%", "", ScottPlot.DataGen.Sin(50));
            csv.Add("title 3", "%", "", ScottPlot.DataGen.Sin(20));
            csv.Add("title 4", "%", "", ScottPlot.DataGen.Cos(50));

            string csvFilePath = System.IO.Path.GetFullPath("./test.csv");
            csv.SaveAs(csvFilePath);
            Console.WriteLine(csvFilePath);
        }
    }
}
