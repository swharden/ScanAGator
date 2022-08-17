using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanAGator.Analysis
{
    public class AnalysisResult
    {
        public readonly AnalysisSettings Settings;
        public readonly double[] Xs;
        public readonly double[] Green;
        public readonly double[] Red;
        public readonly double[] DeltaGreen;
        public readonly double[] DeltaGreenOverRedPercent;
        public readonly double DeltaGreenOverRedPercentMax;

        public bool IsValid => !double.IsNaN(DeltaGreenOverRedPercentMax) && !double.IsInfinity(DeltaGreenOverRedPercentMax);

        public AnalysisResult(AnalysisSettings settings)
        {
            Settings = settings;
            Xs = ScottPlot.DataGen.Consecutive(settings.Image.Green.Height, Settings.Xml.MsecPerPixel);

            Green = ImageDataTools.GetAverageTopdown(Settings.Image.GreenData, Settings.Structure);
            if (IsNotFinite(Green))
                throw new InvalidOperationException($"{nameof(Green)} contains invalid data");

            Red = ImageDataTools.GetAverageTopdown(Settings.Image.RedData, Settings.Structure);
            if (IsNotFinite(Red))
                throw new InvalidOperationException($"{nameof(Red)} contains invalid data");

            DeltaGreen = Operations.SubtractBaseline(Green, Settings.Baseline);
            if (IsNotFinite(DeltaGreen))
                throw new InvalidOperationException($"{nameof(DeltaGreen)} contains invalid data");

            DeltaGreenOverRedPercent = Operations.CreateRatioCurve(DeltaGreen, Red);
            //if (IsNotFinite(DeltaGreenOverRedPercent))
                //throw new InvalidOperationException($"{nameof(DeltaGreenOverRedPercent)} contains invalid data");

            DeltaGreenOverRedPercentMax = DeltaGreenOverRedPercent.Max();
        }

        private static bool IsNotFinite(double[] values)
        {
            double max = values.Max();
            if (double.IsNaN(max))
                return true;
            else if (double.IsInfinity(max))
                return true;
            else return false;
        }
    }
}
