using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DTOs.DropPickRequest
{

    public record DropPickRequestDTO(

    int RequestId,
    int StayId,
    int DriverId,
    int RequestType,
    int Status,
    DateTime RequestedAt,
    string Notes,
    int RoomNo,
    string CustomerName,
    string CustomerPhone,
    string DriverName,
    bool CanEdit


       );

}
