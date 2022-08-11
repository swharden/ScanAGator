using System;
using System.Linq;

namespace ScanAGator;

public static class ImageOperations
{
    public static ImageData Average(ImageData[] images)
    {
        ImageData firstImage = images.First();
        double[] dataSum = new double[firstImage.data.Length];

        foreach (ImageData image in images)
        {
            if (image.data.Length != firstImage.data.Length)
                throw new InvalidOperationException("all images must be the same size");

            if (image.width != firstImage.width)
                throw new InvalidOperationException("all images must be the same size");

            if (image.height != firstImage.height)
                throw new InvalidOperationException("all images must be the same size");

            for (int i = 0; i < dataSum.Length; i++)
            {
                dataSum[i] += image.data[i];
            }
        }

        double[] averageData = dataSum.Select(x => x / images.Length).ToArray();

        return new ImageData(averageData, images.First().width, images.First().height);
    }
}
