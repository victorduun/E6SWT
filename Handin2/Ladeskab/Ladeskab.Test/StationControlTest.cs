using System;
using System.Collections.Generic;
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

        [SetUp]
        public void Setup()
        {
            _usbController = Substitute.For<IUsbCharger>();
            _display = Substitute.For<IDisplay>();
            _door = Substitute.For<IDoor>();

            _uut = new StationControl(_usbController, _door, _display);
        }

        [Test]
        public void StationControl_Overcurrent_DisplayValueIsCorrect()
        {
            Assert.Fail("TODO: Fix this test; connect a device, cause an overcurrent event and assert that the display shows an error message");

            //Setup usb controlller return values and invoke events (cause an overcurrent event)
            //Max 500 mA
            int overcurrentValue = 1000;
            _usbController.CurrentValue.Returns(overcurrentValue);
            var currentEventArgs = new CurrentEventArgs()
            {
                Current = overcurrentValue,
            };
            _usbController.CurrentValueEvent += Raise.Event<EventHandler<CurrentEventArgs>>(this, currentEventArgs);

            //Assert that display shows correct error
            _display.Received().ShowOvercurrentError();


        }




    }
}
