using System;

namespace Ladeskab.Interfaces
{
    public interface IDisplay
    {

        void ShowChargingLockerOccupied();
        void ShowConnectDevice();
        void ShowConnectionError();
        void ShowLoadRfid();
        void ShowRemoveDevice();
        void ShowRfidError();


    }
}
