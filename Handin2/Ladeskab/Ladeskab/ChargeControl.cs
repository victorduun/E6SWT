using Ladeskab.Interfaces;
using Ladeskab.USB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab
{
    public class ChargeControl : IChargeControl
    {
        private readonly IUsbCharger _usbCharger;

        private bool _isConnected = false;

        public ChargeControl(IUsbCharger usbCharger)
        {
            _usbCharger = usbCharger;
        }
        public void ConnectDevice()
        {
            _isConnected = true;
        }

        public void DisconnectDevice()
        {
            _isConnected = false;
        }

        public bool IsConnected()
        {
            return _isConnected;
        }

        public void StartCharge()
        {
            _usbCharger.StartCharge();
        }

        public void StopCharge()
        {
            _usbCharger.StopCharge();
        }
    }
}
