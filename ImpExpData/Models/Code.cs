using System.ComponentModel.DataAnnotations;

namespace ImpExpData.Models
{
    public class Code
    {
        [Key]
        public long CustomerTypeId { get; set; }
        [MaxLength(40)]
        public string Value { get; set; }
    }
}
