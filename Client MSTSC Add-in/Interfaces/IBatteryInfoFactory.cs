namespace FieldEffect.Interfaces
{
    public interface IBatteryInfoFactory
    {
        BatteryInfo Create(string clientName, int estimatedChargeRemaining,
            int estimatedRunTime, int batteryStatus);
    }
}
