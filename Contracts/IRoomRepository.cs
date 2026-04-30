// Contracts/IRoomRepository.cs
using Entities.Models;

namespace Contracts
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllRoomsAsync(bool trackChanges);
        Task<Room?> GetRoomByRoomNoAsync(int roomNo, bool trackChanges);
        void CreateRoom(Room room);
        void UpdateRoom(Room room);
        void DeleteRoom(Room room);
    }
}