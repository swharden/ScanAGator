namespace Resampler;

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
        formsPlot1 = new ScottPlot.FormsPlot();
        label1 = new Label();
        textBox1 = new TextBox();
        label2 = new Label();
        rtbIn = new RichTextBox();
        rtbOut = new RichTextBox();
        label3 = new Label();
        lblError = new Label();
        SuspendLayout();
        // 
        // formsPlot1
        // 
        formsPlot1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        formsPlot1.Location = new Point(15, 227);
        formsPlot1.Margin = new Padding(6, 5, 6, 5);
        formsPlot1.Name = "formsPlot1";
        formsPlot1.Size = new Size(1423, 506);
        formsPlot1.TabIndex = 0;
        // 
        // label1
        // 
        label1.Location = new Point(337, 18);
        label1.Name = "label1";
        label1.Size = new Size(113, 54);
        label1.TabIndex = 2;
        label1.Text = "Sample period:";
        // 
        // textBox1
        // 
        textBox1.Location = new Point(337, 75);
        textBox1.Name = "textBox1";
        textBox1.Size = new Size(101, 31);
        textBox1.TabIndex = 3;
        textBox1.Text = "60";
        textBox1.TextChanged += textBox1_TextChanged;
        // 
        // label2
        // 
        label2.Location = new Point(23, 18);
        label2.Name = "label2";
        label2.Size = new Size(239, 54);
        label2.TabIndex = 11;
        label2.Text = "Input: copy/paste XY columns from OriginLab";
        // 
        // rtbIn
        // 
        rtbIn.Location = new Point(23, 75);
        rtbIn.Name = "rtbIn";
        rtbIn.Size = new Size(283, 144);
        rtbIn.TabIndex = 0;
        rtbIn.Text = "";
        rtbIn.TextChanged += rtbIn_TextChanged;
        // 
        // rtbOut
        // 
        rtbOut.Location = new Point(503, 75);
        rtbOut.Name = "rtbOut";
        rtbOut.Size = new Size(283, 144);
        rtbOut.TabIndex = 0;
        rtbOut.Text = "";
        // 
        // label3
        // 
        label3.Location = new Point(503, 18);
        label3.Name = "label3";
        label3.Size = new Size(192, 54);
        label3.TabIndex = 13;
        label3.Text = "Output: copy/paste Y column to OriginLab";
        // 
        // lblError
        // 
        lblError.AutoSize = true;
        lblError.BackColor = Color.Salmon;
        lblError.Location = new Point(811, 18);
        lblError.Name = "lblError";
        lblError.Size = new Size(68, 25);
        lblError.TabIndex = 14;
        lblError.Text = "ERROR";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1453, 747);
        Controls.Add(lblError);
        Controls.Add(label3);
        Controls.Add(rtbOut);
        Controls.Add(rtbIn);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(textBox1);
        Controls.Add(formsPlot1);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Data Resampler";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private ScottPlot.FormsPlot formsPlot1;
    private Label label1;
    private TextBox textBox1;
    private Label label2;
    private RichTextBox rtbIn;
    private RichTextBox rtbOut;
    private Label label3;
    private Label lblError;
}
