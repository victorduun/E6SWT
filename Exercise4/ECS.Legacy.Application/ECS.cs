    using ECS.Legacy.Application;

    namespace ECS.Legacy.Application
{
    public class ECS
    {
        private int _threshold;
        private readonly ITempSensor _tempSensor;
        private readonly IHeater _heater;
        private readonly IWindow _window;

        public ECS(IHeater heater, ITempSensor tempSensor, IWindow window, int thr)
        {
            SetThreshold(thr);
            _tempSensor = tempSensor;
            _heater = heater;
            _window = window;
        }

        public void Regulate()
        {
            var t = _tempSensor.GetTemp();
            if (t < _threshold)
            {
                _heater.TurnOn();
                _window.CloseWindow();
            }
            else
            {
                _heater.TurnOff();
                _window.OpenWindow();
            }
        }

        public void SetThreshold(int thr)
        {
            _threshold = thr;
        }

        public int GetThreshold()
        {
            return _threshold;
        }

        public int GetCurTemp()
        {
            return _tempSensor.GetTemp();
        }

    }
}
