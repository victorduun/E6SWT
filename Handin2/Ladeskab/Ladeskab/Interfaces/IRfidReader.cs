using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab.Interfaces
{
    public interface IRfidReader
    {
        event EventHandler<int> RfidDetectedEvent;
        void ReadRfidCard(int cardNumber);
    }
}
