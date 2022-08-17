namespace ScanAGator.GUI
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
            this.pbGraph = new System.Windows.Forms.PictureBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.pbGraph)).BeginInit();
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
            this.SuspendLayout();
            // 
            // pbGraph
            // 
            this.pbGraph.BackColor = System.Drawing.Color.Purple;
            this.pbGraph.Location = new System.Drawing.Point(18, 600);
            this.pbGraph.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbGraph.Name = "pbGraph";
            this.pbGraph.Size = new System.Drawing.Size(450, 109);
            this.pbGraph.TabIndex = 12;
            this.pbGraph.TabStop = false;
            // 
            // nudBaseline1
            // 
            this.nudBaseline1.Location = new System.Drawing.Point(112, 736);
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
            this.nudBaseline2.Location = new System.Drawing.Point(196, 736);
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
            this.nudStructure1.Location = new System.Drawing.Point(112, 782);
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
            this.nudStructure2.Location = new System.Drawing.Point(196, 782);
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
            this.panel1.BackColor = System.Drawing.Color.Purple;
            this.panel1.Location = new System.Drawing.Point(18, 18);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 462);
            this.panel1.TabIndex = 31;
            // 
            // tbBaseline2
            // 
            this.tbBaseline2.AutoSize = false;
            this.tbBaseline2.Location = new System.Drawing.Point(524, 5);
            this.tbBaseline2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbBaseline2.Maximum = 50;
            this.tbBaseline2.Name = "tbBaseline2";
            this.tbBaseline2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbBaseline2.Size = new System.Drawing.Size(38, 491);
            this.tbBaseline2.TabIndex = 30;
            this.tbBaseline2.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.tbBaseline2.Value = 20;
            // 
            // tbBaseline1
            // 
            this.tbBaseline1.AutoSize = false;
            this.tbBaseline1.Location = new System.Drawing.Point(478, 5);
            this.tbBaseline1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbBaseline1.Maximum = 50;
            this.tbBaseline1.Name = "tbBaseline1";
            this.tbBaseline1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbBaseline1.Size = new System.Drawing.Size(38, 491);
            this.tbBaseline1.TabIndex = 29;
            this.tbBaseline1.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.tbBaseline1.Value = 10;
            // 
            // tbStructure2
            // 
            this.tbStructure2.AutoSize = false;
            this.tbStructure2.Location = new System.Drawing.Point(4, 537);
            this.tbStructure2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbStructure2.Maximum = 50;
            this.tbStructure2.Name = "tbStructure2";
            this.tbStructure2.Size = new System.Drawing.Size(482, 38);
            this.tbStructure2.TabIndex = 28;
            this.tbStructure2.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.tbStructure2.Value = 30;
            // 
            // tbStructure1
            // 
            this.tbStructure1.AutoSize = false;
            this.tbStructure1.Location = new System.Drawing.Point(4, 489);
            this.tbStructure1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbStructure1.Maximum = 50;
            this.tbStructure1.Name = "tbStructure1";
            this.tbStructure1.Size = new System.Drawing.Size(482, 38);
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
            this.cbDisplay.Location = new System.Drawing.Point(418, 750);
            this.cbDisplay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbDisplay.Name = "cbDisplay";
            this.cbDisplay.Size = new System.Drawing.Size(112, 28);
            this.cbDisplay.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(408, 725);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 20);
            this.label3.TabIndex = 25;
            this.label3.Text = "Display";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 785);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 20);
            this.label2.TabIndex = 20;
            this.label2.Text = "Structure";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 740);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 20);
            this.label1.TabIndex = 17;
            this.label1.Text = "Baseline";
            // 
            // btnAutoBaseline
            // 
            this.btnAutoBaseline.Location = new System.Drawing.Point(280, 732);
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
            this.btnAutoStructure.Location = new System.Drawing.Point(280, 777);
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
            this.cbAverage.Location = new System.Drawing.Point(285, 829);
            this.cbAverage.Name = "cbAverage";
            this.cbAverage.Size = new System.Drawing.Size(94, 24);
            this.cbAverage.TabIndex = 34;
            this.cbAverage.Text = "Average";
            this.cbAverage.UseVisualStyleBackColor = true;
            // 
            // tbFrame
            // 
            this.tbFrame.AutoSize = false;
            this.tbFrame.Location = new System.Drawing.Point(109, 825);
            this.tbFrame.Name = "tbFrame";
            this.tbFrame.Size = new System.Drawing.Size(162, 42);
            this.tbFrame.TabIndex = 35;
            // 
            // lblFrame
            // 
            this.lblFrame.AutoSize = true;
            this.lblFrame.Location = new System.Drawing.Point(21, 829);
            this.lblFrame.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFrame.Name = "lblFrame";
            this.lblFrame.Size = new System.Drawing.Size(81, 20);
            this.lblFrame.TabIndex = 36;
            this.lblFrame.Text = "Frame 3/3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(408, 794);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 20);
            this.label5.TabIndex = 37;
            this.label5.Text = "Filter";
            // 
            // nudFilterPx
            // 
            this.nudFilterPx.Location = new System.Drawing.Point(418, 819);
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
            12,
            0,
            0,
            0});
            // 
            // lblFilterTime
            // 
            this.lblFilterTime.AutoSize = true;
            this.lblFilterTime.Location = new System.Drawing.Point(414, 850);
            this.lblFilterTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFilterTime.Name = "lblFilterTime";
            this.lblFilterTime.Size = new System.Drawing.Size(83, 20);
            this.lblFilterTime.TabIndex = 39;
            this.lblFilterTime.Text = "123.45 ms";
            // 
            // AnalysisSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblFilterTime);
            this.Controls.Add(this.nudFilterPx);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblFrame);
            this.Controls.Add(this.tbFrame);
            this.Controls.Add(this.cbDisplay);
            this.Controls.Add(this.cbAverage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tbBaseline2);
            this.Controls.Add(this.tbBaseline1);
            this.Controls.Add(this.nudBaseline2);
            this.Controls.Add(this.tbStructure2);
            this.Controls.Add(this.nudBaseline1);
            this.Controls.Add(this.tbStructure1);
            this.Controls.Add(this.nudStructure1);
            this.Controls.Add(this.nudStructure2);
            this.Controls.Add(this.btnAutoStructure);
            this.Controls.Add(this.btnAutoBaseline);
            this.Controls.Add(this.pbGraph);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AnalysisSettingsControl";
            this.Size = new System.Drawing.Size(574, 890);
            ((System.ComponentModel.ISupportInitialize)(this.pbGraph)).EndInit();
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pbGraph;
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
    }
}
