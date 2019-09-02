﻿namespace Demo
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.comboPort = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.comboMode = new System.Windows.Forms.ToolStripComboBox();
            this.buttonStart = new System.Windows.Forms.ToolStripButton();
            this.buttonStop = new System.Windows.Forms.ToolStripButton();
            this.textLog = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelHealth = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelSPC = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelPPS = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerScan = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.trackDisplayRange = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackDisplayRange)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.comboPort,
            this.toolStripLabel2,
            this.comboMode,
            this.buttonStart,
            this.buttonStop});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Margin = new System.Windows.Forms.Padding(3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(2);
            this.toolStrip1.Size = new System.Drawing.Size(904, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(32, 20);
            this.toolStripLabel1.Text = "Port:";
            // 
            // comboPort
            // 
            this.comboPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPort.Name = "comboPort";
            this.comboPort.Size = new System.Drawing.Size(101, 23);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(41, 20);
            this.toolStripLabel2.Text = "Mode:";
            // 
            // comboMode
            // 
            this.comboMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMode.Name = "comboMode";
            this.comboMode.Size = new System.Drawing.Size(131, 23);
            // 
            // buttonStart
            // 
            this.buttonStart.Enabled = false;
            this.buttonStart.Image = global::Demo.Properties.Resources.PlayHS;
            this.buttonStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(51, 20);
            this.buttonStart.Text = "&Start";
            this.buttonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Enabled = false;
            this.buttonStop.Image = global::Demo.Properties.Resources.StopHS;
            this.buttonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(51, 20);
            this.buttonStop.Text = "Sto&p";
            this.buttonStop.Click += new System.EventHandler(this.ButtonStop_Click);
            // 
            // textLog
            // 
            this.textLog.BackColor = System.Drawing.SystemColors.Window;
            this.textLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textLog.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.textLog.Location = new System.Drawing.Point(0, 433);
            this.textLog.Multiline = true;
            this.textLog.Name = "textLog";
            this.textLog.ReadOnly = true;
            this.textLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textLog.Size = new System.Drawing.Size(904, 147);
            this.textLog.TabIndex = 2;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 428);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(904, 5);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // pictureBox
            // 
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 27);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(860, 401);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.labelHealth,
            this.toolStripStatusLabel2,
            this.labelSPC,
            this.toolStripStatusLabel3,
            this.labelPPS});
            this.statusStrip1.Location = new System.Drawing.Point(0, 580);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(904, 24);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(45, 19);
            this.toolStripStatusLabel1.Text = "Health:";
            // 
            // labelHealth
            // 
            this.labelHealth.AutoSize = false;
            this.labelHealth.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.labelHealth.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.labelHealth.Name = "labelHealth";
            this.labelHealth.Size = new System.Drawing.Size(100, 19);
            this.labelHealth.Text = "-";
            this.labelHealth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(121, 19);
            this.toolStripStatusLabel2.Text = "Scans per second:";
            // 
            // labelSPC
            // 
            this.labelSPC.AutoSize = false;
            this.labelSPC.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.labelSPC.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.labelSPC.Name = "labelSPC";
            this.labelSPC.Size = new System.Drawing.Size(50, 19);
            this.labelSPC.Text = "-";
            this.labelSPC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(110, 19);
            this.toolStripStatusLabel3.Text = "Points per scan:";
            // 
            // labelPPS
            // 
            this.labelPPS.AutoSize = false;
            this.labelPPS.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.labelPPS.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.labelPPS.Name = "labelPPS";
            this.labelPPS.Size = new System.Drawing.Size(50, 19);
            this.labelPPS.Text = "-";
            this.labelPPS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timerScan
            // 
            this.timerScan.Interval = 20;
            this.timerScan.Tick += new System.EventHandler(this.TimerScan_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.trackDisplayRange);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(860, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(44, 401);
            this.panel1.TabIndex = 6;
            // 
            // trackDisplayRange
            // 
            this.trackDisplayRange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackDisplayRange.Location = new System.Drawing.Point(0, 13);
            this.trackDisplayRange.Maximum = 50;
            this.trackDisplayRange.Minimum = 1;
            this.trackDisplayRange.Name = "trackDisplayRange";
            this.trackDisplayRange.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackDisplayRange.Size = new System.Drawing.Size(44, 388);
            this.trackDisplayRange.TabIndex = 6;
            this.trackDisplayRange.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackDisplayRange.Value = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Range:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 604);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.textLog);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RPLidar.NET demo";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackDisplayRange)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton buttonStart;
        private System.Windows.Forms.ToolStripButton buttonStop;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.TextBox textLog;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ToolStripComboBox comboPort;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox comboMode;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Timer timerScan;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel labelHealth;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel labelSPC;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TrackBar trackDisplayRange;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel labelPPS;
    }
}

