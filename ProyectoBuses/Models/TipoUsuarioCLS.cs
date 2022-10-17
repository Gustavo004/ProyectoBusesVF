using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProyectoBuses.Models
{
    public class TipoUsuarioCLS
    {
        [Display(Name ="Id tipo Usuario")]
        public int iidTipoUsuario { get; set; }

        [Display(Name = "Nombre tipo Usuario")]
        [Required]
        [StringLength(150,ErrorMessage ="Longitud maxima 150")]
        public string nombre { get; set; }

        [Display(Name = "Descripción")]
        [Required]
        [StringLength(250, ErrorMessage = "Longitud maxima 250")]
        public string descripcion { get; set; }
        public string bhabilitado { get; set; }
    }
}