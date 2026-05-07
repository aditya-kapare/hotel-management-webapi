using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{

    public class RoomNotAvailableException : NotAvailableException
    {
        public RoomNotAvailableException(int roomNo)
            : base($"Room {roomNo} is not available.")
        {
        }
    }

}
