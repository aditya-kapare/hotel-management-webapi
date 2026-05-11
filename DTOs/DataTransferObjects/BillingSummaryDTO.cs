using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DataTransferObjects
{

    public record BillingSummaryDTO(
        int Nights,
        decimal RatePerNight,
        decimal TotalCharge,
        decimal DepositPaid,
        decimal AdditionalPaid,
        decimal TotalPaid,
        decimal PendingAmount
    );

}
