
namespace DTOs.Stay
{
    public record StayForCreationDTO(
        int RoomNo,
        string CustomerIdentityId,
        decimal DepositPaid
    );
}

