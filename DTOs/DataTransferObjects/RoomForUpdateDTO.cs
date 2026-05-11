using Entities.Enums;

namespace DTOs.DataTransferObjects
{
    public record RoomForUpdateDTO(
        int RoomType,
        int AcOption,
        int AvailabilityStatus,
        int CleanStatus,
        decimal Price
    );
}