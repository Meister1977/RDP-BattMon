using System;
using System.Collections.Generic;
using System.Text;
using FieldEffect.Interfaces;
using FieldEffect.Properties;
using FieldEffect.VCL.Client;
using FieldEffect.VCL.Client.Interfaces;
using FieldEffect.VCL.Client.WtsApi32;
using FieldEffect.VCL.CommunicationProtocol;
using FieldEffect.VCL.Exceptions;
using log4net;

namespace FieldEffect.Models
{
    public class BatteryDataReporter : IBatteryDataReporter
    {
        readonly IRdpClientVirtualChannel _clientAddIn;
        readonly IBatteryDataCollector _batteryDataCollector;
        string _data = string.Empty;
        private readonly bool _isDisposed = false;
        private readonly ILog _log;

        public BatteryDataReporter(ILog log, IRdpClientVirtualChannel clientAddin, IBatteryDataCollector batteryDataCollector)
        {
            _clientAddIn = clientAddin;
            _batteryDataCollector = batteryDataCollector;
            _log = log;

            _clientAddIn.DataChannelEvent += ClientAddIn_DataChannelEvent;
        }

        public ChannelEntryPoints EntryPoints
        {
            get => _clientAddIn.EntryPoints;
            set => _clientAddIn.EntryPoints = value;
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_isDisposed)
                {

                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Initialize()
        {
            //Will throw a VirtualChannelException on failure
            _clientAddIn.Initialize();
        }

        private void ClientAddIn_DataChannelEvent(object sender, DataChannelEventArgs e)
        {
            if (e.DataFlags == ChannelFlags.First || e.DataFlags == ChannelFlags.Only)
            {
                _data = "";
            }
            if (e.Data == null)
                return;

            var curData = Encoding.UTF8.GetString(e.Data, 0, e.DataLength);
            _data += curData;

            if (e.DataFlags == ChannelFlags.Last || e.DataFlags == ChannelFlags.Only)
            {
                _log.Debug(string.Format(Resources.DebugMsgReceivedRequest, _data));
                var request = Request.Deserialize(_data);
                var response = new Response();

                if (request.Value.Contains("BatteryInfo"))
                {
                    var batteryInfo = new List<IBatteryInfo>(_batteryDataCollector.GetAllBatteries());
                    response.Value.Add("BatteryInfo", batteryInfo);
                }
                var serializedResponse = response.Serialize();
                _log.Debug(string.Format(Resources.DebugMsgSendingBattInfo, serializedResponse));

                var responseBytes = Encoding.UTF8.GetBytes(serializedResponse);

                try
                {
                    _clientAddIn.VirtualChannelWrite(responseBytes);
                }
                catch(VirtualChannelException vce)
                {
                    //If we don't write the response, the client
                    //probably fell asleep. This isn't a fatal error.
                    _log.Warn(vce.ToString());
                }
            }
        }
    }
}
