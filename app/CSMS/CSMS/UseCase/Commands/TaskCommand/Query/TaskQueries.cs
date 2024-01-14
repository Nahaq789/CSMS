using CSMS.Domain.Models;
using CSMS.DTO.Task;
using MediatR;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Runtime.Serialization;

namespace CSMS.UseCase.Commands.TaskCommand.Query;

[DataContract]
public record class GetTaskByIdQuery(Guid id) : IRequest<TaskModel>;
[DataContract]
public record class GetTaskQuery : IRequest<IEnumerable<TaskModel>>;