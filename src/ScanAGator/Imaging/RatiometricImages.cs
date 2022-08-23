using System.Linq;

namespace ScanAGator.Imaging
{
    public class RatiometricImages
    {
        public readonly RatiometricImage[] Frames;
        public int FrameCount => Frames.Length;
        public RatiometricImage Average { get; private set; }

        public RatiometricImages(Prairie.FolderContents pvFolder)
        {
            Frames = Enumerable.Range(0, pvFolder.Frames)
                .Select(i => new RatiometricImage(pvFolder.ImageFilesG[i], pvFolder.ImageFilesR[i]))
                .ToArray();

            Average = CreateAverage(Frames);
        }

        public RatiometricImages(RatiometricImage[] frames)
        {
            Frames = frames;
            Average = CreateAverage(Frames);
        }

        private static RatiometricImage CreateAverage(RatiometricImage[] frames)
        {
            ImageData avgGreen = ImageDataTools.Average(frames.Select(x => x.GreenData));
            ImageData avgRed = ImageDataTools.Average(frames.Select(x => x.RedData));
            return new RatiometricImage(avgGreen, avgRed);
        }

        public void SubtractFloorFromAllImages(double percentile)
        {
            Frames.ToList().ForEach(x => x.SubtractFloor(percentile));
            Average = CreateAverage(Frames);
            Average.FloorPercentile = percentile;
        }
    }
}
