using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{

    public class StayNotFoundException : NotFoundException
    {
        public StayNotFoundException(int stayId)
            : base($"Stay with id {stayId} not found.")
        {
        }
    }

}
