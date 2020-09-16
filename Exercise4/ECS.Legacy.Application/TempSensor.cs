using ECS.Legacy.Application;
using System;

namespace ECS.Legacy.Application
{
    internal class TempSensor : ITempSensor
    {
        private readonly IRandomNumberGenerator _randomNumberGenerator;

        public TempSensor(IRandomNumberGenerator rng)
        {
            _randomNumberGenerator = rng;

        }

        public int GetTemp()
        {
            return _randomNumberGenerator.Next(-5, 45);
        }

    }
}