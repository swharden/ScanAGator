namespace ScanAGator.GUI
{
    partial class DataExportControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCopyR = new System.Windows.Forms.Button();
            this.btnCopyG = new System.Windows.Forms.Button();
            this.btnCopyXs = new System.Windows.Forms.Button();
            this.btnCopyGoR = new System.Windows.Forms.Button();
            this.lblPeak = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSaveAndOrigin = new System.Windows.Forms.Button();
            this.btnCopyPeak = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCopyPeak);
            this.groupBox1.Controls.Add(this.btnSaveAndOrigin);
            this.groupBox1.Controls.Add(this.btnCopyR);
            this.groupBox1.Controls.Add(this.btnCopyG);
            this.groupBox1.Controls.Add(this.btnCopyXs);
            this.groupBox1.Controls.Add(this.btnCopyGoR);
            this.groupBox1.Controls.Add(this.lblPeak);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(802, 200);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Export";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(146, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 20);
            this.label2.TabIndex = 17;
            this.label2.Text = "Curve Data:";
            // 
            // btnCopyR
            // 
            this.btnCopyR.Location = new System.Drawing.Point(272, 100);
            this.btnCopyR.Name = "btnCopyR";
            this.btnCopyR.Size = new System.Drawing.Size(116, 34);
            this.btnCopyR.TabIndex = 16;
            this.btnCopyR.Text = "Copy R";
            this.btnCopyR.UseVisualStyleBackColor = true;
            // 
            // btnCopyG
            // 
            this.btnCopyG.Location = new System.Drawing.Point(150, 100);
            this.btnCopyG.Name = "btnCopyG";
            this.btnCopyG.Size = new System.Drawing.Size(116, 34);
            this.btnCopyG.TabIndex = 15;
            this.btnCopyG.Text = "Copy G";
            this.btnCopyG.UseVisualStyleBackColor = true;
            // 
            // btnCopyXs
            // 
            this.btnCopyXs.Location = new System.Drawing.Point(150, 56);
            this.btnCopyXs.Name = "btnCopyXs";
            this.btnCopyXs.Size = new System.Drawing.Size(116, 34);
            this.btnCopyXs.TabIndex = 14;
            this.btnCopyXs.Text = "Copy Xs";
            this.btnCopyXs.UseVisualStyleBackColor = true;
            // 
            // btnCopyGoR
            // 
            this.btnCopyGoR.Location = new System.Drawing.Point(272, 56);
            this.btnCopyGoR.Name = "btnCopyGoR";
            this.btnCopyGoR.Size = new System.Drawing.Size(116, 34);
            this.btnCopyGoR.TabIndex = 13;
            this.btnCopyGoR.Text = "Copy ΔG/R";
            this.btnCopyGoR.UseVisualStyleBackColor = true;
            // 
            // lblPeak
            // 
            this.lblPeak.AutoSize = true;
            this.lblPeak.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeak.Location = new System.Drawing.Point(22, 63);
            this.lblPeak.Name = "lblPeak";
            this.lblPeak.Size = new System.Drawing.Size(87, 23);
            this.lblPeak.TabIndex = 12;
            this.lblPeak.Text = "12.345%";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Peak ΔG/R";
            // 
            // btnSaveAndOrigin
            // 
            this.btnSaveAndOrigin.Location = new System.Drawing.Point(415, 56);
            this.btnSaveAndOrigin.Name = "btnSaveAndOrigin";
            this.btnSaveAndOrigin.Size = new System.Drawing.Size(135, 78);
            this.btnSaveAndOrigin.TabIndex = 18;
            this.btnSaveAndOrigin.Text = "Save files and copy Origin command";
            this.btnSaveAndOrigin.UseVisualStyleBackColor = true;
            // 
            // btnCopyPeak
            // 
            this.btnCopyPeak.Location = new System.Drawing.Point(15, 100);
            this.btnCopyPeak.Name = "btnCopyPeak";
            this.btnCopyPeak.Size = new System.Drawing.Size(116, 34);
            this.btnCopyPeak.TabIndex = 19;
            this.btnCopyPeak.Text = "Copy Peak";
            this.btnCopyPeak.UseVisualStyleBackColor = true;
            // 
            // DataExportControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "DataExportControl";
            this.Size = new System.Drawing.Size(802, 200);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCopyR;
        private System.Windows.Forms.Button btnCopyG;
        private System.Windows.Forms.Button btnCopyXs;
        private System.Windows.Forms.Button btnCopyGoR;
        private System.Windows.Forms.Label lblPeak;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSaveAndOrigin;
        private System.Windows.Forms.Button btnCopyPeak;
    }
}
