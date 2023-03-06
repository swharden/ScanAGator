namespace ImageRatioTool.Forms;

public partial class MenuForm : Form
{
    public MenuForm()
    {
        InitializeComponent();
    }

    private void btnSquareRoi_Click(object sender, EventArgs e) => new SquareRoiForm().ShowDialog();

    private void btnDendriteRois_Click(object sender, EventArgs e) => new DendriteRoiForm().ShowDialog();
}
