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
    public class StationControlTest
    {

        private StationControl _uut;
        private IUsbCharger _usbController;
        private IDisplay _display;
        private IDoor _door;
        private IRfidReader _rfidReader;

        [SetUp]
        public void Setup()
        {
            _usbController = Substitute.For<IUsbCharger>();
            _display = Substitute.For<IDisplay>();
            _door = Substitute.For<IDoor>();
            _rfidReader = Substitute.For<IRfidReader>();

            _uut = new StationControl(_usbController, _door, _display, _rfidReader);
        }


        //Overcurrent boundary:
        //current > 500
        [TestCase(501)]
        [TestCase(5000)]
        [TestCase(50000)]
        public void StationControl_Overcurrent_DisplayChargingError(int currentValue)
        {
            
            //Setup usb controlller return values and invoke events (cause an overcurrent event)
            _usbController.CurrentValue.Returns(currentValue);
            var currentEventArgs = new CurrentEventArgs()
            {
                Current = currentValue,
            };
            _usbController.CurrentValueEvent += Raise.Event<EventHandler<CurrentEventArgs>>(this, currentEventArgs);

            //Assert that display shows correct error
            _display.Received().ShowOvercurrentError();


        }

        //Normal current boundary:
        //5 < current <= 500
        [TestCase(6)]
        [TestCase(250)]
        [TestCase(500)]
        [Test]
        public void StationControl_NormalCurrent_DisplayChargingNominal(int currentValue)
        {

            //Setup usb controlller return values and invoke events (cause an overcurrent event)
            _usbController.CurrentValue.Returns(currentValue);
            var currentEventArgs = new CurrentEventArgs()
            {
                Current = currentValue,
            };
            _usbController.CurrentValueEvent += Raise.Event<EventHandler<CurrentEventArgs>>(this, currentEventArgs);

            //Assert that display shows correct error
            _display.Received().ShowChargingNominal();
        }

        //Low current boundary:
        //0 < current <= 5
        [TestCase(5)]
        [TestCase(1)]
        [TestCase(2)]
        public void StationControl_LowCurrent_DisplayChargingFinished(int currentValue)
        {

            //Setup usb controlller return values and invoke events (cause an overcurrent event)
            _usbController.CurrentValue.Returns(currentValue);
            var currentEventArgs = new CurrentEventArgs()
            {
                Current = currentValue,
            };
            _usbController.CurrentValueEvent += Raise.Event<EventHandler<CurrentEventArgs>>(this, currentEventArgs);

            //Assert that display shows correct error
            _display.Received().ShowChargingFinished();
        }

        //Low current boundary:
        //0
        [TestCase(0)]
        public void StationControl_NoCurrent_DisplayUnchanged(int currentValue)
        {

            //Setup usb controlller return values and invoke events (cause an overcurrent event)
            _usbController.CurrentValue.Returns(currentValue);
            var currentEventArgs = new CurrentEventArgs()
            {
                Current = currentValue,
            };
            _usbController.CurrentValueEvent += Raise.Event<EventHandler<CurrentEventArgs>>(this, currentEventArgs);

            //Assert that display value does not change (no methods called)
            var calls = _display.ReceivedCalls();
            Assert.AreEqual(0, calls.ToList().Count);
        }

    }
}
