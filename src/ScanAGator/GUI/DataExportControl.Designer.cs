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
            this.btnCopyPeak = new System.Windows.Forms.Button();
            this.btnCopyXs = new System.Windows.Forms.Button();
            this.btnCopyGoR = new System.Windows.Forms.Button();
            this.lblPeak = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSaveAndCopy = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnSaveAndCopy);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnCopyPeak);
            this.groupBox1.Controls.Add(this.btnCopyXs);
            this.groupBox1.Controls.Add(this.btnCopyGoR);
            this.groupBox1.Controls.Add(this.lblPeak);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(535, 130);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Export";
            // 
            // btnCopyPeak
            // 
            this.btnCopyPeak.Location = new System.Drawing.Point(10, 74);
            this.btnCopyPeak.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCopyPeak.Name = "btnCopyPeak";
            this.btnCopyPeak.Size = new System.Drawing.Size(77, 31);
            this.btnCopyPeak.TabIndex = 19;
            this.btnCopyPeak.Text = "Copy Peak";
            this.btnCopyPeak.UseVisualStyleBackColor = true;
            this.btnCopyPeak.Click += new System.EventHandler(this.btnCopyPeak_Click);
            // 
            // btnCopyXs
            // 
            this.btnCopyXs.Location = new System.Drawing.Point(100, 39);
            this.btnCopyXs.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCopyXs.Name = "btnCopyXs";
            this.btnCopyXs.Size = new System.Drawing.Size(77, 31);
            this.btnCopyXs.TabIndex = 14;
            this.btnCopyXs.Text = "Copy Xs";
            this.btnCopyXs.UseVisualStyleBackColor = true;
            this.btnCopyXs.Click += new System.EventHandler(this.btnCopyXs_Click);
            // 
            // btnCopyGoR
            // 
            this.btnCopyGoR.Location = new System.Drawing.Point(100, 74);
            this.btnCopyGoR.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCopyGoR.Name = "btnCopyGoR";
            this.btnCopyGoR.Size = new System.Drawing.Size(77, 31);
            this.btnCopyGoR.TabIndex = 13;
            this.btnCopyGoR.Text = "Copy ΔG/R";
            this.btnCopyGoR.UseVisualStyleBackColor = true;
            this.btnCopyGoR.Click += new System.EventHandler(this.btnCopyGoR_Click);
            // 
            // lblPeak
            // 
            this.lblPeak.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeak.Location = new System.Drawing.Point(10, 39);
            this.lblPeak.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPeak.Name = "lblPeak";
            this.lblPeak.Size = new System.Drawing.Size(77, 31);
            this.lblPeak.TabIndex = 12;
            this.lblPeak.Text = "12.345%";
            this.lblPeak.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(97, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Curve Data:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Peak ΔG/R";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(195, 39);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(96, 31);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "Save All Files";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSaveAndCopy
            // 
            this.btnSaveAndCopy.Location = new System.Drawing.Point(195, 74);
            this.btnSaveAndCopy.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveAndCopy.Name = "btnSaveAndCopy";
            this.btnSaveAndCopy.Size = new System.Drawing.Size(96, 31);
            this.btnSaveAndCopy.TabIndex = 21;
            this.btnSaveAndCopy.Text = "Save and Copy";
            this.btnSaveAndCopy.UseVisualStyleBackColor = true;
            this.btnSaveAndCopy.Click += new System.EventHandler(this.btnSaveAndCopy_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(192, 21);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "CSV Files:";
            // 
            // DataExportControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "DataExportControl";
            this.Size = new System.Drawing.Size(535, 130);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCopyXs;
        private System.Windows.Forms.Button btnCopyGoR;
        private System.Windows.Forms.Label lblPeak;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCopyPeak;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSaveAndCopy;
        private System.Windows.Forms.Button btnSave;
    }
}
