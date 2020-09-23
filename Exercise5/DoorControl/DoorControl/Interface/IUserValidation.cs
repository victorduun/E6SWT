using System;
using System.Collections.Generic;
using System.Text;

namespace DoorControl.Interface
{
    public interface IUserValidation
    {
        bool ValidateEntryRequest(int id);
    }
}
