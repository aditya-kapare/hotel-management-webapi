using Entities.Enums;

namespace DTOs.DataTransferObjects
{
    public record CustomerForUpdateDTO
    (
        string MobileNo,
        string Name,
        Gender Gender,
        string Address,
        string Country
    );
}