namespace Entities.Exceptions
{
    public class RoomNotFoundException : NotFoundException
    {
        public RoomNotFoundException(int RoomNo) : base($"Room with Room number: {RoomNo} doesn't exist in the database.")
        {
        }
    }
}
