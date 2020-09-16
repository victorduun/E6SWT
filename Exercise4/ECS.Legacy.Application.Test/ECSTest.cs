using NUnit.Framework;

namespace ECS.Legacy.Application.Test
{
    public class Tests
    {
        ECS _ecs;

        const int FakeTempSensorValue = 10;
        const int InitializeThreshold = 30;
        FakeHeater _fakeheater;
        FakeTempSensor _faketempSen;
        FakeWindow _fakeWindow;
        [SetUp]
        public void Setup()
        {
            _fakeheater = new FakeHeater();
            _faketempSen = new FakeTempSensor(FakeTempSensorValue);
            _fakeWindow = new FakeWindow();
            FakeRandomNumberGenerator _fakerandomnumbergenerator = new FakeRandomNumberGenerator();
            _ecs = new ECS(_fakeheater, _faketempSen, _fakeWindow, InitializeThreshold);
        }

        [Test]
        public void GetCurTemp_ReturnTempSensorValue_ReturnsTempSensorValue()
        {
            Assert.That(_ecs.GetCurTemp(), Is.EqualTo(FakeTempSensorValue));
        }
        [Test]
        public void GetThreshold_ReturnsInitializedThreshold_ReturnsInitializedThreshold()
        {
            Assert.That(_ecs.GetThreshold(), Is.EqualTo(InitializeThreshold));
        }
        [Test]
        public void SetThreshold_SetNewThreshold_GetThresholdReturnsNewThreshold()
        {
            int newThreshold = 20;
            _ecs.SetThreshold(newThreshold);
            Assert.That(_ecs.GetThreshold(), Is.EqualTo(newThreshold));
        }

        [Test]
        public void Regulate_TemperatureBelowThreshold_HeaterIsTurnedOn()
        {
            _ecs.Regulate();
            Assert.That(_fakeheater.IsOn, Is.True);
        }

        [Test]
        public void Regulate_TemperatureAboveThreshold_HeaterIsTurnedOff()
        {
            _faketempSen.SetGetTempReturnVal(40);
            _ecs.Regulate();
            Assert.That(_fakeheater.IsOn, Is.False);
        }

        [Test]
        public void Regulate_TemperatureBelowThreshold_WindowIsClosed()
        {
            _ecs.Regulate();
            Assert.That(_fakeWindow.IsOpen, Is.False);
        }
    }
}   