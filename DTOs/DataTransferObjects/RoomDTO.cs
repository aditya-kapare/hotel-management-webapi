using Entities.Enums;

namespace DTOs.DataTransferObjects
{
    public record RoomDTO(
        int RoomNo,
        RoomType RoomType,
        AcOption AcOption,
        AvailabilityStatus AvailabilityStatus,
        CleanStatus CleanStatus,
        decimal Price
    );
}