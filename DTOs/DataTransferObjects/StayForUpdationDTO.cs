
namespace DTOs.Stay
{
    public record StayForUpdateDTO(
        int StayId,
        decimal AmountPaid,
        DateTime? CheckOutAt
    );
}

