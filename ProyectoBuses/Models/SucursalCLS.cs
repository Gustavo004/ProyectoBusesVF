using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoBuses.Models
{
    public class SucursalCLS
    {
        [Display(Name ="ID Sucursal")]
        public int idsucursal { get; set; }

        [Display(Name = "Nombre de Sucursal")]
        [Required]
        [StringLength(100,ErrorMessage ="Máximo 100 caracteres")]
        public string nombre { get; set; }

        [Display(Name = "Dirección")]
        [Required]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        public string direccion { get; set; }

        [Display(Name = "Teléfono")]
        [Required]
        [StringLength(10, ErrorMessage = "Máximo 10 caracteres")]
        public string telefono { get; set; }

        [Display(Name = "Email")]
        [Required]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [EmailAddress(ErrorMessage ="Ingrese un Email Valido")]
        public string email { get; set; }


        [Display(Name = "Fecha Apertura")]
        [Required]
        [DataType(DataType.Date)]
        //Para que nos permita registrar;
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime fechaApertura { get; set; }


        public int bdhabilitado { get; set; }



    }
}