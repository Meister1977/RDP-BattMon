using System.ComponentModel;
using System.Windows.Forms;

namespace FieldEffect.Views
{
    partial class BatteryDetail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.BatteryTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.TrayMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.RdpClientName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.RdpTotalEstCharge = new System.Windows.Forms.Label();
            this.BatteryDetailPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.TrayMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // BatteryTray
            // 
            this.BatteryTray.ContextMenuStrip = this.TrayMenuStrip;
            this.BatteryTray.Text = "Remote Battery Status";
            this.BatteryTray.Visible = true;
            this.BatteryTray.DoubleClick += new System.EventHandler(this.BatteryTray_DoubleClick);
            // 
            // TrayMenuStrip
            // 
            this.TrayMenuStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.TrayMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.TrayMenuStrip.Name = "TrayMenuStrip";
            this.TrayMenuStrip.Size = new System.Drawing.Size(94, 26);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 4);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "RDP Client Name:";
            // 
            // RdpClientName
            // 
            this.RdpClientName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RdpClientName.AutoSize = true;
            this.RdpClientName.Location = new System.Drawing.Point(142, 4);
            this.RdpClientName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RdpClientName.Name = "RdpClientName";
            this.RdpClientName.Size = new System.Drawing.Size(59, 15);
            this.RdpClientName.TabIndex = 2;
            this.RdpClientName.Text = "Unknown";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 224);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(254, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "BattMon: RDP Client Battery Reporting Add-In";
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(6, 238);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(118, 15);
            this.linkLabel1.TabIndex = 9;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "GitHub Project Page";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
            // 
            // RdpTotalEstCharge
            // 
            this.RdpTotalEstCharge.AutoSize = true;
            this.RdpTotalEstCharge.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RdpTotalEstCharge.Location = new System.Drawing.Point(11, 27);
            this.RdpTotalEstCharge.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RdpTotalEstCharge.Name = "RdpTotalEstCharge";
            this.RdpTotalEstCharge.Size = new System.Drawing.Size(53, 31);
            this.RdpTotalEstCharge.TabIndex = 11;
            this.RdpTotalEstCharge.Text = "0%";
            // 
            // BatteryDetailPanel
            // 
            this.BatteryDetailPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BatteryDetailPanel.AutoScroll = true;
            this.BatteryDetailPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.BatteryDetailPanel.Location = new System.Drawing.Point(6, 69);
            this.BatteryDetailPanel.Margin = new System.Windows.Forms.Padding(2);
            this.BatteryDetailPanel.Name = "BatteryDetailPanel";
            this.BatteryDetailPanel.Size = new System.Drawing.Size(278, 154);
            this.BatteryDetailPanel.TabIndex = 12;
            this.BatteryDetailPanel.WrapContents = false;
            // 
            // BatteryDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(290, 267);
            this.Controls.Add(this.BatteryDetailPanel);
            this.Controls.Add(this.RdpTotalEstCharge);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.RdpClientName);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "BatteryDetail";
            this.Text = "Remote Battery Detail";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BatteryDetail_FormClosing);
            this.TrayMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NotifyIcon BatteryTray;
        private Label label2;
        private Label RdpClientName;
        private ContextMenuStrip TrayMenuStrip;
        private ToolStripMenuItem exitToolStripMenuItem;
        private Label label5;
        private LinkLabel linkLabel1;
        private Label RdpTotalEstCharge;
        private FlowLayoutPanel BatteryDetailPanel;
    }
}