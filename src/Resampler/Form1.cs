using ScottPlot.Statistics.Interpolation;

namespace Resampler;

public partial class Form1 : Form
{
    double[] Ys = ScottPlot.Generate.NoisySin(Random.Shared, 25);
    double[] Xs = ScottPlot.Generate.Consecutive(25, 2);

    public Form1()
    {
        InitializeComponent();
        ValidateInputAndRecalculate();
    }

    private void textBox1_TextChanged(object sender, EventArgs e) => ValidateInputAndRecalculate();

    private void ValidateInputAndRecalculate()
    {
        bool validInput = double.TryParse(textBox1.Text, out double spacing);

        if (spacing <= 0)
            validInput = false;

        textBox1.BackColor = validInput ? Color.White : Color.Salmon;

        if (validInput)
            Recalculate(spacing);
    }

    private void Recalculate(double spacing)
    {
        formsPlot1.Plot.Clear();
        var orig = formsPlot1.Plot.AddScatter(Xs, Ys, label: "Original");
        orig.MarkerSize = 10;
        orig.LineStyle = ScottPlot.LineStyle.Dash;
        orig.Color = Color.Gray;

        int newCount = (int)(Xs.Max() / spacing) + 1;

        double[] xsEven = Enumerable.Range(0, newCount).Select(x => x * spacing).ToArray();
        (double[] xs, double[] ys) = Interpolation.Interpolate1D(Xs, Ys, xsEven);
        var interp = formsPlot1.Plot.AddScatter(xs, ys, label: "Interpolated");
        interp.Color = Color.Black;

        formsPlot1.Plot.Legend();
        formsPlot1.Refresh();
    }

    private void rtbIn_TextChanged(object sender, EventArgs e)
    {
        double[,]? values = Parsing.GetValues(rtbIn.Text);

        if (values is null)
        {
            rtbIn.BackColor = Color.Salmon;
            return;
        }

        rtbIn.BackColor = Color.White;

        // TODO: resume work here
        //Xs = Enumerable.Range(0, values.Length - 1).Select(x => values[x, 0]).ToArray();
        //Ys = Enumerable.Range(0, values.Length - 1).Select(x => values[x, 1]).ToArray();
        ValidateInputAndRecalculate();
    }

    private void rtbOut_TextChanged(object sender, EventArgs e)
    {

    }
}
