namespace ImageRatioTool;

partial class Main
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        formsPlot1 = new ScottPlot.FormsPlot();
        formsPlot2 = new ScottPlot.FormsPlot();
        btnSelectImage = new Button();
        tSeriesRoiSelector1 = new TSeriesRoiSelector();
        SuspendLayout();
        // 
        // formsPlot1
        // 
        formsPlot1.Location = new Point(545, 81);
        formsPlot1.Margin = new Padding(6, 5, 6, 5);
        formsPlot1.Name = "formsPlot1";
        formsPlot1.Size = new Size(571, 577);
        formsPlot1.TabIndex = 3;
        // 
        // formsPlot2
        // 
        formsPlot2.Location = new Point(1128, 81);
        formsPlot2.Margin = new Padding(6, 5, 6, 5);
        formsPlot2.Name = "formsPlot2";
        formsPlot2.Size = new Size(571, 577);
        formsPlot2.TabIndex = 4;
        // 
        // btnSelectImage
        // 
        btnSelectImage.Location = new Point(12, 12);
        btnSelectImage.Name = "btnSelectImage";
        btnSelectImage.Size = new Size(148, 52);
        btnSelectImage.TabIndex = 9;
        btnSelectImage.Text = "Select TIF";
        btnSelectImage.UseVisualStyleBackColor = true;
        btnSelectImage.Click += btnSelectImage_Click;
        // 
        // tSeriesRoiSelector1
        // 
        tSeriesRoiSelector1.Location = new Point(12, 70);
        tSeriesRoiSelector1.Name = "tSeriesRoiSelector1";
        tSeriesRoiSelector1.Size = new Size(524, 588);
        tSeriesRoiSelector1.TabIndex = 10;
        // 
        // MainWindow
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1715, 672);
        Controls.Add(tSeriesRoiSelector1);
        Controls.Add(btnSelectImage);
        Controls.Add(formsPlot2);
        Controls.Add(formsPlot1);
        Name = "MainWindow";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Image Ratio Tool";
        ResumeLayout(false);
    }

    #endregion
    private ScottPlot.FormsPlot formsPlot1;
    private ScottPlot.FormsPlot formsPlot2;
    private Button btnSelectImage;
    private TSeriesRoiSelector tSeriesRoiSelector1;
}
