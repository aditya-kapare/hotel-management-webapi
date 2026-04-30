using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICabDriverRepository
    {
        Task<IEnumerable<CabDriver>> GetAllCabDriversAsync(bool trackChanges);
        Task<CabDriver?> GetCabDriverByIdAsync(int id, bool trackChanges);
        Task<CabDriver?> GetCabDriverByGovtIdAsync(string govtId, bool trackChanges);
        void CreateDriver(CabDriver cabDriver);
        void UpdateDriver(CabDriver cabDriver);
        void DeleteDriver(CabDriver cabDriver);
    }
}
