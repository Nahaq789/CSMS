using AutoMapper;
using CSMS.Domain.DomainService.Interface;
using CSMS.Domain.Models;
using CSMS.Extensions;
using MediatR;

namespace CSMS.UseCase.Commands.TaskCommand
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Unit>
    {
        private readonly ITaskService<TaskModel> _taskService;
        private readonly IMediator _mediator;
        private readonly ILogger<CreateTaskCommandHandler> _logger;
        private IMapper _mapper;

        public CreateTaskCommandHandler(ITaskService<TaskModel> taskService, 
            IMediator mediator, 
            ILogger<CreateTaskCommandHandler> logger,
            IMapper mapper)
        {
            _taskService = taskService ?? throw new ArgumentException(nameof(taskService));
            _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }

        public async Task<Unit> Handle(CreateTaskCommand message, CancellationToken cancellationToken)
        {
            var task = new TaskModel(
                    message.TaskId,
                    message.TaskName,
                    message.Contents,
                    message.Deadline,
                    message.CustomerId,
                    message.ContractId
                );

            var requestCrateTask = new IdentifiedCommand<CreateTaskCommand>(message);

            _logger.LogInformation("Sending command: {CommandName} - ({@Command})",
                requestCrateTask.GetGenericTypeName(),
                requestCrateTask
                );

            //var task = _mapper.Map<TaskModel>(requestCrateTask);
            var result = await _taskService.Add(task);

            if(result != Guid.Empty)
            {
                _logger.LogInformation("Creating Task Ok - Task: {@Task}", requestCrateTask);
            }
            else
            {
                _logger.LogInformation("Creating Task failed - Task: {@Task}", requestCrateTask);
            }

            return Unit.Value; 
        }
    }
}
