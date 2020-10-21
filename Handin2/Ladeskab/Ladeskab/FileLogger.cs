using Ladeskab.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ladeskab
{
    public class FileLogger : ILog
    {

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        private void WriteMessageToFile(string message)
        {
            using var writer = File.AppendText(logFile);
            writer.WriteLine(message);
        }

        public void LogDoorUnlocked(int id)
        {
            string message = String.Format(DateTime.Now + ": Skab låst op med RFID: {0}", id);
            WriteMessageToFile(message);
        }

        public void LogDoorLocked(int id)
        {
            string message = String.Format(DateTime.Now + ": Skab låst med RFID: {0}", id);
            WriteMessageToFile(message);
        }
    }
}
