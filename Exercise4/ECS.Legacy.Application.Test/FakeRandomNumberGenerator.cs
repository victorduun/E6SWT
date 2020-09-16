using System;
using System.Collections.Generic;
using System.Text;

namespace ECS.Legacy.Application.Test
{
    public class FakeRandomNumberGenerator : IRandomNumberGenerator
    {
        public int Next(int min, int max)
        {
            return 10;
        }
    }
}
