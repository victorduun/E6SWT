using System;
using System.Collections.Generic;
using System.Text;
using Ladeskab.Interfaces;

namespace Ladeskab
{
    class RfidReader : IRfidReader
    {
        public event EventHandler<int> RfidDetectedEvent;
        public void ReadRfidCard(int cardNumber)
        {
            throw new NotImplementedException();
        }
    }
}
