// CabDriverForUpdateDTO
using Entities.Enums;

namespace DTOs.DataTransferObjects
{
    public record CabDriverForUpdateDTO(
        string Name,
        int Age,
        Gender Gender,
        string CarVendor,
        string CarType
    );
}