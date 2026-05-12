using DTOs.DropPickRequest;
using DTOs.DataTransferObjects;

namespace Services.Contracts
{
    public interface IDropPickRequestService
{
    Task<IEnumerable<DropPickRequestDTO>> GetAllAsync();
    Task<DropPickRequestDTO?> GetByIdAsync(int requestId);
    Task<IEnumerable<DropPickRequestDTO>> GetByStayIdAsync(int stayId);
    Task<IEnumerable<DropPickRequestDTO>> GetByDriverIdAsync(int driverId);
    Task<IReadOnlyList<CabDriverBriefDTO>> GetAvailableDriversAsync();

    Task<DropPickRequestDTO> CreateAsync(DropPickRequestForCreationDTO dto);
    Task UpdateAsync(int requestId, DropPickRequestForUpdateDTO dto);
    Task DeleteAsync(int requestId);
}
}



