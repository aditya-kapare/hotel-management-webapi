// Repositories/RoomRepository.cs
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(RepositoryContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .ToListAsync();

        public async Task<Room?> GetRoomByRoomNoAsync(int roomNo, bool trackChanges) =>
            await FindByCondition(r => r.RoomNo == roomNo, trackChanges)
                .SingleOrDefaultAsync();

        public void CreateRoom(Room room) => Create(room);

        public void UpdateRoom(Room room) => Update(room);

        public void DeleteRoom(Room room) => Delete(room);
    }
}