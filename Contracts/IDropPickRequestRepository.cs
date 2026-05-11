using Entities.Models;

namespace Contracts
{
    public interface IDropPickRequestRepository
    {
        Task<IEnumerable<DropPickRequest>> GetAllAsync(bool trackChanges);
        Task<DropPickRequest?> GetByIdAsync(int requestId, bool trackChanges);
        Task<IEnumerable<DropPickRequest>> GetByStayIdAsync(int stayId, bool trackChanges);
        Task<IEnumerable<DropPickRequest>> GetByDriverIdAsync(int driverId, bool trackChanges);

        Task<IEnumerable<CabDriver>> GetAvailableDriversAsync();

        void Create(DropPickRequest request);
        void Update(DropPickRequest request);
        void Delete(DropPickRequest request);
    }
}
