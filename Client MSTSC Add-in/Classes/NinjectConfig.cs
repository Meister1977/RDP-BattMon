using System;
using System.IO;
using System.Reflection;
using FieldEffect.Interfaces;
using FieldEffect.Models;
using FieldEffect.Properties;
using FieldEffect.VCL.Client;
using FieldEffect.VCL.Client.Interfaces;
using log4net;
using log4net.Config;
using log4net.Core;
using log4net.Repository.Hierarchy;
using Ninject;
using Ninject.Extensions.Factory;
using Ninject.Modules;

namespace FieldEffect.Classes
{
    internal class NinjectConfig : NinjectModule
    {
        private static readonly Lazy<IKernel> _instance = new(()=>
        {
            var kernel = new StandardKernel(new NinjectConfig());

            if (!kernel.HasModule(new FuncModule().Name))
            {
                kernel = new StandardKernel(new NinjectConfig(), new FuncModule());
            }

            return kernel;
        });

        public static IKernel Instance => _instance.Value;

        public override void Load()
        {
            //Configure the log here because this section will
            //get called once on startup, before the ILog instance
            //gets created.
            //
            //The client will only log if there is a log4net.config
            //file in the same folder as the DLL.
            var path = Directory.GetParent(Assembly.GetExecutingAssembly().Location)!.FullName;
            var log4NetConfig = Path.Combine(path, Resources.Log4NetConfig);
            if (File.Exists(log4NetConfig))
            {
                XmlConfigurator.Configure(new FileInfo(log4NetConfig));
            }
            else
            {
                //No config file.  Turn logging off.
                var hierarchy = (Hierarchy)LogManager.GetRepository();
                hierarchy.Root.Level = Level.Off;
            }
            KernelInstance.Bind<IRdpClientVirtualChannel>()
                .To<RdpClientVirtualChannel>()
                .InSingletonScope()
                .WithConstructorArgument("channelName", "BATTMON");

            KernelInstance.Bind<IBatteryDataReporter>()
                .To<BatteryDataReporter>()
                .InSingletonScope();

            KernelInstance.Bind<IBatteryDataCollector>()
                .To<Win32BatteryManagementObjectSearcher>()
                .InSingletonScope()
                .WithConstructorArgument("query", "SELECT * FROM Win32_Battery");

            KernelInstance.Bind<IBatteryInfoFactory>()
                .ToFactory()
                .InSingletonScope();

            KernelInstance.Bind<Program>()
                .ToSelf();

            KernelInstance.Bind<ILog>().ToMethod(context =>
                LogManager.GetLogger(context.Request.Target!.Member.ReflectedType));

        }
    }
}
