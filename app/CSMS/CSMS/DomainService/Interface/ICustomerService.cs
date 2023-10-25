using System.Data.Entity;

namespace CSMS.DomainService.Interface
{
    public interface ICustomerService<T> where T : class
    {
        public Task<T> GetByID(Guid id);
        public Task<IEnumerable<T>> GetAll();
        public Task<T> Add(string name, string email);
        public Task<bool> Delete(Guid id);
        public Task<T> Update(T entity);
    }
}
