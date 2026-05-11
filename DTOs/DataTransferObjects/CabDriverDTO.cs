// CabDriverDTO (read)
using Entities.Enums;

namespace DTOs.DataTransferObjects
{
    public record CabDriverDTO(
        int DriverId,
        string GovernmentId,
        string Name,
        int Age,
        int Gender,
        string CarVendor,
        string CarType
    );
}