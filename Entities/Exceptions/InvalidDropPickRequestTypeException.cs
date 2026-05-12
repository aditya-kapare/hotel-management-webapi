using Entities.Enums;

namespace Entities.Exceptions
{
    public sealed class InvalidDropPickRequestTypeException : BadRequestException
    {
        public InvalidDropPickRequestTypeException(RequestType type)
            : base($"Invalid drop-pick request type '{type}'.")
        {
        }
    }
}