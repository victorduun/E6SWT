using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ladeskab.Interfaces;
using Ladeskab.USB;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.Test
{
    [TestFixture]
    public class StationControlTest
    {

        private StationControl _uut;
        private IChargeControl _chargeControl;
        private IUsbCharger _usbController;
        private IDisplay _display;
        private IDoor _door;
        private IRfidReader _rfidReader;
        private ILog _logger;

        [SetUp]
        public void Setup()
        {
            _chargeControl = Substitute.For<IChargeControl>();
            _usbController = Substitute.For<IUsbCharger>();
            _display = Substitute.For<IDisplay>();
            _door = Substitute.For<IDoor>();
            _rfidReader = Substitute.For<IRfidReader>();
            _logger = Substitute.For<ILog>();

            //_uut = new StationControl(_usbController, _door, _display, _rfidReader);
            _uut = new StationControl(_chargeControl, _door, _display, _rfidReader, _logger);
        }

        public void GetStationControlInLockedStateWithOldId123()
        {
            _chargeControl.IsConnected().Returns(true);

            //act
            _rfidReader.RfidDetectedEvent += Raise.Event<EventHandler<int>>(_rfidReader, 123);

        }

        [Test]
        public void StationsControl_DoorOpenInAvailableState_ShowConnectedDeviceIsCalled()
        {
            //Station control is in available state when constructed
            _door.DoorOpenEvent += Raise.EventWith(EventArgs.Empty);

            //Check if ShowConnectDevice(); is called
            _display.Received().ShowConnectDevice();
        }

        [Test]
        public void StationsControl_DoorClosedInDoorOpenState_ShowLoadRfidIsCalled()
        {
            //Station control is in available state when constructed
            _door.DoorOpenEvent += Raise.EventWith(EventArgs.Empty);
            //State is now DoorOpen
            //Call DoorClosedEvent
            _door.DoorClosedEvent += Raise.EventWith(EventArgs.Empty);

            //Check if ShowLoadRfid(); is called
            _display.Received().ShowLoadRfid();
        }

        [Test]
        public void RfidDectected_RfidDetectedInAvailableState_ChargeControlNotConnected_DisplayConnectionErrorCalled()
        {
            //arrange
            _chargeControl.IsConnected().Returns(false);

            //act
            _rfidReader.RfidDetectedEvent += Raise.Event<EventHandler<int>>(_rfidReader, 123);

            //assert
            _display.Received().ShowConnectionError();
        }

        [Test]
        public void RfidDectected_RfidDetectedInAvailableState_ChargeControlConnected_StartCharging()
        {
            //arrange
            _chargeControl.IsConnected().Returns(true);

            //act
            _rfidReader.RfidDetectedEvent += Raise.Event<EventHandler<int>>(_rfidReader, 123);

            //assert
            _chargeControl.Received().StartCharge();
        }

        [Test]
        public void RfidDectected_RfidDetectedInAvailableState_ChargeControlConnected_DoorLockCalled()
        {
            //arrange
            _chargeControl.IsConnected().Returns(true);

            //act
            _rfidReader.RfidDetectedEvent += Raise.Event<EventHandler<int>>(_rfidReader, 123);

            //assert
            _door.Received().Lock();
        }


        [Test]
        public void RfidDectected_RfidDetectedInAvailableState_ChargeControlConnected_Log_doorlocked_id()
        {
            //arrange
            _chargeControl.IsConnected().Returns(true);


            //act
            _rfidReader.RfidDetectedEvent += Raise.Event<EventHandler<int>>(_rfidReader, 123);

            //assert
            _logger.Received().LogDoorLocked(123);
            
        }

        [Test]
        public void RfidDectected_RfidDetectedInAvailableState_ChargeControlConnected_DisplayLockerOccupied()
        {
            //arrange
            _chargeControl.IsConnected().Returns(true);

            //act
            _rfidReader.RfidDetectedEvent += Raise.Event<EventHandler<int>>(_rfidReader, 123);

            //assert
            _display.Received().ShowChargingLockerOccupied();
        }

        [Test]
        public void RfidDectected_RfidDetectedInAvailableState_ChargeControlConnectedButStartChargeFails_DisplayConnectionError()
        {
            //arrange
            _chargeControl.IsConnected().Returns(true);

            _chargeControl
                .When(c => c.StartCharge())
                .Do(c => throw new NotConnectedException());

            //act
            _rfidReader.RfidDetectedEvent += Raise.Event<EventHandler<int>>(_rfidReader, 123);

            //assert
            _display.Received().ShowConnectionError();
        }


        [Test]
        public void RfidDectected_RfidDetectedInLockedStateWithIncorrectId_DisplayRfidError()
        {
            //arrange
            GetStationControlInLockedStateWithOldId123();
            
            //act
            _rfidReader.RfidDetectedEvent += Raise.Event<EventHandler<int>>(_rfidReader, 10000);

            //assert
            _display.Received().ShowRfidError();
        }

        [Test]
        public void RfidDectected_RfidDetectedInLockedStateWithCorrectId_DisplayShowRemoveDevice()
        {
            //arrange
            GetStationControlInLockedStateWithOldId123();

            //act
            _rfidReader.RfidDetectedEvent += Raise.Event<EventHandler<int>>(_rfidReader, 123);

            //assert
            _display.Received().ShowRemoveDevice();
        }

        
        [Test]
        public void RfidDectected_RfidDetectedInLockedStateWithCorrectId_DoorUnlockCalled()
        {
            //arrange
            GetStationControlInLockedStateWithOldId123();

            //act
            _rfidReader.RfidDetectedEvent += Raise.Event<EventHandler<int>>(_rfidReader, 123);

            //assert
            _door.Received().Unlock();
        }

        [Test]
        public void RfidDectected_RfidDetectedInLockedStateWithCorrectId_LogDoorUnlockedCalled()
        {
            //arrange
            GetStationControlInLockedStateWithOldId123();

            //act
            _rfidReader.RfidDetectedEvent += Raise.Event<EventHandler<int>>(_rfidReader, 123);

            //assert
            _logger.Received().LogDoorUnlocked(123);
        }

        [Test]
        public void RfidDectected_RfidDetectedInDoorOpenState_DisplayNothing()
        {
            //arrange
            _door.DoorOpenEvent += Raise.EventWith(EventArgs.Empty);
            _display.ClearReceivedCalls();
            //Act
            _rfidReader.RfidDetectedEvent += Raise.Event<EventHandler<int>>(_rfidReader, 123);
            //Assert
            var calls = _display.ReceivedCalls();
            Assert.That(!calls.Any());

        }
    }
}
