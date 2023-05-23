namespace DendriteTracer.Gui;

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
        tracerControl1 = new ImageTracerControl();
        hScrollBar1 = new HScrollBar();
        panel1 = new Panel();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // tracerControl1
        // 
        tracerControl1.BackColor = SystemColors.ControlDark;
        tracerControl1.Location = new Point(12, 12);
        tracerControl1.Name = "tracerControl1";
        tracerControl1.Size = new Size(500, 500);
        tracerControl1.TabIndex = 0;
        // 
        // hScrollBar1
        // 
        hScrollBar1.Dock = DockStyle.Fill;
        hScrollBar1.Location = new Point(0, 0);
        hScrollBar1.Name = "hScrollBar1";
        hScrollBar1.Size = new Size(498, 23);
        hScrollBar1.TabIndex = 1;
        // 
        // panel1
        // 
        panel1.BorderStyle = BorderStyle.FixedSingle;
        panel1.Controls.Add(hScrollBar1);
        panel1.Location = new Point(12, 518);
        panel1.Name = "panel1";
        panel1.Size = new Size(500, 25);
        panel1.TabIndex = 2;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1140, 559);
        Controls.Add(panel1);
        Controls.Add(tracerControl1);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Form1";
        panel1.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private ImageTracerControl tracerControl1;
    private HScrollBar hScrollBar1;
    private Panel panel1;
}
