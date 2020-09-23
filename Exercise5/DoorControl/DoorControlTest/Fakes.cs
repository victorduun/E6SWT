using System;
using System.Collections.Generic;
using System.Text;
using DoorControl.Interface;

namespace DoorControlTest
{
    public class AlarmFake : IAlarm
    {
        public int RaiseAlarmCalledCount { get; set; } = 0;
        public void RaiseAlarm()
        {
            RaiseAlarmCalledCount++;
        }
    }
    public class DoorFake : IDoor
    {

        public int OpenCalledCount { get; set; } = 0;
        public int CloseCalledCount { get; set; } = 0;
        public void Open()
        {
            OpenCalledCount++;
        }

        public void Close()
        {
            CloseCalledCount++;
        }
    }
    public class EntryNotificationFake : IEntryNotification
    {
        public int NotifyEntryDeniedCalledCount { get; set; } = 0;
        public int NotifyEntryGrantedCalledCount { get; set; } = 0;
        public void NotifyEntryDenied()
        {
            NotifyEntryDeniedCalledCount++;
        }

        public void NotifyEntryGranted()
        {
            NotifyEntryGrantedCalledCount++;
        }
    }
    public class UserValidationFake : IUserValidation
    {
        public bool ValidateEntryRequestReturnValue { get; set; } = false;
        public bool ValidateEntryRequest(int id)
        {
            return ValidateEntryRequestReturnValue;
        }
    }
}
