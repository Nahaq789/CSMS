using CSMS.Domain.Models;
using CSMS.DTO.Task;
using MediatR;
using System.Runtime.Serialization;

namespace CSMS.UseCase.Commands.TaskCommand;

[DataContract]
public class DeleteTaskCommand : IRequest<Guid>
{
    [DataMember]
    public Guid TaskId { get; private set; }

    public DeleteTaskCommand(Guid taskId)
    {
        TaskId = taskId;
    }
}

