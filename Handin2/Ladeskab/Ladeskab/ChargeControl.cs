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
        private readonly IDisplay _display;

        private bool _isConnected = false;

        public ChargeControl(IUsbCharger usbCharger, IDisplay display)
        {
            _usbCharger = usbCharger;
            _display = display;

            _usbCharger.Connected = false;
            _usbCharger.CurrentValueEvent += CurrentValueChanged;
        }
        public void ConnectDevice()
        {
            _isConnected = true;
            _usbCharger.Connected = true;
        }

        public void DisconnectDevice()
        {
            _isConnected = false;
            _usbCharger.Connected = false;
        }

        public bool IsConnected()
        {
            return _isConnected;
        }

        public void StartCharge()
        {
            if (_isConnected == false)
                throw new NotConnectedException();
            _usbCharger.StartCharge();
        }

        public void StopCharge()
        {
            _usbCharger.StopCharge();
        }

        private void CurrentValueChanged(object sender, CurrentEventArgs e)
        {
            if (e.Current == 0)
            {
                //Nothing
            }
            else if (0 < e.Current && e.Current <= 5)
            {
                _display.ShowChargingFinished();
            }
            else if (5 < e.Current && e.Current <= 500)
            {
                _display.ShowChargingNominal();
            }
            else if (e.Current > 500)
            {
                _display.ShowOvercurrentError();
            }
        }
    }
}
