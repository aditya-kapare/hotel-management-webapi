using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CustomerRepository: RepositoryBase<CabDriver>, ICustomerRepository
    {
        public CustomerRepository(RepositoryContext context):base(context)
        {
            
        }
    }
}
