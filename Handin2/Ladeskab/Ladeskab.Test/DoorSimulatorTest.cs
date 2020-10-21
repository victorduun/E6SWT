using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;
using Ladeskab;


namespace Ladeskab.Test
{
    [TestFixture]
    public class DoorSimulatorTests
    {
        DoorSimulator _doorSimulator;

        [SetUp]
        public void Setup()
        {
            _doorSimulator = new DoorSimulator();
        }

        [Test]
        public void DoorSimulator_CloseDoorWhenDoorIsOpen_DoorIsClosed()
        {
            _doorSimulator.Open();
            _doorSimulator.Close();
            Assert.That(_doorSimulator.DoorOpen, Is.False);
        }

        [Test]
        public void DoorSimulator_CloseDoorWhenItsAlreadyClosed_DoorIsClosed()
        {
            _doorSimulator.Close();
            Assert.That(_doorSimulator.DoorOpen, Is.False);
        }

        [Test]
        public void DoorSimulator_OpenDoor_DoorIsOpen()
        {
            _doorSimulator.Open();
            Assert.That(_doorSimulator.DoorOpen, Is.True);
        }

        [Test]
        public void DoorSimulator_OpenDoorWhenItsAlreadyOpen_DoorIsOpen()
        {
            _doorSimulator.Open();
            _doorSimulator.Open();
            Assert.That(_doorSimulator.DoorOpen, Is.True);
        }

        [Test]
        public void DoorSimulator_LockDoorWithClosedDoor_DoorIsLocked()
        {
            _doorSimulator.Lock();
            Assert.That(_doorSimulator.DoorLocked, Is.True);
        }

        [Test]
        public void DoorSimulator_LockDoorWithOpenDoor_DoorIsUnlocked()
        {
            _doorSimulator.Open();
            _doorSimulator.Lock();
            Assert.That(_doorSimulator.DoorLocked, Is.False);
        }

        [Test]
        public void DoorSimulator_UnlockDoorClosedDoor_DoorIsUnlocked()
        {
            _doorSimulator.Lock();
            _doorSimulator.Unlock();
            Assert.That(_doorSimulator.DoorLocked, Is.False);
        }

        [Test]
        public void DoorSimulator_OpenDoorWhenItsLocked_DoorCantOpen()
        {
            _doorSimulator.Lock();
            _doorSimulator.Open();
            Assert.That(_doorSimulator.DoorOpen, Is.False);
        }

    }
}
