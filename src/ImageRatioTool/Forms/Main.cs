namespace ImageRatioTool;

public partial class Main : Form
{
    public Main()
    {
        InitializeComponent();
        tSeriesRoiSelector1.LoadFile(SampleData.RatiometricImageSeries);
    }

    private void btnSelectImage_Click(object sender, EventArgs e)
    {
        OpenFileDialog diag = new()
        {
            Filter = "TIF files (*.tif)|*.tif",
            Title = "Select a 2-channel TIF file",
        };

        if (diag.ShowDialog() == DialogResult.OK)
        {
            tSeriesRoiSelector1.LoadFile(diag.FileName);
        }
    }
}