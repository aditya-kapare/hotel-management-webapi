using System;

namespace Entities.Exceptions
{
    public sealed class InvalidDropPickRequestOperationException : BadRequestException
    {
        public InvalidDropPickRequestOperationException(string message)
            : base(message)
        {
        }
    }
}
