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
        rtbIn = new RichTextBox();
        label2 = new Label();
        label3 = new Label();
        rtbOut = new RichTextBox();
        SuspendLayout();
        // 
        // formsPlot1
        // 
        formsPlot1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        formsPlot1.Location = new Point(15, 222);
        formsPlot1.Margin = new Padding(6, 5, 6, 5);
        formsPlot1.Name = "formsPlot1";
        formsPlot1.Size = new Size(1132, 473);
        formsPlot1.TabIndex = 0;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(47, 42);
        label1.Name = "label1";
        label1.Size = new Size(79, 25);
        label1.TabIndex = 2;
        label1.Text = "Spacing:";
        // 
        // textBox1
        // 
        textBox1.Location = new Point(47, 70);
        textBox1.Name = "textBox1";
        textBox1.Size = new Size(101, 31);
        textBox1.TabIndex = 3;
        textBox1.Text = ".3";
        textBox1.TextChanged += textBox1_TextChanged;
        // 
        // rtbIn
        // 
        rtbIn.Location = new Point(233, 70);
        rtbIn.Name = "rtbIn";
        rtbIn.Size = new Size(270, 144);
        rtbIn.TabIndex = 4;
        rtbIn.Text = "";
        rtbIn.TextChanged += rtbIn_TextChanged;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(233, 42);
        label2.Name = "label2";
        label2.Size = new Size(140, 25);
        label2.TabIndex = 5;
        label2.Text = "Input (X/Y pairs)";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(576, 42);
        label3.Name = "label3";
        label3.Size = new Size(155, 25);
        label3.TabIndex = 6;
        label3.Text = "Output (X/Y pairs)";
        // 
        // rtbOut
        // 
        rtbOut.Location = new Point(576, 70);
        rtbOut.Name = "rtbOut";
        rtbOut.Size = new Size(270, 144);
        rtbOut.TabIndex = 7;
        rtbOut.Text = "";
        rtbOut.TextChanged += rtbOut_TextChanged;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1162, 709);
        Controls.Add(label3);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(rtbOut);
        Controls.Add(rtbIn);
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
    private RichTextBox rtbIn;
    private Label label2;
    private Label label3;
    private RichTextBox rtbOut;
}
