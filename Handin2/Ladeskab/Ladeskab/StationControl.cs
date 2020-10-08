using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Interfaces;
using Ladeskab.USB;

namespace Ladeskab
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        private LadeskabState _state;
        private readonly IUsbCharger _charger;
        private readonly IDoor _door;
        private readonly IDisplay _display;
        private int _oldId;

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

      
        public StationControl(IUsbCharger usbCharger, IDoor door, IDisplay display)
        {
            _charger = usbCharger;
            _door = door;
            _display = display;
        }
        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected)
                    {
                        _door.Lock();
                        _charger.StartCharge();
                        _oldId = id;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

                        _display.ShowChargingLockerOccupied();
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _display.ShowConnectionError();
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.Unlock();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }

                        _display.ShowRemoveDevice();
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.ShowRfidError();
                    }

                    break;
            }
        }

        // Her mangler de andre trigger handlere
    }
}
