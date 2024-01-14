using CSMS.Domain.DomainService.Interface;
using CSMS.Domain.Models;
using CSMS.Infrastracture.Repository.Task;
using MediatR;

namespace CSMS.UseCase.Commands.TaskCommand.Query;
public class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, IEnumerable<TaskModel>>
{
    private readonly ITaskRepository<TaskModel> _taskRepository;

    public GetTaskQueryHandler(ITaskRepository<TaskModel> taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<IEnumerable<TaskModel>> Handle(GetTaskQuery query, CancellationToken cancellationToken)
    {
        var result = await _taskRepository.GetAll();
        return result;
    }
}
