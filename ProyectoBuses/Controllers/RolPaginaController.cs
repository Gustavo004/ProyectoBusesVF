using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoBuses.Models;
namespace ProyectoBuses.Controllers
{
    public class RolPaginaController : Controller
    {


        // GET: RolPagina
        public ActionResult Index()
        {
            List<RolPaginaCLS> listaRol = new List<RolPaginaCLS>();
            listarComboRol();
            listarComboPagina();
            using (var bd = new BDPasajeEntities1())
            {
                listaRol = (from rolPagina in bd.RolPagina
                            join rol in bd.Rol
                            on rolPagina.IIDROL equals rol.IIDROL
                            join pagina in bd.Pagina
                            on rolPagina.IIDPAGINA equals pagina.IIDPAGINA
                            where rolPagina.BHABILITADO == 1
                            select new RolPaginaCLS
                            {
                                iidrolpagina = rolPagina.IIDROLPAGINA,
                                nombreRol = rol.NOMBRE,
                                nombreMensaje = pagina.MENSAJE
                            }).ToList();
            }
            return View(listaRol);
        }
        public void listarComboRol()
        {
            List<SelectListItem> lista = null;
            using (var bd = new BDPasajeEntities1())
            {
                lista = (from item in bd.Rol
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,
                             Value = item.IIDROL.ToString()

                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });

                //Esto lo almaceno en un ViewBag para posterior pasarlo a la vista agregar desde aqui se recupera para la vista;
                ViewBag.listaRol = lista;
            }
        }
        public void listarComboPagina()
        {
            List<SelectListItem> lista = null;
            using (var bd = new BDPasajeEntities1())
            {
                lista = (from item in bd.Pagina
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.MENSAJE,
                             Value = item.IIDPAGINA.ToString()

                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });

                //Esto lo almaceno en un ViewBag para posterior pasarlo a la vista agregar desde aqui se recupera para la vista;
                ViewBag.listaPagina = lista;
            }
        }
        public ActionResult Filtrar(int? iidrolFiltro)
        {
            List<RolPaginaCLS> listaRol = new List<RolPaginaCLS>();
            using (var bd = new BDPasajeEntities1())
            {

                if (iidrolFiltro == null)
                {
                    listaRol = (from rolPagina in bd.RolPagina
                                join rol in bd.Rol
                                on rolPagina.IIDROL equals rol.IIDROL
                                join pagina in bd.Pagina
                                on rolPagina.IIDPAGINA equals pagina.IIDPAGINA
                                where rolPagina.BHABILITADO == 1
                                select new RolPaginaCLS
                                {
                                    iidrolpagina = rolPagina.IIDROLPAGINA,
                                    nombreRol = rol.NOMBRE,
                                    nombreMensaje = pagina.MENSAJE
                                }).ToList();
                }
                else
                {
                    listaRol = (from rolPagina in bd.RolPagina
                                join rol in bd.Rol
                                on rolPagina.IIDROL equals rol.IIDROL
                                join pagina in bd.Pagina
                                on rolPagina.IIDPAGINA equals pagina.IIDPAGINA
                                where rolPagina.BHABILITADO == 1 && rolPagina.IIDROL == iidrolFiltro
                                select new RolPaginaCLS
                                {
                                    iidrolpagina = rolPagina.IIDROLPAGINA,
                                    nombreRol = rol.NOMBRE,
                                    nombreMensaje = pagina.MENSAJE
                                }).ToList();
                }
            }
            return PartialView("_TableRolPagina", listaRol); ;
        }
        public string Guardar(RolPaginaCLS rolPaginaCLS, int titulo)
        {
            //Vacio es error;
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
                        if (titulo == -1)
                        {
                            RolPagina oRolPagina = new RolPagina();
                            oRolPagina.IIDROL = rolPaginaCLS.iidrol;
                            oRolPagina.IIDPAGINA = rolPaginaCLS.iidpagina;
                            oRolPagina.BHABILITADO = 1;
                            bd.RolPagina.Add(oRolPagina);
                            rpta = bd.SaveChanges().ToString();
                            if (rpta == "0") rpta = "";
                        }
                        else
                        {
                            RolPagina oRolPagina = bd.RolPagina.Where(p => p.IIDROLPAGINA == titulo).First();
                            oRolPagina.IIDROL = rolPaginaCLS.iidrol;
                            oRolPagina.IIDPAGINA = rolPaginaCLS.iidpagina;
                            rpta = bd.SaveChanges().ToString();

                        }
                    }
                }
             
            }catch (Exception ex)  //Fin del try 
            {
                Console.WriteLine(ex.Message);
                rpta = "Algo ha ocurrido por favor comuniquese con su gerencia";
            }
            return rpta;
        }


        public  JsonResult recuperarInformacion(int idRolPagina) 
        {
            RolPaginaCLS oRolPaginaCLS = new RolPaginaCLS();

            using (var bd = new BDPasajeEntities1())
            {
                RolPagina oRolPagina = bd.RolPagina.Where(p => p.IIDROLPAGINA == idRolPagina).First();

                oRolPaginaCLS.iidrol = (int)oRolPagina.IIDROL;
                oRolPaginaCLS.iidpagina = oRolPagina.IIDROLPAGINA;

            }
            return Json(oRolPaginaCLS, JsonRequestBehavior.AllowGet);
        }












    }
}
