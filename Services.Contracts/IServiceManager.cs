using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceManager
    {
        IRoomService RoomService { get; }
        ICabDriverService CabDriverService { get; }

        ICustomerService CustomerService { get; }

        IStayService StayService { get; }

        IDropPickRequestService DropPickRequestService {  get; }
    }
}
