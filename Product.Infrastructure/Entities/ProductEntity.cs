using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Infrastructure.Entities
{
    public class ProductEntity
    {
        [Key]
        [Column(TypeName = "varchar(8)")]
        public string? DIN { get; set; }
        
        [Column(TypeName = "varchar(30)")]
        public string? Name { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? Shape { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? Strength { get; set; }
        public int LegalStatus { get; set; }
    }
}