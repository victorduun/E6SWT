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
        private readonly IRfidReader _rfidReader;
        private int _oldId;

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

      
        public StationControl(IUsbCharger usbCharger, IDoor door, IDisplay display, IRfidReader rfidReader)
        {
            //Set dependencies through DI
            _charger = usbCharger;
            _door = door;
            _display = display;
            _rfidReader = rfidReader;

            //Set trigger handlers
            _rfidReader.RfidDetectedEvent += RfidDetected;
            _door.DoorOpenEvent += DoorOpened;
            _door.DoorOpenEvent += DoorClosed;
            _charger.CurrentValueEvent += CurrentValueChanged;


        }

        private void CurrentValueChanged(object sender, CurrentEventArgs e)
        {
            if (e.Current == 0)
            {
                //Nothing
            }
            else if (0 < e.Current && e.Current <= 5)
            {
                _display.ShowChargingFinished();
            }
            else if (5 < e.Current && e.Current <= 500)
            {
                _display.ShowChargingNominal();
            }
            else if (e.Current > 500)
            {
                _display.ShowOvercurrentError();
            }
        }

        private void DoorClosed(object sender, EventArgs e)
        {
            //TODO: Set correct state
            _display.ShowLoadRfid();
        }


        private void DoorOpened(object sender, EventArgs e)
        {
            _state = LadeskabState.DoorOpen;
            _display.ShowConnectDevice();
        }



        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(object sender, int id)
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

        private bool CheckId(int oldId, int id)
        {

            throw new NotImplementedException();
        }
        // Her mangler de andre trigger handlere
    }
}
