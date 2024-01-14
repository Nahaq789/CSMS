using CSMS.DTO.Task;
using System.Runtime.Serialization;

namespace CSMS.UseCase.Commands.TaskCommand;

[DataContract]
public class DeleteTaskCommand
{
    [DataMember]
    public Guid TaskId { get; private set; }

    public DeleteTaskCommand(Guid taskId)
    {
        TaskId = taskId;
    }
}

