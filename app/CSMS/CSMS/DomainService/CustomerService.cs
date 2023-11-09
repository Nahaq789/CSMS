using CSMS.DomainService.Interface;
using CSMS.Models;

using Microsoft.EntityFrameworkCore;
using CSMS.DomainInterface;


namespace CSMS.DomainService
{
    public class CustomerService : IBaseEntityID, ICustomerService<CustomerModel>
  {
        private readonly ApplicationDbContext _context;
        private DbSet<CustomerModel> DbSet { get; set; }
        public Guid EntityID { get; set; }

        public CustomerService (ApplicationDbContext context)
        {
            this._context = context;
            DbSet = _context.Set<CustomerModel>();
        }
        public async Task<CustomerModel> GetByID(Guid id)
        {
            var result = await _context.Customers.FindAsync(id);
            if (result == null)
            {
                throw new Exception();
            }
            return result;
        }
        public async Task<IEnumerable<CustomerModel>> GetAll()
        {
            var result = await DbSet.ToListAsync();
            if (result == null) { throw new Exception(); }
            return result;
        }
        public async Task<Guid> Add(CustomerModel customer)
        {
            try
            {
                await _context.AddAsync(customer);
                _context.SaveChanges();
                return customer.CustomerId;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        public async Task<bool> Update(CustomerModel customer)
        {
            var transaction = _context.Database.CurrentTransaction;
            if(transaction != null)
            {
                await transaction.CreateSavepointAsync("Update");
            }
            try
            {
                CustomerModel customerModel = new CustomerModel(
                    customer.CustomerId,
                    customer.Name,
                    customer.Email,
                    customer.Age
                );

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                if(transaction != null)
                {
                    await transaction.RollbackToSavepointAsync("Update");
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    throw;
                }
                return false;
            }
            
        }
        public async Task<bool> Delete(Guid id)
        {
            DbSet.Remove(await GetByID(id));
            _context.SaveChanges();
            return await Task.FromResult(true);
        }

        //public Task AddAssociateCustomer(ICustomerRepositry repositry, int otherId)
        //{
        //    var other = await Repository.GetById(targetId);
        //    Associates.Add(other);
        //    await repositry.Update(customer);
        //}
    }
}

