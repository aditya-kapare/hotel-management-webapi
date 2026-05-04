using AutoMapper;
using Contracts;
using DTOs.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Services.Contracts;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CustomerService(
            IRepositoryManager repository,
            ILoggerManager logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<CustomerDTO> GetAllCustomers(bool trackChanges)
        {
            var customers = _repository.Customer.GetAllCustomers(trackChanges);
            return _mapper.Map<IEnumerable<CustomerDTO>>(customers);
        }

        public CustomerDTO GetCustomer(string identityId, bool trackChanges)
        {
            var customer = _repository.Customer.GetCustomer(identityId, trackChanges);
            if (customer == null)
                throw new CustomerNotFoundException(identityId);

            return _mapper.Map<CustomerDTO>(customer);
        }

        public CustomerDTO CreateCustomer(CustomerForCreationDTO customer)
        {
            var customerEntity = _mapper.Map<Customer>(customer);
            _repository.Customer.CreateCustomer(customerEntity);
            _repository.Save();

            return _mapper.Map<CustomerDTO>(customerEntity);
        }

        public void UpdateCustomer(
            string identityId,
            CustomerForUpdateDTO customer,
            bool trackChanges)
        {
            var customerEntity =
                _repository.Customer.GetCustomer(identityId, trackChanges);

            if (customerEntity == null)
                throw new CustomerNotFoundException(identityId);

            _mapper.Map(customer, customerEntity);
            _repository.Save();
        }

        public void DeleteCustomer(string identityId, bool trackChanges)
        {
            var customer =
                _repository.Customer.GetCustomer(identityId, trackChanges);

            if (customer == null)
                throw new CustomerNotFoundException(identityId);

            _repository.Customer.DeleteCustomer(customer);
            _repository.Save();
        }
    }
}