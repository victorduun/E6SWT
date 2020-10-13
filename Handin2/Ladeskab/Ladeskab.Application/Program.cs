using System;
using Ladeskab.Interfaces;
using Ladeskab.USB;

namespace Ladeskab.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            // Assemble your system here from all the classes
            IRfidReader rfidReader = new RfidReader();
            IDoor door = new DoorSimulator();
            IUsbCharger usbCharger = new UsbChargerSimulator();
            IDisplay display = new DisplaySimulator();
            IChargeControl chargeControl = new ChargeControl(usbCharger, display);
            IStationControl stationControl = new StationControl(chargeControl, door, display, rfidReader);

            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, F, A, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        door.Open();
                        break;

                    case 'C':
                        door.Close();
                        break;

                    case 'F':
                        chargeControl.ConnectDevice();
                        break;

                    case 'A':
                        chargeControl.DisconnectDevice();
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        rfidReader.OnRfidRead(id);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}
