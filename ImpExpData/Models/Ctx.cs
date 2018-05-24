using System.Data.Entity;

namespace ImpExpData.Models
{
    public class Ctx : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Code> Codes { get; set; }

        public Ctx() : base("DefaultConnection")
        {
        }
    }    
}
