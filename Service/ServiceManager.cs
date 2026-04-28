using AutoMapper;
using Contracts;
using Services.Contracts;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {

        private readonly Lazy<ICustomerService> _customerService;
        private readonly Lazy<IRoomService> _roomService;
        private readonly Lazy<IStayService> _stayService;
        private readonly Lazy<ICabDriverService> _cabDriverService;
        private readonly Lazy<IDropPickRequestService> _dropPickRequestService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _customerService = new Lazy<ICustomerService>(() => new CustomerService(repositoryManager, loggerManager, mapper));
            _roomService = new Lazy<IRoomService>(() => new RoomService(repositoryManager, loggerManager, mapper));
            _stayService = new Lazy<IStayService>(() => new StayService(repositoryManager, loggerManager, mapper));
            _cabDriverService = new Lazy<ICabDriverService>(() => new CabDriverService(repositoryManager, loggerManager, mapper));
            _dropPickRequestService = new Lazy<IDropPickRequestService>(() => new DropPickRequestService(repositoryManager, loggerManager, mapper));

        }
        public ICustomerService CustomerService => _customerService.Value;

        public IRoomService RoomService => _roomService.Value;
        public IStayService StayService => _stayService.Value;

        public ICabDriverService CabDriverService => _cabDriverService.Value;

        public IDropPickRequestService DropPickRequestService => _dropPickRequestService.Value;

       
    }
}
