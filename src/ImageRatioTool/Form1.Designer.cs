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
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
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
        pictureBox1.Size = new Size(150, 75);
        pictureBox1.TabIndex = 1;
        pictureBox1.TabStop = false;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1078, 837);
        Controls.Add(pictureBox1);
        Controls.Add(btnSetImages);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Image Ratio Tool";
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Button btnSetImages;
    private PictureBox pictureBox1;
}
