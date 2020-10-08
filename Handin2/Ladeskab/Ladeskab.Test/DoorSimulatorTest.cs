using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;
using Ladeskab;


namespace Ladeskab.Test
{
    class DoorSimulatorTests
    {
        DoorSimulator _doorSimulator;

        [SetUp]
        public void Setup()
        {
            _doorSimulator = new DoorSimulator();
        }

        [Test]
        public void DoorSimulator_CloseDoor_DoorIsClosed()
        {
            _doorSimulator.Close();
            Assert.That(_doorSimulator.DoorOpen, Is.False);
        }

        [Test]
        public void DoorSimulator_OpenDoor_DoorIsOpen()
        {
            _doorSimulator.Close();
            _doorSimulator.Open();
            Assert.That(_doorSimulator.DoorOpen, Is.True);
        }

        [Test]
        public void DoorSimulator_LockDoorClosedDoor_DoorIsLocked()
        {
            _doorSimulator.Close();
            _doorSimulator.Lock();
            Assert.That(_doorSimulator.DoorLocked, Is.True);
        }

        [Test]
        public void DoorSimulator_UnlockDoorClosedDoor_DoorIsUnlocked()
        {
            _doorSimulator.Close();
            _doorSimulator.Lock();
            _doorSimulator.Unlock();
            Assert.That(_doorSimulator.DoorLocked, Is.False);
        }
    }
}
