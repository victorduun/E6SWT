using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab.Interfaces
{
    public interface IDoor
    {
        event EventHandler DoorClosedEvent;
        event EventHandler DoorOpenEvent;
        void Close();
        void Open();
        void Unlock();
        void Lock();
    }
}