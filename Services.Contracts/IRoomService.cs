using DTOs.DataTransferObjects;
using Entities.Enums;

namespace Services.Contracts
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDTO>> GetAllRooms(bool trackChanges);
        Task<RoomDTO> GetRoomByRoomNo(int roomNo, bool trackChanges);
        Task<IEnumerable<RoomDTO>> GetRoomsByType(RoomType roomType, bool trackChanges);
        Task<RoomDTO> CreateRoom(RoomForCreationDTO room);
        Task UpdateRoom(int roomNo, RoomForUpdateDTO roomForUpdate, bool trackChanges);
        Task DeleteRoom(int roomNo, bool trackChanges);

    }
}