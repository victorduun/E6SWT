using System;
using DoorControl.Interface;

namespace DoorControl
{

    public enum DoorControlState
    {
        DoorClosed,
        DoorOpened,
        DoorBreached,
        Validating,
    }




    public class DoorControl
    {
       

        private readonly IAlarm _alarm;
        private readonly IDoor _door;
        private readonly IEntryNotification _entryNotification;
        private readonly IUserValidation _userValidation;

        private DoorControlState _controlState;



        public DoorControl(IAlarm alarm, IDoor door, IEntryNotification entryNotification, IUserValidation userValidation)
        {
            _alarm = alarm;
            _door = door;
            _entryNotification = entryNotification;
            _userValidation = userValidation;

            _controlState = DoorControlState.DoorClosed;
        }


        public void DoorOpen()
        {
            switch (_controlState)
            {
                case DoorControlState.DoorClosed:
                    _controlState = DoorControlState.DoorBreached;
                    _alarm.RaiseAlarm();
                    break;
                case DoorControlState.Validating:
                    _controlState = DoorControlState.DoorOpened;
                    break;

            }
        }

        public void DoorClosed()
        {
            switch (_controlState)
            {
                case DoorControlState.DoorOpened:
                    _controlState = DoorControlState.DoorClosed;
                    break;
                case DoorControlState.Validating:
                    _controlState = DoorControlState.DoorClosed;
                    break;

            }
        }


        public void RequestEntry(int id)
        {
            switch (_controlState)
            {
                case DoorControlState.DoorClosed:
                    _controlState = DoorControlState.Validating;
                    bool isValidId = _userValidation.ValidateEntryRequest(id);
                    if (isValidId)
                    {
                        _entryNotification.NotifyEntryGranted();
                        _door.Open();
                        DoorOpen();
                    }
                    else
                    {
                        _entryNotification.NotifyEntryDenied();
                        _door.Close();
                        DoorClosed();

                    }
                    break;

            }

            
        }


    }
}
