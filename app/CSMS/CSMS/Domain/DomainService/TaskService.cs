using CSMS.Domain.DomainService.Interface;
using CSMS.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CSMS.Domain.DomainService;

public class TaskService : IBaseEntityID, ITaskService<TaskModel>
{
    private readonly ApplicationDbContext _context;
    private DbSet<TaskModel> DbSet { get; set; }
    public Guid EntityID { get; set; }

    public TaskService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TaskModel> GetByID(Guid id)
    {
        var transaction = _context.Database.CurrentTransaction;
        if (transaction != null)
        {
            await transaction.CreateSavepointAsync("GetByID");
        }

        try
        {
            var result = await _context.Task.FindAsync(id);
            if (result == null)
            {
                throw new NullReferenceException();
            }
            return result;
        }
        catch (Exception ex)
        {
            if (transaction != null)
            {
                await transaction.RollbackToSavepointAsync("GetByID");
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
            if (ex.Message == "Object reference not set to an instance of an object.")
            {
                throw new NullReferenceException();
            }
            throw new Exception();
        }
    }

    public async Task<IEnumerable<TaskModel>> GetAll()
    {
        var transaction = _context.Database.CurrentTransaction;
        if (transaction != null)
        {
            await transaction.CreateSavepointAsync("GetAll");
        }

        try
        {
            var result = await _context.Task.ToListAsync();
            if (result == null) { throw new Exception(); }
            return result;
        }
        catch (Exception ex)
        {
            if (transaction != null)
            {
                await transaction.RollbackToSavepointAsync("GetAll");
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
            throw new Exception();
        }
    }

    public async Task<TaskModel> Add(TaskModel task, CancellationToken cancellationToken)
    {
        var transaction = _context.Database.CurrentTransaction;
        if (transaction != null)
        {
            await transaction.CreateSavepointAsync("Add");
        }

        try
        {
            await _context.Task.AddAsync(task);
            await _context.SaveChangesAsync();
            return await Task.FromResult(task);
        }
        catch (Exception ex)
        {
            if (transaction != null)
            {
                await transaction.RollbackToSavepointAsync("Add");
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
            throw new Exception();
        }
    }

    public async Task<TaskModel> Update(TaskModel task, CancellationToken cancellationToken)
    {
        var transaction = _context.Database.CurrentTransaction;
        if (transaction != null)
        {
            await transaction.CreateSavepointAsync("Update");
        }

        try
        {
            var target = await _context.Task.FirstOrDefaultAsync(x => x.TaskId == task.TaskId);
            if (target == null) { throw new Exception(); }

            TaskModel taskModel = new TaskModel(
                task.TaskId,
                task.TaskName,
                task.Contents,
                task.Deadline,
                task.CustomerId,
                task.ContractId
            );

            _context.Task.Entry(target).State = EntityState.Detached;
            _context.Task.Attach(taskModel);
            _context.Task.Update(taskModel);

            await _context.SaveChangesAsync();

            return await Task.FromResult(task);
        }
        catch (Exception ex)
        {
            if (transaction != null)
            {
                await transaction.RollbackToSavepointAsync("Update");
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
            return await Task.FromResult(task);
        }
    }

    public async Task<Guid> Delete(Guid guid, CancellationToken cancellationToken)
    {
        var transaction = _context.Database.CurrentTransaction;
        if (transaction != null)
        {
            await transaction.CreateSavepointAsync("Delete");
        }

        try
        {
            var result = await _context.Task.FindAsync(guid);
            if(result != null)
            {
                _context.Task.Remove(result);
            }
            else
            {
                throw new ArgumentException();
            }

            await _context.SaveChangesAsync();
            return await Task.FromResult(guid);
        }
        catch (Exception ex)
        {
            if (transaction != null)
            {
                await transaction.RollbackToSavepointAsync("Delete");
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
            return await Task.FromResult(guid);
        }
    }
}