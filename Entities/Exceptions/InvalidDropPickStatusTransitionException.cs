using Entities.Enums;

namespace Entities.Exceptions
{
    public sealed class InvalidDropPickStatusTransitionException : BadRequestException
    {
        public InvalidDropPickStatusTransitionException(
            DropPickStatus current,
            DropPickStatus next)
            : base($"Invalid status transition from '{current}' to '{next}'.")
        {
        }
    }
}
