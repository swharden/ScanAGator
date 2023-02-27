namespace ImageRatioTool;

partial class Form1
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
        btnSetImages = new Button();
        pictureBox1 = new PictureBox();
        pictureBox2 = new PictureBox();
        formsPlot1 = new ScottPlot.FormsPlot();
        formsPlot2 = new ScottPlot.FormsPlot();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
        SuspendLayout();
        // 
        // btnSetImages
        // 
        btnSetImages.Location = new Point(12, 12);
        btnSetImages.Name = "btnSetImages";
        btnSetImages.Size = new Size(112, 34);
        btnSetImages.TabIndex = 0;
        btnSetImages.Text = "Set Images";
        btnSetImages.UseVisualStyleBackColor = true;
        btnSetImages.Click += btnSetImages_Click;
        // 
        // pictureBox1
        // 
        pictureBox1.BackColor = SystemColors.ControlDark;
        pictureBox1.Location = new Point(12, 52);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(512, 512);
        pictureBox1.TabIndex = 1;
        pictureBox1.TabStop = false;
        // 
        // pictureBox2
        // 
        pictureBox2.BackColor = SystemColors.ControlDark;
        pictureBox2.Location = new Point(530, 52);
        pictureBox2.Name = "pictureBox2";
        pictureBox2.Size = new Size(113, 87);
        pictureBox2.TabIndex = 2;
        pictureBox2.TabStop = false;
        // 
        // formsPlot1
        // 
        formsPlot1.Location = new Point(533, 147);
        formsPlot1.Margin = new Padding(6, 5, 6, 5);
        formsPlot1.Name = "formsPlot1";
        formsPlot1.Size = new Size(571, 417);
        formsPlot1.TabIndex = 3;
        // 
        // formsPlot2
        // 
        formsPlot2.Location = new Point(1116, 147);
        formsPlot2.Margin = new Padding(6, 5, 6, 5);
        formsPlot2.Name = "formsPlot2";
        formsPlot2.Size = new Size(571, 417);
        formsPlot2.TabIndex = 4;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1735, 604);
        Controls.Add(formsPlot2);
        Controls.Add(formsPlot1);
        Controls.Add(pictureBox2);
        Controls.Add(pictureBox1);
        Controls.Add(btnSetImages);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Image Ratio Tool";
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Button btnSetImages;
    private PictureBox pictureBox1;
    private PictureBox pictureBox2;
    private ScottPlot.FormsPlot formsPlot1;
    private ScottPlot.FormsPlot formsPlot2;
}
