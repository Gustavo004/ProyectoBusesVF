using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoBuses.Models
{
    public class ClienteCLS
    {
        [Display (Name = "ID Cliente")]
        public int idcliente { get; set; }


        [Display(Name = "Nombre")]
        [Required]
        [StringLength(100,ErrorMessage ="Longitud Máxima 100")]
        public string nombre { get; set; }


        [Display(Name = "Apellido Paterno")]
        [Required]
        [StringLength(150, ErrorMessage = "Longitud Máxima 150")]
        public string appaterno { get; set; }


        [Display(Name = "Apellido Materno")]
        [Required]
        [StringLength(150, ErrorMessage = "Longitud Máxima 150")]
        public string apmaterno { get; set; }


        [Display(Name = "Email")]
        [Required]
        [StringLength(200, ErrorMessage = "Longitud Máxima 200")]
        [EmailAddress(ErrorMessage ="Ingrese un email valido")]
        public string email { get; set; }


        [Display(Name = "Dirección")]
        [Required]
        [StringLength(200, ErrorMessage = "Longitud Máxima 200")]
        public string direccion { get; set; }

        [Required]
        [Display(Name = "Sexo")]
        public int idsexo { get; set; }

        [Required]
        [Display(Name = "Telefono Fijo")]
        [StringLength(10, ErrorMessage = "Longitud Máxima 10")]
        public string telefonoFijo { get; set; }

        [Required]
        [Display(Name = "Celular")]
        [StringLength(10, ErrorMessage = "Longitud Máxima 10")]
        public string telefonoCelular { get; set; }






        public int bdhabilitado { get; set; }
        public int btieneUsuario { get; set; }
        public string tipoUsuario { get; set; }



    }
}