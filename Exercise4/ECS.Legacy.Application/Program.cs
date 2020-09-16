using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ECS.Legacy.Application
{
    public static class Program
    {
        static void Main(string[] args)
        {
            IHeater heater = new Heater();
            IRandomNumberGenerator rng = new RandomNumberGenerator();
            ITempSensor tempSensor = new TempSensor(rng);
            IWindow window = new Window();
            ECS ecs = new ECS(heater, tempSensor, window, 3, 10);

        }

    }
}
