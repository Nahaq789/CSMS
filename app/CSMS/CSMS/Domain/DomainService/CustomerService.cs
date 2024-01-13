using Microsoft.EntityFrameworkCore;
using CSMS.DomainInterface;
using static CSMS.GlobalEnum.GlobalEnum;
using CSMS.Domain.DomainService.Interface;
using CSMS.Domain.Models;

namespace CSMS.Domain.DomainService
{
    public class CustomerService : IBaseEntityID, ICustomerService<CustomerModel>
    {
        private readonly ApplicationDbContext _context;
        private DbSet<CustomerModel> DbSet { get; set; }
        public Guid EntityID { get; set; }

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
            DbSet = _context.Set<CustomerModel>();
        }
        public async Task<CustomerModel> GetByID(Guid id)
        {
            var transaction = _context.Database.CurrentTransaction;
            if (transaction != null)
            {
                await transaction.CreateSavepointAsync("GetByID");
            }
            try
            {
                var result = await _context.Customers.FindAsync(id);
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
        public async Task<IEnumerable<CustomerModel>> GetAll()
        {
            var transaction = _context.Database.CurrentTransaction;
            if (transaction != null)
            {
                await transaction.CreateSavepointAsync("GetAll");
            }
            try
            {
                var result = await _context.Customers.ToListAsync();
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
        public async Task<Guid> Add(CustomerModel customer)
        {
            var transaction = _context.Database.CurrentTransaction;
            if (transaction != null)
            {
                await transaction.CreateSavepointAsync("Add");
            }
            try
            {
                await _context.AddAsync(customer);
                await _context.SaveChangesAsync();
                return customer.CustomerId;
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
        public async Task<UpdateResult> Update(CustomerModel customer)
        {
            var transaction = _context.Database.CurrentTransaction;
            if (transaction != null)
            {
                await transaction.CreateSavepointAsync("Update");
            }
            try
            {
                var target = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == customer.CustomerId);
                if (target == null) { throw new Exception(); }
                CustomerModel customerModel = new CustomerModel(
                    customer.CustomerId,
                    customer.Name,
                    customer.Email,
                    customer.Age
                );

                _context.Customers.Entry(target).State = EntityState.Detached;

                _context.Customers.Attach(customerModel);

                _context.Customers.Update(customerModel);
                await _context.SaveChangesAsync();

                return UpdateResult.Success;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    await transaction.RollbackToSavepointAsync("Update");
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    throw;
                }
                return UpdateResult.Failed;
            }
        }
        public async Task<DeleteResult> Delete(CustomerModel customer)
        {
            var transaction = _context.Database.CurrentTransaction;
            if (transaction != null)
            {
                await transaction.CreateSavepointAsync("Delete");
            }
            try
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                return DeleteResult.Success;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    await transaction.RollbackToSavepointAsync("Delete");
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    throw;
                }
                return DeleteResult.Failed;
            }
        }

        //public Task AddAssociateCustomer(ICustomerRepositry repositry, int otherId)
        //{
        //    var other = await Repository.GetById(targetId);
        //    Associates.Add(other);
        //    await repositry.Update(customer);
        //}
    }
}

