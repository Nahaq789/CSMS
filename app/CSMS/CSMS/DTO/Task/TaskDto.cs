using System.Text.Json.Serialization;

namespace CSMS.DTO.Task;

public class TaskDto
{
    public Guid TaskId { get; private set; }
 
    public string TaskName { get; private set; }
 
    public string Contents { get; private set; }
 
    public DateTime Deadline { get; private set; }
    public Guid CustomerId { get; private set; }
    public Guid ContractId { get; private set; }
    public long TaskStatusId { get; private set; }

    [JsonConstructor]
    public TaskDto(Guid taskId, string taskName, string contents, DateTime deadline, Guid customerId, Guid contractId, long taskStatusId)
    {
        TaskId = taskId;
        TaskName = taskName;
        Contents = contents;
        Deadline = deadline;
        CustomerId = customerId;
        ContractId = contractId;
        TaskStatusId = taskStatusId;
    }
}