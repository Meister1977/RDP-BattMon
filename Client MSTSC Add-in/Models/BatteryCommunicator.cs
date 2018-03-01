﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FieldEffect.Classes;
using FieldEffect.Interfaces;
using Win32.WtsApi32;

namespace FieldEffect.Models
{
    public class BatteryCommunicator : IBatteryCommunicator
    {
        ITsClientAddIn _clientAddIn;
        IBatteryInfo _batteryInfo;
        string _data = String.Empty;
        private bool _isDisposed = false;

        public BatteryCommunicator(ITsClientAddIn clientAddin, IBatteryInfo batteryInfo)
        {
            _clientAddIn = clientAddin;
            _batteryInfo = batteryInfo;

            _clientAddIn.DataChannelEvent += _clientAddIn_DataChannelEvent;
        }

        public ChannelEntryPoints EntryPoints
        {
            get { return _clientAddIn.EntryPoints; }
            set { _clientAddIn.EntryPoints = value; }
        }
        
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_isDisposed)
                {
                    if (_batteryInfo != null)
                        _batteryInfo.Dispose();
                }
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        public void Initialize()
        {
            //Will throw a VirtualChannelException on failure
            _clientAddIn.Initialize();
        }

        private void _clientAddIn_DataChannelEvent(object sender, DataChannelEventArgs e)
        {
            string curData;
            if (e.DataFlags == ChannelFlags.First || e.DataFlags == ChannelFlags.Only)
            {
                _data = "";
            }
            if (e.Data == null)
                return;

            curData = Encoding.UTF8.GetString(e.Data, 0, e.DataLength);
            _data = _data + curData;

            if (e.DataFlags == ChannelFlags.Last || e.DataFlags == ChannelFlags.Only)
            {
                if (_data == "EstimatedChargeRemaining\0")
                {
                    byte[] response = Encoding.UTF8.GetBytes(String.Format("EstimatedChargeRemaining,{0}\0", _batteryInfo.EstimatedChargeRemaining));
                    _clientAddIn.VirtualChannelWrite(response);
                }
            }
        }
    }
}
