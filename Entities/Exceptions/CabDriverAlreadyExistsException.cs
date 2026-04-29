using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class CabDriverAlreadyExistsException : AlreadyExistsException
    {
        public CabDriverAlreadyExistsException(int id) : base($"Cabdriver with driver Id : {id} already exists.")
        {
        }

        public CabDriverAlreadyExistsException(string id) : base($"Cabdriver with driver Id : {id} already exists.")
        {
        }
    }
}
