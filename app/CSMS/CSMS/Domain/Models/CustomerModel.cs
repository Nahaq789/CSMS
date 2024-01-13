using CSMS.Repository;
using NuGet.Protocol.Core.Types;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace CSMS.Domain.Models
{
    public class CustomerModel
    {
        [Key]
        public Guid CustomerId { get; private set; }
        [Required, NotNull, StringLength(35)]
        public string Name { get; private set; }
        [Required, NotNull, StringLength(35)]
        public string Email { get; private set; }
        [Required]
        public int Age { get; private set; }
        [JsonConstructor]
        public CustomerModel(Guid customerID, string name, string email, int age)
        {
            CustomerId = customerID;
            Name = name;
            Email = email;
            Age = age;
        }

        public CustomerModel(string name, string email, int age)
        {
            CustomerId = Guid.NewGuid();
            Name = name;
            Email = email;
            Age = age;
        }
        //public Guid AssociateId { get; set; }
        //public ICollection<CustomerModel> Associates { get; set; }
    }
}

//class SomeController
//{
//    public SomeController(IRepository<CustomerModel> repository)
//    {
//        Repository = repository;
//    }

//    public async Task OnPostAsync()
//    {
//        var customer = Repository.GetById(targetId);
//        await customer.AddAssociateCustomer(Repositry, otherId);
//    }
//}

//public async Task OnPostAsync()
//{
//    await CustomerSevice.SetAssociate(targetId, otherId);
//}
