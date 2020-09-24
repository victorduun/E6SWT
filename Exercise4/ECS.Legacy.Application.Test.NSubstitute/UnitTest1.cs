using NUnit.Framework;
using NSubstitute;
namespace ECS.Legacy.Application.Test
{
    public class Tests
    {
        ECS _ecs;

        //Values are initialized such that temperature sensor returns values below the thresholds

        const int InitializeHeaterThreshold = 30;
        const int InitializeWindowThreshold = 35;
        IHeater _fakeheater;
        ITempSensor _faketempSen;
        IWindow _fakeWindow;
        [SetUp]
        public void Setup()
        {
            _fakeheater = Substitute.For<IHeater>();
            _fakeWindow = Substitute.For<IWindow>();
            _faketempSen = Substitute.For<ITempSensor>();
            _ecs = new ECS(_fakeheater, _faketempSen, _fakeWindow, InitializeHeaterThreshold, InitializeWindowThreshold);

        }

        [Test]
        public void GetCurTemp_ReturnTempSensorValue_ReturnsTempSensorValue()
        {
            int fakeTempSensorValue = 10;
            _faketempSen.GetTemp().Returns(fakeTempSensorValue);
            Assert.That(_ecs.GetCurTemp(), Is.EqualTo(fakeTempSensorValue));
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
            //Act
            int newThreshold = 20;
            _ecs.SetWindowThreshold(newThreshold);
            //Assert
            Assert.That(_ecs.GetWindowThreshold(), Is.EqualTo(newThreshold));
        }

        [Test]
        public void Regulate_TemperatureBelowHeaterThreshold_HeaterIsTurnedOn()
        {
            //Act
            _ecs.Regulate();
            //Assert
            _fakeheater.Received(1).TurnOn();
        }

        [Test]
        public void Regulate_TemperatureAboveHeaterThreshold_HeaterIsTurnedOff()
        {
            //Act
            _ecs.Regulate();
            //Assert
            _fakeheater.Received(1).TurnOff();
        }

        [Test]
        public void Regulate_TemperatureBelowWindowThreshold_WindowIsClosed()
        {
            //Arrange
            int fakeTempSensorValue = InitializeWindowThreshold - 10;
            _faketempSen.GetTemp().Returns(fakeTempSensorValue);
            //Act
            _ecs.Regulate();
            //Assert
            _fakeWindow.Received(1).OpenWindow();
        }

        [Test]
        public void Regulate_TemperatureSameAsWindowThreshold_WindowIsClosed()
        {
            //Arrange
            int fakeTempSensorValue = InitializeWindowThreshold;
            _faketempSen.GetTemp().Returns(fakeTempSensorValue);
            //Act
            _ecs.Regulate();
            //Assert
            _fakeWindow.DidNotReceive().OpenWindow();
        }

        [Test]
        public void Regulate_TemperatureAboveWindowThreshold_WindowIsOpen()
        {
            //Arrange
            int fakeTempSensorValue = InitializeWindowThreshold + 10;
            _faketempSen.GetTemp().Returns(fakeTempSensorValue);
            //Act
            _ecs.Regulate();
            //Assert
            _fakeWindow.Received(1).OpenWindow();
        }
    }
}