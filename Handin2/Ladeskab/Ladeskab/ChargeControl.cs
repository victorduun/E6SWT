using Ladeskab.Interfaces;
using Ladeskab.USB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab
{
    class ChargeControl : IChargeControl
    {
        IUsbCharger _usbCharger;
        public void ConnectDevice()
        {
            throw new NotImplementedException();
        }

        public bool IsConnected()
        {
            throw new NotImplementedException();
        }

        public void StartCharge()
        {
            throw new NotImplementedException();
        }

        public void StopCharge()
        {
            throw new NotImplementedException();
        }
    }
}
