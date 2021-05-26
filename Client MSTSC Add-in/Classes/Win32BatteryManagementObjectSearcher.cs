using System;
using System.Collections.Generic;
using System.Management;
using System.Runtime.Versioning;
using FieldEffect.Interfaces;

namespace FieldEffect.Classes
{
    [SupportedOSPlatform("windows")]
    public class Win32BatteryManagementObjectSearcher : ManagementObjectSearcher, IBatteryDataCollector
    {
        private readonly IBatteryInfoFactory _batteryInfoFactory;

        public Win32BatteryManagementObjectSearcher(IBatteryInfoFactory batteryInfoFactory, string query) : base(query)
        {
            _batteryInfoFactory = batteryInfoFactory;
        }

        private static TTargetType ConvertValue<TTargetType>(string propertyAsString)
        {
            object outValue = default(TTargetType);

            if (typeof(TTargetType) == typeof(int))
            {
                if (int.TryParse(propertyAsString, out var parsedString))
                {
                    //Boxing conversion
                    outValue = parsedString;
                }
            }

            if (typeof(TTargetType) == typeof(string))
            {
                outValue = propertyAsString;
            }

            if (typeof(TTargetType) == typeof(TimeSpan))
            {
                var seconds = ConvertValue<int>(propertyAsString);
                outValue = new TimeSpan(0, 0, seconds);
            }

            return (TTargetType)outValue;
        }

        public IEnumerable<IBatteryInfo> GetAllBatteries()
        {
            var batteryInfoList = new List<IBatteryInfo>();
            var batteries = Get();
            foreach (var battery in batteries)
            {
                var batteryInfo = _batteryInfoFactory.Create(
                        Environment.MachineName,
                        ConvertValue<int>(battery["EstimatedChargeRemaining"]?.ToString() ?? "0"),
                        ConvertValue<int>(battery["EstimatedRunTime"]?.ToString() ?? "0"),
                        ConvertValue<int>(battery["BatteryStatus"]?.ToString() ?? "0")
                    );
                batteryInfoList.Add(batteryInfo);
                battery.Dispose();
            }
            return batteryInfoList;
        }
    }
}
