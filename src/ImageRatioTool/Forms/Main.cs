namespace ImageRatioTool;

public partial class Main : Form
{
    public Main()
    {
        InitializeComponent();
        tSeriesRoiSelector1.AnalysisUpdated += TSeriesRoiSelector1_AnalysisUpdated;
        tSeriesRoiSelector1.LoadFile(SampleData.RatiometricImageSeries);
    }

    private void btnSelectImage_Click(object sender, EventArgs e)
    {
        OpenFileDialog diag = new()
        {
            Filter = "TIF files (*.tif)|*.tif",
            Title = "Select a 2-channel TSeries TIF file",
        };

        if (diag.ShowDialog() == DialogResult.OK)
            tSeriesRoiSelector1.LoadFile(diag.FileName);
    }

    private void TSeriesRoiSelector1_AnalysisUpdated(object? sender, RoiAnalysis e)
    {
        GraphOperations.PlotIntensities(formsPlot1, e);
        GraphOperations.PlotRatios(formsPlot2, e);
    }
}