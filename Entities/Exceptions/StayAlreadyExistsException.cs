using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{

    public class StayAlreadyExistsException : AlreadyExistsException
    {
        public StayAlreadyExistsException(int roomNo)
            : base($"Room {roomNo} already has an active stay.")
        {
        }
    }

}
