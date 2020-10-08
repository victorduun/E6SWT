using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab.Interfaces
{
    public interface IStationControl
    {
        bool CheckId(int oldId, int Id);
        void RfidDetected();
    }
}
