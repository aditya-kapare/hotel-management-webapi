using Contracts;
using Entities.Models;

namespace Repositories
{
    public class CustomerRepository
        : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(RepositoryContext context)
            : base(context)
        {
        }

        public IEnumerable<Customer> GetAllCustomers(bool trackChanges) =>
            FindAll(trackChanges).ToList();

        public Customer? GetCustomer(string identityId, bool trackChanges) =>
            FindByCondition(c => c.IdentityId == identityId, trackChanges)
            .SingleOrDefault();

        public void CreateCustomer(Customer customer) =>
            Create(customer);

        public void DeleteCustomer(Customer customer) =>
            Delete(customer);
    }
}