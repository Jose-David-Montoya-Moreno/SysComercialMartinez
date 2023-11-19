using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysComercialMartinez.EntidadesDeNegocio
{
    public class DetalleCompra
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDetalleCompra { get; set; }

        [Required(ErrorMessage = "Cantidad es obligatorio")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "Valor Total es obligatorio")]
        public decimal ValorTotal { get; set; }

        [ForeignKey("Compra")]
        [Required(ErrorMessage = "Compra es obligatorio")]
        [Display(Name = "Compra")]
        public int ICompra { get; set; }

        [ForeignKey("Producto")]
        [Required(ErrorMessage = "Producto es obligatorio")]
        [Display(Name = "Producto")]
        public int IdProducto { get; set; }

        public Compra? Compra { get; set; }


        public Producto? Producto { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }
    }
}
