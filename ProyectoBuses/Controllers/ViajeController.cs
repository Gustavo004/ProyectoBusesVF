using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoBuses.Models;

namespace ProyectoBuses.Controllers
{
    public class ViajeController : Controller
    {
        // GET: Viaje
        public ActionResult Index()
        {

            List<ViajeCLS> listaViaje= null;
            ListarCombo();

            using (var bd = new BDPasajeEntities1()) 
            {

                listaViaje = (from viaje in bd.Viaje
                              join lugarOrigen in bd.Lugar
                              on viaje.IIDLUGARORIGEN equals lugarOrigen.IIDLUGAR
                              join lugarDestino in bd.Lugar
                              on viaje.IIDLUGARDESTINO equals lugarDestino.IIDLUGAR
                              join bus in bd.Bus
                              on viaje.IIDBUS equals bus.IIDBUS
                              select new ViajeCLS
                              {
                                  iidViaje=viaje.IIDVIAJE,
                                  nombreBus=bus.PLACA,
                                  nombreLugarOrigen=lugarOrigen.NOMBRE,
                                  nombreLugarDestino=lugarDestino.NOMBRE
                              }).ToList();

            }
           return View(listaViaje);
        }


        //Creando una lista para lista los lugares de origen y destino;
        List<SelectListItem> listaLugar = null;
        public void listarLugar()
        {
            //Agregar;

            //Abriendo una conexion a la db
            using (var bd = new BDPasajeEntities1())
            {
                listaLugar = (from item in bd.Lugar
                              where item.BHABILITADO == 1
                              select new SelectListItem
                              {
                                  Text = item.NOMBRE,
                                  Value = item.IIDLUGAR.ToString()

                              }).ToList();
                listaLugar.Insert(0, new SelectListItem { Text = "SELECCIONE", Value = "" });

                //Esto lo almaceno en un ViewBag para posterior pasarlo a la vista agregar desde aqui se recupera para la vista;
                ViewBag.listaLugar = listaLugar;
            }
        }
  

        //Creando una lista para lista los buses;B
        List<SelectListItem> listaBus = null;
        public void ListarBus()
        {
            //Agregar;

            //Abriendo una conexion a la db
            using (var bd = new BDPasajeEntities1())
            {
                listaBus = (from item in bd.Bus
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.PLACA,
                             Value = item.IIDBUS.ToString()

                         }).ToList();
                listaBus.Insert(0, new SelectListItem { Text = "SELECCIONE", Value = "" });

                //Esto lo almaceno en un ViewBag para posterior pasarlo a la vista agregar desde aqui se recupera para la vista;
                ViewBag.listaBus = listaBus;
            }
        }



        public void ListarCombo()
        {
            listarLugar();
            ListarBus();
        }





        //Vista Agregar;
        public ActionResult Agregar() 
        {
            ListarCombo();
            return View();
        }


       














    }
}