using CSMS.Domain.Models;
using CSMS.DTO.Task;
using CSMS.Infrastracture.Repository.Task;
using MediatR;

namespace CSMS.UseCase.Commands.TaskCommand.Query;

public class GetTaskByIdQueryHandler : IQueryHandler<GetTaskByIdQuery, TaskModel>

{
    private readonly ITaskRepository<TaskModel> _taskRepository;

    public GetTaskByIdQueryHandler(ITaskRepository<TaskModel> taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskModel> Handle(GetTaskByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await _taskRepository.GetById(query.id);
        return result;
    }
}
