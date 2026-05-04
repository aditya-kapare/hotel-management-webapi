using DTOs.DataTransferObjects;

namespace Services.Contracts
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDTO> GetAllCustomers(bool trackChanges);
        CustomerDTO GetCustomer(string identityId, bool trackChanges);
        CustomerDTO CreateCustomer(CustomerForCreationDTO customer);
        void UpdateCustomer(string identityId, CustomerForUpdateDTO customer, bool trackChanges);
        void DeleteCustomer(string identityId, bool trackChanges);
    }
}