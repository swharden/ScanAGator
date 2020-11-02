namespace ScanAGator.XmlTool
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.XmlFilePathLabel = new System.Windows.Forms.Label();
            this.XmlFileNameLabel = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.MiraPowerLabel = new System.Windows.Forms.TextBox();
            this.X3TunablePowerLabel = new System.Windows.Forms.TextBox();
            this.X3FixedPowerLabel = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LaserPowerGroupBox = new System.Windows.Forms.GroupBox();
            this.PmtGainGroupBox = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.PmtCh1Label = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.PmtCh2Label = new System.Windows.Forms.TextBox();
            this.ImageGroupBox = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ImageScaleLabel = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ImageWidthLabel = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ImageHeightLabel = new System.Windows.Forms.TextBox();
            this.SettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.DwellLabel = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ZoomLabel = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.LaserPowerGroupBox.SuspendLayout();
            this.PmtGainGroupBox.SuspendLayout();
            this.ImageGroupBox.SuspendLayout();
            this.SettingsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // XmlFilePathLabel
            // 
            this.XmlFilePathLabel.AutoSize = true;
            this.XmlFilePathLabel.Location = new System.Drawing.Point(14, 39);
            this.XmlFilePathLabel.Name = "XmlFilePathLabel";
            this.XmlFilePathLabel.Size = new System.Drawing.Size(257, 13);
            this.XmlFilePathLabel.TabIndex = 0;
            this.XmlFilePathLabel.Text = "drag/drop an XML file onto this window to get started";
            // 
            // XmlFileNameLabel
            // 
            this.XmlFileNameLabel.AutoSize = true;
            this.XmlFileNameLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XmlFileNameLabel.Location = new System.Drawing.Point(12, 9);
            this.XmlFileNameLabel.Name = "XmlFileNameLabel";
            this.XmlFileNameLabel.Size = new System.Drawing.Size(152, 30);
            this.XmlFileNameLabel.TabIndex = 1;
            this.XmlFileNameLabel.Text = "No File Loaded";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 178);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(776, 260);
            this.dataGridView1.TabIndex = 2;
            // 
            // MiraPowerLabel
            // 
            this.MiraPowerLabel.Location = new System.Drawing.Point(77, 16);
            this.MiraPowerLabel.Name = "MiraPowerLabel";
            this.MiraPowerLabel.ReadOnly = true;
            this.MiraPowerLabel.Size = new System.Drawing.Size(42, 20);
            this.MiraPowerLabel.TabIndex = 8;
            this.MiraPowerLabel.Text = "123";
            // 
            // X3TunablePowerLabel
            // 
            this.X3TunablePowerLabel.Location = new System.Drawing.Point(77, 42);
            this.X3TunablePowerLabel.Name = "X3TunablePowerLabel";
            this.X3TunablePowerLabel.ReadOnly = true;
            this.X3TunablePowerLabel.Size = new System.Drawing.Size(42, 20);
            this.X3TunablePowerLabel.TabIndex = 9;
            this.X3TunablePowerLabel.Text = "123";
            // 
            // X3FixedPowerLabel
            // 
            this.X3FixedPowerLabel.Location = new System.Drawing.Point(77, 68);
            this.X3FixedPowerLabel.Name = "X3FixedPowerLabel";
            this.X3FixedPowerLabel.ReadOnly = true;
            this.X3FixedPowerLabel.Size = new System.Drawing.Size(42, 20);
            this.X3FixedPowerLabel.TabIndex = 10;
            this.X3FixedPowerLabel.Text = "123";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Mira 810 nm";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "X3 Tunable";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "X3 1040 nm";
            // 
            // LaserPowerGroupBox
            // 
            this.LaserPowerGroupBox.Controls.Add(this.label1);
            this.LaserPowerGroupBox.Controls.Add(this.label3);
            this.LaserPowerGroupBox.Controls.Add(this.MiraPowerLabel);
            this.LaserPowerGroupBox.Controls.Add(this.label2);
            this.LaserPowerGroupBox.Controls.Add(this.X3TunablePowerLabel);
            this.LaserPowerGroupBox.Controls.Add(this.X3FixedPowerLabel);
            this.LaserPowerGroupBox.Location = new System.Drawing.Point(12, 66);
            this.LaserPowerGroupBox.Name = "LaserPowerGroupBox";
            this.LaserPowerGroupBox.Size = new System.Drawing.Size(130, 98);
            this.LaserPowerGroupBox.TabIndex = 14;
            this.LaserPowerGroupBox.TabStop = false;
            this.LaserPowerGroupBox.Text = "Laser Power";
            // 
            // PmtGainGroupBox
            // 
            this.PmtGainGroupBox.Controls.Add(this.label4);
            this.PmtGainGroupBox.Controls.Add(this.PmtCh1Label);
            this.PmtGainGroupBox.Controls.Add(this.label6);
            this.PmtGainGroupBox.Controls.Add(this.PmtCh2Label);
            this.PmtGainGroupBox.Location = new System.Drawing.Point(257, 66);
            this.PmtGainGroupBox.Name = "PmtGainGroupBox";
            this.PmtGainGroupBox.Size = new System.Drawing.Size(97, 72);
            this.PmtGainGroupBox.TabIndex = 15;
            this.PmtGainGroupBox.TabStop = false;
            this.PmtGainGroupBox.Text = "PMT Gain";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Ch1";
            // 
            // PmtCh1Label
            // 
            this.PmtCh1Label.Location = new System.Drawing.Point(41, 16);
            this.PmtCh1Label.Name = "PmtCh1Label";
            this.PmtCh1Label.ReadOnly = true;
            this.PmtCh1Label.Size = new System.Drawing.Size(42, 20);
            this.PmtCh1Label.TabIndex = 8;
            this.PmtCh1Label.Text = "123";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Ch2";
            // 
            // PmtCh2Label
            // 
            this.PmtCh2Label.Location = new System.Drawing.Point(41, 42);
            this.PmtCh2Label.Name = "PmtCh2Label";
            this.PmtCh2Label.ReadOnly = true;
            this.PmtCh2Label.Size = new System.Drawing.Size(42, 20);
            this.PmtCh2Label.TabIndex = 9;
            this.PmtCh2Label.Text = "123";
            // 
            // ImageGroupBox
            // 
            this.ImageGroupBox.Controls.Add(this.label8);
            this.ImageGroupBox.Controls.Add(this.ImageScaleLabel);
            this.ImageGroupBox.Controls.Add(this.label5);
            this.ImageGroupBox.Controls.Add(this.ImageWidthLabel);
            this.ImageGroupBox.Controls.Add(this.label7);
            this.ImageGroupBox.Controls.Add(this.ImageHeightLabel);
            this.ImageGroupBox.Location = new System.Drawing.Point(148, 66);
            this.ImageGroupBox.Name = "ImageGroupBox";
            this.ImageGroupBox.Size = new System.Drawing.Size(103, 98);
            this.ImageGroupBox.TabIndex = 16;
            this.ImageGroupBox.TabStop = false;
            this.ImageGroupBox.Text = "2D Image";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "µm/px";
            // 
            // ImageScaleLabel
            // 
            this.ImageScaleLabel.Location = new System.Drawing.Point(50, 68);
            this.ImageScaleLabel.Name = "ImageScaleLabel";
            this.ImageScaleLabel.ReadOnly = true;
            this.ImageScaleLabel.Size = new System.Drawing.Size(42, 20);
            this.ImageScaleLabel.TabIndex = 13;
            this.ImageScaleLabel.Text = "123";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Width";
            // 
            // ImageWidthLabel
            // 
            this.ImageWidthLabel.Location = new System.Drawing.Point(50, 16);
            this.ImageWidthLabel.Name = "ImageWidthLabel";
            this.ImageWidthLabel.ReadOnly = true;
            this.ImageWidthLabel.Size = new System.Drawing.Size(42, 20);
            this.ImageWidthLabel.TabIndex = 8;
            this.ImageWidthLabel.Text = "123";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Height";
            // 
            // ImageHeightLabel
            // 
            this.ImageHeightLabel.Location = new System.Drawing.Point(50, 42);
            this.ImageHeightLabel.Name = "ImageHeightLabel";
            this.ImageHeightLabel.ReadOnly = true;
            this.ImageHeightLabel.Size = new System.Drawing.Size(42, 20);
            this.ImageHeightLabel.TabIndex = 9;
            this.ImageHeightLabel.Text = "123";
            // 
            // SettingsGroupBox
            // 
            this.SettingsGroupBox.Controls.Add(this.label10);
            this.SettingsGroupBox.Controls.Add(this.DwellLabel);
            this.SettingsGroupBox.Controls.Add(this.label11);
            this.SettingsGroupBox.Controls.Add(this.ZoomLabel);
            this.SettingsGroupBox.Location = new System.Drawing.Point(360, 66);
            this.SettingsGroupBox.Name = "SettingsGroupBox";
            this.SettingsGroupBox.Size = new System.Drawing.Size(103, 72);
            this.SettingsGroupBox.TabIndex = 17;
            this.SettingsGroupBox.TabStop = false;
            this.SettingsGroupBox.Text = "Settings";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Dwell";
            // 
            // DwellLabel
            // 
            this.DwellLabel.Location = new System.Drawing.Point(50, 16);
            this.DwellLabel.Name = "DwellLabel";
            this.DwellLabel.ReadOnly = true;
            this.DwellLabel.Size = new System.Drawing.Size(42, 20);
            this.DwellLabel.TabIndex = 8;
            this.DwellLabel.Text = "123";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 45);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "Zoom";
            // 
            // ZoomLabel
            // 
            this.ZoomLabel.Location = new System.Drawing.Point(50, 42);
            this.ZoomLabel.Name = "ZoomLabel";
            this.ZoomLabel.ReadOnly = true;
            this.ZoomLabel.Size = new System.Drawing.Size(42, 20);
            this.ZoomLabel.TabIndex = 9;
            this.ZoomLabel.Text = "123";
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SettingsGroupBox);
            this.Controls.Add(this.ImageGroupBox);
            this.Controls.Add(this.PmtGainGroupBox);
            this.Controls.Add(this.LaserPowerGroupBox);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.XmlFileNameLabel);
            this.Controls.Add(this.XmlFilePathLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "ScanAGator - XML Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.LaserPowerGroupBox.ResumeLayout(false);
            this.LaserPowerGroupBox.PerformLayout();
            this.PmtGainGroupBox.ResumeLayout(false);
            this.PmtGainGroupBox.PerformLayout();
            this.ImageGroupBox.ResumeLayout(false);
            this.ImageGroupBox.PerformLayout();
            this.SettingsGroupBox.ResumeLayout(false);
            this.SettingsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label XmlFilePathLabel;
        private System.Windows.Forms.Label XmlFileNameLabel;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox MiraPowerLabel;
        private System.Windows.Forms.TextBox X3TunablePowerLabel;
        private System.Windows.Forms.TextBox X3FixedPowerLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox LaserPowerGroupBox;
        private System.Windows.Forms.GroupBox PmtGainGroupBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox PmtCh1Label;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox PmtCh2Label;
        private System.Windows.Forms.GroupBox ImageGroupBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox ImageScaleLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ImageWidthLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox ImageHeightLabel;
        private System.Windows.Forms.GroupBox SettingsGroupBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox DwellLabel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox ZoomLabel;
    }
}

