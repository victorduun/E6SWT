using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;
using Ladeskab;

namespace Ladeskab.Test
{

    [TestFixture]
    public class DisplaySimulatorTests
    {
        DisplaySimulator _displaySimulator;


        [SetUp]
        public void Setup()
        {
            _displaySimulator = new DisplaySimulator();

        }

        [Test]
        public void DisplaySimulator_ShowChargingLockerOccupied_DisplayValueIsCorrect()
        {
            _displaySimulator.ShowChargingLockerOccupied();
            string correctString = "Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.";
            Assert.AreSame(correctString, _displaySimulator.DisplayValue);
        }

        [Test]
        public void DisplaySimulator_ShowConnectDevice_DisplayValueIsCorrect()
        {
            _displaySimulator.ShowConnectDevice();
            string correctString = "Tilslut telefon til laderen og luk døren.";
            Assert.AreSame(correctString, _displaySimulator.DisplayValue);
        }


        [Test]
        public void DisplaySimulator_ShowConnectionError_DisplayValueIsCorrect()
        {
            _displaySimulator.ShowConnectionError();
            string correctString = "Din telefon er ikke ordentlig tilsluttet. Prøv igen.";
            Assert.AreSame(correctString, _displaySimulator.DisplayValue);
        }

        [Test]
        public void DisplaySimulator_ShowLoadRfid_DisplayValueIsCorrect()
        {
            _displaySimulator.ShowLoadRfid();
            string correctString = "Indlæs RFID tag vha. RFID læseren.";
            Assert.AreSame(correctString, _displaySimulator.DisplayValue);
        }

        [Test]
        public void DisplaySimulator_ShowRemoveDevice_DisplayValueIsCorrect()
        {
            _displaySimulator.ShowRemoveDevice();
            string correctString = "Tag din telefon ud af skabet og luk døren";
            Assert.AreSame(correctString, _displaySimulator.DisplayValue);
        }

        [Test]
        public void DisplaySimulator_ShowRfidError_DisplayValueIsCorrect()
        {
            _displaySimulator.ShowRfidError();
            string correctString = "Forkert RFID tag";
            Assert.AreSame(correctString, _displaySimulator.DisplayValue);
        }

        [Test]
        public void DisplaySimulator_ShowOvercurrentError_DisplayValueIsCorrect()
        {
            _displaySimulator.ShowOvercurrentError();
            string correctString = "Der skete en fejl under opladningen. Opladningen blev afbrudt.";
            Assert.AreSame(correctString, _displaySimulator.DisplayValue);
        }

        [Test]
        public void DisplaySimulator_ShowChargingNominal_DisplayValueIsCorrect()
        {
            _displaySimulator.ShowChargingNominal();
            string correctString = "Telefonen er i gang med at blive opladt.";
            Assert.AreSame(correctString, _displaySimulator.DisplayValue);
        }

        [Test]
        public void DisplaySimulator_ShowChargingFinished_DisplayValueIsCorrect()
        {
            _displaySimulator.ShowChargingFinished();
            string correctString = "Opladning afsluttet. Telefonen er fuldt opladt.";
            Assert.AreSame(correctString, _displaySimulator.DisplayValue);
        }

    }
}