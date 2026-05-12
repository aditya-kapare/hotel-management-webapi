using Entities.Enums;

namespace DTOs.DataTransferObjects
{
    public record RoomDTO(
        int RoomNo,
        int RoomType,
        int AcOption,
        int AvailabilityStatus,
        int CleanStatus,
        decimal Price   
    );
}