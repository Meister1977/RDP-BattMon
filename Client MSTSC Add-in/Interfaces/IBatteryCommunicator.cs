using System;
using FieldEffect.VCL.Client.WtsApi32;

namespace FieldEffect.Interfaces
{
    public interface IBatteryDataReporter : IDisposable
    {
        ChannelEntryPoints EntryPoints { get; set; }
        void Initialize();
    }
}
