using CSMS.DomainService.Interface;
using CSMS.Models;
using CSMS.Repository;

namespace CSMS.DomainService
{
    public class CustomerService : IDomainService<CustomerModel>
    {
        private IRepository<CustomerModel> _CustomerRepository;

        public CustomerService (IRepository<CustomerModel> CustomerRepository)
        {
            this._CustomerRepository = CustomerRepository;
        }
        public CustomerModel GetByID(int id)
        {
            return _CustomerRepository.GetByID(id);
        }
        public IEnumerable<CustomerModel> GetAll()
        {
            return _CustomerRepository.GetAll();
        }
        public void Add (CustomerModel customerModel)
        {
            _CustomerRepository.Add(customerModel);
        }
        public void Update (CustomerModel customerModel)
        {
            _CustomerRepository.Update(customerModel);
        }
        public void Delete (int id)
        {
            _CustomerRepository.Delete(id);
        }
    }
}

