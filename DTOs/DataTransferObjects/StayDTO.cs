
using DTOs.DataTransferObjects;

namespace DTOs.Stay
{
    public record StayDTO(
        int StayId,
        int RoomNo,
        string CustomerIdentityId,
        DateTime CheckInAt,
        DateTime? CheckOutAt,
        decimal DepositPaid,
        decimal AmountPaid,
        decimal PendingAmount,
        CustomerBriefDTO Customer
    );
}

