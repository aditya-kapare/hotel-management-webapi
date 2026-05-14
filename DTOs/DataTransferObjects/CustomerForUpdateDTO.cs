using Entities.Enums;

namespace DTOs.DataTransferObjects
{
    public record CustomerForUpdateDTO
    (
        string MobileNo,
        string Name,
        int Gender,
        string Address,
        string Country
    );
}