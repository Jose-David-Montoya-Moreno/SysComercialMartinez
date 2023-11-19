using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace SysComercialMartinez.EntidadesDeNegocio
{
    public class Compra
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCompra { get; set; }

        [Required(ErrorMessage = "Numero de Compra es obligatorio")]
        public int NumeroCompra { get; set; }

        [Display(Name = "Fecha Compra")]
        public DateTime FechaCompra { get; set; }
        [Required(ErrorMessage = "Forma de Pago es obligatorio")]
        public byte FormaPago { get; set; }

        [Required(ErrorMessage = "Tota de pago es obligatorio")]
        public decimal TotalPago { get; set; }

        [Required(ErrorMessage = "Total es obligatorio")]
        public decimal Total { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }
    }
}
