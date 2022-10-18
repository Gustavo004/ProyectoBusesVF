using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoBuses.Models
{
    public class RolCLS
    {
        [Display(Name ="Id Rol")]
        public int iidRol { get; set; }

        [Display(Name = "Nombre Rol")]
        [Required]
        public string nombre { get; set; }
        
        [Display(Name = "Descripción Rol")]
        [Required]
        public string descripcion { get; set; }
        public int bhabilitado { get; set; }

    }
}