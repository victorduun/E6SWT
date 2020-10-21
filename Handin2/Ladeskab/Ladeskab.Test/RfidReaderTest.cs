using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NSubstitute;
using Ladeskab.Interfaces;
using Ladeskab.USB;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.DataCollection;
namespace Ladeskab.Test
{

    [TestFixture]
    class RfidReaderTest
    {
        private RfidReader _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new RfidReader();
        }

        [Test]
        public void RfidReader_OnRfidRead_setIdTo123_EventRaisedWithArgs123()
        {
            //arrange
            int id = 123;
            int eventId = 0;
            _uut.RfidDetectedEvent += (sender, args) => eventId = args;

            //act
            _uut.OnRfidRead(123);

            //assert
            Assert.That(eventId, Is.EqualTo(id));
        }
    }

}

