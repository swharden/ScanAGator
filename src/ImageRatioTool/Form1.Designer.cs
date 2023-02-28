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
        pictureBox1 = new PictureBox();
        formsPlot1 = new ScottPlot.FormsPlot();
        formsPlot2 = new ScottPlot.FormsPlot();
        btnSelectImage = new Button();
        btnCopyImage = new Button();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        SuspendLayout();
        // 
        // pictureBox1
        // 
        pictureBox1.BackColor = SystemColors.ControlDark;
        pictureBox1.Location = new Point(12, 12);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(1024, 1024);
        pictureBox1.TabIndex = 1;
        pictureBox1.TabStop = false;
        // 
        // formsPlot1
        // 
        formsPlot1.Location = new Point(1071, 105);
        formsPlot1.Margin = new Padding(6, 5, 6, 5);
        formsPlot1.Name = "formsPlot1";
        formsPlot1.Size = new Size(571, 450);
        formsPlot1.TabIndex = 3;
        // 
        // formsPlot2
        // 
        formsPlot2.Location = new Point(1071, 586);
        formsPlot2.Margin = new Padding(6, 5, 6, 5);
        formsPlot2.Name = "formsPlot2";
        formsPlot2.Size = new Size(571, 450);
        formsPlot2.TabIndex = 4;
        // 
        // btnSelectImage
        // 
        btnSelectImage.Location = new Point(1071, 12);
        btnSelectImage.Name = "btnSelectImage";
        btnSelectImage.Size = new Size(148, 52);
        btnSelectImage.TabIndex = 9;
        btnSelectImage.Text = "Select TIF";
        btnSelectImage.UseVisualStyleBackColor = true;
        btnSelectImage.Click += btnSelectImage_Click;
        // 
        // btnCopyImage
        // 
        btnCopyImage.Location = new Point(1225, 12);
        btnCopyImage.Name = "btnCopyImage";
        btnCopyImage.Size = new Size(148, 52);
        btnCopyImage.TabIndex = 10;
        btnCopyImage.Text = "Copy Image";
        btnCopyImage.UseVisualStyleBackColor = true;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1688, 1051);
        Controls.Add(btnCopyImage);
        Controls.Add(btnSelectImage);
        Controls.Add(formsPlot2);
        Controls.Add(formsPlot1);
        Controls.Add(pictureBox1);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Image Ratio Tool";
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        ResumeLayout(false);
    }

    #endregion
    private PictureBox pictureBox1;
    private ScottPlot.FormsPlot formsPlot1;
    private ScottPlot.FormsPlot formsPlot2;
    private Button btnSelectImage;
    private Button btnCopyImage;
}
