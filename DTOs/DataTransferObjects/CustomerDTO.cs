using Entities.Enums;

namespace DTOs.DataTransferObjects
{
    public record CustomerDTO
    (
        string IdentityId,
        int IdentityIdType,
        string MobileNo,
        string Name,
        int Gender,
        string Address,
        string Country
    );
}