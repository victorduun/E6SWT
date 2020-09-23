using NUnit.Framework;
using DoorControl.Interface;
namespace DoorControlTest
{
    public class Tests
    {

        private AlarmFake _alarmFake;
        private DoorFake _doorFake;
        private EntryNotificationFake _entryNotificationFake;
        private UserValidationFake _userValidationFake; 

        private DoorControl.DoorControl _doorControl;



        [SetUp]
        public void Setup()
        {
            _alarmFake = new AlarmFake();
            _doorFake = new DoorFake();
            _entryNotificationFake = new EntryNotificationFake();
            _userValidationFake = new UserValidationFake();
            _doorControl = new DoorControl.DoorControl(_alarmFake,_doorFake,_entryNotificationFake,_userValidationFake);
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
        public void RequestEntry_RequestEntryWithInvalidId_DoorClosed()
        {
            _userValidationFake.ValidateEntryRequestReturnValue = false; //Any id will be invalid
            _doorControl.RequestEntry(-100);
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