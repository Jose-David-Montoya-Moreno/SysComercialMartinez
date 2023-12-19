using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysComercialMartinez.EntidadesDeNegocio
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "Codigo es obligatorio")]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "Nombre es obligatorio")]
        [StringLength(30, ErrorMessage = "Maximo 30 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Marca es obligatorio")]
        [StringLength(30, ErrorMessage = "Maximo 50 caracteres")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Descripcion es obligatorio")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Modelo o Serie es obligatorio")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string ModeloSerie { get; set; }

        [Required(ErrorMessage = "Garantia es obligatorio")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string Garantia { get; set; }

        [ReadOnly(true)]
        public decimal PrecioUnitario { get; set; }

        [Required(ErrorMessage = "La Cantidad es obligatorio")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El Precio de compra es obligatorio")]
        public decimal PrecioCompra { get; set; }

        [Required(ErrorMessage = "La Cantidad es obligatorio")]
        public int Porcentaje { get; set; }

        [ForeignKey("Proveedor")]
        [Required(ErrorMessage = "Proveedor es obligatorio")]
        [Display(Name = "Proveedor")]
        public int IdProveedor { get; set; }

        public Proveedor Proveedor { get; set; }

        [ForeignKey("Categoria")]
        [Required(ErrorMessage = "Categoria es obligatorio")]
        [Display(Name = "Proveedor")]
        public int IdCategoria { get; set; }

        public Categoria Categoria { get; set; }

        public List<DetalleCompra>? DetalleCompra { get; set; }

        public List<DetalleVenta>? DetalleVenta { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }
    }
}
