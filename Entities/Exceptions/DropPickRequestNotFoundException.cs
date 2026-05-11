using System;

namespace Entities.Exceptions
{
    public sealed class DropPickRequestNotFoundException : NotFoundException
    {
        public DropPickRequestNotFoundException(int requestId)
            : base($"Drop-Pick request with id '{requestId}' was not found.")
        {
        }
    }
}
