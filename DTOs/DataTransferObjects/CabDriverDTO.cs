// CabDriverDTO (read)
using Entities.Enums;

namespace DTOs.DataTransferObjects
{
    public record CabDriverDTO(
        int DriverId,
        string GovernmentId,
        string Name,
        int Age,
        Gender Gender,
        string CarVendor,
        string CarType
    );
}