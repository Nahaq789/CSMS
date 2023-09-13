namespace CSMS.Repository
{
    public interface IRepository<T> where T : class
    {
        public T GetByID(int id);
        public IEnumerable<T> GetAll();
        public void Add(T entity);
        public void Delete(int id);
        public void Update(T entity);
    }
}
