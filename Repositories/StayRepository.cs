using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories;

public class StayRepository : RepositoryBase<Stay>, IStayRepository
{
    public StayRepository(RepositoryContext context) : base(context)
    {
    }

    // ---------- READ ----------

    public async Task<IEnumerable<Stay>> GetAllAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .Include(s => s.Customer)
            .Include(s => s.Room)
            .Include(s => s.DropPickRequests)
            .ToListAsync();
    }

    public async Task<Stay?> GetByIdAsync(int stayId, bool trackChanges)
    {
        return await FindByCondition(s => s.StayId == stayId, trackChanges)
            .Include(s => s.Customer)
            .Include(s => s.Room)
            .Include(s => s.DropPickRequests)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Stay>> GetByRoomNoAsync(int roomNo, bool trackChanges)
    {
        return await FindByCondition(s => s.RoomNo == roomNo, trackChanges)
            .Include(s => s.Customer)
            .Include(s => s.Room)
            .Include(s => s.DropPickRequests)
            .ToListAsync();
    }

    public async Task<IEnumerable<Stay>> GetByCustomerIdentityIdAsync(
        string customerIdentityId, bool trackChanges)
    {
        return await FindByCondition(
                s => s.CustomerIdentityId == customerIdentityId,
                trackChanges)
            .Include(s => s.Customer)
            .Include(s => s.Room)
            .Include(s => s.DropPickRequests)
            .ToListAsync();
    }

    public async Task<IEnumerable<Stay>> GetByCheckInDateAsync(DateTime date, bool trackChanges)
    {
        var start = date.Date;
        var end = start.AddDays(1);

        return await FindByCondition(
                s => s.CheckInAt >= start && s.CheckInAt < end,
                trackChanges)
            .Include(s => s.Customer)
            .Include(s => s.Room)
            .Include(s => s.DropPickRequests)
            .ToListAsync();
    }


    public async Task<bool> HasActiveStayAsync(int roomNo)
    {
        return await FindByCondition(
            s => s.RoomNo == roomNo && s.CheckOutAt == null,
            trackChanges: false)
            .AnyAsync();
    }

    // ---------- CREATE ----------
    public void CreateStay(Stay stay) => Create(stay);

    // ---------- UPDATE ----------
    public void UpdateStay(Stay stay) => Update(stay);

    // ---------- DELETE ----------
    public void DeleteStay(Stay stay) => Delete(stay);
}