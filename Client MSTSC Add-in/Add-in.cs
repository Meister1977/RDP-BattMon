using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FieldEffect.Classes;
using FieldEffect.Interfaces;
using FieldEffect.VCL.Client.WtsApi32;
using FieldEffect.VCL.Exceptions;
using log4net;
using Ninject;

namespace FieldEffect
{
    class Program
    {
        private static Program _instance;
        private static ILog _log;
        private readonly IBatteryDataReporter _batteryReporter;

        public Program(ILog log, IBatteryDataReporter batteryDataReporter)
        {
            _log = log;
            _batteryReporter = batteryDataReporter;

            Application.ThreadException += (_, e) => _log.Fatal(e.Exception.ToString());
            AppDomain.CurrentDomain.UnhandledException += (_, e) => _log.Fatal(e.ExceptionObject.ToString());

            _log.Info("BattMon Remote Desktop client battery reporter started.");
        }

        ~Program()
        {
            //Don't GC the batteryCommunicator until the program is done
            GC.KeepAlive(_batteryReporter);

            //TODO: We need a good place to Dispose() of batteryCommunicator.
            //The destructor is the wrong place, but I'm not sure how
            //else to tell when the add-in is exiting.
            _batteryReporter.Dispose();
            _log.Info("BattMon Remote Desktop client battery reporter exited.");
        }

        public bool Run(ChannelEntryPoints entry)
        {
            try
            {
                _batteryReporter.EntryPoints = entry;
                _batteryReporter.Initialize();
            }
            catch (VirtualChannelException e)
            {
                //Communication problem
                _log.Fatal(e.ToString());
                return false;
            }
            catch (ActivationException e)
            {
                _log.Fatal(e.ToString());
                return false;
            }
            catch (Exception e)
            {
                _log.Fatal(e.ToString());
                return false;
            }
            return true;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) }, EntryPoint = "VirtualChannelEntry")]
        public static bool VirtualChannelEntry(IntPtr entry)
        {
            _log.Info("BattMon Remote Desktop client battery VirtualChannelEntry");
            var entryPoint = (ChannelEntryPoints)Marshal.PtrToStructure(entry, typeof(ChannelEntryPoints))!;
            //Composition root
            _instance = NinjectConfig.Instance.Get<Program>();
            return _instance.Run(entryPoint);
        }
    }
}
