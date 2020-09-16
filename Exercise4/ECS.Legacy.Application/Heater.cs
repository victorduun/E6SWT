using ECS.Legacy.Application;

namespace ECS.Legacy.Application
{
    public class Heater : IHeater
    {
        public void TurnOn()
        {
            System.Console.WriteLine("Heater is on");
        }

        public void TurnOff()
        {
            System.Console.WriteLine("Heater is off");
        }

    }
}