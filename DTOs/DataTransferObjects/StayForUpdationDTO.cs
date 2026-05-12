
using System.Text.Json.Serialization;

namespace DTOs.Stay
{
    public record StayForUpdateDTO(
    //int StayId,
    int? RoomNo,
    DateTime? CheckInAt,
    decimal? DepositPaid,
    decimal? AmountPaid,
    DateTime? CheckOutAt

    );
}

