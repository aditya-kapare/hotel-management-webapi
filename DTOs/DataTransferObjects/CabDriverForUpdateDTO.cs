// CabDriverForUpdateDTO
using Entities.Enums;

namespace DTOs.DataTransferObjects
{
    public record CabDriverForUpdateDTO(
        string governmentId,
        string Name,
        int Age,
        Gender Gender,
        string CarVendor,
        string CarType
    );
}