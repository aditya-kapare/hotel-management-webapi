using DTOs.DataTransferObjects;
using DTOs.Stay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IStayService
    {

        Task<IEnumerable<StayDTO>> GetAllAsync();
        Task<IEnumerable<StayDTO>> GetActiveAsync();

        Task<IEnumerable<StayDTO>> GetPastAsync();
        Task<StayDTO?> GetByIdAsync(int stayId);
        Task<IEnumerable<StayDTO>> GetByRoomNoAsync(int roomNo);
        Task<IEnumerable<StayDTO>> GetByCustomerIdentityIdAsync(string customerIdentityId);
        Task<IEnumerable<StayDTO>> GetByCheckInDateAsync(DateTime date);

        Task<StayDTO> CheckOutAsync(int stayId, CheckOutRequestDTO dto);

        Task<BillingSummaryDTO> GetBillingSummaryAsync(int stayId);

        // -------- WRITE --------
        Task<StayDTO> CreateAsync(StayForCreationDTO dto);
        Task UpdateAsync(int stayId, StayForUpdateDTO dto);
        Task DeleteAsync(int stayId);

    }
}
