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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tbRoiSpacing = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbRoiSize = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRoiSpacing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRoiSize)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(512, 512);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tbRoiSpacing
            // 
            this.tbRoiSpacing.Location = new System.Drawing.Point(521, 39);
            this.tbRoiSpacing.Maximum = 100;
            this.tbRoiSpacing.Minimum = 5;
            this.tbRoiSpacing.Name = "tbRoiSpacing";
            this.tbRoiSpacing.Size = new System.Drawing.Size(331, 45);
            this.tbRoiSpacing.TabIndex = 1;
            this.tbRoiSpacing.Value = 20;
            this.tbRoiSpacing.Scroll += new System.EventHandler(this.tbRoiSpacing_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(521, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "ROI Spacing";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(521, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "ROI Size";
            // 
            // tbRoiSize
            // 
            this.tbRoiSize.Location = new System.Drawing.Point(521, 150);
            this.tbRoiSize.Maximum = 50;
            this.tbRoiSize.Minimum = 2;
            this.tbRoiSize.Name = "tbRoiSize";
            this.tbRoiSize.Size = new System.Drawing.Size(331, 45);
            this.tbRoiSize.TabIndex = 3;
            this.tbRoiSize.Value = 5;
            this.tbRoiSize.Scroll += new System.EventHandler(this.tbRoiSize_Scroll);
            // 
            // DendriteTracerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbRoiSize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbRoiSpacing);
            this.Controls.Add(this.pictureBox1);
            this.Name = "DendriteTracerControl";
            this.Size = new System.Drawing.Size(1070, 535);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRoiSpacing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRoiSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private PictureBox pictureBox1;
    private TrackBar tbRoiSpacing;
    private Label label1;
    private Label label2;
    private TrackBar tbRoiSize;
}
