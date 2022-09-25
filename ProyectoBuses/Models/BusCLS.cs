using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoBuses.Models
{
    public class BusCLS
    {
        [Display(Name ="Id Bus")]
        public int iidbus { get; set; }

        [Display(Name = "Nombre Sucursal")]
        [Required]
        public int iisucursal { get; set; }


        [Display(Name = "Tipo Bus")]
        [Required]
        public int iidTipoBus { get; set; }


        [Display(Name = "Placa")]
        [Required]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string placa { get; set; }

        //Para que nos permita registrar;
        [Display(Name = "Fecha De Compra")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaCompra { get; set; }



        [Display(Name = "Nombre Modelo")]
        [Required]
        public int iidModelo { get; set; }


        [Display(Name = "Numero de Filas")]
        [Required]
        public int numeroFilas { get; set; }


        [Display(Name = "Numero de Columnas")]
        [Required]
        public int numeroColumnas { get; set; }


        public int bHabilitado { get; set; }


        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        [Required]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        public string descripcion { get; set; }


        [Display(Name = "Observación")]
        [DataType(DataType.MultilineText)]
        [StringLength(200,ErrorMessage ="Máximo 200 caracteres")]
        public string observacion { get; set; }


        [Display(Name = "Nombre De Marca")]
        [Required]
        public int iidmarca { get; set; }


        //Adicionales
        [Display(Name = "Nombre Sucursal")]
        public string nombreSucursal { get; set; }

        [Display(Name = "Nombre Tipo Bus")]
        public string nombreTipoBus { get; set; }

        [Display(Name = "Nombre Modelo")]
        public string nombreModelo { get; set; }



    }
}