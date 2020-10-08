using Ladeskab.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab
{
    public class DoorSimulator : IDoor
    {
        //public event EventHandler<DoorEventArgs> DoorClosedEvent;
        public event EventHandler DoorClosedEvent;
        public event EventHandler DoorOpenEvent;

        public bool DoorOpen { get; private set; }
        public bool DoorLocked { get; private set; }

        public DoorSimulator()
        {
            DoorOpen = true;
            DoorLocked = false;
        }

        public void Close()
        {
            if (!DoorOpen)
                return;
            DoorOpen = false;
            RaiseDoorClosedEvent();
        }

        public void Lock()
        {
            if (DoorOpen)
                return;
            DoorLocked = true;
        }

        public void Open()
        {
            if (DoorOpen && !DoorLocked)
                return;
            DoorOpen = true;
            RaiseDoorOpenEvent();
        }

        public void Unlock()
        {
            if (!DoorLocked)
                return;
            DoorLocked = false;
        }

        protected virtual void RaiseDoorClosedEvent()
        {
            DoorClosedEvent?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void RaiseDoorOpenEvent()
        {
            DoorOpenEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
