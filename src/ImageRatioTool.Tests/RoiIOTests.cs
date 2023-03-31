namespace ImageRatioTool.Tests;

internal class RoiIOTests
{
    [Test]
    public void Test_ROI_Save()
    {
        // create sample ROIs
        SquareRoiCollection rois = new("test path", 512, 512);
        rois.Points.AddRange(SampleData.RandomPoints(10));

        // simulate ROI AFU measurement
        List<double[]> afusByFrame = SampleData.RandomAfuCurves(20, rois.Count);
        double[] frameTimes = Enumerable.Range(0, afusByFrame.Count).Select(x => (double)x * 60).ToArray();

        rois.Save("roi-test.csv", afusByFrame, frameTimes);
    }
}
