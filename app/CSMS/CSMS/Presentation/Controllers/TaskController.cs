using AutoMapper;
using CSMS.Domain.DomainService.Interface;
using CSMS.Domain.Models;
using CSMS.DTO.Task;
using CSMS.UseCase.Commands.TaskCommand;
using CSMS.UseCase.Commands.TaskCommand.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CSMS.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    //private readonly ITaskService<TaskModel> _taskService;
    private readonly IMediator _mediator;
    private IMapper _mapper;

    public TaskController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<TaskModel>> GetAll()
    {
        try
        {
            var query = new GetTaskQuery();
            
            return await _mediator.Send(query); ;
        }
        catch (Exception ex)
        {
            throw new Exception();
        }
    }

    [HttpGet("{id}")]
    public async Task<TaskModel> GetById(Guid id)
    {
        try
        {
            var query = new GetTaskByIdQuery(id);
            var result = await _mediator.Send(query);
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception();
        }
    }

    //[HttpPost]
    //[ProducesResponseType(StatusCodes.Status201Created)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> PostAsync([FromBody] TaskDto task)
    //{
    //    try
    //    {
    //        var _task = _mapper.Map<TaskModel>(task);
    //        if (ModelState.IsValid)
    //        {
    //            //await _taskService.Add(_task);
    //            return Ok(task);
    //        }
    //        else
    //        {
    //            {
    //                return BadRequest("Failed to create task");
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        var result =
    //            $"It was not possible to create a new task, please try later on ({ex.GetType().Name} - {ex.Message})";
    //        return BadRequest(result);
    //    }
    //}

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAsync([FromBody] TaskDto task)
    {
        try
        {
            //var _task = _mapper.Map<TaskModel>(task);
            if (ModelState.IsValid)
            {
                var _taskCreateCommand = _mapper.Map<UpdateTaskCommand>(task);
                var result = await _mediator.Send(_taskCreateCommand);

                return Ok(result);
            }
            else
            {
                return BadRequest("Failed to update task");
            }
        }
        catch (Exception ex)
        {
            var result =
                $"It was not possible to update a new task, please try later on ({ex.GetType().Name} - {ex.Message})";
            return BadRequest(result);
        }
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete([FromBody] TaskDto task)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var _taskDeleteCommand = _mapper.Map<Guid>(task);
                var result = await _mediator.Send(_taskDeleteCommand);
                //return result != Guid.Empty
                //    ? Ok(task)
                //    : BadRequest("Failed to delete task");

                return Ok(result);
            }
            else
            {
                return BadRequest("Failed to delete task");
            }

        }
        catch (Exception ex)
        {
            var result =
                $"It was not possible to delete a new task, please try later on ({ex.GetType().Name} - {ex.Message})";
            return BadRequest(result);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTask([FromBody] TaskDto task)
    {
        try
        Å@{
            if (ModelState.IsValid)
            {
                var _taskCreateCommand = _mapper.Map<CreateTaskCommand>(task);
                var result = await _mediator.Send(_taskCreateCommand);

                return Ok(result);
            }
            else
            {
                {
                    return BadRequest("Failed to create task");
                }
            }
        }
        catch (Exception ex)
        {
            var result =
                $"It was not possible to create a new task, please try later on ({ex.GetType().Name} - {ex.Message})";
            return BadRequest(result);
        }
    }
}