using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashLight
{
    class FLControl
    {
        private enum FLState
        {
            Off,
            On
        }

        private FLState _state;
        private ILed _led;

        public FLControl(Led led)
        {
            this._led = led;
            _led.Off();
            _state = FLState.Off;
        }

        public void BtnPushed()
        {
            switch (_state)
            {
                case FLState.Off:
                    _led.On();
                    _state = FLState.On;
                    break;

                case FLState.On:
                    _led.Off();
                    _state = FLState.Off;
                    break;
            }
        }
    }
}
