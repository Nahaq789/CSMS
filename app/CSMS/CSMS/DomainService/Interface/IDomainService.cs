namespace CSMS.DomainService.Interface
{
    public interface IDomainService<T> where T : class
    {
        public Task<T> GetByID(int id);
        public Task<IEnumerable<T>> GetAll();
        public void Add(T entity);
        public void Delete(int id);
        public void Update(T entity);
    }
}
