using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CSMS.Models;

public class TaskModel
{
    [Key]
    public Guid TaskId { get; private set; }
    [Required, NotNull, StringLength(20)]
    public string TaskName { get; private set; }
    [Required, NotNull, StringLength(50)]
    public string Contents { get; private set; }
    [Required, NotNull]
    public DateTime Deadline { get; private set; }
    public Guid CustomerId { get; private set; }
    public Guid ContractId { get; private set; }

    public TaskModel(Guid taskId, string taskName, string contents, DateTime deadline, Guid customerId, Guid contractId)
    {
        TaskId = taskId;
        TaskName = taskName;
        Contents = contents;
        Deadline = deadline;
        CustomerId = customerId;
        ContractId = contractId;
    }
}