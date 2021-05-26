using System;
using System.Collections.Generic;

namespace FieldEffect.Interfaces
{
    public interface IBatteryDataCollector : IDisposable
    {
        //Get all batteries for the system
        IEnumerable<IBatteryInfo> GetAllBatteries();
    }
}
