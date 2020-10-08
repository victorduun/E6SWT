using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab.Interfaces
{
    public interface IChargeControl
    {
        void ConnectDevice();
        void DisconnectDevice();
        bool IsConnected();
        void StartCharge();
        void StopCharge();
    }
}
