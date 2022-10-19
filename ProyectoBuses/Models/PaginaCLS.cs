using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoBuses.Models
{
    public class PaginaCLS
    {
        [Display(Name ="Id Pagina")]
        public int iidpagina { get; set; }
        
        [Display(Name ="Titulo Pagina")]
        [Required]
        public string mensaje { get; set; }
        
        [Display(Name ="Nombre de la Accion")]
        [Required]
        public string accion { get; set; }
        
        [Display(Name ="Nombre del controlador")]
        [Required]
        public string controlador { get; set; }
        public int bhabilitado { get; set; }
    }
}