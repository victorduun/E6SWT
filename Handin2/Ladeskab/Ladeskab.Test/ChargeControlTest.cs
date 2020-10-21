using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ladeskab.Interfaces;
using Ladeskab.USB;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.Test
{
    [TestFixture]
     public class ChargeControlTest
    {
            ChargeControl _uut;
            private  IUsbCharger _usbCharger;
            private  IDisplay _display;
            [SetUp]
            public void Setup()
            {
                _usbCharger = Substitute.For<IUsbCharger>();
                _display = Substitute.For<IDisplay>();
                _uut = new ChargeControl(_usbCharger, _display);
            }

            [Test]
            public void ChargeControl_Device_Disconnected()
            {
                _uut.DisconnectDevice();
                Assert.That(_uut.IsConnected, Is.False);

            }

            [Test]
            public void ChargeControl_Device_Connected()
            {
                _uut.ConnectDevice();
                Assert.That(_uut.IsConnected, Is.True);
            }

            [Test]
            public void ChargeControl_Device_Stop_Charge()
            {
                _uut.StopCharge();
                double CurrentValueEnd = _usbCharger.CurrentValue;
                Assert.That(_usbCharger.CurrentValue, Is.EqualTo(CurrentValueEnd));
            }

            [Test]
            public void ChargeControl_Device_Start_Charge()
            {
                _uut.ConnectDevice();
                _uut.StartCharge();
                double CurrentValueStart = _usbCharger.CurrentValue;
                Assert.That(_usbCharger.CurrentValue, Is.EqualTo(CurrentValueStart));
            }

            [Test]
            public void ChargeControl_Device_try_Charge()
            {
                _uut.DisconnectDevice();
                try
                { 
                    _uut.StartCharge();
                    Assert.Fail("No exception thrown");
                }
                catch (Exception e)
                {
                    Assert.Pass();
                }
            }
            [Test]
            public void ChargeControl_Device_exception_thrown()
            {
                _uut.ConnectDevice();
                Assert.DoesNotThrow(_uut.StartCharge);
                
            }

            [Test]
            public void ChargeControl_CurrentValueChanged_0_test() 
            {
                double currentValue = 0;
                _usbCharger.CurrentValueEvent += (sender, args) => currentValue = args.Current;
                
                Assert.AreSame("", _display.DisplayValue);
            }


            [TestCase(0.1)]
            [TestCase(2)]
            [TestCase(5)]
        public void ChargeControl_CurrentValueChangedBetween0and5_DisplayChargingFinishedCalled(double currentValue)
            {
                //Arrange
                CurrentEventArgs currentEventArgs = new CurrentEventArgs()
                {
                    Current = currentValue,
                };
                //Act?
                _usbCharger.CurrentValueEvent += Raise.EventWith(currentEventArgs);

                //Assert
                _display.Received().ShowChargingFinished();
            }

        [TestCase(5.1)]
        [TestCase(200)]
        [TestCase(500)]
        public void ChargeControl_CurrentValueChangedBetween5and500_DisplayChargingNominalCalled(double currentValue)
        {
            //Arrange
            CurrentEventArgs currentEventArgs = new CurrentEventArgs()
            {
                Current = currentValue,
            };
            //Act?
            _usbCharger.CurrentValueEvent += Raise.EventWith(currentEventArgs);

            //Assert
            _display.Received().ShowChargingNominal();
        }

        [TestCase(0)]
        public void ChargeControl_CurrentValueChanged0_DisplayDoesNotUpdate(double currentValue)
        {
            //Arrange
            CurrentEventArgs currentEventArgs = new CurrentEventArgs()
            {
                Current = currentValue,
            };
            //Act?
            _usbCharger.CurrentValueEvent += Raise.EventWith(currentEventArgs);

            //Assert
            var calls = _display.ReceivedCalls();
            Assert.That(calls.Count() == 0);
        }

    }
}
