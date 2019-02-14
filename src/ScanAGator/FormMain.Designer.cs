namespace ScanAGator
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.pbRef = new System.Windows.Forms.PictureBox();
            this.pbData = new System.Windows.Forms.PictureBox();
            this.tbBaseline1 = new System.Windows.Forms.TrackBar();
            this.gbBaseline = new System.Windows.Forms.GroupBox();
            this.tbBaseline2 = new System.Windows.Forms.TrackBar();
            this.gbStructure = new System.Windows.Forms.GroupBox();
            this.tbStructure2 = new System.Windows.Forms.TrackBar();
            this.tbStructure1 = new System.Windows.Forms.TrackBar();
            this.scottPlotUC1 = new ScottPlotDev2.ScottPlotUC();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioG = new System.Windows.Forms.RadioButton();
            this.radioDeltaG = new System.Windows.Forms.RadioButton();
            this.radioPMT = new System.Windows.Forms.RadioButton();
            this.radioGoR = new System.Windows.Forms.RadioButton();
            this.radioDeltaGoR = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblFilterMs = new System.Windows.Forms.Label();
            this.nudFilter = new System.Windows.Forms.NumericUpDown();
            this.gbPeak = new System.Windows.Forms.GroupBox();
            this.lblPeak = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioImageG = new System.Windows.Forms.RadioButton();
            this.radioImageR = new System.Windows.Forms.RadioButton();
            this.gbFrame = new System.Windows.Forms.GroupBox();
            this.cbFrameAverage = new System.Windows.Forms.CheckBox();
            this.nudFrame = new System.Windows.Forms.NumericUpDown();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnCopyCurves = new System.Windows.Forms.Button();
            this.btnCopyCurve = new System.Windows.Forms.Button();
            this.btnCopyPeak = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshFoldersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.structureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoselectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.summaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.multilinescanSummaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pbRef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbBaseline1)).BeginInit();
            this.gbBaseline.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbBaseline2)).BeginInit();
            this.gbStructure.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbStructure2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbStructure1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFilter)).BeginInit();
            this.gbPeak.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.gbFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrame)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(12, 27);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(474, 250);
            this.treeView1.TabIndex = 0;
            this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // pbRef
            // 
            this.pbRef.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbRef.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbRef.Location = new System.Drawing.Point(492, 27);
            this.pbRef.Name = "pbRef";
            this.pbRef.Size = new System.Drawing.Size(375, 375);
            this.pbRef.TabIndex = 2;
            this.pbRef.TabStop = false;
            // 
            // pbData
            // 
            this.pbData.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbData.Location = new System.Drawing.Point(873, 27);
            this.pbData.Name = "pbData";
            this.pbData.Size = new System.Drawing.Size(375, 375);
            this.pbData.TabIndex = 3;
            this.pbData.TabStop = false;
            // 
            // tbBaseline1
            // 
            this.tbBaseline1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBaseline1.Location = new System.Drawing.Point(6, 19);
            this.tbBaseline1.Name = "tbBaseline1";
            this.tbBaseline1.Size = new System.Drawing.Size(363, 45);
            this.tbBaseline1.TabIndex = 6;
            this.tbBaseline1.Scroll += new System.EventHandler(this.tbBaseline1_Scroll);
            // 
            // gbBaseline
            // 
            this.gbBaseline.Controls.Add(this.tbBaseline2);
            this.gbBaseline.Controls.Add(this.tbBaseline1);
            this.gbBaseline.Location = new System.Drawing.Point(492, 408);
            this.gbBaseline.Name = "gbBaseline";
            this.gbBaseline.Size = new System.Drawing.Size(375, 118);
            this.gbBaseline.TabIndex = 7;
            this.gbBaseline.TabStop = false;
            this.gbBaseline.Text = "Baseline";
            // 
            // tbBaseline2
            // 
            this.tbBaseline2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBaseline2.Location = new System.Drawing.Point(6, 70);
            this.tbBaseline2.Name = "tbBaseline2";
            this.tbBaseline2.Size = new System.Drawing.Size(363, 45);
            this.tbBaseline2.TabIndex = 7;
            this.tbBaseline2.Scroll += new System.EventHandler(this.tbBaseline2_Scroll);
            // 
            // gbStructure
            // 
            this.gbStructure.Controls.Add(this.tbStructure2);
            this.gbStructure.Controls.Add(this.tbStructure1);
            this.gbStructure.Location = new System.Drawing.Point(873, 408);
            this.gbStructure.Name = "gbStructure";
            this.gbStructure.Size = new System.Drawing.Size(375, 118);
            this.gbStructure.TabIndex = 8;
            this.gbStructure.TabStop = false;
            this.gbStructure.Text = "Structure";
            // 
            // tbStructure2
            // 
            this.tbStructure2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStructure2.Location = new System.Drawing.Point(6, 64);
            this.tbStructure2.Name = "tbStructure2";
            this.tbStructure2.Size = new System.Drawing.Size(363, 45);
            this.tbStructure2.TabIndex = 7;
            this.tbStructure2.Scroll += new System.EventHandler(this.tbStructure2_Scroll);
            // 
            // tbStructure1
            // 
            this.tbStructure1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStructure1.Location = new System.Drawing.Point(6, 19);
            this.tbStructure1.Name = "tbStructure1";
            this.tbStructure1.Size = new System.Drawing.Size(363, 45);
            this.tbStructure1.TabIndex = 6;
            this.tbStructure1.Scroll += new System.EventHandler(this.tbStructure1_Scroll);
            // 
            // scottPlotUC1
            // 
            this.scottPlotUC1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.scottPlotUC1.Location = new System.Drawing.Point(12, 280);
            this.scottPlotUC1.Name = "scottPlotUC1";
            this.scottPlotUC1.Size = new System.Drawing.Size(474, 243);
            this.scottPlotUC1.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioG);
            this.groupBox1.Controls.Add(this.radioDeltaG);
            this.groupBox1.Controls.Add(this.radioPMT);
            this.groupBox1.Controls.Add(this.radioGoR);
            this.groupBox1.Controls.Add(this.radioDeltaGoR);
            this.groupBox1.Location = new System.Drawing.Point(12, 529);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(114, 107);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Graph";
            // 
            // radioG
            // 
            this.radioG.AutoSize = true;
            this.radioG.Location = new System.Drawing.Point(65, 51);
            this.radioG.Name = "radioG";
            this.radioG.Size = new System.Drawing.Size(33, 17);
            this.radioG.TabIndex = 4;
            this.radioG.Text = "G";
            this.radioG.UseVisualStyleBackColor = true;
            this.radioG.CheckedChanged += new System.EventHandler(this.radioG_CheckedChanged);
            // 
            // radioDeltaG
            // 
            this.radioDeltaG.AutoSize = true;
            this.radioDeltaG.Location = new System.Drawing.Point(65, 23);
            this.radioDeltaG.Name = "radioDeltaG";
            this.radioDeltaG.Size = new System.Drawing.Size(39, 17);
            this.radioDeltaG.TabIndex = 3;
            this.radioDeltaG.Text = "dG";
            this.radioDeltaG.UseVisualStyleBackColor = true;
            this.radioDeltaG.CheckedChanged += new System.EventHandler(this.radioDeltaG_CheckedChanged);
            // 
            // radioPMT
            // 
            this.radioPMT.AutoSize = true;
            this.radioPMT.Location = new System.Drawing.Point(7, 79);
            this.radioPMT.Name = "radioPMT";
            this.radioPMT.Size = new System.Drawing.Size(41, 17);
            this.radioPMT.TabIndex = 2;
            this.radioPMT.Text = "GR";
            this.radioPMT.UseVisualStyleBackColor = true;
            this.radioPMT.CheckedChanged += new System.EventHandler(this.radioPMT_CheckedChanged);
            // 
            // radioGoR
            // 
            this.radioGoR.AutoSize = true;
            this.radioGoR.Location = new System.Drawing.Point(7, 51);
            this.radioGoR.Name = "radioGoR";
            this.radioGoR.Size = new System.Drawing.Size(46, 17);
            this.radioGoR.TabIndex = 1;
            this.radioGoR.Text = "G/R";
            this.radioGoR.UseVisualStyleBackColor = true;
            this.radioGoR.CheckedChanged += new System.EventHandler(this.radioGoR_CheckedChanged);
            // 
            // radioDeltaGoR
            // 
            this.radioDeltaGoR.AutoSize = true;
            this.radioDeltaGoR.Checked = true;
            this.radioDeltaGoR.Location = new System.Drawing.Point(7, 23);
            this.radioDeltaGoR.Name = "radioDeltaGoR";
            this.radioDeltaGoR.Size = new System.Drawing.Size(52, 17);
            this.radioDeltaGoR.TabIndex = 0;
            this.radioDeltaGoR.Text = "dG/R";
            this.radioDeltaGoR.UseVisualStyleBackColor = true;
            this.radioDeltaGoR.CheckedChanged += new System.EventHandler(this.radioDeltaGoR_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblFilterMs);
            this.groupBox2.Controls.Add(this.nudFilter);
            this.groupBox2.Location = new System.Drawing.Point(966, 538);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(87, 66);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter (px)";
            // 
            // lblFilterMs
            // 
            this.lblFilterMs.AutoSize = true;
            this.lblFilterMs.Location = new System.Drawing.Point(6, 44);
            this.lblFilterMs.Name = "lblFilterMs";
            this.lblFilterMs.Size = new System.Drawing.Size(56, 13);
            this.lblFilterMs.TabIndex = 1;
            this.lblFilterMs.Text = "123.12 ms";
            // 
            // nudFilter
            // 
            this.nudFilter.Location = new System.Drawing.Point(6, 19);
            this.nudFilter.Name = "nudFilter";
            this.nudFilter.Size = new System.Drawing.Size(74, 20);
            this.nudFilter.TabIndex = 0;
            this.nudFilter.ValueChanged += new System.EventHandler(this.nudFilter_ValueChanged);
            // 
            // gbPeak
            // 
            this.gbPeak.Controls.Add(this.lblPeak);
            this.gbPeak.Location = new System.Drawing.Point(132, 529);
            this.gbPeak.Name = "gbPeak";
            this.gbPeak.Size = new System.Drawing.Size(182, 66);
            this.gbPeak.TabIndex = 11;
            this.gbPeak.TabStop = false;
            this.gbPeak.Text = "Peak Value";
            // 
            // lblPeak
            // 
            this.lblPeak.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPeak.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeak.Location = new System.Drawing.Point(3, 16);
            this.lblPeak.Name = "lblPeak";
            this.lblPeak.Size = new System.Drawing.Size(176, 47);
            this.lblPeak.TabIndex = 0;
            this.lblPeak.Text = "234.56%";
            this.lblPeak.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button4);
            this.groupBox4.Controls.Add(this.button5);
            this.groupBox4.Controls.Add(this.button2);
            this.groupBox4.Controls.Add(this.btnSave);
            this.groupBox4.Location = new System.Drawing.Point(492, 532);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(275, 54);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Actions";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(180, 21);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(88, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "Save CSV";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(115, 21);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(59, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = "Launch";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(61, 21);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(49, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Reload";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(6, 21);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(49, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radioImageG);
            this.groupBox5.Controls.Add(this.radioImageR);
            this.groupBox5.Location = new System.Drawing.Point(1059, 535);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(79, 69);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Display";
            // 
            // radioImageG
            // 
            this.radioImageG.AutoSize = true;
            this.radioImageG.Checked = true;
            this.radioImageG.Location = new System.Drawing.Point(6, 43);
            this.radioImageG.Name = "radioImageG";
            this.radioImageG.Size = new System.Drawing.Size(61, 17);
            this.radioImageG.TabIndex = 3;
            this.radioImageG.TabStop = true;
            this.radioImageG.Text = "G (Ch2)";
            this.radioImageG.UseVisualStyleBackColor = true;
            this.radioImageG.CheckedChanged += new System.EventHandler(this.radioImageG_CheckedChanged);
            // 
            // radioImageR
            // 
            this.radioImageR.AutoSize = true;
            this.radioImageR.Location = new System.Drawing.Point(6, 15);
            this.radioImageR.Name = "radioImageR";
            this.radioImageR.Size = new System.Drawing.Size(61, 17);
            this.radioImageR.TabIndex = 2;
            this.radioImageR.Text = "R (Ch1)";
            this.radioImageR.UseVisualStyleBackColor = true;
            this.radioImageR.CheckedChanged += new System.EventHandler(this.radioImageR_CheckedChanged);
            // 
            // gbFrame
            // 
            this.gbFrame.Controls.Add(this.cbFrameAverage);
            this.gbFrame.Controls.Add(this.nudFrame);
            this.gbFrame.Location = new System.Drawing.Point(873, 535);
            this.gbFrame.Name = "gbFrame";
            this.gbFrame.Size = new System.Drawing.Size(87, 69);
            this.gbFrame.TabIndex = 14;
            this.gbFrame.TabStop = false;
            this.gbFrame.Text = "Frame";
            // 
            // cbFrameAverage
            // 
            this.cbFrameAverage.AutoSize = true;
            this.cbFrameAverage.Enabled = false;
            this.cbFrameAverage.Location = new System.Drawing.Point(6, 45);
            this.cbFrameAverage.Name = "cbFrameAverage";
            this.cbFrameAverage.Size = new System.Drawing.Size(65, 17);
            this.cbFrameAverage.TabIndex = 1;
            this.cbFrameAverage.Text = "average";
            this.cbFrameAverage.UseVisualStyleBackColor = true;
            this.cbFrameAverage.CheckedChanged += new System.EventHandler(this.cbFrameAverage_CheckedChanged);
            // 
            // nudFrame
            // 
            this.nudFrame.Location = new System.Drawing.Point(6, 19);
            this.nudFrame.Name = "nudFrame";
            this.nudFrame.Size = new System.Drawing.Size(74, 20);
            this.nudFrame.TabIndex = 0;
            this.nudFrame.ValueChanged += new System.EventHandler(this.nudFrame_ValueChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnCopyCurves);
            this.groupBox7.Controls.Add(this.btnCopyCurve);
            this.groupBox7.Controls.Add(this.btnCopyPeak);
            this.groupBox7.Location = new System.Drawing.Point(320, 529);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(112, 107);
            this.groupBox7.TabIndex = 15;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Copy";
            // 
            // btnCopyCurves
            // 
            this.btnCopyCurves.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyCurves.Location = new System.Drawing.Point(6, 77);
            this.btnCopyCurves.Name = "btnCopyCurves";
            this.btnCopyCurves.Size = new System.Drawing.Size(100, 23);
            this.btnCopyCurves.TabIndex = 4;
            this.btnCopyCurves.Text = "All Curves";
            this.btnCopyCurves.UseVisualStyleBackColor = true;
            this.btnCopyCurves.Click += new System.EventHandler(this.btnCopyCurves_Click);
            // 
            // btnCopyCurve
            // 
            this.btnCopyCurve.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyCurve.Location = new System.Drawing.Point(6, 48);
            this.btnCopyCurve.Name = "btnCopyCurve";
            this.btnCopyCurve.Size = new System.Drawing.Size(100, 23);
            this.btnCopyCurve.TabIndex = 3;
            this.btnCopyCurve.Text = "Curve";
            this.btnCopyCurve.UseVisualStyleBackColor = true;
            this.btnCopyCurve.Click += new System.EventHandler(this.btnCopyCurve_Click);
            // 
            // btnCopyPeak
            // 
            this.btnCopyPeak.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyPeak.Location = new System.Drawing.Point(6, 19);
            this.btnCopyPeak.Name = "btnCopyPeak";
            this.btnCopyPeak.Size = new System.Drawing.Size(100, 23);
            this.btnCopyPeak.TabIndex = 2;
            this.btnCopyPeak.Text = "Peak";
            this.btnCopyPeak.UseVisualStyleBackColor = true;
            this.btnCopyPeak.Click += new System.EventHandler(this.btnCopyPeak_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 648);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1264, 22);
            this.statusStrip1.TabIndex = 16;
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
            this.structureToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.summaryToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1264, 24);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setFolderToolStripMenuItem,
            this.refreshFoldersToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // setFolderToolStripMenuItem
            // 
            this.setFolderToolStripMenuItem.Name = "setFolderToolStripMenuItem";
            this.setFolderToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.setFolderToolStripMenuItem.Text = "Set Folder";
            this.setFolderToolStripMenuItem.Click += new System.EventHandler(this.setFolderToolStripMenuItem_Click);
            // 
            // refreshFoldersToolStripMenuItem
            // 
            this.refreshFoldersToolStripMenuItem.Name = "refreshFoldersToolStripMenuItem";
            this.refreshFoldersToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.refreshFoldersToolStripMenuItem.Text = "Refresh Folders";
            this.refreshFoldersToolStripMenuItem.Click += new System.EventHandler(this.refreshFoldersToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(151, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // structureToolStripMenuItem
            // 
            this.structureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoselectToolStripMenuItem});
            this.structureToolStripMenuItem.Name = "structureToolStripMenuItem";
            this.structureToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.structureToolStripMenuItem.Text = "Structure";
            // 
            // autoselectToolStripMenuItem
            // 
            this.autoselectToolStripMenuItem.Name = "autoselectToolStripMenuItem";
            this.autoselectToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.autoselectToolStripMenuItem.Text = "auto-select";
            this.autoselectToolStripMenuItem.Click += new System.EventHandler(this.autoselectToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLogToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // showLogToolStripMenuItem
            // 
            this.showLogToolStripMenuItem.Name = "showLogToolStripMenuItem";
            this.showLogToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.showLogToolStripMenuItem.Text = "Show Log";
            this.showLogToolStripMenuItem.Click += new System.EventHandler(this.showLogToolStripMenuItem_Click);
            // 
            // summaryToolStripMenuItem
            // 
            this.summaryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.multilinescanSummaryToolStripMenuItem});
            this.summaryToolStripMenuItem.Name = "summaryToolStripMenuItem";
            this.summaryToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.summaryToolStripMenuItem.Text = "Summary";
            // 
            // multilinescanSummaryToolStripMenuItem
            // 
            this.multilinescanSummaryToolStripMenuItem.Name = "multilinescanSummaryToolStripMenuItem";
            this.multilinescanSummaryToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.multilinescanSummaryToolStripMenuItem.Text = "multi-linescan summary";
            this.multilinescanSummaryToolStripMenuItem.Click += new System.EventHandler(this.multilinescanSummaryToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.documentationToolStripMenuItem,
            this.toolStripSeparator1,
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(154, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 670);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gbFrame);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.gbPeak);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbStructure);
            this.Controls.Add(this.gbBaseline);
            this.Controls.Add(this.scottPlotUC1);
            this.Controls.Add(this.pbData);
            this.Controls.Add(this.pbRef);
            this.Controls.Add(this.treeView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Scan-A-Gator";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbRef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbBaseline1)).EndInit();
            this.gbBaseline.ResumeLayout(false);
            this.gbBaseline.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbBaseline2)).EndInit();
            this.gbStructure.ResumeLayout(false);
            this.gbStructure.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbStructure2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbStructure1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFilter)).EndInit();
            this.gbPeak.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.gbFrame.ResumeLayout(false);
            this.gbFrame.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrame)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.PictureBox pbRef;
        private System.Windows.Forms.PictureBox pbData;
        private ScottPlotDev2.ScottPlotUC scottPlotUC1;
        private System.Windows.Forms.TrackBar tbBaseline1;
        private System.Windows.Forms.GroupBox gbBaseline;
        private System.Windows.Forms.TrackBar tbBaseline2;
        private System.Windows.Forms.GroupBox gbStructure;
        private System.Windows.Forms.TrackBar tbStructure2;
        private System.Windows.Forms.TrackBar tbStructure1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioPMT;
        private System.Windows.Forms.RadioButton radioGoR;
        private System.Windows.Forms.RadioButton radioDeltaGoR;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown nudFilter;
        private System.Windows.Forms.GroupBox gbPeak;
        private System.Windows.Forms.Label lblPeak;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblFilterMs;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radioImageG;
        private System.Windows.Forms.RadioButton radioImageR;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox gbFrame;
        private System.Windows.Forms.NumericUpDown nudFrame;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnCopyCurves;
        private System.Windows.Forms.Button btnCopyCurve;
        private System.Windows.Forms.Button btnCopyPeak;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.CheckBox cbFrameAverage;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshFoldersToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem structureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoselectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem summaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem multilinescanSummaryToolStripMenuItem;
        private System.Windows.Forms.RadioButton radioG;
        private System.Windows.Forms.RadioButton radioDeltaG;
    }
}