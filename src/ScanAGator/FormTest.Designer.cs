namespace ScanAGator
{
    partial class FormTest
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.calcGR = new System.Windows.Forms.RadioButton();
            this.calcR = new System.Windows.Forms.RadioButton();
            this.calcG = new System.Windows.Forms.RadioButton();
            this.calcGoR = new System.Windows.Forms.RadioButton();
            this.calcDeltaGoR = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.nudStructure2 = new System.Windows.Forms.NumericUpDown();
            this.nudStructure1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nudBaseline2 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nudBaseline1 = new System.Windows.Forms.NumericUpDown();
            this.btnFolderRefresh = new System.Windows.Forms.Button();
            this.btnFolderSelect = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.groupBox8.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStructure2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStructure1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseline2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseline1)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(191, 433);
            this.listBox1.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox4);
            this.groupBox1.Location = new System.Drawing.Point(209, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(266, 278);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Reference";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Location = new System.Drawing.Point(486, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(447, 171);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Linescan (image)";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Gray;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(441, 152);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pictureBox2);
            this.groupBox3.Location = new System.Drawing.Point(486, 189);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(447, 171);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "dG/R (graph)";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Gray;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Location = new System.Drawing.Point(3, 16);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(441, 152);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.pictureBox3);
            this.groupBox4.Location = new System.Drawing.Point(486, 366);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(447, 171);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "G and R (graph)";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Gray;
            this.pictureBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox3.Location = new System.Drawing.Point(3, 16);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(441, 152);
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Gray;
            this.pictureBox4.Location = new System.Drawing.Point(6, 19);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(250, 250);
            this.pictureBox4.TabIndex = 1;
            this.pictureBox4.TabStop = false;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.calcGR);
            this.groupBox8.Controls.Add(this.calcR);
            this.groupBox8.Controls.Add(this.calcG);
            this.groupBox8.Controls.Add(this.calcGoR);
            this.groupBox8.Controls.Add(this.calcDeltaGoR);
            this.groupBox8.Location = new System.Drawing.Point(215, 381);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(195, 73);
            this.groupBox8.TabIndex = 28;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Calculation";
            // 
            // calcGR
            // 
            this.calcGR.AutoSize = true;
            this.calcGR.Location = new System.Drawing.Point(118, 23);
            this.calcGR.Name = "calcGR";
            this.calcGR.Size = new System.Drawing.Size(65, 17);
            this.calcGR.TabIndex = 4;
            this.calcGR.TabStop = true;
            this.calcGR.Text = "G and R";
            this.calcGR.UseVisualStyleBackColor = true;
            // 
            // calcR
            // 
            this.calcR.AutoSize = true;
            this.calcR.Location = new System.Drawing.Point(79, 46);
            this.calcR.Name = "calcR";
            this.calcR.Size = new System.Drawing.Size(33, 17);
            this.calcR.TabIndex = 3;
            this.calcR.Text = "R";
            this.calcR.UseVisualStyleBackColor = true;
            // 
            // calcG
            // 
            this.calcG.AutoSize = true;
            this.calcG.Location = new System.Drawing.Point(79, 23);
            this.calcG.Name = "calcG";
            this.calcG.Size = new System.Drawing.Size(33, 17);
            this.calcG.TabIndex = 2;
            this.calcG.Text = "G";
            this.calcG.UseVisualStyleBackColor = true;
            // 
            // calcGoR
            // 
            this.calcGoR.AutoSize = true;
            this.calcGoR.Location = new System.Drawing.Point(15, 46);
            this.calcGoR.Name = "calcGoR";
            this.calcGoR.Size = new System.Drawing.Size(46, 17);
            this.calcGoR.TabIndex = 1;
            this.calcGoR.Text = "G/R";
            this.calcGoR.UseVisualStyleBackColor = true;
            // 
            // calcDeltaGoR
            // 
            this.calcDeltaGoR.AutoSize = true;
            this.calcDeltaGoR.Checked = true;
            this.calcDeltaGoR.Location = new System.Drawing.Point(15, 23);
            this.calcDeltaGoR.Name = "calcDeltaGoR";
            this.calcDeltaGoR.Size = new System.Drawing.Size(58, 17);
            this.calcDeltaGoR.TabIndex = 0;
            this.calcDeltaGoR.TabStop = true;
            this.calcDeltaGoR.Text = "d[G/R]";
            this.calcDeltaGoR.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button4);
            this.groupBox5.Controls.Add(this.button3);
            this.groupBox5.Controls.Add(this.button2);
            this.groupBox5.Controls.Add(this.button1);
            this.groupBox5.Location = new System.Drawing.Point(215, 460);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(166, 79);
            this.groupBox5.TabIndex = 27;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Analysis";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(6, 48);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "Reset";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(87, 48);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(73, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Copy";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(87, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(73, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Load";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.nudStructure2);
            this.groupBox6.Controls.Add(this.nudStructure1);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.nudBaseline2);
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Controls.Add(this.nudBaseline1);
            this.groupBox6.Location = new System.Drawing.Point(215, 296);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(224, 79);
            this.groupBox6.TabIndex = 26;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Settings";
            // 
            // nudStructure2
            // 
            this.nudStructure2.Location = new System.Drawing.Point(151, 45);
            this.nudStructure2.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudStructure2.Name = "nudStructure2";
            this.nudStructure2.Size = new System.Drawing.Size(60, 20);
            this.nudStructure2.TabIndex = 18;
            this.nudStructure2.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // nudStructure1
            // 
            this.nudStructure1.Location = new System.Drawing.Point(85, 45);
            this.nudStructure1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudStructure1.Name = "nudStructure1";
            this.nudStructure1.Size = new System.Drawing.Size(60, 20);
            this.nudStructure1.TabIndex = 17;
            this.nudStructure1.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Structure (px):";
            // 
            // nudBaseline2
            // 
            this.nudBaseline2.Location = new System.Drawing.Point(151, 19);
            this.nudBaseline2.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudBaseline2.Name = "nudBaseline2";
            this.nudBaseline2.Size = new System.Drawing.Size(60, 20);
            this.nudBaseline2.TabIndex = 14;
            this.nudBaseline2.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Baseline (px):";
            // 
            // nudBaseline1
            // 
            this.nudBaseline1.Location = new System.Drawing.Point(85, 19);
            this.nudBaseline1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudBaseline1.Name = "nudBaseline1";
            this.nudBaseline1.Size = new System.Drawing.Size(60, 20);
            this.nudBaseline1.TabIndex = 13;
            this.nudBaseline1.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // btnFolderRefresh
            // 
            this.btnFolderRefresh.Location = new System.Drawing.Point(118, 460);
            this.btnFolderRefresh.Name = "btnFolderRefresh";
            this.btnFolderRefresh.Size = new System.Drawing.Size(85, 23);
            this.btnFolderRefresh.TabIndex = 30;
            this.btnFolderRefresh.Text = "refresh";
            this.btnFolderRefresh.UseVisualStyleBackColor = true;
            // 
            // btnFolderSelect
            // 
            this.btnFolderSelect.Location = new System.Drawing.Point(12, 460);
            this.btnFolderSelect.Name = "btnFolderSelect";
            this.btnFolderSelect.Size = new System.Drawing.Size(100, 23);
            this.btnFolderSelect.TabIndex = 29;
            this.btnFolderSelect.Text = "select folder";
            this.btnFolderSelect.UseVisualStyleBackColor = true;
            // 
            // FormTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 543);
            this.Controls.Add(this.btnFolderRefresh);
            this.Controls.Add(this.btnFolderSelect);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listBox1);
            this.Name = "FormTest";
            this.Text = "Form2";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStructure2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStructure1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseline2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseline1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.RadioButton calcGR;
        private System.Windows.Forms.RadioButton calcR;
        private System.Windows.Forms.RadioButton calcG;
        private System.Windows.Forms.RadioButton calcGoR;
        private System.Windows.Forms.RadioButton calcDeltaGoR;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.NumericUpDown nudStructure2;
        private System.Windows.Forms.NumericUpDown nudStructure1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudBaseline2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudBaseline1;
        private System.Windows.Forms.Button btnFolderRefresh;
        private System.Windows.Forms.Button btnFolderSelect;
    }
}