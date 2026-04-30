// CabDriverForCreationDTO
using Entities.Enums;

namespace DTOs.DataTransferObjects
{
    public record CabDriverForCreationDTO(
        string GovernmentId,
        string Name,
        int Age,
        Gender Gender,
        string CarVendor,
        string CarType
    );
}