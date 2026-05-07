using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IStayRepository
    {

        Task<IEnumerable<Stay>> GetAllAsync(bool trackChanges);
        Task<Stay?> GetByIdAsync(int stayId, bool trackChanges);
        Task<IEnumerable<Stay>> GetByRoomNoAsync(int roomNo, bool trackChanges);
        Task<IEnumerable<Stay>> GetByCustomerIdentityIdAsync(string customerIdentityId, bool trackChanges);
        Task<IEnumerable<Stay>> GetByCheckInDateAsync(DateTime date, bool trackChanges);

        void CreateStay(Stay stay);
        void UpdateStay(Stay stay);
        void DeleteStay(Stay stay);

    }
}
