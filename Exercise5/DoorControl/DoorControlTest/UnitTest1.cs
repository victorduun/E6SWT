using NUnit.Framework;
using DoorControl.Interface;
namespace DoorControlTest
{
    public class Tests
    {

        private readonly AlarmFake _alarmFake = new AlarmFake();
        private readonly DoorFake _doorFake = new DoorFake();
        private readonly EntryNotificationFake _entryNotificationFake = new EntryNotificationFake();
        private readonly UserValidationFake _userValidationFake = new UserValidationFake(); 

        private DoorControl.DoorControl _doorControl;



        [SetUp]
        public void Setup()
        {
            _doorControl = new DoorControl.DoorControl(_alarmFake,_doorFake,_entryNotificationFake,_userValidationFake);

        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}