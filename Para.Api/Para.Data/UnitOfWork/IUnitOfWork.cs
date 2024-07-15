using Para.Data.Domain;
using Para.Data.GenericRepository;
using System.Linq.Expressions;

namespace Para.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task Complete();
        Task CompleteWithTransaction();
        IGenericRepository<Customer> CustomerRepository { get; }
        IGenericRepository<CustomerDetail> CustomerDetailRepository { get; }
        IGenericRepository<CustomerAddress> CustomerAddressRepository { get; }
        IGenericRepository<CustomerPhone> CustomerPhoneRepository { get; }
        Task<List<Customer>> GetCustomersWithIncludes(params Expression<Func<Customer, object>>[] includeProperties);
        Task<List<Customer>> FindCustomersByCondition(Expression<Func<Customer, bool>> expression);
    }
}
