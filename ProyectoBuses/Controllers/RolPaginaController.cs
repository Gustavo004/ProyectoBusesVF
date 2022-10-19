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
            using (var bd=new BDPasajeEntities1())
            {
                listaRol = (from rolPagina in bd.RolPagina
                            join rol in bd.Rol
                            on rolPagina.IIDROL equals rol.IIDROL
                            join pagina in bd.Pagina
                            on rolPagina.IIDPAGINA equals pagina.IIDPAGINA
                            where rolPagina.BHABILITADO==1
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
        public ActionResult Filtrar(int? iidrol)
        {
            List<RolPaginaCLS> listaRol = new List<RolPaginaCLS>();
            using (var bd = new BDPasajeEntities1())
            {

                if (iidrol == null)
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
                                where rolPagina.BHABILITADO == 1 && rolPagina.IIDROL == iidrol
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
        public int Guardar(RolPaginaCLS rolPaginaCLS, int titulo)
        {
            int rpta = 0;
            using(var bd=new BDPasajeEntities1())
            {
                if (titulo == 1)
                {
                    RolPagina oRolPagina = new RolPagina();
                    oRolPagina.IIDROL = rolPaginaCLS.iidrol;
                    oRolPagina.IIDPAGINA = rolPaginaCLS.iidpagina;
                    oRolPagina.BHABILITADO =1;
                    bd.RolPagina.Add(oRolPagina);
                    rpta = bd.SaveChanges();
                }
            }
            return rpta;
        }


    }
}
