using System.Data.Entity;

namespace CSMS.DomainService.Interface
{
    public interface ICustomerService<T> where T : class
    {
        public Task<T> GetByID(int id);
        public Task<IEnumerable<T>> GetAll();
        public Task<T> Add(string name, string email);
        public Task<bool> Delete(int id);
        public Task<T> Update(T entity);
    }
}
