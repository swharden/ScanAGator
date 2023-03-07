﻿namespace ImageRatioTool.Forms;

public partial class DendriteRoiForm : Form
{

    public DendriteRoiForm()
    {
        InitializeComponent();

        SciTIF.TifFile tif = new(SampleData.RatiometricImageSeries);
        SciTIF.Image red = tif.GetImage(0, 0, 0);
        SciTIF.Image green = tif.GetImage(0, 0, 1);
        dendriteTracerControl1.SetData(red, green);
    }
}
