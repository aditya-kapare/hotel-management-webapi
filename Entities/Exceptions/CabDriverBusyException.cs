
using System;

namespace Entities.Exceptions
{
    public sealed class CabDriverBusyException : BadRequestException
    {
        public CabDriverBusyException(int driverId)
            : base($"Cab driver with id '{driverId}' is currently busy.")
        {
        }
    }
}
