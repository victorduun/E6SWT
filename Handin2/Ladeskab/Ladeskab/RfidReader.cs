using System;
using System.Collections.Generic;
using System.Text;
using Ladeskab.Interfaces;

namespace Ladeskab
{
    public class RfidReader : IRfidReader
    {
        public event EventHandler<int> RfidDetectedEvent;
        public void OnRfidRead(int id)
        {
            if (RfidDetectedEvent != null)
                RfidDetectedEvent(this, id);
        }
    }
}
