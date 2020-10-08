using Ladeskab.Interfaces;
using System;
using System.Text;

namespace Ladeskab
{
    public class DisplaySimulator : IDisplay
    {
        public string DisplayValue { get; private set; } = "Skabet er frit.";

        public void ShowChargingLockerOccupied()
        {
            DisplayValue = "Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.";
            Console.WriteLine(DisplayValue);
        }

        public void ShowConnectDevice()
        {
            DisplayValue = "Tilslut telefon til laderen og luk døren.";
            Console.WriteLine(DisplayValue);
        }

        public void ShowConnectionError()
        {
            DisplayValue = "Din telefon er ikke ordentlig tilsluttet. Prøv igen.";
            Console.WriteLine(DisplayValue);
        }

        public void ShowLoadRfid()
        {
            DisplayValue = "Indlæs RFID tag vha. RFID læseren.";
            Console.WriteLine(DisplayValue);
        }

        public void ShowRemoveDevice()
        {
            DisplayValue = "Tag din telefon ud af skabet og luk døren";
            Console.WriteLine(DisplayValue);
        }

        public void ShowRfidError()
        {
            DisplayValue = "Forkert RFID tag";
            Console.WriteLine(DisplayValue);
        }
    }
}
