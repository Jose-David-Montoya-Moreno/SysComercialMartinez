using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysComercialMartinez.EntidadesDeNegocio
{
    public class Categoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "Codigo es obligatorio")]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "Nombre es obligatorio")]
        [StringLength(30, ErrorMessage = "Maximo 30 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Nombre es obligatorio")]
        [StringLength(30, ErrorMessage = "Maximo 30 caracteres")]
        public string Descripcion { get; set; }

        public List<Producto>? Productos { get; set; }


        [NotMapped]
        public int Top_Aux { get; set; }
    }
}
       


