using Entities.Models;

namespace Entities.Exceptions
{
    public class RoomAlreadyExistsException : AlreadyExistsException
    {
        public RoomAlreadyExistsException(int RoomNo) : base($"Room with Room number: {RoomNo} already exists in the database.")
        {
        }
    }
}
