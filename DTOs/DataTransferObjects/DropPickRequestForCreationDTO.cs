using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DropPickRequest
{

    public record DropPickRequestForCreationDTO(
        DateTime? RequestedAt,
           int StayId,
           int DriverId,
           int RequestType,
           string Notes
       );

}
