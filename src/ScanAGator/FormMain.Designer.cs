﻿namespace ScanAGator
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyCurveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbAuto = new System.Windows.Forms.GroupBox();
            this.btnAutoStructure = new System.Windows.Forms.Button();
            this.btnAutoBase = new System.Windows.Forms.Button();
            this.pbLinescan = new System.Windows.Forms.PictureBox();
            this.tbBaseline1 = new System.Windows.Forms.TrackBar();
            this.tbStructure1 = new System.Windows.Forms.TrackBar();
            this.tbStructure2 = new System.Windows.Forms.TrackBar();
            this.pbRef = new System.Windows.Forms.PictureBox();
            this.tbBaseline2 = new System.Windows.Forms.TrackBar();
            this.gbFrame = new System.Windows.Forms.GroupBox();
            this.cbFrame = new System.Windows.Forms.CheckBox();
            this.nudFrame = new System.Windows.Forms.NumericUpDown();
            this.gbDisplayType = new System.Windows.Forms.GroupBox();
            this.cbRatio = new System.Windows.Forms.CheckBox();
            this.cbDelta = new System.Windows.Forms.CheckBox();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.lblFilterMs = new System.Windows.Forms.Label();
            this.nudFilter = new System.Windows.Forms.NumericUpDown();
            this.gbPeak = new System.Windows.Forms.GroupBox();
            this.lblPeak = new System.Windows.Forms.Label();
            this.btnPeakCopy = new System.Windows.Forms.Button();
            this.gbDisplay = new System.Windows.Forms.GroupBox();
            this.cbG = new System.Windows.Forms.CheckBox();
            this.cbR = new System.Windows.Forms.CheckBox();
            this.gbBaseline = new System.Windows.Forms.GroupBox();
            this.nudBaseline2 = new System.Windows.Forms.NumericUpDown();
            this.nudBaseline1 = new System.Windows.Forms.NumericUpDown();
            this.gbStructure = new System.Windows.Forms.GroupBox();
            this.nudStructure2 = new System.Windows.Forms.NumericUpDown();
            this.nudStructure1 = new System.Windows.Forms.NumericUpDown();
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.formsPlot1 = new ScottPlot.FormsPlot();
            this.formsPlot2 = new ScottPlot.FormsPlot();
            this.hScrollRef = new System.Windows.Forms.HScrollBar();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.treeViewDirUC1 = new ScanAGator.TreeViewDirUC();
            this.menuStrip1.SuspendLayout();
            this.gbAuto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLinescan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbBaseline1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbStructure1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbStructure2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbBaseline2)).BeginInit();
            this.gbFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrame)).BeginInit();
            this.gbDisplayType.SuspendLayout();
            this.gbFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFilter)).BeginInit();
            this.gbPeak.SuspendLayout();
            this.gbDisplay.SuspendLayout();
            this.gbBaseline.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseline2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseline1)).BeginInit();
            this.gbStructure.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStructure2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStructure1)).BeginInit();
            this.gbSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.dataToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1202, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setFolderToolStripMenuItem,
            this.refreshFolderToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // setFolderToolStripMenuItem
            // 
            this.setFolderToolStripMenuItem.Name = "setFolderToolStripMenuItem";
            this.setFolderToolStripMenuItem.Size = new System.Drawing.Size(227, 34);
            this.setFolderToolStripMenuItem.Text = "Set Folder";
            this.setFolderToolStripMenuItem.Click += new System.EventHandler(this.setFolderToolStripMenuItem_Click);
            // 
            // refreshFolderToolStripMenuItem
            // 
            this.refreshFolderToolStripMenuItem.Name = "refreshFolderToolStripMenuItem";
            this.refreshFolderToolStripMenuItem.Size = new System.Drawing.Size(227, 34);
            this.refreshFolderToolStripMenuItem.Text = "Refresh Folder";
            this.refreshFolderToolStripMenuItem.Click += new System.EventHandler(this.refreshFolderToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(227, 34);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyCurveToolStripMenuItem,
            this.copyAllToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveCSVToolStripMenuItem});
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            this.dataToolStripMenuItem.Text = "Data";
            // 
            // copyCurveToolStripMenuItem
            // 
            this.copyCurveToolStripMenuItem.Name = "copyCurveToolStripMenuItem";
            this.copyCurveToolStripMenuItem.Size = new System.Drawing.Size(206, 34);
            this.copyCurveToolStripMenuItem.Text = "Copy Curve";
            this.copyCurveToolStripMenuItem.Click += new System.EventHandler(this.copyCurveToolStripMenuItem_Click);
            // 
            // copyAllToolStripMenuItem
            // 
            this.copyAllToolStripMenuItem.Name = "copyAllToolStripMenuItem";
            this.copyAllToolStripMenuItem.Size = new System.Drawing.Size(206, 34);
            this.copyAllToolStripMenuItem.Text = "Copy All";
            this.copyAllToolStripMenuItem.Click += new System.EventHandler(this.copyAllToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(203, 6);
            // 
            // saveCSVToolStripMenuItem
            // 
            this.saveCSVToolStripMenuItem.Name = "saveCSVToolStripMenuItem";
            this.saveCSVToolStripMenuItem.Size = new System.Drawing.Size(206, 34);
            this.saveCSVToolStripMenuItem.Text = "Save CSV";
            this.saveCSVToolStripMenuItem.Click += new System.EventHandler(this.saveCSVToolStripMenuItem_Click);
            // 
            // gbAuto
            // 
            this.gbAuto.Controls.Add(this.btnAutoStructure);
            this.gbAuto.Controls.Add(this.btnAutoBase);
            this.gbAuto.Location = new System.Drawing.Point(740, 379);
            this.gbAuto.Name = "gbAuto";
            this.gbAuto.Size = new System.Drawing.Size(96, 78);
            this.gbAuto.TabIndex = 32;
            this.gbAuto.TabStop = false;
            this.gbAuto.Text = "Auto-Select";
            // 
            // btnAutoStructure
            // 
            this.btnAutoStructure.Location = new System.Drawing.Point(6, 48);
            this.btnAutoStructure.Name = "btnAutoStructure";
            this.btnAutoStructure.Size = new System.Drawing.Size(84, 23);
            this.btnAutoStructure.TabIndex = 3;
            this.btnAutoStructure.Text = "Structure";
            this.btnAutoStructure.UseVisualStyleBackColor = true;
            this.btnAutoStructure.Click += new System.EventHandler(this.btnAutoStructure_Click);
            // 
            // btnAutoBase
            // 
            this.btnAutoBase.Location = new System.Drawing.Point(6, 19);
            this.btnAutoBase.Name = "btnAutoBase";
            this.btnAutoBase.Size = new System.Drawing.Size(84, 23);
            this.btnAutoBase.TabIndex = 2;
            this.btnAutoBase.Text = "Baseline";
            this.btnAutoBase.UseVisualStyleBackColor = true;
            this.btnAutoBase.Click += new System.EventHandler(this.btnAutoBase_Click);
            // 
            // pbLinescan
            // 
            this.pbLinescan.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbLinescan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbLinescan.Location = new System.Drawing.Point(384, 27);
            this.pbLinescan.Name = "pbLinescan";
            this.pbLinescan.Size = new System.Drawing.Size(350, 350);
            this.pbLinescan.TabIndex = 36;
            this.pbLinescan.TabStop = false;
            // 
            // tbBaseline1
            // 
            this.tbBaseline1.Location = new System.Drawing.Point(740, 27);
            this.tbBaseline1.Name = "tbBaseline1";
            this.tbBaseline1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbBaseline1.Size = new System.Drawing.Size(69, 350);
            this.tbBaseline1.TabIndex = 37;
            this.tbBaseline1.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.tbBaseline1.Scroll += new System.EventHandler(this.tbBaseline1_Scroll);
            // 
            // tbStructure1
            // 
            this.tbStructure1.Location = new System.Drawing.Point(384, 383);
            this.tbStructure1.Name = "tbStructure1";
            this.tbStructure1.Size = new System.Drawing.Size(350, 69);
            this.tbStructure1.TabIndex = 40;
            this.tbStructure1.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.tbStructure1.Scroll += new System.EventHandler(this.tbStructure1_Scroll);
            // 
            // tbStructure2
            // 
            this.tbStructure2.Location = new System.Drawing.Point(384, 434);
            this.tbStructure2.Name = "tbStructure2";
            this.tbStructure2.Size = new System.Drawing.Size(350, 69);
            this.tbStructure2.TabIndex = 41;
            this.tbStructure2.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.tbStructure2.Scroll += new System.EventHandler(this.tbStructure2_Scroll);
            // 
            // pbRef
            // 
            this.pbRef.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbRef.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbRef.Location = new System.Drawing.Point(842, 27);
            this.pbRef.Name = "pbRef";
            this.pbRef.Size = new System.Drawing.Size(350, 350);
            this.pbRef.TabIndex = 42;
            this.pbRef.TabStop = false;
            // 
            // tbBaseline2
            // 
            this.tbBaseline2.Location = new System.Drawing.Point(791, 27);
            this.tbBaseline2.Name = "tbBaseline2";
            this.tbBaseline2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbBaseline2.Size = new System.Drawing.Size(69, 350);
            this.tbBaseline2.TabIndex = 38;
            this.tbBaseline2.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.tbBaseline2.Scroll += new System.EventHandler(this.tbBaseline2_Scroll);
            // 
            // gbFrame
            // 
            this.gbFrame.Controls.Add(this.cbFrame);
            this.gbFrame.Controls.Add(this.nudFrame);
            this.gbFrame.Location = new System.Drawing.Point(134, 447);
            this.gbFrame.Name = "gbFrame";
            this.gbFrame.Size = new System.Drawing.Size(107, 49);
            this.gbFrame.TabIndex = 49;
            this.gbFrame.TabStop = false;
            this.gbFrame.Text = "Frame";
            // 
            // cbFrame
            // 
            this.cbFrame.AutoSize = true;
            this.cbFrame.Enabled = false;
            this.cbFrame.Location = new System.Drawing.Point(56, 20);
            this.cbFrame.Name = "cbFrame";
            this.cbFrame.Size = new System.Drawing.Size(62, 24);
            this.cbFrame.TabIndex = 26;
            this.cbFrame.Text = "Avg";
            this.cbFrame.UseVisualStyleBackColor = true;
            this.cbFrame.CheckedChanged += new System.EventHandler(this.cbFrame_CheckedChanged);
            // 
            // nudFrame
            // 
            this.nudFrame.Location = new System.Drawing.Point(6, 19);
            this.nudFrame.Name = "nudFrame";
            this.nudFrame.Size = new System.Drawing.Size(44, 26);
            this.nudFrame.TabIndex = 24;
            this.nudFrame.ValueChanged += new System.EventHandler(this.nudFrame_ValueChanged);
            // 
            // gbDisplayType
            // 
            this.gbDisplayType.Controls.Add(this.cbRatio);
            this.gbDisplayType.Controls.Add(this.cbDelta);
            this.gbDisplayType.Location = new System.Drawing.Point(740, 462);
            this.gbDisplayType.Name = "gbDisplayType";
            this.gbDisplayType.Size = new System.Drawing.Size(96, 66);
            this.gbDisplayType.TabIndex = 44;
            this.gbDisplayType.TabStop = false;
            this.gbDisplayType.Text = "Display";
            // 
            // cbRatio
            // 
            this.cbRatio.AutoSize = true;
            this.cbRatio.Location = new System.Drawing.Point(9, 44);
            this.cbRatio.Name = "cbRatio";
            this.cbRatio.Size = new System.Drawing.Size(73, 24);
            this.cbRatio.TabIndex = 5;
            this.cbRatio.Text = "Ratio";
            this.cbRatio.UseVisualStyleBackColor = true;
            this.cbRatio.CheckedChanged += new System.EventHandler(this.cbRatio_CheckedChanged);
            // 
            // cbDelta
            // 
            this.cbDelta.AutoSize = true;
            this.cbDelta.Location = new System.Drawing.Point(9, 21);
            this.cbDelta.Name = "cbDelta";
            this.cbDelta.Size = new System.Drawing.Size(73, 24);
            this.cbDelta.TabIndex = 4;
            this.cbDelta.Text = "Delta";
            this.cbDelta.UseVisualStyleBackColor = true;
            this.cbDelta.CheckedChanged += new System.EventHandler(this.cbDelta_CheckedChanged);
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.lblFilterMs);
            this.gbFilter.Controls.Add(this.nudFilter);
            this.gbFilter.Location = new System.Drawing.Point(247, 447);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(131, 49);
            this.gbFilter.TabIndex = 47;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Filter";
            // 
            // lblFilterMs
            // 
            this.lblFilterMs.AutoSize = true;
            this.lblFilterMs.Location = new System.Drawing.Point(57, 21);
            this.lblFilterMs.Name = "lblFilterMs";
            this.lblFilterMs.Size = new System.Drawing.Size(83, 20);
            this.lblFilterMs.TabIndex = 24;
            this.lblFilterMs.Text = "123.45 ms";
            // 
            // nudFilter
            // 
            this.nudFilter.Location = new System.Drawing.Point(6, 19);
            this.nudFilter.Name = "nudFilter";
            this.nudFilter.Size = new System.Drawing.Size(45, 26);
            this.nudFilter.TabIndex = 23;
            this.nudFilter.ValueChanged += new System.EventHandler(this.nudFilter_ValueChanged);
            // 
            // gbPeak
            // 
            this.gbPeak.Controls.Add(this.lblPeak);
            this.gbPeak.Controls.Add(this.btnPeakCopy);
            this.gbPeak.Location = new System.Drawing.Point(740, 533);
            this.gbPeak.Name = "gbPeak";
            this.gbPeak.Size = new System.Drawing.Size(96, 73);
            this.gbPeak.TabIndex = 46;
            this.gbPeak.TabStop = false;
            this.gbPeak.Text = "Peak";
            // 
            // lblPeak
            // 
            this.lblPeak.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPeak.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeak.Location = new System.Drawing.Point(6, 16);
            this.lblPeak.Name = "lblPeak";
            this.lblPeak.Size = new System.Drawing.Size(84, 25);
            this.lblPeak.TabIndex = 8;
            this.lblPeak.Text = "4433.22%";
            this.lblPeak.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPeakCopy
            // 
            this.btnPeakCopy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPeakCopy.Location = new System.Drawing.Point(6, 44);
            this.btnPeakCopy.Name = "btnPeakCopy";
            this.btnPeakCopy.Size = new System.Drawing.Size(84, 23);
            this.btnPeakCopy.TabIndex = 6;
            this.btnPeakCopy.Text = "Copy";
            this.btnPeakCopy.UseVisualStyleBackColor = true;
            this.btnPeakCopy.Click += new System.EventHandler(this.btnPeakCopy_Click);
            // 
            // gbDisplay
            // 
            this.gbDisplay.Controls.Add(this.cbG);
            this.gbDisplay.Controls.Add(this.cbR);
            this.gbDisplay.Location = new System.Drawing.Point(12, 447);
            this.gbDisplay.Name = "gbDisplay";
            this.gbDisplay.Size = new System.Drawing.Size(110, 49);
            this.gbDisplay.TabIndex = 48;
            this.gbDisplay.TabStop = false;
            this.gbDisplay.Text = "Display";
            // 
            // cbG
            // 
            this.cbG.AutoSize = true;
            this.cbG.Location = new System.Drawing.Point(62, 23);
            this.cbG.Name = "cbG";
            this.cbG.Size = new System.Drawing.Size(48, 24);
            this.cbG.TabIndex = 1;
            this.cbG.Text = "G";
            this.cbG.UseVisualStyleBackColor = true;
            this.cbG.CheckedChanged += new System.EventHandler(this.cbG_CheckedChanged);
            // 
            // cbR
            // 
            this.cbR.AutoSize = true;
            this.cbR.Location = new System.Drawing.Point(15, 23);
            this.cbR.Name = "cbR";
            this.cbR.Size = new System.Drawing.Size(47, 24);
            this.cbR.TabIndex = 0;
            this.cbR.Text = "R";
            this.cbR.UseVisualStyleBackColor = true;
            this.cbR.CheckedChanged += new System.EventHandler(this.cbR_CheckedChanged);
            // 
            // gbBaseline
            // 
            this.gbBaseline.Controls.Add(this.nudBaseline2);
            this.gbBaseline.Controls.Add(this.nudBaseline1);
            this.gbBaseline.Location = new System.Drawing.Point(12, 502);
            this.gbBaseline.Name = "gbBaseline";
            this.gbBaseline.Size = new System.Drawing.Size(110, 49);
            this.gbBaseline.TabIndex = 50;
            this.gbBaseline.TabStop = false;
            this.gbBaseline.Text = "Baseline";
            // 
            // nudBaseline2
            // 
            this.nudBaseline2.Location = new System.Drawing.Point(56, 19);
            this.nudBaseline2.Name = "nudBaseline2";
            this.nudBaseline2.Size = new System.Drawing.Size(44, 26);
            this.nudBaseline2.TabIndex = 25;
            this.nudBaseline2.ValueChanged += new System.EventHandler(this.nudBaseline2_ValueChanged);
            // 
            // nudBaseline1
            // 
            this.nudBaseline1.Location = new System.Drawing.Point(6, 19);
            this.nudBaseline1.Name = "nudBaseline1";
            this.nudBaseline1.Size = new System.Drawing.Size(44, 26);
            this.nudBaseline1.TabIndex = 24;
            this.nudBaseline1.ValueChanged += new System.EventHandler(this.nudBaseline1_ValueChanged);
            // 
            // gbStructure
            // 
            this.gbStructure.Controls.Add(this.nudStructure2);
            this.gbStructure.Controls.Add(this.nudStructure1);
            this.gbStructure.Location = new System.Drawing.Point(128, 502);
            this.gbStructure.Name = "gbStructure";
            this.gbStructure.Size = new System.Drawing.Size(113, 49);
            this.gbStructure.TabIndex = 51;
            this.gbStructure.TabStop = false;
            this.gbStructure.Text = "Structure";
            // 
            // nudStructure2
            // 
            this.nudStructure2.Location = new System.Drawing.Point(56, 19);
            this.nudStructure2.Name = "nudStructure2";
            this.nudStructure2.Size = new System.Drawing.Size(44, 26);
            this.nudStructure2.TabIndex = 25;
            this.nudStructure2.ValueChanged += new System.EventHandler(this.nudStructure2_ValueChanged);
            // 
            // nudStructure1
            // 
            this.nudStructure1.Location = new System.Drawing.Point(6, 19);
            this.nudStructure1.Name = "nudStructure1";
            this.nudStructure1.Size = new System.Drawing.Size(44, 26);
            this.nudStructure1.TabIndex = 24;
            this.nudStructure1.ValueChanged += new System.EventHandler(this.nudStructure1_ValueChanged);
            // 
            // gbSettings
            // 
            this.gbSettings.Controls.Add(this.btnReload);
            this.gbSettings.Controls.Add(this.btnSave);
            this.gbSettings.Location = new System.Drawing.Point(247, 502);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Size = new System.Drawing.Size(131, 49);
            this.gbSettings.TabIndex = 52;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "Settings";
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(63, 20);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(62, 23);
            this.btnReload.TabIndex = 4;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(6, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(51, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // formsPlot1
            // 
            this.formsPlot1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.formsPlot1.Location = new System.Drawing.Point(842, 398);
            this.formsPlot1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(348, 208);
            this.formsPlot1.TabIndex = 53;
            // 
            // formsPlot2
            // 
            this.formsPlot2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.formsPlot2.Location = new System.Drawing.Point(384, 485);
            this.formsPlot2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.formsPlot2.Name = "formsPlot2";
            this.formsPlot2.Size = new System.Drawing.Size(350, 121);
            this.formsPlot2.TabIndex = 56;
            // 
            // hScrollRef
            // 
            this.hScrollRef.LargeChange = 1;
            this.hScrollRef.Location = new System.Drawing.Point(842, 379);
            this.hScrollRef.Maximum = 5;
            this.hScrollRef.Name = "hScrollRef";
            this.hScrollRef.Size = new System.Drawing.Size(350, 18);
            this.hScrollRef.TabIndex = 57;
            this.hScrollRef.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollRef_Scroll);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(12, 567);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 36);
            this.btnCopy.TabIndex = 59;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(93, 567);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(148, 36);
            this.btnExport.TabIndex = 60;
            this.btnExport.Text = "Save and Show";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // treeViewDirUC1
            // 
            this.treeViewDirUC1.AllowDrop = true;
            this.treeViewDirUC1.Location = new System.Drawing.Point(12, 27);
            this.treeViewDirUC1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.treeViewDirUC1.Name = "treeViewDirUC1";
            this.treeViewDirUC1.Size = new System.Drawing.Size(366, 414);
            this.treeViewDirUC1.TabIndex = 54;
            this.treeViewDirUC1.PathSelected += new System.EventHandler(this.treeViewDirUC1_PathSelected);
            this.treeViewDirUC1.PathDragDropped += new System.EventHandler(this.treeViewDirUC1_PathDragDropped);
            this.treeViewDirUC1.Load += new System.EventHandler(this.treeViewDirUC1_Load);
            // 
            // FormMain
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1202, 615);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.hScrollRef);
            this.Controls.Add(this.formsPlot2);
            this.Controls.Add(this.treeViewDirUC1);
            this.Controls.Add(this.formsPlot1);
            this.Controls.Add(this.gbSettings);
            this.Controls.Add(this.gbStructure);
            this.Controls.Add(this.gbBaseline);
            this.Controls.Add(this.gbFrame);
            this.Controls.Add(this.gbDisplayType);
            this.Controls.Add(this.gbFilter);
            this.Controls.Add(this.gbPeak);
            this.Controls.Add(this.gbDisplay);
            this.Controls.Add(this.pbRef);
            this.Controls.Add(this.tbStructure1);
            this.Controls.Add(this.tbStructure2);
            this.Controls.Add(this.pbLinescan);
            this.Controls.Add(this.tbBaseline1);
            this.Controls.Add(this.tbBaseline2);
            this.Controls.Add(this.gbAuto);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Scan-A-Gator";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gbAuto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLinescan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbBaseline1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbStructure1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbStructure2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbBaseline2)).EndInit();
            this.gbFrame.ResumeLayout(false);
            this.gbFrame.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrame)).EndInit();
            this.gbDisplayType.ResumeLayout(false);
            this.gbDisplayType.PerformLayout();
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFilter)).EndInit();
            this.gbPeak.ResumeLayout(false);
            this.gbDisplay.ResumeLayout(false);
            this.gbDisplay.PerformLayout();
            this.gbBaseline.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseline2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseline1)).EndInit();
            this.gbStructure.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudStructure2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStructure1)).EndInit();
            this.gbSettings.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox gbAuto;
        private System.Windows.Forms.Button btnAutoStructure;
        private System.Windows.Forms.Button btnAutoBase;
        private System.Windows.Forms.PictureBox pbLinescan;
        private System.Windows.Forms.TrackBar tbBaseline1;
        private System.Windows.Forms.TrackBar tbStructure1;
        private System.Windows.Forms.TrackBar tbStructure2;
        private System.Windows.Forms.PictureBox pbRef;
        private System.Windows.Forms.TrackBar tbBaseline2;
        private System.Windows.Forms.GroupBox gbFrame;
        private System.Windows.Forms.CheckBox cbFrame;
        private System.Windows.Forms.NumericUpDown nudFrame;
        private System.Windows.Forms.GroupBox gbDisplayType;
        private System.Windows.Forms.CheckBox cbRatio;
        private System.Windows.Forms.CheckBox cbDelta;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.Label lblFilterMs;
        private System.Windows.Forms.NumericUpDown nudFilter;
        private System.Windows.Forms.GroupBox gbPeak;
        private System.Windows.Forms.Label lblPeak;
        private System.Windows.Forms.Button btnPeakCopy;
        private System.Windows.Forms.GroupBox gbDisplay;
        private System.Windows.Forms.CheckBox cbG;
        private System.Windows.Forms.CheckBox cbR;
        private System.Windows.Forms.GroupBox gbBaseline;
        private System.Windows.Forms.NumericUpDown nudBaseline2;
        private System.Windows.Forms.NumericUpDown nudBaseline1;
        private System.Windows.Forms.GroupBox gbStructure;
        private System.Windows.Forms.NumericUpDown nudStructure2;
        private System.Windows.Forms.NumericUpDown nudStructure1;
        private System.Windows.Forms.GroupBox gbSettings;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnSave;
        private ScottPlot.FormsPlot formsPlot1;
        private TreeViewDirUC treeViewDirUC1;
        private System.Windows.Forms.ToolStripMenuItem setFolderToolStripMenuItem;
        private ScottPlot.FormsPlot formsPlot2;
        private System.Windows.Forms.HScrollBar hScrollRef;
        private System.Windows.Forms.ToolStripMenuItem refreshFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyCurveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveCSVToolStripMenuItem;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnExport;
    }
}