using DendriteTracer.Core;

namespace DendriteTracer.Gui;

using PixelBitmap = DendriteTracer.Core.Bitmap;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        string initialPath = "350,410; 417,406; 430,384; 457,359; 515,364; " +
            "531,359; 645,427; 651,437; 645,452; 652,478; 663,502; 659,530; " +
            "670,554; 665,580; 643,602;";

        imageTracerControl1.DendritePath.LoadFromString(initialPath);

        Load += (s, e) =>
        {
            string startupImagePath = Path.GetFullPath("../../../../../../data/tseries/TSeries-03022023-1227-2098-2ch.tif");
            if (File.Exists(startupImagePath))
                LoadTif(startupImagePath);
        };

        imageTracerControl1.PointsChanged += (s, e) =>
        {
            
        };
    }

    void LoadTif(string tifFilePath)
    {
        SciTIF.TifFile tif = new(tifFilePath);

        if (tif.Channels != 2)
            throw new InvalidOperationException("Tif file must have 2 channels");

        if (tif.Frames <= 1)
            throw new InvalidOperationException("Tif file must have multiple frames");

        if (tif.Slices != 1)
            throw new InvalidOperationException("Tif file must be a projection image (not a stack)");

        SciTIF.Image red = tif.GetImage(frame: 0, slice: 0, channel: 0);
        SciTIF.Image green = tif.GetImage(frame: 0, slice: 0, channel: 1);

        PixelBitmap bmp = ImageOperations.MakeBitmap(red, green);
        imageTracerControl1.SetImage(bmp);
        Text = Path.GetFileName(tifFilePath);
    }
}
