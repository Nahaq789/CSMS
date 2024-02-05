using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CSMS.Domain.Models.Master;

public class TaskStatusModel
{
    [Key]
    public long TaskStatusId { get; private set; }
    [Required, StringLength(30), NotNull]
    public string Status { get; private set; }
    [NotMapped]
    public ICollection<TaskModel>? _Tasks { get; private set; }

    public TaskStatusModel( long taskStatusId, string status) 
    {
        TaskStatusId = taskStatusId;
        Status = status;
    }
}
