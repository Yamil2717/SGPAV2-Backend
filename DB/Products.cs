using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        [MinLength(3)]
        public string Name { get; set; } = "Producto";

        [Required]
        [StringLength(500)]
        [MinLength(3)]
        public string Description { get; set; } = "Descripcion";

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } = 0.0M;

        [Required]
        [StringLength(100)]
        [MinLength(1)]
        public int Stock {  get; set; } = 0;

        [Required]
        public int Category_ID { get; set; }
        [ForeignKey("Category_ID")]

        public virtual required Categories Categories { get; set; }
    }
}
