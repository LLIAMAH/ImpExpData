using System.Data.Entity;

namespace ImpExpData.Models
{
    public class Ctx : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }

        public Ctx() : base("DefaultConnection")
        {
        }
    }    
}
