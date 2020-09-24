using NUnit.Framework;
using DoorControl.Interface;
using System.Threading;

namespace DoorControlTestNSubstitute
{
    public class Tests
    {

        private IAlarm _alarmFake;
        private IDoor _doorFake;
        private IEntryNotification _entryNotificationFake;
        private IUserValidation _userValidationFake;

        private DoorControl.DoorControl _doorControl;



        [SetUp]
        public void Setup()
        {
            _alarmFake = new AlarmFake();
            _doorFake = new DoorFake();
            _entryNotificationFake = new EntryNotificationFake();
            _userValidationFake = new UserValidationFake();
            _doorControl = new DoorControl.DoorControl(_alarmFake, _doorFake, _entryNotificationFake, _userValidationFake);
        }

        [Test]
        public void DoorOpen_OpenedWithoutValidation_AlarmRaised()
        {
            _doorControl.DoorOpen();
            Assert.That(_alarmFake.RaiseAlarmCalledCount, Is.EqualTo(1));
        }



        [Test]
        public void RequestEntry_RequestEntryWithValidId_DoorOpened()
        {
            _userValidationFake.ValidateEntryRequestReturnValue = true; //Any id will be valid
            _doorControl.RequestEntry(100);
            Assert.That(_doorFake.OpenCalledCount, Is.EqualTo(1));

        }
        [Test]
        public void RequestEntry_RequestEntryWithInvalidId_DoorIsNotOpened()
        {
            _userValidationFake.ValidateEntryRequestReturnValue = false; //Any id will be invalid
            _doorControl.RequestEntry(-100);
            Assert.That(_doorFake.OpenCalledCount, Is.EqualTo(0));

        }



        [Test]
        public void RequestEntry_EntryGrantedScenario_CorrectAmountOfCalls()
        {
            //Request access
            _userValidationFake.ValidateEntryRequestReturnValue = true; //Any id will be valid
            _doorControl.RequestEntry(100);

            //Wait some amount of time
            Thread.Sleep(1);

            //Simulate door closing
            _doorFake.Close();
            _doorControl.DoorClosed();

            Assert.That(_doorFake.CloseCalledCount, Is.EqualTo(1));

        }

        [Test]
        public void RequestEntry_RequestEntryWithValidId_NotifyEntryGrantedIsCalled()
        {
            _userValidationFake.ValidateEntryRequestReturnValue = true; //Any id will be valid
            _doorControl.RequestEntry(100);
            Assert.That(_entryNotificationFake.NotifyEntryGrantedCalledCount, Is.EqualTo(1));
        }

        [Test]
        public void RequestEntry_RequestEntryWithValidId_NotifyEntryDeniedIsCalled()
        {
            _userValidationFake.ValidateEntryRequestReturnValue = false; //Any id will be invalid
            _doorControl.RequestEntry(-100);
            Assert.That(_entryNotificationFake.NotifyEntryDeniedCalledCount, Is.EqualTo(1));
        }
    }
}