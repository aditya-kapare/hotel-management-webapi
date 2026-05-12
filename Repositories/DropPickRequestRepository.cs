using Contracts;
using Entities.Enums;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class DropPickRequestRepository : RepositoryBase<DropPickRequest>, IDropPickRequestRepository
    {
        public DropPickRequestRepository(RepositoryContext context):base(context)
        {
            
        }



            public async Task<IEnumerable<DropPickRequest>> GetAllAsync(bool trackChanges)
            {
                return await FindAll(trackChanges)
                    .Include(r => r.CabDriver)
                    .Include(r => r.Stay)
                        .ThenInclude(s => s.Customer)
                    .ToListAsync();
            }

            public async Task<DropPickRequest?> GetByIdAsync(int requestId, bool trackChanges)
            {
                return await FindByCondition(r => r.RequestId == requestId, trackChanges)
                    .Include(r => r.CabDriver)
                    .Include(r => r.Stay)
                        .ThenInclude(s => s.Customer)
                    .SingleOrDefaultAsync();
            }

            public async Task<IEnumerable<DropPickRequest>> GetByStayIdAsync(int stayId, bool trackChanges)
            {
                return await FindByCondition(r => r.StayId == stayId, trackChanges)
                    .Include(r => r.CabDriver)
                    .Include(r => r.Stay)
                        .ThenInclude(s => s.Customer)
                    .ToListAsync();
            }

            public async Task<IEnumerable<DropPickRequest>> GetByDriverIdAsync(int driverId, bool trackChanges)
            {
                return await FindByCondition(r => r.DriverId == driverId, trackChanges)
                    .Include(r => r.CabDriver)
                    .Include(r => r.Stay)
                        .ThenInclude(s => s.Customer)
                    .ToListAsync();
            }

            public async Task<IEnumerable<CabDriver>> GetAvailableDriversAsync()
            {
                var busyDriverIds = await context.DropPickRequests
                    .Where(r => r.Status != DropPickStatus.Completed &&
                                r.Status != DropPickStatus.Cancelled)
                    .Select(r => r.DriverId)
                    .Distinct()
                    .ToListAsync();

                return await context.CabDrivers
                    .Where(d => !busyDriverIds.Contains(d.DriverId))
                    .AsNoTracking()
                    .ToListAsync();
            }

            public void Create(DropPickRequest request) => base.Create(request);
            public void Update(DropPickRequest request) => base.Update(request);
            public void Delete(DropPickRequest request) => base.Delete(request);
        }
    }


