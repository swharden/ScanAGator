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
        pictureBox2 = new PictureBox();
        formsPlot1 = new ScottPlot.FormsPlot();
        formsPlot2 = new ScottPlot.FormsPlot();
        label1 = new Label();
        label2 = new Label();
        label3 = new Label();
        btnSelectImage = new Button();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
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
        // pictureBox2
        // 
        pictureBox2.BackColor = SystemColors.ControlDark;
        pictureBox2.Location = new Point(1071, 12);
        pictureBox2.Name = "pictureBox2";
        pictureBox2.Size = new Size(119, 114);
        pictureBox2.TabIndex = 2;
        pictureBox2.TabStop = false;
        // 
        // formsPlot1
        // 
        formsPlot1.Location = new Point(1071, 126);
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
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(1196, 51);
        label1.Name = "label1";
        label1.Size = new Size(59, 25);
        label1.TabIndex = 6;
        label1.Text = "label1";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(1196, 76);
        label2.Name = "label2";
        label2.Size = new Size(59, 25);
        label2.TabIndex = 7;
        label2.Text = "label2";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(1196, 101);
        label3.Name = "label3";
        label3.Size = new Size(59, 25);
        label3.TabIndex = 8;
        label3.Text = "label3";
        // 
        // btnSelectImage
        // 
        btnSelectImage.Location = new Point(1196, 12);
        btnSelectImage.Name = "btnSelectImage";
        btnSelectImage.Size = new Size(112, 34);
        btnSelectImage.TabIndex = 9;
        btnSelectImage.Text = "Select TIF";
        btnSelectImage.UseVisualStyleBackColor = true;
        btnSelectImage.Click += btnSelectImage_Click;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1688, 1051);
        Controls.Add(btnSelectImage);
        Controls.Add(label3);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(formsPlot2);
        Controls.Add(formsPlot1);
        Controls.Add(pictureBox2);
        Controls.Add(pictureBox1);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Image Ratio Tool";
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private PictureBox pictureBox1;
    private PictureBox pictureBox2;
    private ScottPlot.FormsPlot formsPlot1;
    private ScottPlot.FormsPlot formsPlot2;
    private Label label1;
    private Label label2;
    private Label label3;
    private Button btnSelectImage;
}
