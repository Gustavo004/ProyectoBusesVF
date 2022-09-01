using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoBuses.Models
{

    public class MarcaCLS
    {
        //Get and Set ;
        //Etiquetas ;
        //Display es para mostrar con el nombre requerido

        [Display(Name = "ID Marca")]
        public int iddmarca { get; set; }


        [Display(Name = "Nombre de Marca")]
        [Required]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string nombre { get; set; }


        [Display(Name = "Descripción De la Marca")]
        [Required]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        public string descripcion { get; set; }


        public int bdhabilitado { get; set; }


    }





}