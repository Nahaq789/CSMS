using CSMS.Domain.DomainService.Interface;
using CSMS.Domain.Models;
using CSMS.Extensions;
using MediatR;

namespace CSMS.UseCase.Commands.TaskCommand;

public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Guid>
{
    private readonly ITaskService<TaskModel> _taskService;
    private readonly ILogger<DeleteTaskCommandHandler> _logger;

    public DeleteTaskCommandHandler(ITaskService<TaskModel> taskService, ILogger<DeleteTaskCommandHandler> logger)
    {
        _taskService = taskService ?? throw new ArgumentException(nameof(taskService));
        _logger = logger ?? throw new ArgumentException(nameof(logger));
    }

    public async Task<Guid> Handle (DeleteTaskCommand message, CancellationToken cancellationToken)
    {
        var requestDeleteTask = new IdentifiedCommand<DeleteTaskCommand>(message);

        _logger.LogInformation("Sending command: {CommandName} - ({@Command})",
                requestDeleteTask.GetGenericTypeName(),
                requestDeleteTask
                );

        var result = await _taskService.Delete(message.TaskId, cancellationToken);

        if (result != Guid.Empty)
        {
            _logger.LogInformation("Deleting Task Ok - Task: {@Task}", requestDeleteTask);
        }
        else
        {
            _logger.LogInformation("Deleting Task failed - Task: {@Task}", requestDeleteTask);
        }

        return result;
    } 
}