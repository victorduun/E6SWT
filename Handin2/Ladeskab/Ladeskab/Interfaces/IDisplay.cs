using System;

namespace Ladeskab.Interfaces
{
    public interface IDisplay
    {
        string DisplayValue { get; }
        void ShowChargingLockerOccupied();
        void ShowConnectDevice();
        void ShowConnectionError();
        void ShowLoadRfid();
        void ShowRemoveDevice();
        void ShowRfidError();


    }
}
