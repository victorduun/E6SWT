using NUnit.Framework;
using DoorControl.Interface;
using System.Threading;
using NSubstitute;

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
            _alarmFake = Substitute.For<IAlarm>();
            _doorFake = Substitute.For<IDoor>();
            _entryNotificationFake = Substitute.For<IEntryNotification>();
            _userValidationFake = Substitute.For<IUserValidation>();
            _doorControl = new DoorControl.DoorControl(_alarmFake, _doorFake, _entryNotificationFake, _userValidationFake);
        }

        [Test]
        public void DoorOpen_OpenedWithoutValidation_AlarmRaised()
        {
            _doorControl.DoorOpen();
            _alarmFake.Received(1).RaiseAlarm();
        }



        [Test]
        public void RequestEntry_RequestEntryWithValidId_DoorOpened()
        {
            _userValidationFake.ValidateEntryRequest(100).Returns(true);
            _doorControl.RequestEntry(100);
            _doorFake.Received(1).Open();

        }
        [Test]
        public void RequestEntry_RequestEntryWithInvalidId_DoorIsNotOpened()
        {
            _userValidationFake.ValidateEntryRequest(-100).Returns(false);
            _doorControl.RequestEntry(-100);
            _doorFake.Received(0).Open();

        }



        [Test]
        public void RequestEntry_EntryGrantedScenario_CorrectAmountOfCalls()
        {
            //Request access
            _userValidationFake.ValidateEntryRequest(100).Returns(true);
            _doorControl.RequestEntry(100);

            //Wait some amount of time
            Thread.Sleep(1);

            //Simulate door closing
            _doorFake.Close();
            _doorControl.DoorClosed();

            _doorFake.Received(1).Close();

        }

        [Test]
        public void RequestEntry_RequestEntryWithValidId_NotifyEntryGrantedIsCalled()
        {
            _userValidationFake.ValidateEntryRequest(100).Returns(true);
            _doorControl.RequestEntry(100);
            _entryNotificationFake.Received(1).NotifyEntryGranted();
        }

        [Test]
        public void RequestEntry_RequestEntryWithValidId_NotifyEntryDeniedIsCalled()
        {
            _userValidationFake.ValidateEntryRequest(-100).Returns(false); 
            _doorControl.RequestEntry(-100);
            _entryNotificationFake.Received(1).NotifyEntryDenied();
        }
    }
}