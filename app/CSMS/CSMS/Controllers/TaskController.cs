using AutoMapper;
using CSMS.DomainService.Interface;
using CSMS.DTO.Task;
using CSMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CSMS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService<TaskModel> _taskService;
    private IMapper _mapper;

    public TaskController(ITaskService<TaskModel> taskService, IMapper mapper)
    {
        this._taskService = taskService;
        this._mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<TaskModel>> GetAll()
    {
        try
        {
            var result = await _taskService.GetAll();
            return result;
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
            var result = await _taskService.GetByID(id);
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception();
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsync([FromBody] TaskDto task)
    {
        try
        {
            var _task = _mapper.Map<TaskModel>(task);
            if (ModelState.IsValid)
            {
                await _taskService.Add(_task);
                return Ok(task);
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

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAsync([FromBody]TaskDto task)
    {
        try
        {
            var _task = _mapper.Map<TaskModel>(task);
            if (ModelState.IsValid)
            {
                
                var result = await _taskService.Update(_task);
                return result == GlobalEnum.GlobalEnum.UpdateResult.Success
                    ? Ok(task)
                    : BadRequest("Failed to update task");
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromBody] TaskDto task)
    {
        try
        {
            var _task = _mapper.Map<TaskModel>(task);
            if (ModelState.IsValid)
            {
                var result = await _taskService.Delete(_task);
                return result == GlobalEnum.GlobalEnum.DeleteResult.Success
                    ? Ok(task)
                    : BadRequest("Failed to delete task");
            }
            else
            {
                return BadRequest("Failed to delete task");
            }
            
        }
        catch(Exception ex)
        {
            var result =
                $"It was not possible to delete a new task, please try later on ({ex.GetType().Name} - {ex.Message})";
            return BadRequest(result);
        }
    }
}