using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab.Interfaces
{
    public interface IChargeControl
    {
        void ConnectDevice();
        bool IsConnected();
        void StartCharge();
        void StopCharge();
    }
}
