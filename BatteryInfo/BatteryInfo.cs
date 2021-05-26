using System;

namespace FieldEffect
{
    [Serializable]
    public sealed class BatteryInfo : IBatteryInfo
    {
        public BatteryInfo()
        { }
        public BatteryInfo(string clientName, int estimatedChargeRemaining,
            int estimatedRunTime, int batteryStatus)
        {
            ClientName = clientName;
            EstimatedChargeRemaining = estimatedChargeRemaining;
            EstimatedRunTime = estimatedRunTime;
            BatteryStatus = batteryStatus;
        }

        public string ClientName { get; set; }

        public int EstimatedChargeRemaining { get; set; }

        public int EstimatedRunTime { get; set; }

        public int BatteryStatus { get; set; }
    }
}
