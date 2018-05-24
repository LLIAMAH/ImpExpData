using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImpExpData.Models
{
    public class Customer
    {
        [Key]
        public long Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Notes { get; set; }
        public long? CodeId { get; set; }
        [ForeignKey("CodeId")]
        public virtual Code Code { get; set; }
    }
}
