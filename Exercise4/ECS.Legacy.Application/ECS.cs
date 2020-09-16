    using ECS.Legacy.Application;

    namespace ECS.Legacy.Application
{
    public class ECS
    {
        private int _heaterTempThreshold;
        private int _windowTempThreshold;
        private readonly ITempSensor _tempSensor;
        private readonly IHeater _heater;
        private readonly IWindow _window;



        public ECS(IHeater heater, ITempSensor tempSensor, IWindow window, int heaterThreshold, int windowThreshold)
        {
            SetHeaterThreshold(heaterThreshold);
            SetWindowThreshold(windowThreshold);
            _tempSensor = tempSensor;
            _heater = heater;
            _window = window;
        }

        public void Regulate()
        {
            bool isHeaterOn = RegulateHeater();
            if (isHeaterOn)
            {
                _window.CloseWindow();
            }
            else
            {
                RegulateWindow();
            }
        }
        private bool RegulateHeater()
        {
            var t = _tempSensor.GetTemp();
            if (t < _heaterTempThreshold)
            {
                _heater.TurnOn();
                return true;
            }
            else
            {
                _heater.TurnOff();
                return false;
            }
        }

        private bool RegulateWindow()
        {
            var t = _tempSensor.GetTemp();
            if (t <= _windowTempThreshold)
            {
                _window.CloseWindow();
                return false;
            }
            else
            {
                _window.OpenWindow();
                return true;
            }
        }

        public void SetHeaterThreshold(int thr)
        {
            _heaterTempThreshold = thr;
        }

        public void SetWindowThreshold(int thr)
        {
            _windowTempThreshold = thr;
        }

        public int GetHeaterThreshold()
        {
            return _heaterTempThreshold;
        }
        public int GetWindowThreshold()
        {
            return _windowTempThreshold;
        }

        public int GetCurTemp()
        {
            return _tempSensor.GetTemp();
        }

    }
}
