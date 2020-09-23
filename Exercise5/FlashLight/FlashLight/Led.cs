using System;

namespace FlashLight
{
    class Led : ILed
    {
        public void On()
        {
            Console.WriteLine("LED ON");
        }

        public void Off()
        {
            Console.WriteLine("LED OFF");
        }
    }
}