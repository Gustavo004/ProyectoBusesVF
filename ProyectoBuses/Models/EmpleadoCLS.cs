using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoBuses.Models
{
    public class EmpleadoCLS
    {
        [Display(Name = "Id Empleado")]
        public int idempleado { get; set; }


        [Display(Name = "Nombre")]
        [Required]
        [StringLength(100, ErrorMessage = "Longitud Máxima 100")]
        public string nombre { get; set; }


        [Display(Name = "Apellido Paterno")]
        [Required]
        [StringLength(200, ErrorMessage = "Longitud Máxima 200")]
        public string apPaterno { get; set; }


        [Display(Name = "Apellido Materno")]
        [Required]
        [StringLength(200, ErrorMessage = "Longitud Máxima 200")]
        public string apMaterno { get; set; }


        [Display(Name = "Fecha de Contrato")]
        [Required]
        [DataType(DataType.Date)]
        //Para que nos permita registrar;
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaContrato { get; set; }


        [Display(Name = "Tipo de Usuario")]
        [Required]
        public int iidTipousuario { get; set; }


        [Display(Name = "Tipo de Contrato")]
        [Required]
        public int iidTipocontrato { get; set; }


        [Display(Name = "Sexo")]
        [Required]
        public int iidSexo { get; set; }


        [Display(Name = "Sueldo")]
        [Required]
        [Range(0,50000,ErrorMessage ="Fuera de rango")]
        public decimal sueldo { get; set; }



        public int bHabilitado { get; set; }


        //Propiedades adicionales;
        [Display(Name = "Tipo Contrato")]
        public string nombreTipocontrato { get; set; }

        [Display(Name = "Tipo Usuario")]
        public string nombreTipoUsuario { get; set; }

        public string mensajeError { get; set; }















    }
}