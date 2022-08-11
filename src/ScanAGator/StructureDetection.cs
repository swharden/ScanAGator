using System.Linq;

namespace ScanAGator;

internal static class StructureDetection
{
    /// <summary>
    /// Returns the range of pixels that spans the brightest structure in the image.
    /// The range spans the brightest column down to 20% of its peak relative to the noise floor
    /// </summary>
    public static PixelRange GetBrightestStructure(ImageData image)
    {
        double[] intensities = ImageDataTools.GetAverageLeftright(image);
        int brightestIndex = GetBrightestIndex(intensities);
        double noiseFloor = GetPercentile(intensities, 20);
        return GetStructureBounds(intensities, brightestIndex, noiseFloor);
    }

    private static double GetPercentile(double[] values, double percentile = 20)
    {
        double[] sorted = values.OrderBy(x => x).ToArray();
        int floorIndex = (int)(sorted.Length * (percentile / 100));
        return sorted[floorIndex];
    }

    private static int GetBrightestIndex(double[] values)
    {
        double brightestValue = 0;
        int brightestIndex = 0;
        for (int i = 0; i < values.Length; i++)
        {
            if (values[i] > brightestValue)
            {
                brightestValue = values[i];
                brightestIndex = i;
            }
        }
        return brightestIndex;
    }

    /// <summary>
    /// Given the index of a bright structure, return the bounds on each side at the edge of the noise floor
    /// </summary>
    private static PixelRange GetStructureBounds(double[] columnIntensities, int brightestIndex, double noiseFloor)
    {
        // intensity cut-off is half-way to the noise floor
        double brightestValue = columnIntensities[brightestIndex];
        double peakAboveNoise = brightestValue - noiseFloor;
        double cutOff = (peakAboveNoise * .5) + noiseFloor;

        // start both structures at the brighest point, then walk away
        int structure1 = brightestIndex;
        int structure2 = brightestIndex;

        // walk to the left
        while (columnIntensities[structure1] > cutOff && structure1 > 0)
            structure1--;

        // walk to the right
        while (columnIntensities[structure2] > cutOff && structure2 < columnIntensities.Length - 1)
            structure2++;

        return new PixelRange(structure1, structure2);
    }
}
