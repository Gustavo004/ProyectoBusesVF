using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoBuses.Models
{
    public class ViajeCLS
    {
        [Display(Name ="Id Viaje")]
        public int iidViaje { get; set; }


        [Display(Name = "Lugar Origen")]
        [Required]
        public int iidLugarOrigen { get; set; }


        [Display(Name = "Lugar Destino")]
        [Required]
        public int iidLugarDestino { get; set; }


        [Display(Name = "Precio")]
        [Range(0,2000,ErrorMessage ="Fuera de rango")]
        [Required]
        public double precio { get; set; }


        [Display(Name = "Fecha Viaje")]
        [Required]
        [DataType(DataType.Date)]
        //Para que nos permita registrar;
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaViaje { get; set; }


        [Display(Name = "Bus")]
        [Required]
        public int iidBus { get; set; }



        [Display(Name = " # Asientos Disponibles")]
        [Required]
        public int numeroAsientosDisponibles { get; set; }



        //Propiedades adicionales;
        [Display(Name = "Lugar Origen")]
        public string nombreLugarOrigen { get; set; }


        [Display(Name = "Lugar Destino")]
        public string nombreLugarDestino{ get; set; }


        [Display(Name = "Nombre Bus")]
        public string nombreBus { get; set; }

        //Propiedades Adicionales 
        public string nombreFoto { get; set; }










    }














}