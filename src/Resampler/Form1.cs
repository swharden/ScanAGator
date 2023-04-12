namespace Resampler;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        Load += (s, e) => { rtbIn.Text = SampleData.XYPair; };
    }

    private void textBox1_TextChanged(object sender, EventArgs e) => ValidateAndAnalyze();

    private void rtbIn_TextChanged(object sender, EventArgs e) => ValidateAndAnalyze();

    private void ValidateAndAnalyze()
    {
        double? spacing = ReadSpacingTextbox();
        var xys = ReadXYTextbox();

        bool validInputs = spacing is not null && xys is not null;
        formsPlot1.Visible = validInputs;
        rtbOut.Visible = validInputs;

        if (!validInputs)
            return;

        (double[] xs, double[] ys) = xys!.Value;
        (double[] xs2, double[] ys2) = Interpolation.Resample(xs, ys, spacing!.Value);
        rtbOut.Text = string.Join("\n", ys2.Select(x => x.ToString()));
        UpdatePlot(xs, ys, xs2, ys2);
    }

    private double? ReadSpacingTextbox()
    {
        bool spacingIsNumeric = double.TryParse(textBox1.Text, out double spacing);
        bool isValid = spacingIsNumeric && spacing > 0;
        textBox1.BackColor = isValid ? Color.White : Color.Salmon;
        return isValid ? spacing : null;
    }

    private (double[] xs, double[] ys)? ReadXYTextbox()
    {
        var parsedColumns = Parsing.GetXY(rtbIn.Text);
        bool isValid = parsedColumns is not null;
        rtbIn.BackColor = isValid ? Color.White : Color.Salmon;
        return parsedColumns;
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
