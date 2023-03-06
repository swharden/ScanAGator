namespace ImageRatioTool.Forms;

partial class MenuForm
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.btnSquareRoi = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDendriteRois = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSquareRoi
            // 
            this.btnSquareRoi.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSquareRoi.Location = new System.Drawing.Point(26, 126);
            this.btnSquareRoi.Name = "btnSquareRoi";
            this.btnSquareRoi.Size = new System.Drawing.Size(329, 60);
            this.btnSquareRoi.TabIndex = 0;
            this.btnSquareRoi.Text = "Single Manually-Placed ROI";
            this.btnSquareRoi.UseVisualStyleBackColor = true;
            this.btnSquareRoi.Click += new System.EventHandler(this.btnSquareRoi_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(26, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(478, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "This application measures G/R in multi-frame 2-channel TIF images.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(26, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(308, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "What type of ROI do you want to measure?";
            // 
            // btnDendriteRois
            // 
            this.btnDendriteRois.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnDendriteRois.Location = new System.Drawing.Point(401, 126);
            this.btnDendriteRois.Name = "btnDendriteRois";
            this.btnDendriteRois.Size = new System.Drawing.Size(343, 60);
            this.btnDendriteRois.TabIndex = 3;
            this.btnDendriteRois.Text = "Multiple ROIs Along a Dendrite";
            this.btnDendriteRois.UseVisualStyleBackColor = true;
            this.btnDendriteRois.Click += new System.EventHandler(this.btnDendriteRois_Click);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 222);
            this.Controls.Add(this.btnDendriteRois);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSquareRoi);
            this.Name = "MenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Image Ratio Tool";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private Button btnSquareRoi;
    private Label label1;
    private Label label2;
    private Button btnDendriteRois;
}