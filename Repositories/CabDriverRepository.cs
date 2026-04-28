using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CabDriverRepository: RepositoryBase<CabDriver> , ICabDriverRepository
    {
       
        public CabDriverRepository(RepositoryContext context):base(context)
        {
            
        }
    }
}
