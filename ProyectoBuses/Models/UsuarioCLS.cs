using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoBuses.Models
{
    public class UsuarioCLS
    {
        [Display(Name="Id Usuario")]
        public int iidusuario { get; set; }

        [Display(Name = "Nombre Usuario")]
        public string nombreusuario { get; set; }

        public string contra { get; set; }


        public string iidtipousuario { get; set; }

        // el iid es el conjunto de cliente + empleados

        public int iid { get; set; }

        public int iidrol { get; set; }

        //propiedad adicional
        [Display(Name = "Nombre Persona")]
        public string nombrePersona { get; set; }

        [Display(Name = "Nombre Rol")]
        public string nombreRol { get; set; }
        [Display(Name = "Tipo Usuario")]
        public string nombreTipoEmpleado { get; set; }

    }
}