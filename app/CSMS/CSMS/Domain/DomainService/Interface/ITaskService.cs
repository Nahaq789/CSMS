using CSMS.Domain.Models;

namespace CSMS.Domain.DomainService.Interface;

public interface ITaskService<T> where T : class
{
    public Task<T> GetByID(Guid id);
    public Task<IEnumerable<T>> GetAll();
    public Task<T> Add(T TEntity, CancellationToken cancellationToken);
    public Task<GlobalEnum.GlobalEnum.DeleteResult> Delete(T TEntity);
    public Task<TaskModel> Update(T TEntity, CancellationToken cancellationToken);
}