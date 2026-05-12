namespace Entities.Exceptions
{
    public sealed class CustomerAlreadyExistsException : BadRequestException
    {
        public CustomerAlreadyExistsException(string identityId)
            : base($"Customer with IdentityId '{identityId}' already exists.")
        {
        }
    }
}