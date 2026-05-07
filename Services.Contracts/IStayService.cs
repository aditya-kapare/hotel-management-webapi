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
        Task<StayDTO?> GetByIdAsync(int stayId);
        Task<IEnumerable<StayDTO>> GetByRoomNoAsync(int roomNo);
        Task<IEnumerable<StayDTO>> GetByCustomerIdentityIdAsync(string customerIdentityId);
        Task<IEnumerable<StayDTO>> GetByCheckInDateAsync(DateTime date);

        // -------- WRITE --------
        Task<StayDTO> CreateAsync(StayForCreationDTO dto);
        Task UpdateAsync(int stayId, StayForUpdateDTO dto);
        Task DeleteAsync(int stayId);

    }
}
