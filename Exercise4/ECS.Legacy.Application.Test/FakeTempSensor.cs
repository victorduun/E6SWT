using System;
using System.Collections.Generic;
using System.Text;

namespace ECS.Legacy.Application.Test
{
    public class FakeTempSensor : ITempSensor
    {
        private int _getTempValue;
        public FakeTempSensor(int getTempValue)
        {
            _getTempValue = getTempValue;
        }
        public int GetTemp()
        {
            return _getTempValue;
        }
        public void SetGetTempReturnVal(int value)
        {
            _getTempValue = value;
        }
    }
}
