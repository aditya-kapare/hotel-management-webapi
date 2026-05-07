using System;

namespace Entities.Exceptions
{
    public sealed class CustomerNotFoundException : NotFoundException
    {
        public CustomerNotFoundException(string identityId)
            : base($"Customer with IdentityId '{identityId}' was not found.")
        {
        }
    }
}