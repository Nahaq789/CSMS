using CSMS.Models;
using System.Security.Permissions;

namespace CSMS.DomainInterface
{
    public interface ICustomerRepository
    {
        public List<CustomerModel> GetAllCustomers();
        public CustomerModel GetCustomerByID(int id);
        public void UpdateCustomer();
        public void DeleteCustomer(int customerID);
    }
}
