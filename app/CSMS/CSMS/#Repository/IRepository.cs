namespace CSMS.Repository
{
    public interface IRepository<T> where T : class
    {
        public Task<T> GetByID(int id);
        // public Task<IEnumerable<T>> GetAll();
        public IQueryable<T> GetQueryable();
        public void Add(T entity);
        public void Delete(int id);
        public void Update(T entity);
    }
}
