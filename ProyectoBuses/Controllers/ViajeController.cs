using System;
using System.Collections.Generic;
using System.IO;
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

            List<ViajeCLS> listaViaje = null;
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
                                  iidViaje = viaje.IIDVIAJE,
                                  nombreBus = bus.PLACA,
                                  nombreLugarOrigen = lugarOrigen.NOMBRE,
                                  nombreLugarDestino = lugarDestino.NOMBRE
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


        //Accion  al metodo filtrar
        public ActionResult Filtrar(int? lugarDestinoParametro)
        {
            List<ViajeCLS> listaViaje = new List<ViajeCLS>();

            using (var bd = new BDPasajeEntities1())
            {

                if (lugarDestinoParametro == null)
                {
                    listaViaje = (from viaje in bd.Viaje
                                  join lugarOrigen in bd.Lugar
                                  on viaje.IIDLUGARORIGEN equals lugarOrigen.IIDLUGAR
                                  join lugarDestino in bd.Lugar
                                  on viaje.IIDLUGARDESTINO equals lugarDestino.IIDLUGAR
                                  join bus in bd.Bus
                                  on viaje.IIDBUS equals bus.IIDBUS
                                  where viaje.BHABILITADO == 1
                                  select new ViajeCLS
                                  {
                                      iidViaje = viaje.IIDVIAJE,
                                      nombreBus = bus.PLACA,
                                      nombreLugarOrigen = lugarOrigen.NOMBRE,
                                      nombreLugarDestino = lugarDestino.NOMBRE
                                  }).ToList();
                }
                else
                {
                    listaViaje = (from viaje in bd.Viaje
                                  join lugarOrigen in bd.Lugar
                                  on viaje.IIDLUGARORIGEN equals lugarOrigen.IIDLUGAR
                                  join lugarDestino in bd.Lugar
                                  on viaje.IIDLUGARDESTINO equals lugarDestino.IIDLUGAR
                                  join bus in bd.Bus
                                  on viaje.IIDBUS equals bus.IIDBUS
                                  where viaje.BHABILITADO == 1
                                  && viaje.IIDLUGARDESTINO == lugarDestinoParametro
                                  select new ViajeCLS
                                  {
                                      iidViaje = viaje.IIDVIAJE,
                                      nombreBus = bus.PLACA,
                                      nombreLugarOrigen = lugarOrigen.NOMBRE,
                                      nombreLugarDestino = lugarDestino.NOMBRE
                                  }).ToList();
                }
            }
            //Me quede aqui ; revisar video.
            return PartialView("_TablaViaje", listaViaje);
        }









        //Controlador asociado al foto ;
        public string Agregar(ViajeCLS oviajeCLS, HttpPostedFileBase foto, int titulo)
        {

            string mensaje = "";
            try
            {
                //Si el modelo no es correcto ;
                if (!ModelState.IsValid || (foto == null && titulo == -1))
                {
                    var query = (from state in ModelState.Values
                                 from error in state.Errors
                                 select error.ErrorMessage).ToList();

                    if (foto == null)
                    {
                        oviajeCLS.mensaje = "La foto es obligatoria";
                        mensaje += "<ul><li> Debe ingresar la foto </li></ul>";
                    }

                    mensaje += "<ul class='list-group'>";

                    foreach (var item in query)
                    {
                        mensaje += "<li class='list-group-item'>" + item + "</li>";
                    }
                    mensaje += "</ul>";
                }
                else
                {
                    //Si es correcto:
                    byte[] fotoBD = null;
                    if (foto != null)
                    {
                        BinaryReader lector = new BinaryReader(foto.InputStream);

                        //Almacenando un arreglo de Bytes lo que me mandaron en foto;
                        fotoBD = lector.ReadBytes((int)foto.ContentLength);
                    }
                    //Abriendo conexion y guardando
                    using (var bd = new BDPasajeEntities1())
                    {
                        if (titulo == -1)
                        {
                            Viaje oViaje = new Viaje();
                            oViaje.IIDBUS = oviajeCLS.iidBus;
                            oViaje.IIDLUGARDESTINO = oviajeCLS.iidLugarDestino;
                            oViaje.IIDLUGARORIGEN = oviajeCLS.iidLugarOrigen;
                            oViaje.PRECIO = oviajeCLS.precio;
                            oViaje.FECHAVIAJE = oviajeCLS.fechaViaje;
                            oViaje.NUMEROASIENTOSDISPONIBLES = oviajeCLS.numeroAsientosDisponibles;
                            oViaje.FOTO = fotoBD;
                            oViaje.nombrefoto = oviajeCLS.nombreFoto;
                            oViaje.BHABILITADO = 1;

                            bd.Viaje.Add(oViaje);
                            bd.SaveChanges();
                            mensaje = bd.SaveChanges().ToString();

                            if (mensaje == "0") mensaje = "";

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                mensaje = "";
            }
            return mensaje;
        }


    }
}