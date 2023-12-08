using CSMS.DomainService.Interface;
using CSMS.Models;
using Microsoft.EntityFrameworkCore;

namespace CSMS.DomainService;

public class TaskService : IBaseEntityID, ITaskService<TaskModel>
{
    private readonly ApplicationDbContext _context;
    private DbSet<TaskModel> DbSet { get; set; }
    public Guid EntityID { get; set; }

    public TaskService(ApplicationDbContext context)
    {
        this._context = context;
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
                throw new Exception();
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

    public async Task<Guid> Add(TaskModel task)
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
            return task.TaskId;
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

    public async Task<GlobalEnum.GlobalEnum.UpdateResult> Update(TaskModel task)
    {
        var transaction = _context.Database.CurrentTransaction;
        if(transaction != null)
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

            return GlobalEnum.GlobalEnum.UpdateResult.Success;
        }
        catch (Exception ex)
        {
            if(transaction != null)
            {
                await transaction.RollbackToSavepointAsync("Update");
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
            return GlobalEnum.GlobalEnum.UpdateResult.Failed;
        }
    }

    public async Task<GlobalEnum.GlobalEnum.DeleteResult> Delete(TaskModel task)
    {
        var transaction = _context.Database.CurrentTransaction;
        if (transaction != null)
        {
            await transaction.CreateSavepointAsync("Delete");
        }

        try
        {
            _context.Task.Remove(task);
            await _context.SaveChangesAsync();
            return GlobalEnum.GlobalEnum.DeleteResult.Success;
        }
        catch (Exception ex)
        {
            if (transaction != null)
            {
                await transaction.RollbackToSavepointAsync("Delete");
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
            return GlobalEnum.GlobalEnum.DeleteResult.Failed;
        }
    }
}