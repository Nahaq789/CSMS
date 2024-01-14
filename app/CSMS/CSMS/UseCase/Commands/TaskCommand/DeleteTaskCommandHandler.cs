using CSMS.Domain.DomainService.Interface;
using CSMS.Domain.Models;
using CSMS.Extensions;

namespace CSMS.UseCase.Commands.TaskCommand;

public class DeleteTaskCommandHandler
{
    private readonly ITaskService<TaskModel> _taskService;
    private readonly ILogger _logger;

    public DeleteTaskCommandHandler(ITaskService<TaskModel> taskService, ILogger logger)
    {
        _taskService = taskService;
        _logger = logger;
    }

    public async Task<Guid> Handle (DeleteTaskCommand message, CancellationToken cancellationToken)
    {
        var requestDeleteTask = new IdentifiedCommand<DeleteTaskCommand>(message);

        _logger.LogInformation("Sending command: {CommandName} - ({@Command})",
                requestDeleteTask.GetGenericTypeName(),
                requestDeleteTask
                );

        var result = await _taskService.Delete(message.TaskId);

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