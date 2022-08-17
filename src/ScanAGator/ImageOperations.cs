using System;
using System.Linq;

namespace ScanAGator;

public static class ImageOperations
{
    public static ImageData Average(ImageData[] images)
    {
        ImageData firstImage = images.First();
        double[] dataSum = new double[firstImage.Values.Length];

        foreach (ImageData image in images)
        {
            if (image.Values.Length != firstImage.Values.Length)
                throw new InvalidOperationException("all images must be the same size");

            if (image.Width != firstImage.Width)
                throw new InvalidOperationException("all images must be the same size");

            if (image.Height != firstImage.Height)
                throw new InvalidOperationException("all images must be the same size");

            for (int i = 0; i < dataSum.Length; i++)
            {
                dataSum[i] += image.Values[i];
            }
        }

        double[] averageData = dataSum.Select(x => x / images.Length).ToArray();

        return new ImageData(averageData, images.First().Width, images.First().Height);
    }
}
