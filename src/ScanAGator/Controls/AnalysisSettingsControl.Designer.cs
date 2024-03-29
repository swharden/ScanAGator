﻿namespace ScanAGator.Controls
{
    partial class AnalysisSettingsControl
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
            this.pbGraphRaw = new System.Windows.Forms.PictureBox();
            this.nudBaseline1 = new System.Windows.Forms.NumericUpDown();
            this.nudBaseline2 = new System.Windows.Forms.NumericUpDown();
            this.nudStructure1 = new System.Windows.Forms.NumericUpDown();
            this.nudStructure2 = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbBaseline2 = new System.Windows.Forms.TrackBar();
            this.tbBaseline1 = new System.Windows.Forms.TrackBar();
            this.tbStructure2 = new System.Windows.Forms.TrackBar();
            this.tbStructure1 = new System.Windows.Forms.TrackBar();
            this.cbDisplay = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAutoBaseline = new System.Windows.Forms.Button();
            this.btnAutoStructure = new System.Windows.Forms.Button();
            this.cbAverage = new System.Windows.Forms.CheckBox();
            this.tbFrame = new System.Windows.Forms.TrackBar();
            this.lblFrame = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nudFilterPx = new System.Windows.Forms.NumericUpDown();
            this.lblFilterTime = new System.Windows.Forms.Label();
            this.cbFloor = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pbGraphRatio = new System.Windows.Forms.PictureBox();
            this.lblLineScanTime = new System.Windows.Forms.Label();
            this.cbFilter = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbGraphRaw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseline1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseline2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStructure1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStructure2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbBaseline2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbBaseline1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbStructure2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbStructure1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFilterPx)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGraphRatio)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbGraphRaw
            // 
            this.pbGraphRaw.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbGraphRaw.BackColor = System.Drawing.Color.Purple;
            this.pbGraphRaw.Location = new System.Drawing.Point(11, 14);
            this.pbGraphRaw.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbGraphRaw.Name = "pbGraphRaw";
            this.pbGraphRaw.Size = new System.Drawing.Size(1103, 136);
            this.pbGraphRaw.TabIndex = 12;
            this.pbGraphRaw.TabStop = false;
            // 
            // nudBaseline1
            // 
            this.nudBaseline1.Location = new System.Drawing.Point(98, 328);
            this.nudBaseline1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudBaseline1.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudBaseline1.Name = "nudBaseline1";
            this.nudBaseline1.Size = new System.Drawing.Size(75, 26);
            this.nudBaseline1.TabIndex = 15;
            this.nudBaseline1.Value = new decimal(new int[] {
            123,
            0,
            0,
            0});
            // 
            // nudBaseline2
            // 
            this.nudBaseline2.Location = new System.Drawing.Point(182, 328);
            this.nudBaseline2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudBaseline2.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudBaseline2.Name = "nudBaseline2";
            this.nudBaseline2.Size = new System.Drawing.Size(75, 26);
            this.nudBaseline2.TabIndex = 16;
            this.nudBaseline2.Value = new decimal(new int[] {
            123,
            0,
            0,
            0});
            // 
            // nudStructure1
            // 
            this.nudStructure1.Location = new System.Drawing.Point(98, 374);
            this.nudStructure1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudStructure1.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudStructure1.Name = "nudStructure1";
            this.nudStructure1.Size = new System.Drawing.Size(75, 26);
            this.nudStructure1.TabIndex = 18;
            this.nudStructure1.Value = new decimal(new int[] {
            123,
            0,
            0,
            0});
            // 
            // nudStructure2
            // 
            this.nudStructure2.Location = new System.Drawing.Point(182, 374);
            this.nudStructure2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudStructure2.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudStructure2.Name = "nudStructure2";
            this.nudStructure2.Size = new System.Drawing.Size(75, 26);
            this.nudStructure2.TabIndex = 19;
            this.nudStructure2.Value = new decimal(new int[] {
            123,
            0,
            0,
            0});
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Purple;
            this.panel1.Location = new System.Drawing.Point(11, 14);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1103, 540);
            this.panel1.TabIndex = 31;
            // 
            // tbBaseline2
            // 
            this.tbBaseline2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBaseline2.AutoSize = false;
            this.tbBaseline2.Location = new System.Drawing.Point(1163, -3);
            this.tbBaseline2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbBaseline2.Maximum = 50;
            this.tbBaseline2.Name = "tbBaseline2";
            this.tbBaseline2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbBaseline2.Size = new System.Drawing.Size(38, 549);
            this.tbBaseline2.TabIndex = 30;
            this.tbBaseline2.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.tbBaseline2.Value = 20;
            // 
            // tbBaseline1
            // 
            this.tbBaseline1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBaseline1.AutoSize = false;
            this.tbBaseline1.Location = new System.Drawing.Point(1122, -3);
            this.tbBaseline1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbBaseline1.Maximum = 50;
            this.tbBaseline1.Name = "tbBaseline1";
            this.tbBaseline1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbBaseline1.Size = new System.Drawing.Size(38, 549);
            this.tbBaseline1.TabIndex = 29;
            this.tbBaseline1.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.tbBaseline1.Value = 10;
            // 
            // tbStructure2
            // 
            this.tbStructure2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStructure2.AutoSize = false;
            this.tbStructure2.Location = new System.Drawing.Point(-6, 605);
            this.tbStructure2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbStructure2.Maximum = 50;
            this.tbStructure2.Name = "tbStructure2";
            this.tbStructure2.Size = new System.Drawing.Size(1120, 38);
            this.tbStructure2.TabIndex = 28;
            this.tbStructure2.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.tbStructure2.Value = 30;
            // 
            // tbStructure1
            // 
            this.tbStructure1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStructure1.AutoSize = false;
            this.tbStructure1.Location = new System.Drawing.Point(-6, 564);
            this.tbStructure1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbStructure1.Maximum = 50;
            this.tbStructure1.Name = "tbStructure1";
            this.tbStructure1.Size = new System.Drawing.Size(1120, 38);
            this.tbStructure1.TabIndex = 27;
            this.tbStructure1.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.tbStructure1.Value = 20;
            // 
            // cbDisplay
            // 
            this.cbDisplay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDisplay.FormattingEnabled = true;
            this.cbDisplay.Items.AddRange(new object[] {
            "Merge",
            "Green",
            "Red"});
            this.cbDisplay.Location = new System.Drawing.Point(404, 342);
            this.cbDisplay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbDisplay.Name = "cbDisplay";
            this.cbDisplay.Size = new System.Drawing.Size(112, 28);
            this.cbDisplay.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(394, 317);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 20);
            this.label3.TabIndex = 25;
            this.label3.Text = "Display";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 377);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 20);
            this.label2.TabIndex = 20;
            this.label2.Text = "Structure";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 332);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 20);
            this.label1.TabIndex = 17;
            this.label1.Text = "Baseline";
            // 
            // btnAutoBaseline
            // 
            this.btnAutoBaseline.Location = new System.Drawing.Point(266, 324);
            this.btnAutoBaseline.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAutoBaseline.Name = "btnAutoBaseline";
            this.btnAutoBaseline.Size = new System.Drawing.Size(92, 34);
            this.btnAutoBaseline.TabIndex = 22;
            this.btnAutoBaseline.Text = "Default";
            this.btnAutoBaseline.UseVisualStyleBackColor = true;
            this.btnAutoBaseline.Click += new System.EventHandler(this.btnAutoBaseline_Click);
            // 
            // btnAutoStructure
            // 
            this.btnAutoStructure.Location = new System.Drawing.Point(266, 369);
            this.btnAutoStructure.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAutoStructure.Name = "btnAutoStructure";
            this.btnAutoStructure.Size = new System.Drawing.Size(92, 34);
            this.btnAutoStructure.TabIndex = 21;
            this.btnAutoStructure.Text = "Auto";
            this.btnAutoStructure.UseVisualStyleBackColor = true;
            this.btnAutoStructure.Click += new System.EventHandler(this.btnAutoStructure_Click);
            // 
            // cbAverage
            // 
            this.cbAverage.AutoSize = true;
            this.cbAverage.Checked = true;
            this.cbAverage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAverage.Location = new System.Drawing.Point(271, 421);
            this.cbAverage.Name = "cbAverage";
            this.cbAverage.Size = new System.Drawing.Size(94, 24);
            this.cbAverage.TabIndex = 34;
            this.cbAverage.Text = "Average";
            this.cbAverage.UseVisualStyleBackColor = true;
            // 
            // tbFrame
            // 
            this.tbFrame.AutoSize = false;
            this.tbFrame.Location = new System.Drawing.Point(95, 417);
            this.tbFrame.Name = "tbFrame";
            this.tbFrame.Size = new System.Drawing.Size(162, 42);
            this.tbFrame.TabIndex = 35;
            // 
            // lblFrame
            // 
            this.lblFrame.AutoSize = true;
            this.lblFrame.Location = new System.Drawing.Point(7, 421);
            this.lblFrame.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFrame.Name = "lblFrame";
            this.lblFrame.Size = new System.Drawing.Size(81, 20);
            this.lblFrame.TabIndex = 36;
            this.lblFrame.Text = "Frame 3/3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(394, 386);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 20);
            this.label5.TabIndex = 37;
            this.label5.Text = "Filter";
            // 
            // nudFilterPx
            // 
            this.nudFilterPx.Location = new System.Drawing.Point(404, 411);
            this.nudFilterPx.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudFilterPx.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudFilterPx.Name = "nudFilterPx";
            this.nudFilterPx.Size = new System.Drawing.Size(75, 26);
            this.nudFilterPx.TabIndex = 38;
            this.nudFilterPx.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // lblFilterTime
            // 
            this.lblFilterTime.AutoSize = true;
            this.lblFilterTime.Location = new System.Drawing.Point(400, 442);
            this.lblFilterTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFilterTime.Name = "lblFilterTime";
            this.lblFilterTime.Size = new System.Drawing.Size(83, 20);
            this.lblFilterTime.TabIndex = 39;
            this.lblFilterTime.Text = "123.45 ms";
            // 
            // cbFloor
            // 
            this.cbFloor.AutoSize = true;
            this.cbFloor.Checked = true;
            this.cbFloor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFloor.Location = new System.Drawing.Point(271, 451);
            this.cbFloor.Name = "cbFloor";
            this.cbFloor.Size = new System.Drawing.Size(71, 24);
            this.cbFloor.TabIndex = 40;
            this.cbFloor.Text = "Floor";
            this.cbFloor.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 500F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1207, 1154);
            this.tableLayoutPanel1.TabIndex = 41;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pbGraphRatio);
            this.panel2.Controls.Add(this.lblLineScanTime);
            this.panel2.Controls.Add(this.cbFilter);
            this.panel2.Controls.Add(this.pbGraphRaw);
            this.panel2.Controls.Add(this.cbFloor);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lblFilterTime);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.nudFilterPx);
            this.panel2.Controls.Add(this.btnAutoBaseline);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.btnAutoStructure);
            this.panel2.Controls.Add(this.lblFrame);
            this.panel2.Controls.Add(this.nudStructure2);
            this.panel2.Controls.Add(this.tbFrame);
            this.panel2.Controls.Add(this.nudStructure1);
            this.panel2.Controls.Add(this.cbDisplay);
            this.panel2.Controls.Add(this.nudBaseline1);
            this.panel2.Controls.Add(this.cbAverage);
            this.panel2.Controls.Add(this.nudBaseline2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 657);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1201, 494);
            this.panel2.TabIndex = 0;
            // 
            // pbGraphRatio
            // 
            this.pbGraphRatio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbGraphRatio.BackColor = System.Drawing.Color.Purple;
            this.pbGraphRatio.Location = new System.Drawing.Point(11, 160);
            this.pbGraphRatio.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbGraphRatio.Name = "pbGraphRatio";
            this.pbGraphRatio.Size = new System.Drawing.Size(1103, 136);
            this.pbGraphRatio.TabIndex = 43;
            this.pbGraphRatio.TabStop = false;
            // 
            // lblLineScanTime
            // 
            this.lblLineScanTime.AutoSize = true;
            this.lblLineScanTime.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblLineScanTime.Location = new System.Drawing.Point(7, 462);
            this.lblLineScanTime.Name = "lblLineScanTime";
            this.lblLineScanTime.Size = new System.Drawing.Size(157, 20);
            this.lblLineScanTime.TabIndex = 42;
            this.lblLineScanTime.Text = "2021-02-03 15:16:17";
            // 
            // cbFilter
            // 
            this.cbFilter.AutoSize = true;
            this.cbFilter.Checked = true;
            this.cbFilter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFilter.Location = new System.Drawing.Point(486, 415);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(22, 21);
            this.cbFilter.TabIndex = 41;
            this.cbFilter.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.tbStructure1);
            this.panel3.Controls.Add(this.tbBaseline2);
            this.panel3.Controls.Add(this.tbStructure2);
            this.panel3.Controls.Add(this.tbBaseline1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1201, 648);
            this.panel3.TabIndex = 1;
            // 
            // AnalysisSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AnalysisSettingsControl";
            this.Size = new System.Drawing.Size(1207, 1154);
            ((System.ComponentModel.ISupportInitialize)(this.pbGraphRaw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseline1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseline2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStructure1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStructure2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbBaseline2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbBaseline1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbStructure2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbStructure1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFilterPx)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGraphRatio)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pbGraphRaw;
        private System.Windows.Forms.NumericUpDown nudBaseline1;
        private System.Windows.Forms.NumericUpDown nudBaseline2;
        private System.Windows.Forms.NumericUpDown nudStructure1;
        private System.Windows.Forms.NumericUpDown nudStructure2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbDisplay;
        private System.Windows.Forms.Button btnAutoBaseline;
        private System.Windows.Forms.Button btnAutoStructure;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar tbBaseline1;
        private System.Windows.Forms.TrackBar tbStructure1;
        private System.Windows.Forms.TrackBar tbBaseline2;
        private System.Windows.Forms.TrackBar tbStructure2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cbAverage;
        private System.Windows.Forms.TrackBar tbFrame;
        private System.Windows.Forms.Label lblFrame;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudFilterPx;
        private System.Windows.Forms.Label lblFilterTime;
        private System.Windows.Forms.CheckBox cbFloor;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox cbFilter;
        private System.Windows.Forms.Label lblLineScanTime;
        private System.Windows.Forms.PictureBox pbGraphRatio;
    }
}
