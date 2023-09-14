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
        public Task<CustomerModel> GetByID(int id)
        {
            var result = _CustomerRepository.GetByID(id);
            return result;
        }
        public Task<IEnumerable<CustomerModel>> GetAll()
        {
            var result = _CustomerRepository.GetAll();
            return result;
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

