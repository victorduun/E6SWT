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

        [SetUp]
        public void Setup()
        {
            _chargeControl = Substitute.For<IChargeControl>();
            _usbController = Substitute.For<IUsbCharger>();
            _display = Substitute.For<IDisplay>();
            _door = Substitute.For<IDoor>();
            _rfidReader = Substitute.For<IRfidReader>();

            //_uut = new StationControl(_usbController, _door, _display, _rfidReader);
            _uut = new StationControl(_chargeControl, _door, _display, _rfidReader);
        }


    }
}
