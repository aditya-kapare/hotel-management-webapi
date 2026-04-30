using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class CabDriverNotFoundException : NotFoundException
    {
        public CabDriverNotFoundException(int id) : base($"Cabdriver with {id} does no exist in database.")
        {
        }

        public CabDriverNotFoundException(string id) : base($"Cabdriver with {id} does no exist in database.")
        {
        }
    }
}
