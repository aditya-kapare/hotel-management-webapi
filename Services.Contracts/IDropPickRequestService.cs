using DTOs.DropPickRequest;

namespace Services.Contracts
{
    public interface IDropPickRequestService
{
    Task<IEnumerable<DropPickRequestDTO>> GetAllAsync();
    Task<DropPickRequestDTO?> GetByIdAsync(int requestId);
    Task<IEnumerable<DropPickRequestDTO>> GetByStayIdAsync(int stayId);
    Task<IEnumerable<DropPickRequestDTO>> GetByDriverIdAsync(int driverId);
    Task<IEnumerable<int>> GetAvailableDriverIdsAsync();

    Task<DropPickRequestDTO> CreateAsync(DropPickRequestForCreationDTO dto);
    Task UpdateAsync(int requestId, DropPickRequestForUpdateDTO dto);
    Task DeleteAsync(int requestId);
}
}



