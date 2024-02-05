using CSMS.Domain.Models;
using CSMS.DTO.Task;
using Microsoft.EntityFrameworkCore;

namespace CSMS.Infrastracture.Repository.Task;

public class TaskRepository : ITaskRepository<TaskModel>
{
    private readonly ApplicationDbContext _context;

    public TaskRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TaskModel> GetById(Guid id)
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
            var a = await _context.Task.Include(t => t._TaskStatusModel).ToListAsync();
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
}
