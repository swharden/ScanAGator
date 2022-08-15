using System;
using System.Collections.Generic;
using System.Linq;

namespace ScanAGator.Imaging
{
    public class RatiometricImages
    {
        public readonly RatiometricImage[] Frames;
        public int FrameCount => Frames.Length;
        public readonly RatiometricImage Average;

        public RatiometricImages(RatiometricImage[] frames)
        {
            Frames = frames;
            ImageData avgGreen = ImageDataTools.Average(Frames.Select(x => x.GreenData));
            ImageData avgRed = ImageDataTools.Average(Frames.Select(x => x.RedData));
            Average = new(avgGreen, avgRed);
        }
    }
}
