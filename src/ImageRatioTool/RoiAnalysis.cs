namespace ImageRatioTool;

public struct RoiAnalysis
{
    public double[] SortedValues;
    public int NoiseFloorIndex;
    public double NoiseFloor;
    public double Threshold;
    public int ThresholdIndex;
    public int PixelsTotal => SortedValues.Length;
    public int PixelsAboveThreshold => SortedValues.Length - ThresholdIndex - 1;
    public double FractionAboveThreshold => PixelsAboveThreshold / (double)PixelsTotal;

    public double[] SortedRatios;
    public int MedianIndex => SortedRatios.Length / 2;
    public double MedianRatio => SortedRatios[MedianIndex];

    public RoiAnalysis(SciTIF.Image red, SciTIF.Image green, double noiseFloorFraction = 0.2, double signalThresholdNoiseFloorMultiple = 5)
    {
        // TODO: pass in non-cropped images and a rectangle and let this constructor do the cropping

        SortedValues = red.Values.OrderBy(x => x).ToArray();
        NoiseFloorIndex = (int)(SortedValues.Length * noiseFloorFraction);
        NoiseFloor = SortedValues[NoiseFloorIndex];

        Threshold = NoiseFloor * signalThresholdNoiseFloorMultiple;
        for (ThresholdIndex = 0; ThresholdIndex < SortedValues.Length; ThresholdIndex++)
        {
            if (SortedValues[ThresholdIndex] >= Threshold)
                break;
        }

        List<double> ratios = new();
        for (int x = 0; x < red.Width; x++)
        {
            for (int y = 0; y < red.Height; y++)
            {
                double redValue = red.GetPixel(x, y);
                if (redValue < Threshold)
                    continue;

                double greenValue = green.GetPixel(x, y);
                double ratio = greenValue / redValue;
                ratios.Add(ratio);
            }
        }
        SortedRatios = ratios.OrderBy(x => x).ToArray();
    }
}
