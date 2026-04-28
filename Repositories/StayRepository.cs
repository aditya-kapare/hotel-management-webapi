using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class StayRepository: RepositoryBase<CabDriver>, IStayRepository
    {
        public StayRepository(RepositoryContext context):base(context)
        {
            
        }
    }
}
