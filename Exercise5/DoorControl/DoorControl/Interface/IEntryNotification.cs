using System;
using System.Collections.Generic;
using System.Text;

namespace DoorControl.Interface
{
    public interface IEntryNotification
    {
        void NotifyEntryDenied();
        void NotifyEntryGranted();
    }
}
