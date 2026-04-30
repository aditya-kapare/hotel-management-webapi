using Entities.Enums;

namespace DTOs.DataTransferObjects
{
    public record RoomForCreationDTO(
        int RoomNo,
        RoomType RoomType,
        AcOption AcOption,
        decimal Price
    );
}