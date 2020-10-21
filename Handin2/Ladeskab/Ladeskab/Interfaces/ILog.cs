using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab.Interfaces
{
    public interface ILog
    {
        void LogDoorUnlocked(int id);
        void LogDoorLocked(int id);
    }
}
