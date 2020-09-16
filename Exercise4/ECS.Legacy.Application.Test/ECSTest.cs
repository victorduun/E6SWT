using NUnit.Framework;

namespace ECS.Legacy.Application.Test
{
    public class Tests
    {
        ECS _ecs;

        //Values are initialized such that temperature sensor returns values below the thresholds
        const int FakeTempSensorValue = 10;
        const int InitializeHeaterThreshold = 30;
        const int InitializeWindowThreshold = 35;
        FakeHeater _fakeheater;
        FakeTempSensor _faketempSen;
        FakeWindow _fakeWindow;
        [SetUp]
        public void Setup()
        {
            _fakeheater = new FakeHeater();
            _faketempSen = new FakeTempSensor(FakeTempSensorValue);
            _fakeWindow = new FakeWindow();
            _ecs = new ECS(_fakeheater, _faketempSen, _fakeWindow, InitializeHeaterThreshold, InitializeWindowThreshold);
        }

        [Test]
        public void GetCurTemp_ReturnTempSensorValue_ReturnsTempSensorValue()
        {
            Assert.That(_ecs.GetCurTemp(), Is.EqualTo(FakeTempSensorValue));
        }
        [Test]
        public void GetHeaterThreshold_ReturnsInitializedHeaterThreshold_ReturnsInitializedHeaterThreshold()
        {
            Assert.That(_ecs.GetHeaterThreshold(), Is.EqualTo(InitializeHeaterThreshold));
        }
        [Test]
        public void SetHeaterThreshold_SetNewHeaterThreshold_GetThresholdReturnsNewHeaterThreshold()
        {
            int newThreshold = 20;
            _ecs.SetHeaterThreshold(newThreshold);
            Assert.That(_ecs.GetHeaterThreshold(), Is.EqualTo(newThreshold));
        }

        [Test]
        public void GetWindowThreshold_ReturnsInitializedWindowThreshold_ReturnsInitializedWindowThreshold()
        {
            Assert.That(_ecs.GetWindowThreshold(), Is.EqualTo(InitializeWindowThreshold));
        }
        [Test]
        public void SetWindowThreshold_SetNewWindowThreshold_GetThresholdReturnsNewWindowThreshold()
        {
            int newThreshold = 20;
            _ecs.SetWindowThreshold(newThreshold);
            Assert.That(_ecs.GetWindowThreshold(), Is.EqualTo(newThreshold));
        }

        [Test]
        public void Regulate_TemperatureBelowHeaterThreshold_HeaterIsTurnedOn()
        {
            _ecs.Regulate();
            Assert.That(_fakeheater.IsOn, Is.True);
        }

        [Test]
        public void Regulate_TemperatureAboveHeaterThreshold_HeaterIsTurnedOff()
        {
            _faketempSen.SetGetTempReturnVal(40);
            _ecs.Regulate();
            Assert.That(_fakeheater.IsOn, Is.False);
        }

        [Test]
        public void Regulate_TemperatureBelowWindowThreshold_WindowIsClosed()
        {
            _ecs.Regulate();
            Assert.That(_fakeWindow.IsOpen, Is.False);
        }

        [Test]
        public void Regulate_TemperatureSameAsWindowThreshold_WindowIsClosed()
        {
            _faketempSen.SetGetTempReturnVal(35);
            _ecs.Regulate();
            Assert.That(_fakeWindow.IsOpen, Is.False);
        }

        [Test]
        public void Regulate_TemperatureAboveWindowThreshold_WindowIsOpen()
        {
            _faketempSen.SetGetTempReturnVal(40);
            _ecs.Regulate();
            Assert.That(_fakeWindow.IsOpen, Is.True);
        }
    }
}   