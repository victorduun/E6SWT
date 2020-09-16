using System;
using System.Collections.Generic;
using System.Text;

namespace ECS.Legacy.Application.Test
{
    public class FakeHeater : IHeater

    {
        public bool IsOn = false;
        public void TurnOn()
        {
            IsOn = true;
        }

        public void TurnOff()
        {
            IsOn = false;
        }
    }
}
