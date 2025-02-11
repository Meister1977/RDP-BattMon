﻿using System;
using System.Drawing;
using System.Timers;
using FieldEffect.Interfaces;
using FieldEffect.Models;
using FieldEffect.Presenters;
using FieldEffect.Properties;
using FieldEffect.VCL.Server;
using FieldEffect.VCL.Server.Interfaces;
using FieldEffect.Views;
using log4net;
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
            KernelInstance.Bind<IBatteryInfo>()
                .To<BatteryInfo>();

            KernelInstance.Bind<IBatteryParameters>()
                .To<BatteryParameters>();

            KernelInstance.Bind<IBatteryParametersFactory>()
                .ToFactory()
                .InSingletonScope();

            KernelInstance.Bind<IBatteryDetailPresenter>()
                .To<BatteryDetailPresenter>()
                .InSingletonScope();

            KernelInstance.Bind<IBatteryDetail>()
                .To<BatteryDetail>()
                .InSingletonScope();

            KernelInstance.Bind<IBatteryIcon>()
                .To<BatteryIcon>()
                .InSingletonScope()
                .WithConstructorArgument("batteryTemplate", Resources.BattLevel)
                .WithConstructorArgument("batteryLevelMask", new Rectangle(5, 10, 20, 10))
                .WithConstructorArgument("batteryOrientation", BatteryIcon.BatteryOrientation.HorizontalL)
                .WithPropertyValue("BatteryLevel", 0);

            KernelInstance.Bind<IBatteryDataRetriever>()
                .To<BatteryDataRetriever>()
                .InSingletonScope();

            KernelInstance.Bind<IRdpServerVirtualChannel>()
                 .To<RdpServerVirtualChannel>()
                 .InSingletonScope()
                 .WithConstructorArgument("channelName","BATTMON");

            KernelInstance.Bind<ILog>().ToMethod(context =>
                LogManager.GetLogger(context.Request.Target!.Member.ReflectedType));

            KernelInstance.Bind<Timer>()
                .ToSelf()
                .InSingletonScope()
                .WithPropertyValue("AutoReset", true)
                .WithPropertyValue("Interval", Settings.Default.PollingInterval * 1000.0);
        }
    }
}
