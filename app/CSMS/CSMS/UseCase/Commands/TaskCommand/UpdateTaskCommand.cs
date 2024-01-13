using CSMS.Domain.Models;
using CSMS.DTO.Task;
using MediatR;
using System.Runtime.Serialization;

namespace CSMS.UseCase.Commands.TaskCommand
{
    public class UpdateTaskCommand : IRequest<TaskModel>
    {
        private readonly List<TaskDto> _tasks;
        [DataMember]
        public Guid TaskId { get; private set; }
        [DataMember]
        public string TaskName { get; private set; }
        [DataMember]
        public string Contents { get; private set; }
        [DataMember]
        public DateTime Deadline { get; private set; }
        [DataMember]
        public Guid CustomerId { get; private set; }
        [DataMember]
        public Guid ContractId { get; private set; }
        [DataMember]
        public IEnumerable<TaskDto> Tasks => _tasks;

        public UpdateTaskCommand(Guid taskId, string taskName, string contents, DateTime deadline, Guid customerId, Guid contractId)
        {
            TaskId = taskId;
            TaskName = taskName;
            Contents = contents;
            Deadline = deadline;
            CustomerId = customerId;
            ContractId = contractId;
        }
    }
}
