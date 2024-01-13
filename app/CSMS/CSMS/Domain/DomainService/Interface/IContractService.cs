using static CSMS.GlobalEnum.GlobalEnum;

namespace CSMS.Domain.DomainService.Interface
{
    public interface IContractService<T> where T : class
    {
        public Task<T> GetByID(Guid id);
        public Task<IEnumerable<T>> GetAll();
        public Task<Guid> Add(T TEntity);
        public Task<DeleteResult> Delete(T TEntity);
        public Task<UpdateResult> Update(T TEntity);
    }
}
