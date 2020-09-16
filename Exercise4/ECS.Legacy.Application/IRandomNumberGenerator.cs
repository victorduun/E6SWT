using System;
using System.Collections.Generic;
using System.Text;

namespace ECS.Legacy.Application
{
    public interface IRandomNumberGenerator
    {
        public int Next(int min , int max);

    }
}
