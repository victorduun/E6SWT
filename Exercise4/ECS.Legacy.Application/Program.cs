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
            ECS ecs = new ECS(heater, tempSensor, 3);



        }

    }
}
