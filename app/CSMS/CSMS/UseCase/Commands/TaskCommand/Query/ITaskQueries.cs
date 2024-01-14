using CSMS.Domain.Models;

namespace CSMS.UseCase.Commands.TaskCommand.Query;

public interface ITaskQueries
{
    Task<IEnumerable<TaskModel>> GetTaskAsync();
    Task<TaskModel> GetTaskByIdAsync(Guid taskId);
}
