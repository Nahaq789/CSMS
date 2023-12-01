//using CSMS.DomainInterface;
//using CSMS.Models;
//using Microsoft.EntityFrameworkCore;
//using Npgsql;
//using SQLitePCL;

//namespace CSMS.Repository
//{
//    public class CustomerRepository : ICustomerRepository<CustomerModel>
//    {
//        private readonly ApplicationDbContext _context;
//        private DbSet<CustomerModel> DbSet { get; set; }
//        public CustomerRepository(ApplicationDbContext context)
//        {
//            this._context = context;
//            DbSet = _context.Set<CustomerModel>();
//        }

//        public async Task<CustomerModel> GetByID(int id)
//        {
//            var result = await DbSet.FindAsync(id);
//            if (result == null)
//            {
//                throw new Exception();
//            }
//            return result;
//        }
//        public async Task<IEnumerable<CustomerModel>> GetAll()
//        {
//            var result = await DbSet.Include(x => x.Associates).ToListAsync();
//            if (result == null) { throw new Exception(); }
//            return result;
//        }
//        public void Add (CustomerModel customerModel)
//        {
//            customerModel.CustomerId = Guid.NewGuid();
//            DbSet.Add(customerModel);
//            _context.SaveChangesAsync();
//            return Task.FromResult(0);
//            return Task.CompletedTask;
//        }
//        public void Update (CustomerModel customerModel)
//        {
//            DbSet.Attach(customerModel);
//            _context.Entry(customerModel).State = EntityState.Modified;
//            _context.SaveChanges();
//        }
//        public void Delete (int id) 
//        {
//            DbSet.Remove(GetByID(id));
//            _context.SaveChanges();
//        }
//    }
//}
