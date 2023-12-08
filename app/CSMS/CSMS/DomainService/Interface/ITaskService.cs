namespace CSMS.DomainService.Interface;

public interface ITaskService<T> where T : class
{
    public Task<T> GetByID (Guid id);
    public Task<IEnumerable<T>> GetAll();
    public Task<Guid> Add(T TEntity);
    public Task<GlobalEnum.GlobalEnum.DeleteResult> Delete(T TEntity);
    public Task<GlobalEnum.GlobalEnum.UpdateResult> Update(T TEntity);
}