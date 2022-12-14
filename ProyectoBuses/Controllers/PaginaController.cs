using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoBuses.Models;
namespace ProyectoBuses.Controllers
{
    public class PaginaController : Controller
    {
        // GET: Pagina
        public ActionResult Index()
        {
            List<PaginaCLS> listaPagina = new List<PaginaCLS>();
            using (var bd = new BDPasajeEntities1())
            {
                listaPagina = (from pagina in bd.Pagina
                               where pagina.BHABILITADO == 1
                               select new PaginaCLS
                               {
                                   iidpagina = pagina.IIDPAGINA,
                                   mensaje = pagina.MENSAJE,
                                   controlador = pagina.CONTROLADOR,
                                   accion = pagina.ACCION
                               }).ToList();
            }
            return View(listaPagina);
        }
        public ActionResult Filtrar(PaginaCLS oPaginaCLS)
        {
            string mensaje = oPaginaCLS.mensajeFiltro;
            List<PaginaCLS> listaPagina = new List<PaginaCLS>();
            using (var bd = new BDPasajeEntities1())
            {
                if (mensaje == null)
                {
                    listaPagina = (from pagina in bd.Pagina
                                   where pagina.BHABILITADO == 1
                                   select new PaginaCLS
                                   {
                                       iidpagina = pagina.IIDPAGINA,
                                       mensaje = pagina.MENSAJE,
                                       controlador = pagina.CONTROLADOR,
                                       accion = pagina.ACCION
                                   }).ToList();
                }
                else
                {
                    listaPagina = (from pagina in bd.Pagina
                                   where pagina.BHABILITADO == 1 && pagina.MENSAJE.Contains(mensaje)
                                   select new PaginaCLS
                                   {
                                       iidpagina = pagina.IIDPAGINA,
                                       mensaje = pagina.MENSAJE,
                                       controlador = pagina.CONTROLADOR,
                                       accion = pagina.ACCION
                                   }).ToList();
                }
            }
            return PartialView("_TablaPagina", listaPagina);
        }
        public string Guardar(PaginaCLS oPaginaCLS, int titulo)
        {
            string rpta = "";

            try
            {
                if (!ModelState.IsValid)
                {
                    var query = (from state in ModelState.Values
                                 from error in state.Errors
                                 select error.ErrorMessage).ToList();

                    rpta += "<ul class='list-group'>";

                    foreach (var item in query)
                    {
                        rpta += "<li class='list-group-item'>" + item + "</li>";
                    }
                    rpta += "</ul>";

                }
                else
                {

                    using (var bd = new BDPasajeEntities1())
                    {

                        //AGREGAR
                        if (titulo == -1)
                        {
                            Pagina oPagina = new Pagina();
                            oPagina.MENSAJE = oPaginaCLS.mensaje;
                            oPagina.ACCION = oPaginaCLS.accion;
                            oPagina.CONTROLADOR = oPaginaCLS.controlador;
                            oPagina.BHABILITADO = 1;
                            bd.Pagina.Add(oPagina);
                            rpta = bd.SaveChanges().ToString();
                            if (rpta == "0") rpta = "";
                        }
                        else
                        {
                            //ESTO ES PARA EDITAR
                            Pagina oPagina = bd.Pagina.Where(p => p.IIDPAGINA == titulo).First();
                            oPagina.MENSAJE = oPaginaCLS.mensaje;
                            oPaginaCLS.controlador = oPaginaCLS.controlador;
                            oPaginaCLS.accion = oPaginaCLS.accion;
                            rpta = bd.SaveChanges().ToString();

                        }
                    }
                }
            }//Fin del try
            catch (Exception ex)
            {
                rpta = "";
                Console.WriteLine(ex.Message);
            }
            return rpta;
        }

        public JsonResult recuperarInformacion(int idPagina)
        {

            PaginaCLS oPaginaCLS = new PaginaCLS();

            using (var bd = new BDPasajeEntities1())
            {
                Pagina oPagina = bd.Pagina.Where(p => p.IIDPAGINA == idPagina).First();

                oPaginaCLS.mensaje = oPagina.MENSAJE;
                oPaginaCLS.accion = oPagina.ACCION;
                oPaginaCLS.controlador = oPagina.CONTROLADOR;
            }
            return Json(oPaginaCLS, JsonRequestBehavior.AllowGet);
        }


        //Para eliminar
        public int EliminarPagina(int iidpagina)
        {

            int rpta = 0;

            try
            {
                using (var bd = new BDPasajeEntities1())
                {
                    Pagina oPagina = bd.Pagina.Where(p => p.IIDPAGINA == iidpagina).First();
                    oPagina.BHABILITADO = 0;
                    rpta = bd.SaveChanges();
}
                return rpta;
            }
            catch (Exception ex)
            {
                //Aca se considera a 0 como error para nosostros 
                Console.WriteLine(ex.Message);
                rpta = 0;
            }

            return rpta;
        }











    }
}