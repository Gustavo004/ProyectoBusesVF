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
            using (var bd = new BDPasajeEntities1())
            {
                listaSucursal = (from sucursal in bd.Sucursal
                                 where sucursal.BHABILITADO == 1

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

            int nRegistroEncontrados = 0;
            string nombreSucursal = oSucursalCLS.nombre;

            //Abriendo conexion para la validacion
            using (var bd = new BDPasajeEntities1() )
            {
                nRegistroEncontrados = bd.Sucursal.Where(p => p.NOMBRE.Equals(nombreSucursal)).Count();
            }


            if (!ModelState.IsValid || nRegistroEncontrados>=1)
            {
                if (nRegistroEncontrados >= 1) oSucursalCLS.mensajeError = "El nombre de la sucursal ya existe";
                return View(oSucursalCLS);
            }
            else
            {
                using (var bd = new BDPasajeEntities1())
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



        //Vista para el action Result
        public ActionResult Editar(int id)
        {
            //Creando la clase OmarcaCLS
            SucursalCLS OsucursalCLS = new SucursalCLS();

            //Abriendo la conexion con la base de datos;
            using (var bd = new BDPasajeEntities1())
            {
                //Recuperando el objeto y trayendo solo la primera fila es por ello que usamos el metodo.first();
                Sucursal OSucursal = bd.Sucursal.Where(p => p.IIDSUCURSAL.Equals(id)).First();

                //Recuperando los parametros para pasarlo al  modelo
                OsucursalCLS.idsucursal = OSucursal.IIDSUCURSAL;
                OsucursalCLS.nombre = OSucursal.NOMBRE;
                OsucursalCLS.direccion = OSucursal.DIRECCION;
                OsucursalCLS.telefono = OSucursal.TELEFONO;
                OsucursalCLS.email = OSucursal.EMAIL;
                OsucursalCLS.fechaApertura = (DateTime)OSucursal.FECHAAPERTURA;

            }
            //Del modelo lo mandamos a la vista del programador
            return View(OsucursalCLS);
        }

        [HttpPost]
        public ActionResult Editar(SucursalCLS oSucursalCLS)
        {
            int nRegistrosEncontrados = 0;
            string nombreSucursal = oSucursalCLS.nombre;
            int idSucursal_ = oSucursalCLS.idsucursal;


            using (var bd = new BDPasajeEntities1() )
            {
                nRegistrosEncontrados = bd.Sucursal.Where(p => p.NOMBRE.Equals(nombreSucursal) && 
                !p.IIDSUCURSAL.Equals(idSucursal_)).Count();
            }


           int idSucursal = oSucursalCLS.idsucursal;

            //Si el modelo no es valido retorna la misma vista;
            if (!ModelState.IsValid || nRegistrosEncontrados>=1 )
            {
                if (nRegistrosEncontrados >= 1) oSucursalCLS.mensajeError = "Ya se encuentra registrada la sucursal";
                return View(oSucursalCLS);
            }

            using (var bd = new BDPasajeEntities1())
            {

                Sucursal oSucursal = bd.Sucursal.Where(p => p.IIDSUCURSAL.Equals(idSucursal)).First();

                oSucursal.NOMBRE = oSucursalCLS.nombre;
                oSucursal.DIRECCION = oSucursalCLS.direccion;
                oSucursal.TELEFONO = oSucursalCLS.telefono;
                oSucursal.EMAIL = oSucursalCLS.email;
                oSucursal.FECHAAPERTURA = oSucursalCLS.fechaApertura;

                bd.SaveChanges();

            }
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            using (var bd = new BDPasajeEntities1() )
            {
                Sucursal oSucursal = bd.Sucursal.Where(p => p.IIDSUCURSAL.Equals(id)).First();

                //Cambiandole el estado de visibilidad;
                oSucursal.BHABILITADO = 0;

                //Guardando los cambios ;
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }




    }
}