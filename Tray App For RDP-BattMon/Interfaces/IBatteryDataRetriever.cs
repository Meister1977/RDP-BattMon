using System.Collections.Generic;

namespace FieldEffect.Interfaces
{
    public interface IBatteryDataRetriever
    {
        IEnumerable<IBatteryInfo> BatteryInfo { get; }
    }
}