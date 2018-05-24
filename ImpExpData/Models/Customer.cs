using System.ComponentModel.DataAnnotations.Schema;

namespace ImpExpData.Models
{
    public class Customer
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public long? Code { get; set; }
        [ForeignKey("Code")]
        public virtual CustomerType CustomerType { get; set; }
    }
}
