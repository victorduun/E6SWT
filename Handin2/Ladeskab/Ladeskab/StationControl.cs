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
    public class StationControl : IStationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        private LadeskabState _state;
        private readonly IChargeControl _chargeControl;
        private readonly IDoor _door;
        private readonly IDisplay _display;
        private readonly IRfidReader _rfidReader;
        private int _oldId;

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

      
        public StationControl(IChargeControl chargeControl, IDoor door, IDisplay display, IRfidReader rfidReader)
        {
            _state = LadeskabState.Available;
            //Set dependencies through DI
            _chargeControl = chargeControl;
            _door = door;
            _display = display;
            _rfidReader = rfidReader;

            //Set trigger handlers
            _rfidReader.RfidDetectedEvent += RfidDetected;
            _door.DoorOpenEvent += DoorOpened;
            _door.DoorClosedEvent += DoorClosed;
            

        }

  

        private void DoorClosed(object sender, EventArgs e)
        {
            if (_state == LadeskabState.DoorOpen)
            {
                _state = LadeskabState.Available;
                _display.ShowLoadRfid();
            }

        }


        private void DoorOpened(object sender, EventArgs e)
        {
            if (_state == LadeskabState.Available)
            {
                _state = LadeskabState.DoorOpen;
                _display.ShowConnectDevice();
            }
                
        }



        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(object sender, int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (!_chargeControl.IsConnected())
                    {
                        _display.ShowConnectionError();
                    }
                    
                    _door.Lock();
                    try
                    {
                        _chargeControl.StartCharge();

                        _oldId = id;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

                        _display.ShowChargingLockerOccupied();
                        _state = LadeskabState.Locked;
                    }
                    catch (NotConnectedException ex)
                    {
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    bool idIsOk = CheckId(_oldId, id);

                    if (idIsOk)
                    {
                        _chargeControl.StopCharge();
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

        private bool CheckId(int oldId, int id)
        {
            return id == _oldId;
        }
        // Her mangler de andre trigger handlere
    }
}
