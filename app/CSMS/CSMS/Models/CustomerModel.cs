using CSMS.Models;
using CSMS.Repository;
using NuGet.Protocol.Core.Types;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CSMS.Models
{
    public class CustomerModel
    {
        [Key]
        public Guid CustomerId { get; set; }
        [Required, NotNull, StringLength(100)]
        public string Name { get; set; }
        [Required, NotNull]
        public string Email { get; set; }


        public Guid AssociateId { get; set; }
        public ICollection<CustomerModel> Associates { get; set; }


        public Task AddAssociateCustomer(ICustomerRepositry repositry, int otherId)
        {
            var other = await Repository.GetById(targetId);
            Associates.Add(other);
            await repositry.Update(customer);
        }
    }
}

class SomeController
{
    public SomeController(IRepository<CustomerModel> repository)
    {
        Repository = repository;
    }

    public async Task OnPostAsync()
    {
        var customer = Repository.GetById(targetId);
        await customer.AddAssociateCustomer(Repositry, otherId);
    }
}

public async Task OnPostAsync()
{
    await CustomerSevice.SetAssociate(targetId, otherId);
}
