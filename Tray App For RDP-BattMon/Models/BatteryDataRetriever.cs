using System;
using System.Collections.Generic;
using FieldEffect.Interfaces;
using FieldEffect.Properties;
using FieldEffect.VCL.CommunicationProtocol;
using FieldEffect.VCL.Server.Interfaces;
using log4net;

namespace FieldEffect.Models
{
    public class BatteryDataRetriever : IBatteryDataRetriever
    {
        private readonly IRdpServerVirtualChannel _channel;
        private readonly ILog _log;

        public BatteryDataRetriever (ILog log, IRdpServerVirtualChannel channel)
        {
            _channel = channel;
            _log = log;
        }

        public IEnumerable<IBatteryInfo> BatteryInfo
        {
            get
            {
                try
                {
                    var batteryInfo = RetrieveBatteryInfoFromClient();
                    return batteryInfo;
                }
                catch(Exception e)
                {
                    //Log the exception
                    _log.Warn(e.ToString());
                    //Return empty list
                    return new List<IBatteryInfo>();
                }
            }
        }

        private List<IBatteryInfo> RetrieveBatteryInfoFromClient()
        {
            var request = new Request();
            request.Value.Add("BatteryInfo");

            _channel.OpenChannel();

            var serializedRequest = request.Serialize();
            _log.Debug(string.Format(Resources.DebugMsgRequestingBattInfo, serializedRequest));
            _channel.WriteChannel(request.Serialize());

            var reply = _channel.ReadChannel();
            _log.Debug(string.Format(Resources.DebugMsgReceivedBattInfo, reply));

            var response = Response.Deserialize(reply);
            var propertyValue = (List<IBatteryInfo>)response.Value["BatteryInfo"];

            _channel.CloseChannel();

            return propertyValue;
        }
    }
}
