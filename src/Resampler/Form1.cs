namespace Resampler;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        Load += (s, e) => { rtbIn.Text = SampleData.XYPair; };
    }

    private void textBox1_TextChanged(object sender, EventArgs e) => Analyze();

    private void rtbIn_TextChanged(object sender, EventArgs e) => Analyze();

    private void Analyze()
    {
        lblError.Text = string.Empty;

        (double[]? xs, double[]? ys) = Parsing.GetXY(rtbIn.Text);
        if (xs is null || ys is null)
            lblError.Text = "Invalid XY data";

        bool spacingIsNumeric = double.TryParse(textBox1.Text, out double spacing);
        if (!spacingIsNumeric || spacing <= 0)
            lblError.Text = "Invalid spacing";

        if (lblError.Text == string.Empty)
        {
            (double[] xs2, double[] ys2) = Interpolation.Resample(xs!, ys!, spacing);
            UpdateOutputTextbox(ys2);
            UpdatePlot(xs!, ys!, xs2, ys2);
        }
    }

    private void UpdateOutputTextbox(double[] ys)
    {
        rtbOut.Text = string.Join("\n", ys.Select(x => x.ToString()));
    }

    private void UpdatePlot(double[] xs, double[] ys, double[] xs2, double[] ys2)
    {
        formsPlot1.Plot.Clear();
        var orig = formsPlot1.Plot.AddScatter(xs, ys, label: "Original");
        orig.MarkerSize = 10;
        orig.LineStyle = ScottPlot.LineStyle.Dash;
        orig.Color = Color.Gray;

        var interp = formsPlot1.Plot.AddScatter(xs2, ys2, label: "Interpolated");
        interp.Color = Color.Black;

        formsPlot1.Plot.Legend();
        formsPlot1.Refresh();
    }
}
