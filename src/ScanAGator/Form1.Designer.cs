namespace ScanAGator
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rescanFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbFolders = new System.Windows.Forms.ListBox();
            this.pbLinescan = new System.Windows.Forms.PictureBox();
            this.gbLinescan = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.pbRef = new System.Windows.Forms.PictureBox();
            this.scottPlotUC1 = new ScottPlotDev2.ScottPlotUC();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudStructure2 = new System.Windows.Forms.NumericUpDown();
            this.nudStructure1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nudBaseline2 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nudBaseline1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.tbInformation = new System.Windows.Forms.TextBox();
            this.btnFolderSelect = new System.Windows.Forms.Button();
            this.btnFolderRefresh = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.calcDeltaGoR = new System.Windows.Forms.RadioButton();
            this.calcGoR = new System.Windows.Forms.RadioButton();
            this.calcG = new System.Windows.Forms.RadioButton();
            this.calcR = new System.Windows.Forms.RadioButton();
            this.calcGR = new System.Windows.Forms.RadioButton();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLinescan)).BeginInit();
            this.gbLinescan.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRef)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStructure2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStructure1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseline2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseline1)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 672);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1042, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(118, 17);
            this.lblStatus.Text = "toolStripStatusLabel1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1042, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setFolderToolStripMenuItem,
            this.rescanFolderToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // setFolderToolStripMenuItem
            // 
            this.setFolderToolStripMenuItem.Name = "setFolderToolStripMenuItem";
            this.setFolderToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.setFolderToolStripMenuItem.Text = "Set Folder";
            // 
            // rescanFolderToolStripMenuItem
            // 
            this.rescanFolderToolStripMenuItem.Name = "rescanFolderToolStripMenuItem";
            this.rescanFolderToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.rescanFolderToolStripMenuItem.Text = "Rescan Folder";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(144, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.documentationToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // documentationToolStripMenuItem
            // 
            this.documentationToolStripMenuItem.Name = "documentationToolStripMenuItem";
            this.documentationToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.documentationToolStripMenuItem.Text = "Documentation";
            this.documentationToolStripMenuItem.Click += new System.EventHandler(this.documentationToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // lbFolders
            // 
            this.lbFolders.FormattingEnabled = true;
            this.lbFolders.Location = new System.Drawing.Point(12, 27);
            this.lbFolders.Name = "lbFolders";
            this.lbFolders.Size = new System.Drawing.Size(199, 472);
            this.lbFolders.TabIndex = 3;
            this.lbFolders.SelectedIndexChanged += new System.EventHandler(this.lbFolders_SelectedIndexChanged);
            this.lbFolders.DoubleClick += new System.EventHandler(this.lbFolders_DoubleClick);
            // 
            // pbLinescan
            // 
            this.pbLinescan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pbLinescan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbLinescan.Location = new System.Drawing.Point(6, 19);
            this.pbLinescan.Name = "pbLinescan";
            this.pbLinescan.Size = new System.Drawing.Size(250, 250);
            this.pbLinescan.TabIndex = 5;
            this.pbLinescan.TabStop = false;
            // 
            // gbLinescan
            // 
            this.gbLinescan.Controls.Add(this.pbLinescan);
            this.gbLinescan.Location = new System.Drawing.Point(485, 27);
            this.gbLinescan.Name = "gbLinescan";
            this.gbLinescan.Size = new System.Drawing.Size(262, 276);
            this.gbLinescan.TabIndex = 6;
            this.gbLinescan.TabStop = false;
            this.gbLinescan.Text = "Linescan (G)";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.pbRef);
            this.groupBox4.Location = new System.Drawing.Point(217, 27);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(262, 276);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Reference";
            // 
            // pbRef
            // 
            this.pbRef.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pbRef.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbRef.Location = new System.Drawing.Point(6, 19);
            this.pbRef.Name = "pbRef";
            this.pbRef.Size = new System.Drawing.Size(250, 250);
            this.pbRef.TabIndex = 5;
            this.pbRef.TabStop = false;
            // 
            // scottPlotUC1
            // 
            this.scottPlotUC1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.scottPlotUC1.Location = new System.Drawing.Point(217, 309);
            this.scottPlotUC1.Name = "scottPlotUC1";
            this.scottPlotUC1.Size = new System.Drawing.Size(530, 350);
            this.scottPlotUC1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(753, 276);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(166, 79);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Analysis";
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudStructure2);
            this.groupBox1.Controls.Add(this.nudStructure1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.nudBaseline2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.nudBaseline1);
            this.groupBox1.Location = new System.Drawing.Point(753, 112);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 79);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
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
            this.nudStructure2.ValueChanged += new System.EventHandler(this.nudStructure2_ValueChanged);
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
            this.nudStructure1.ValueChanged += new System.EventHandler(this.nudStructure1_ValueChanged);
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
            this.nudBaseline2.ValueChanged += new System.EventHandler(this.nudBaseline2_ValueChanged);
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
            this.nudBaseline1.ValueChanged += new System.EventHandler(this.nudBaseline1_ValueChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.tbInformation);
            this.groupBox7.Location = new System.Drawing.Point(12, 530);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(199, 129);
            this.groupBox7.TabIndex = 22;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Information";
            // 
            // tbInformation
            // 
            this.tbInformation.BackColor = System.Drawing.SystemColors.Control;
            this.tbInformation.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbInformation.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbInformation.Location = new System.Drawing.Point(3, 16);
            this.tbInformation.Multiline = true;
            this.tbInformation.Name = "tbInformation";
            this.tbInformation.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbInformation.Size = new System.Drawing.Size(193, 110);
            this.tbInformation.TabIndex = 0;
            this.tbInformation.WordWrap = false;
            // 
            // btnFolderSelect
            // 
            this.btnFolderSelect.Location = new System.Drawing.Point(12, 501);
            this.btnFolderSelect.Name = "btnFolderSelect";
            this.btnFolderSelect.Size = new System.Drawing.Size(100, 23);
            this.btnFolderSelect.TabIndex = 23;
            this.btnFolderSelect.Text = "select folder";
            this.btnFolderSelect.UseVisualStyleBackColor = true;
            this.btnFolderSelect.Click += new System.EventHandler(this.btnFolderSelect_Click);
            // 
            // btnFolderRefresh
            // 
            this.btnFolderRefresh.Location = new System.Drawing.Point(118, 501);
            this.btnFolderRefresh.Name = "btnFolderRefresh";
            this.btnFolderRefresh.Size = new System.Drawing.Size(93, 23);
            this.btnFolderRefresh.TabIndex = 24;
            this.btnFolderRefresh.Text = "refresh";
            this.btnFolderRefresh.UseVisualStyleBackColor = true;
            this.btnFolderRefresh.Click += new System.EventHandler(this.btnFolderRefresh_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.calcGR);
            this.groupBox8.Controls.Add(this.calcR);
            this.groupBox8.Controls.Add(this.calcG);
            this.groupBox8.Controls.Add(this.calcGoR);
            this.groupBox8.Controls.Add(this.calcDeltaGoR);
            this.groupBox8.Location = new System.Drawing.Point(753, 197);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(195, 73);
            this.groupBox8.TabIndex = 25;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Calculation";
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
            this.calcDeltaGoR.CheckedChanged += new System.EventHandler(this.calcDeltaGoR_CheckedChanged);
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
            this.calcGoR.CheckedChanged += new System.EventHandler(this.calcGoR_CheckedChanged);
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
            this.calcG.CheckedChanged += new System.EventHandler(this.calcG_CheckedChanged);
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
            this.calcR.CheckedChanged += new System.EventHandler(this.calcR_CheckedChanged);
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
            this.calcGR.CheckedChanged += new System.EventHandler(this.calcGR_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 694);
            this.Controls.Add(this.scottPlotUC1);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.btnFolderRefresh);
            this.Controls.Add(this.btnFolderSelect);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.gbLinescan);
            this.Controls.Add(this.lbFolders);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Scan-A-Gator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLinescan)).EndInit();
            this.gbLinescan.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbRef)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStructure2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStructure1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseline2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseline1)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rescanFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ListBox lbFolders;
        private System.Windows.Forms.PictureBox pbLinescan;
        private System.Windows.Forms.GroupBox gbLinescan;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.PictureBox pbRef;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nudStructure2;
        private System.Windows.Forms.NumericUpDown nudStructure1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudBaseline2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudBaseline1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox tbInformation;
        private System.Windows.Forms.Button btnFolderSelect;
        private System.Windows.Forms.Button btnFolderRefresh;
        private ScottPlotDev2.ScottPlotUC scottPlotUC1;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.RadioButton calcDeltaGoR;
        private System.Windows.Forms.RadioButton calcR;
        private System.Windows.Forms.RadioButton calcG;
        private System.Windows.Forms.RadioButton calcGoR;
        private System.Windows.Forms.RadioButton calcGR;
    }
}

