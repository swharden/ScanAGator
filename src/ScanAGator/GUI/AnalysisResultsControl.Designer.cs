namespace ScanAGator.GUI
{
    partial class AnalysisResultsControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.formsPlot1 = new ScottPlot.FormsPlot();
            this.formsPlot2 = new ScottPlot.FormsPlot();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLaunch = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCopyPeak = new System.Windows.Forms.Button();
            this.lblPeak = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.formsPlot1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.formsPlot2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(786, 890);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // formsPlot1
            // 
            this.formsPlot1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlot1.Location = new System.Drawing.Point(5, 4);
            this.formsPlot1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(776, 397);
            this.formsPlot1.TabIndex = 0;
            // 
            // formsPlot2
            // 
            this.formsPlot2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlot2.Location = new System.Drawing.Point(5, 409);
            this.formsPlot2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.formsPlot2.Name = "formsPlot2";
            this.formsPlot2.Size = new System.Drawing.Size(776, 397);
            this.formsPlot2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnLaunch);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnCopyPeak);
            this.panel1.Controls.Add(this.lblPeak);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 813);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(780, 74);
            this.panel1.TabIndex = 2;
            // 
            // btnLaunch
            // 
            this.btnLaunch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLaunch.Location = new System.Drawing.Point(682, 15);
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Size = new System.Drawing.Size(84, 47);
            this.btnLaunch.TabIndex = 4;
            this.btnLaunch.Text = "Launch";
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.Click += new System.EventHandler(this.btnLaunch_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(522, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(149, 47);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save and Copy";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCopyPeak
            // 
            this.btnCopyPeak.Location = new System.Drawing.Point(137, 15);
            this.btnCopyPeak.Name = "btnCopyPeak";
            this.btnCopyPeak.Size = new System.Drawing.Size(116, 47);
            this.btnCopyPeak.TabIndex = 2;
            this.btnCopyPeak.Text = "Copy Peak";
            this.btnCopyPeak.UseVisualStyleBackColor = true;
            this.btnCopyPeak.Click += new System.EventHandler(this.btnCopyPeak_Click);
            // 
            // lblPeak
            // 
            this.lblPeak.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeak.Location = new System.Drawing.Point(3, 14);
            this.lblPeak.Name = "lblPeak";
            this.lblPeak.Size = new System.Drawing.Size(128, 28);
            this.lblPeak.TabIndex = 1;
            this.lblPeak.Text = "123.45%";
            this.lblPeak.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Peak ΔF/F";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AnalysisResultsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AnalysisResultsControl";
            this.Size = new System.Drawing.Size(786, 890);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ScottPlot.FormsPlot formsPlot1;
        private ScottPlot.FormsPlot formsPlot2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnLaunch;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCopyPeak;
        private System.Windows.Forms.Label lblPeak;
        private System.Windows.Forms.Label label1;
    }
}
