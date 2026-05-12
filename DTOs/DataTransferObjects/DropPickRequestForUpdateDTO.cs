using Entities.Enums;
using System.Text.Json.Serialization;

namespace DTOs.DropPickRequest
{
    public record DropPickRequestForUpdateDTO(
        // Identity (comes from route, not JSON)
     

        // Editable fields (same intent as Web App)
        DateTime? RequestedAt,
        int? RequestType,
        int? Status,
        int? DriverId,
        string? Notes
    );
}