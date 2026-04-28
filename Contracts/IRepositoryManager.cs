using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {


        ICabDriverRepository CabDriver { get; }
        ICustomerRepository Customer { get; }
        IRoomRepository Room { get; }
        IStayRepository Stay { get; }
        IDropPickRequestRepository DropPickRequest { get; }

        void Save();

    }
}
