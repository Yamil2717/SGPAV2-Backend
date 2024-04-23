using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class SaleDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int ProductID { get; set; }
        [ForeignKey("ProductID")]

        public virtual Products Product { get; set; }

        public int SaleID { get; set; }
        [ForeignKey("SaleID")]

        public virtual Sale Sale { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
    }

    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        [MinLength(3)]
        public string Client { get; set; } = "Invitado";

        public virtual List<SaleDetail> SaleDetails { get; set; } = [];

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; } = 0.0M;

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
