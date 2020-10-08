using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;
using Ladeskab;

namespace Ladeskab.Test
{
    public class Tests
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
            string correctString = "Skabet er l�st og din telefon lades. Brug dit RFID tag til at l�se op.";
            Assert.AreSame(correctString, _displaySimulator.DisplayValue);
        }

        [Test]
        public void DisplaySimulator_ShowConnectDevice_DisplayValueIsCorrect()
        {
            _displaySimulator.ShowConnectDevice();
            string correctString = "Tilslut telefon til laderen og luk d�ren.";
            Assert.AreSame(correctString, _displaySimulator.DisplayValue);
        }


        [Test]
        public void DisplaySimulator_ShowConnectionError_DisplayValueIsCorrect()
        {
            _displaySimulator.ShowConnectionError();
            string correctString = "Din telefon er ikke ordentlig tilsluttet. Pr�v igen.";
            Assert.AreSame(correctString, _displaySimulator.DisplayValue);
        }

        [Test]
        public void DisplaySimulator_ShowLoadRfid_DisplayValueIsCorrect()
        {
            _displaySimulator.ShowLoadRfid();
            string correctString = "Indl�s RFID tag vha. RFID l�seren.";
            Assert.AreSame(correctString, _displaySimulator.DisplayValue);
        }

        [Test]
        public void DisplaySimulator_ShowRemoveDevice_DisplayValueIsCorrect()
        {
            _displaySimulator.ShowRemoveDevice();
            string correctString = "Tag din telefon ud af skabet og luk d�ren";
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
    }
}