using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using FieldEffect.Interfaces;
using FieldEffect.Properties;

namespace FieldEffect.Views
{
    public partial class BatteryDetail : Form, IBatteryDetail
    {
        public event EventHandler<FormClosingEventArgs> RequestClose;

        protected Lazy<List<IBatteryParameters>> _batteryParameters =
            new();

        protected int _totalEstimatedCharge;

        public BatteryDetail()
        {
            InitializeComponent();
            Shown += (_, _) => Visible = false;
        }

        public Icon BatteryTrayIcon
        {
            get => BatteryTray.Icon;
            set
            {
                BatteryTray.Icon = value;
                Icon = value;
            }
        }

        public NotifyIcon BatteryTrayControl => BatteryTray;

        public string ClientName
        {
            get => RdpClientName.Text;
            set => RdpClientName.Text = value;
        }

        public IEnumerable<IBatteryParameters> Batteries
        {
            get
            {
                //return _batteryParameters.Value;
                foreach (var control in BatteryDetailPanel.Controls)
                {
                    if (control is IBatteryParameters parameters)
                        yield return parameters;
                }
            }
        }
        public void ClearBatteries()
        {
            foreach(var battery in Batteries)
            {
                BatteryDetailPanel.Controls.Remove((Control)battery);
                battery.Dispose();
            }
        }
        public void AddBattery(IBatteryParameters parametersView)
        {
            var battView = (Control)parametersView;
            BatteryDetailPanel.Controls.Add(battView);
        }

        public int TotalEstimatedCharge
        {
            get => _totalEstimatedCharge;
            set
            {
                _totalEstimatedCharge = value;
                RdpTotalEstCharge.Text = $"{_totalEstimatedCharge}%";
            }
        }

        protected void OnRequestClose(FormClosingEventArgs args)
        {
            RequestClose?.Invoke(this, args);
        }

        private void BatteryTray_DoubleClick(object sender, EventArgs e)
        {
            Visible = !Visible;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BatteryDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            OnRequestClose(e);
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Resources.SourceCode);
        }
    }
}
