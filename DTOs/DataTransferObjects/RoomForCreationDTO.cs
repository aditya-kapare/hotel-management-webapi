using Entities.Enums;

namespace DTOs.DataTransferObjects
{
    public record RoomForCreationDTO(
        int RoomNo,
        int RoomType,
        int AcOption,
        decimal Price
    );
}