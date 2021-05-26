using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using FieldEffect.Interfaces;
using FieldEffect.Properties;
using log4net;
using Timer = System.Timers.Timer;

namespace FieldEffect.Presenters
{
    public class BatteryDetailPresenter : IBatteryDetailPresenter
    {
        private readonly IBatteryDataRetriever _batteryDataRetriever;
        private readonly IBatteryIcon _batteryIcon;
        private bool _isDisposed;
        private readonly IBatteryParametersFactory _batteryParametersFactory;
        private readonly ILog _log;
        private readonly Timer _timer;

        public IBatteryDetail BatteryDetailView { get; set; }

        public BatteryDetailPresenter(IBatteryDataRetriever batteryDataRetriever, IBatteryIcon batteryIcon, IBatteryDetail batteryDetailView, IBatteryParametersFactory batteryParametersFactory, ILog log, Timer timer)
        {
            _batteryDataRetriever = batteryDataRetriever;
            _batteryIcon = batteryIcon;
            _batteryParametersFactory = batteryParametersFactory;
            _log = log;
            _timer = timer;

            BatteryDetailView = batteryDetailView;

            WireEvents();

            //Render the battery right away
            RenderBattery();

            _timer.Start();

            _log.Info(Resources.InfoMsgBattMonStart);

        }

        private void WireEvents()
        {
            _timer.Elapsed += Timer_Elapsed;
            BatteryDetailView.RequestClose += BatteryDetailView_RequestClose;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            RenderBattery();
        }

        private void BatteryDetailView_RequestClose(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.ApplicationExitCall)
            {
                e.Cancel = true;
                BatteryDetailView.Visible = false;
                BatteryDetailView.BatteryTrayControl.BalloonTipTitle = Resources.MinimizedTitle;
                BatteryDetailView.BatteryTrayControl.BalloonTipText = Resources.MinimizedMessage;
                BatteryDetailView.BatteryTrayControl.ShowBalloonTip(5000);
            }
        }

        private static string BatteryStatus(int status)
        {

            return new Dictionary<int, string>
            {
                { 0, "Unknown"                    },
                { 1, "Discharging"                },
                { 2, "Unknown"                    },
                { 3, "Fully Charged"              },
                { 4, "Low"                        },
                { 5, "Critical"                   },
                { 6, "Charging"                   },
                { 7, "Charging and High"          },
                { 8, "Charging and Low"           },
                { 9, "Charging and Critical"      },
                { 10, "Undefined"                 },
                { 11, "Partially Charged"         }
            }[status];
        }

        private void RenderBattery()
        {
            if (BatteryDetailView.InvokeRequired)
            {
                BatteryDetailView.Invoke(new Action(RenderBattery));
                return;
            }

            var batteryInfo = new List<IBatteryInfo>(_batteryDataRetriever.BatteryInfo);

            //TODO: Count() method is probably going to be slow. Rethink the Batteries::IEnumerable?
            if (batteryInfo.Count != BatteryDetailView.Batteries.Count())
            {
                //Dispose of existing
                BatteryDetailView.ClearBatteries();
                for (var i = 0; i < batteryInfo.Count; i++)
                {
                    BatteryDetailView.AddBattery(_batteryParametersFactory.Create());
                }
            }

            //GitHub #20 - somehow the batteryInfo array can become
            //out of sync with the BatteryDetailView.Batteries IEnumerable,
            //even though they're being synced above.
            //Using the length of the battInfo array as the indexer,
            //and queuing the battery view elements should help, but
            //we should get to the root cause.
            double totalBattery = 0; double totalPercent = 0;
            var batteryQueue = new Queue<IBatteryParameters>(BatteryDetailView.Batteries);
            for (var battIdx = 0; battIdx < batteryInfo.Count; battIdx++)
            {

                if (batteryQueue.Count > 0)
                {
                    var battery = batteryQueue.Dequeue();
                    battery.BatteryStatus = BatteryStatus(batteryInfo[battIdx].BatteryStatus);
                    battery.ClientEstRuntime = new TimeSpan(0, 0, batteryInfo[battIdx].EstimatedRunTime).ToString();
                    battery.EstimatedChargeRemaining = batteryInfo[battIdx].EstimatedChargeRemaining;
                    battery.BatteryName = string.Format(Resources.BatteryName, battIdx + 1);
                    totalBattery += batteryInfo[battIdx].EstimatedChargeRemaining;
                    totalPercent += 100;
                }
            }

            if (batteryInfo.Count > 0)
            {
                var estCharge = (int)((totalBattery / totalPercent) * 100.0);
                BatteryDetailView.ClientName = batteryInfo[0].ClientName;
                BatteryDetailView.TotalEstimatedCharge = estCharge;
                BatteryDetailView.BatteryTrayControl.Text = string.Format(Resources.RemoteBatteryText, estCharge);
                _batteryIcon.BatteryLevel = estCharge;
                _batteryIcon.Render();
                BatteryDetailView.BatteryTrayIcon = _batteryIcon.RenderedIcon;
            }
            else
            {
                BatteryDetailView.ClientName = Resources.NoBattFound;
                BatteryDetailView.BatteryTrayIcon = Resources.NoBatt;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            _log.Info(Resources.InfoMsgBattMonExit);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_isDisposed)
                {
                    _batteryIcon?.Dispose();

                    _timer?.Dispose();
                }
                _isDisposed = true;
            }
        }
    }
}
