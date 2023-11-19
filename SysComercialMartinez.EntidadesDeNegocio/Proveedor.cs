using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysComercialMartinez.EntidadesDeNegocio
{
    public class Proveedor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProveedor { get; set; }
        [Required(ErrorMessage = "El codigo es obligatorio")]

        public int Codigo { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(30, ErrorMessage = "Maximo30 caracteres")]

        public string? Nombre { get; set; }
        [Required(ErrorMessage = "La direccion es obligatoria")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string? Direccion { get; set; }
        [Required(ErrorMessage = "El telefono es obligatorio")]
        [StringLength(20, ErrorMessage = "Maximo 20 caracteres")]
        public string? Telefono { get; set; }
        [Required(ErrorMessage = "El Correo es obligatorio")]
        [StringLength(20, ErrorMessage = "Maximo 50 caracteres")]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "El Numero de Cuenta es obligatorio")]
        [StringLength(20, ErrorMessage = "Maximo 20 caracteres")]
        public string? NumeroCuenta { get; set; }
        [NotMapped]
        public int Top_Aux { get; set; }
    }
}
