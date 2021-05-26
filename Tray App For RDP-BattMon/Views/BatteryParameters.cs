using System.Windows.Forms;
using FieldEffect.Interfaces;

namespace FieldEffect.Views
{
    public partial class BatteryParameters : UserControl, IBatteryParameters
    {
        public BatteryParameters()
        {
            InitializeComponent();
        }

        public string BatteryName
        {
            get => RdpBatteryName.Text;
            set => RdpBatteryName.Text = value;
        }

        public string BatteryStatus
        {
            get => RdpClientBattStatus.Text;
            set => RdpClientBattStatus.Text = value;
        }

        public string ClientEstRuntime
        {
            get => RdpClientEstRuntime.Text;
            set => RdpClientEstRuntime.Text = value;
        }

        private int _estimatedChargeRemaining;
        public int EstimatedChargeRemaining
        {
            get => _estimatedChargeRemaining;
            set
            {
                _estimatedChargeRemaining = value;
                RdpClientBattery.Text = $"{_estimatedChargeRemaining}%";
            }
        }
    }
}
