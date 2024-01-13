using AutoMapper;
using CSMS.Domain.DomainService.Interface;
using CSMS.Domain.Models;
using CSMS.Extensions;
using MediatR;

namespace CSMS.UseCase.Commands.TaskCommand
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskModel>
    {
        private readonly ITaskService<TaskModel> _taskService;
        private readonly IMediator _mediator;
        private readonly ILogger<CreateTaskCommandHandler> _logger;

        public CreateTaskCommandHandler(ITaskService<TaskModel> taskService, 
            IMediator mediator, 
            ILogger<CreateTaskCommandHandler> logger)
        {
            _taskService = taskService ?? throw new ArgumentException(nameof(taskService));
            _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }

        public async Task<TaskModel> Handle(CreateTaskCommand message, CancellationToken cancellationToken)
        {
            var task = new TaskModel(
                    message.TaskId,
                    message.TaskName,
                    message.Contents,
                    message.Deadline,
                    message.CustomerId,
                    message.ContractId
                );

            var requestCreateTask = new IdentifiedCommand<CreateTaskCommand>(message);

            _logger.LogInformation("Sending command: {CommandName} - ({@Command})",
                requestCreateTask.GetGenericTypeName(),
                requestCreateTask
                );

            var result = await _taskService.Add(task, cancellationToken);

            if(result != null)
            {
                _logger.LogInformation("Creating Task Ok - Task: {@Task}", requestCreateTask);
            }
            else
            {
                _logger.LogInformation("Creating Task failed - Task: {@Task}", requestCreateTask);
            }

            return result; 
        }
    }
}
