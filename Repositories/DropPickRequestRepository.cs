using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class DropPickRequestRepository : RepositoryBase<CabDriver>,IDropPickRequestRepository
    {
        public DropPickRequestRepository(RepositoryContext context):base(context)
        {
            
        }
    }
}
