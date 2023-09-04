using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CSMS.Models
{
    public class CustomerModel
    {
        [Key]
        public int CustomerId { get; set; }
        [Required, NotNull]
        public string Name { get; set; }
        [Required, NotNull]
        public string Email { get; set; }

    }
}
