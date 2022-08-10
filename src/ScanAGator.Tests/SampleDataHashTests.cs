using NUnit.Framework;
using System;
using System.Linq;

/* Regression tests to define and lock-in existing analysis behavior using real sample data */

namespace ScanAGator.Tests
{
    public class SampleDataHashTests
    {
        private static int SimpleHash(double[] input)
        {
            byte[] bytes = input.SelectMany(n => BitConverter.GetBytes(n)).ToArray();
            int hash = 0;
            foreach (byte b in bytes)
                hash = (hash * 31) ^ b;
            return hash;
        }

        [Test]
        public void Test_DeltaGreenOverRed_CheckCurveValues()
        {
            var lsFolder = SampleData.GreenOverRed();

            double peakDeltaGreenOverRed = lsFolder.GetFilteredYs(lsFolder.curveDeltaGoR).Max();
            Assert.AreEqual(112.45, peakDeltaGreenOverRed, .1);

            Assert.AreEqual(-1684596658, SimpleHash(lsFolder.curveG));
            Assert.AreEqual(-1497736758, SimpleHash(lsFolder.curveR));
            Assert.AreEqual(-559501835, SimpleHash(lsFolder.curveGoR));
            Assert.AreEqual(-33284870, SimpleHash(lsFolder.curveDeltaG));
            Assert.AreEqual(-307337996, SimpleHash(lsFolder.curveDeltaGoR));
        }

        [Test]
        public void Test_GreenOnly_CheckCurveValues()
        {
            var lsFolder = SampleData.GreenOnly();

            Assert.AreEqual(241425423, SimpleHash(lsFolder.curveG));
            Assert.AreEqual(1707214479, SimpleHash(lsFolder.curveDeltaG));
        }

        [Test]
        public void Test_MultipleGreenOverRed_CheckCurveValues()
        {
            ScanAGator.LineScanFolder lsFolder = SampleData.MultipleGreenOverRed();

            double peakDeltaGreenOverRed = lsFolder.GetFilteredYs(lsFolder.curveDeltaGoR).Max();
            Assert.AreEqual(37.32, peakDeltaGreenOverRed, .1);

            Assert.AreEqual(1292941114, SimpleHash(lsFolder.curveG));
            Assert.AreEqual(1672499138, SimpleHash(lsFolder.curveR));
            Assert.AreEqual(1019857017, SimpleHash(lsFolder.curveGoR));
            Assert.AreEqual(-1710320160, SimpleHash(lsFolder.curveDeltaG));
            Assert.AreEqual(1123356056, SimpleHash(lsFolder.curveDeltaGoR));
        }
    }
}