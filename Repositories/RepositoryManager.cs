using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
  
            private readonly RepositoryContext _context;

            private readonly Lazy<IRoomRepository> _roomRepository;
            private readonly Lazy<ICustomerRepository> _customerRepository;
            private readonly Lazy<IStayRepository> _stayRepository;
            private readonly Lazy<ICabDriverRepository> _cabDriverRepository;
            private readonly Lazy<IDropPickRequestRepository> _dropPickRequestRepository;

            public RepositoryManager(RepositoryContext context)
            {
                _context = context;

                _roomRepository =
                    new Lazy<IRoomRepository>(() => new RoomRepository(context));

                _customerRepository =
                    new Lazy<ICustomerRepository>(() => new CustomerRepository(context));

                _stayRepository =
                    new Lazy<IStayRepository>(() => new StayRepository(context));

                _cabDriverRepository =
                    new Lazy<ICabDriverRepository>(() => new CabDriverRepository(context));

                _dropPickRequestRepository =
                    new Lazy<IDropPickRequestRepository>(() => new DropPickRequestRepository(context));
            }

            public IRoomRepository Room => _roomRepository.Value;

            public ICustomerRepository Customer => _customerRepository.Value;

            public IStayRepository Stay => _stayRepository.Value;

            public ICabDriverRepository CabDriver => _cabDriverRepository.Value;

            public IDropPickRequestRepository DropPickRequest => _dropPickRequestRepository.Value;

            public void Save() => _context.SaveChanges();
    }
}


