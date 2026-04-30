using Entities.Enums;

namespace DTOs.DataTransferObjects
{
    public record RoomForUpdateDTO(
        RoomType RoomType,
        AcOption AcOption,
        AvailabilityStatus AvailabilityStatus,
        CleanStatus CleanStatus,
        decimal Price
    );
}