using Entities.Enums;

namespace DTOs.DataTransferObjects
{
    public record CustomerDTO
    (
        string IdentityId,
        IdentityIdType IdentityIdType,
        string MobileNo,
        string Name,
        Gender Gender,
        string Address,
        string Country
    );
}