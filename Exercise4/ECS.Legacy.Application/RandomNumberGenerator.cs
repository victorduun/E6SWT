using System;
using System.Collections.Generic;
using System.Text;

namespace ECS.Legacy.Application
{
    public class RandomNumberGenerator :IRandomNumberGenerator
    {
        public int Next(int min, int max)
        {
            Random rand = new Random();
            return rand.Next(min, max);
        }
    }
}
