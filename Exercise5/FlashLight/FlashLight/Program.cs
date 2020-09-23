using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashLight
{
    class Program
    {
        static void Main(string[] args)
        {

            FLControl ctrl = new FLControl(new Led());

            ctrl.BtnPushed();
            ctrl.BtnPushed();
        }
    }
}
