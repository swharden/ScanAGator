namespace ImageRatioTool.Controls;

partial class DendriteTracerControl
{
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        pictureBox1 = new PictureBox();
        tbRoiSpacing = new TrackBar();
        label1 = new Label();
        label2 = new Label();
        tbRoiSize = new TrackBar();
        formsPlot1 = new ScottPlot.FormsPlot();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)tbRoiSpacing).BeginInit();
        ((System.ComponentModel.ISupportInitialize)tbRoiSize).BeginInit();
        SuspendLayout();
        // 
        // pictureBox1
        // 
        pictureBox1.BackColor = SystemColors.ControlDark;
        pictureBox1.Location = new Point(4, 5);
        pictureBox1.Margin = new Padding(4, 5, 4, 5);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(731, 853);
        pictureBox1.TabIndex = 0;
        pictureBox1.TabStop = false;
        // 
        // tbRoiSpacing
        // 
        tbRoiSpacing.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        tbRoiSpacing.Location = new Point(744, 65);
        tbRoiSpacing.Margin = new Padding(4, 5, 4, 5);
        tbRoiSpacing.Maximum = 100;
        tbRoiSpacing.Minimum = 5;
        tbRoiSpacing.Name = "tbRoiSpacing";
        tbRoiSpacing.Size = new Size(486, 69);
        tbRoiSpacing.TabIndex = 1;
        tbRoiSpacing.Value = 50;
        tbRoiSpacing.Scroll += tbRoiSpacing_Scroll;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(744, 35);
        label1.Margin = new Padding(4, 0, 4, 0);
        label1.Name = "label1";
        label1.Size = new Size(110, 25);
        label1.TabIndex = 2;
        label1.Text = "ROI Spacing";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(744, 220);
        label2.Margin = new Padding(4, 0, 4, 0);
        label2.Name = "label2";
        label2.Size = new Size(78, 25);
        label2.TabIndex = 4;
        label2.Text = "ROI Size";
        // 
        // tbRoiSize
        // 
        tbRoiSize.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        tbRoiSize.Location = new Point(744, 250);
        tbRoiSize.Margin = new Padding(4, 5, 4, 5);
        tbRoiSize.Maximum = 50;
        tbRoiSize.Minimum = 2;
        tbRoiSize.Name = "tbRoiSize";
        tbRoiSize.Size = new Size(486, 69);
        tbRoiSize.TabIndex = 3;
        tbRoiSize.Value = 20;
        tbRoiSize.Scroll += tbRoiSize_Scroll;
        // 
        // formsPlot1
        // 
        formsPlot1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        formsPlot1.Location = new Point(745, 329);
        formsPlot1.Margin = new Padding(6, 5, 6, 5);
        formsPlot1.Name = "formsPlot1";
        formsPlot1.Size = new Size(485, 529);
        formsPlot1.TabIndex = 5;
        // 
        // DendriteTracerControl
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(formsPlot1);
        Controls.Add(label2);
        Controls.Add(tbRoiSize);
        Controls.Add(label1);
        Controls.Add(tbRoiSpacing);
        Controls.Add(pictureBox1);
        Margin = new Padding(4, 5, 4, 5);
        Name = "DendriteTracerControl";
        Size = new Size(1234, 892);
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        ((System.ComponentModel.ISupportInitialize)tbRoiSpacing).EndInit();
        ((System.ComponentModel.ISupportInitialize)tbRoiSize).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private PictureBox pictureBox1;
    private TrackBar tbRoiSpacing;
    private Label label1;
    private Label label2;
    private TrackBar tbRoiSize;
    private ScottPlot.FormsPlot formsPlot1;
}
