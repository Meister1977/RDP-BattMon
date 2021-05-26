using System;
using System.Threading;
using System.Windows.Forms;
using FieldEffect.Classes;
using FieldEffect.Interfaces;
using log4net;

namespace FieldEffect
{
    public class Program
    {
        private static ILog Log => LogManager.GetLogger(typeof(Program));

        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.ThreadException += Application_ThreadException;
            using var presenter = (IBatteryDetailPresenter)NinjectConfig.Instance.GetService(typeof(IBatteryDetailPresenter));
            Application.Run((Form)presenter!.BatteryDetailView);
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Log.Error(e.Exception.ToString());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Error(e.ToString());
        }
    }
}
