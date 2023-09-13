using CSMS.DomainInterface;
using CSMS.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using SQLitePCL;

namespace CSMS.Repository
{
    public class CustomerRepository : IRepository<CustomerModel>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ApplicationDbContext _context;
        private DbSet<CustomerModel> DbSet { get; set; }
        public CustomerRepository(ICustomerRepository customerRepository, ApplicationDbContext context)
        {
            this._customerRepository = customerRepository;
            this._context = context;
            DbSet = _context.Set<CustomerModel>();
        }

        public CustomerModel GetByID(int id)
        {
            return _customerRepository.GetCustomerByID(id);
        }
        public IEnumerable<CustomerModel> GetAll()
        {
            return DbSet.AsNoTracking();
        }
        public void Add (CustomerModel customerModel)
        {
            DbSet.Add(customerModel);
            _context.SaveChanges();
        }
        public void Update (CustomerModel customerModel)
        {
            DbSet.Attach(customerModel);
            _context.Entry(customerModel).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void Delete (int id) 
        {
            DbSet.Remove(GetByID(id));
            _context.SaveChanges();
        }
    }
}
