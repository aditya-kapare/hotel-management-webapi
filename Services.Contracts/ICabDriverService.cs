using DTOs.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICabDriverService
    {
        Task<IEnumerable<CabDriverDTO>> GetAllDrivers(bool trackChanges);

        Task<CabDriverDTO> GetDriverById(int driverId, bool trackChanges);
        Task<CabDriverDTO> GetDriverByGovtId(string govtId, bool trackChanges);
        Task<CabDriverDTO> CreateDriver(CabDriverForCreationDTO driver);

        Task UpdateDriver(int driverId, CabDriverForUpdateDTO driverForUpdate, bool trackChanges);

        Task DeleteDriver(int driverId, bool trackChanges);
    }
}