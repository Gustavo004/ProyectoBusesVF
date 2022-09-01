using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoBuses.Models;

namespace ProyectoBuses.Controllers
{
    public class SucursalController : Controller
    {
        // GET: Sucursal
        public ActionResult Index()
        {

            //Creando la lista;
            List<SucursalCLS> listaSucursal = null;

            //Abriendo conexion bd
            using (var bd = new BDPasajeEntities1() )
            {
                listaSucursal = (from sucursal in bd.Sucursal
                                 where sucursal.BHABILITADO==1

                                 select new SucursalCLS
                                 {
                                     idsucursal = sucursal.IIDSUCURSAL,
                                     nombre = sucursal.NOMBRE,
                                     direccion = sucursal.DIRECCION,
                                     telefono = sucursal.TELEFONO,
                                     email = sucursal.EMAIL,
                                     fechaApertura = (DateTime)sucursal.FECHAAPERTURA
                                 }).ToList(); 
            }
            return View(listaSucursal);
        }



        public ActionResult Agregar() 
        {

            return View();
        }


        [HttpPost]
        public ActionResult Agregar(SucursalCLS oSucursalCLS)
        {
            if (!ModelState.IsValid)
            {

                return View(oSucursalCLS);
            }
            else
            {
                using (var bd = new BDPasajeEntities1() ) 
                {
                    Sucursal oSucursal = new Sucursal();

                    oSucursal.NOMBRE = oSucursalCLS.nombre;
                    oSucursal.DIRECCION = oSucursalCLS.direccion;
                    oSucursal.TELEFONO = oSucursalCLS.telefono;
                    oSucursal.EMAIL = oSucursalCLS.email;
                    oSucursal.FECHAAPERTURA = oSucursalCLS.fechaApertura;
                    oSucursal.BHABILITADO = 1;

                    bd.Sucursal.Add(oSucursal);
                    bd.SaveChanges();

                }
            }
            return RedirectToAction("Index");
        }
    }
}