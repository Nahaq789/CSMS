using CSMS.Domain.DomainService.Interface;
using CSMS.Domain.Models;
using CSMS.Extensions;
using MediatR;

namespace CSMS.UseCase.Commands.TaskCommand
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, TaskModel>
    {
        private readonly ITaskService<TaskModel> _taskService;
        private readonly ILogger<UpdateTaskCommandHandler> _logger;

        public UpdateTaskCommandHandler(ITaskService<TaskModel> taskService, ILogger<UpdateTaskCommandHandler> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        public async Task<TaskModel> Handle(UpdateTaskCommand message, CancellationToken cancellationToken)
        {
            var task = new TaskModel(
                    message.TaskId,
                    message.TaskName,
                    message.Contents,
                    message.Deadline,
                    message.CustomerId,
                    message.ContractId
                );

            var requestUpdateTask = new IdentifiedCommand<UpdateTaskCommand>(message);

            _logger.LogInformation("Sending command: {CommandName} - ({@Command})",
                requestUpdateTask.GetGenericTypeName(),
                requestUpdateTask
                );

            var result = await _taskService.Update(task, cancellationToken);

            if (result != null)
            {
                _logger.LogInformation("Updating Task Ok - Task: {@Task}", requestUpdateTask);
            }
            else
            {
                _logger.LogInformation("Updating Task failed - Task: {@Task}", requestUpdateTask);
            }

            return result;
        }
    }
}
