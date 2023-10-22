namespace CSMS.DomainService.Interface
{
    public interface IDomainService<T> where T : class
    {
        public Task<T> GetByID(int id);
        public Task<IEnumerable<T>> GetAll();
        public Task<T> Add(T entity);
        public Task<T> Delete(int id);
        public Task<T> Update(T entity);
    }
}
