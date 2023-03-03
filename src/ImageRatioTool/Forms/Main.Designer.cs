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
        formsPlot3 = new ScottPlot.FormsPlot();
        button1 = new Button();
        button2 = new Button();
        SuspendLayout();
        // 
        // formsPlot1
        // 
        formsPlot1.Location = new Point(545, 12);
        formsPlot1.Margin = new Padding(6, 5, 6, 5);
        formsPlot1.Name = "formsPlot1";
        formsPlot1.Size = new Size(571, 418);
        formsPlot1.TabIndex = 3;
        // 
        // formsPlot2
        // 
        formsPlot2.Location = new Point(1128, 14);
        formsPlot2.Margin = new Padding(6, 5, 6, 5);
        formsPlot2.Name = "formsPlot2";
        formsPlot2.Size = new Size(571, 416);
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
        // formsPlot3
        // 
        formsPlot3.Location = new Point(545, 440);
        formsPlot3.Margin = new Padding(6, 5, 6, 5);
        formsPlot3.Name = "formsPlot3";
        formsPlot3.Size = new Size(571, 218);
        formsPlot3.TabIndex = 11;
        // 
        // button1
        // 
        button1.Location = new Point(1170, 537);
        button1.Name = "button1";
        button1.Size = new Size(123, 68);
        button1.TabIndex = 12;
        button1.Text = "Analyze All Frames";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // button2
        // 
        button2.Location = new Point(1318, 537);
        button2.Name = "button2";
        button2.Size = new Size(123, 68);
        button2.TabIndex = 13;
        button2.Text = "Copy to Clipboard";
        button2.UseVisualStyleBackColor = true;
        button2.Click += button2_Click;
        // 
        // Main
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1715, 674);
        Controls.Add(button2);
        Controls.Add(button1);
        Controls.Add(formsPlot3);
        Controls.Add(tSeriesRoiSelector1);
        Controls.Add(btnSelectImage);
        Controls.Add(formsPlot2);
        Controls.Add(formsPlot1);
        Name = "Main";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Image Ratio Tool";
        ResumeLayout(false);
    }

    #endregion
    private ScottPlot.FormsPlot formsPlot1;
    private ScottPlot.FormsPlot formsPlot2;
    private Button btnSelectImage;
    private TSeriesRoiSelector tSeriesRoiSelector1;
    private ScottPlot.FormsPlot formsPlot3;
    private Button button1;
    private Button button2;
}
