using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DataTransferObjects
{

    public record CustomerBriefDTO(
        string IdentityId,
        string Name,
        string MobileNo
    );

}
