using CSMS.Domain.Models;
using CSMS.DTO.Task;

namespace CSMS.Infrastracture.Repository.Task;

public interface ITaskRepository<T> where T : class
{
    public Task<T> GetById(Guid id);
    public Task<IEnumerable<T>> GetAll();
}
