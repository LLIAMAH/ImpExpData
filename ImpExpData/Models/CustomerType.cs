using System.ComponentModel.DataAnnotations;

namespace ImpExpData.Models
{
    public class CustomerType
    {
        [Key]
        public long CustomerTypeId { get; set; }
        [MaxLength(40)]
        public string Value { get; set; }
    }
}
