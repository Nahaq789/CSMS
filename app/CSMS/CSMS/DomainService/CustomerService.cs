using CSMS.DomainService.Interface;
using CSMS.Models;

using Microsoft.EntityFrameworkCore;
using CSMS.DomainInterface;


namespace CSMS.DomainService
{
    public class CustomerService : IBaseEntityID
    {
        private readonly ApplicationDbContext _context;
        private DbSet<CustomerModel> DbSet { get; set; }
        public Guid EntityID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public CustomerService (ApplicationDbContext context)
        {
            this._context = context;
            DbSet = _context.Set<CustomerModel>();
        }
        public async Task<CustomerModel> GetByID(int id)
        {
            var result = await DbSet.FindAsync(id);
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
        public async Task<CustomerModel> Add(string name, string email)
        {
            EntityID = Guid.NewGuid();
            //customerModel.CustomerId = Guid.NewGuid();
            CustomerModel customerModel = new CustomerModel(EntityID, name, email);
            DbSet.Add(customerModel);
            _context.SaveChanges();
            return await Task.FromResult(customerModel);
        }
        public async Task<CustomerModel> Update (CustomerModel customerModel)
        {
            DbSet.Attach(customerModel);
            _context.Entry(customerModel).State = EntityState.Modified;
            _context.SaveChanges();
            return await Task.FromResult(customerModel);
        }
        public async Task<bool> Delete(int id)
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

