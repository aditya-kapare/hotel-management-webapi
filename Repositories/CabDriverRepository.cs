using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CabDriverRepository: RepositoryBase<CabDriver> , ICabDriverRepository
    {
       
        public CabDriverRepository(RepositoryContext context):base(context) { }

        public async Task<IEnumerable<CabDriver>> GetAllCabDriversAsync(bool trackChanges)
            => await FindAll(trackChanges).ToListAsync();

        public async Task<CabDriver?> GetCabDriverByIdAsync(int id, bool trackChanges)
            => await FindByCondition(r => r.DriverId == id, trackChanges)
                        .SingleOrDefaultAsync();

        public async Task<CabDriver?> GetCabDriverByGovtIdAsync(string govtId, bool trackChanges)
            => await FindByCondition(r => r.GovernmentId.Equals(govtId), trackChanges)
                        .SingleOrDefaultAsync();

        public void CreateDriver(CabDriver cabDriver) => Create(cabDriver);

        public void UpdateDriver(CabDriver cabDriver) => Update(cabDriver);

        public void DeleteDriver(CabDriver cabDriver) => Delete(cabDriver);
    }
}
