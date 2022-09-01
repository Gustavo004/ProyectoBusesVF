using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoBuses.Models;

namespace ProyectoBuses.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        public ActionResult Index()
        {


            List<EmpleadoCLS> listaEmpleados = null;


            using (var bd = new BDPasajeEntities1() ) 
            {

                listaEmpleados = (from empleado in bd.Empleado
                                  join tipousuario in bd.TipoUsuario
                                  on empleado.IIDTIPOUSUARIO equals tipousuario.IIDTIPOUSUARIO
                                  join tipocontrato in bd.TipoContrato
                                  on empleado.IIDTIPOCONTRATO equals tipocontrato.IIDTIPOCONTRATO
                                  where empleado.BHABILITADO==1
                                  select new EmpleadoCLS
                                  {
                                      idempleado = empleado.IIDEMPLEADO,
                                      nombre = empleado.NOMBRE,
                                      apPaterno = empleado.APPATERNO,
                                      apMaterno = empleado.APMATERNO,
                                      nombreTipoUsuario = tipousuario.NOMBRE,
                                      nombreTipocontrato = tipocontrato.NOMBRE
                                  }).ToList(); 
                               

            }

            return View(listaEmpleados);
        }

        //Listando los sexos por valor;
        public void listarComboSexo() 
        {
            //Agregar;
            List<SelectListItem> lista = null;

            using (var bd = new BDPasajeEntities1() ) 
            {
                lista = (from sexo in bd.Sexo
                         where sexo.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = sexo.NOMBRE,
                             Value = sexo.IIDSEXO.ToString()

                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "SELECCIONE", Value = "" });
                ViewBag.listaSexo = lista;
            }
        }


        //Listando tipoUsuario
        public void listaTipoUsuarios() 
        {
            List<SelectListItem> lista = null;
            using (var bd = new BDPasajeEntities1() ) 
            {
                lista = (from tipousuario in bd.TipoUsuario
                         where tipousuario.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = tipousuario.NOMBRE,
                             Value = tipousuario.IIDTIPOUSUARIO.ToString()

                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "SELECCIONE", Value = "" });
                ViewBag.listaTipoUsuarios = lista;
            }     
        }

        //Listando tipoContrato
        public void listaTipoContrato()
        {
            List<SelectListItem> lista = null;
            using (var bd = new BDPasajeEntities1() )
            {
                lista = (from tipocontrato in bd.TipoContrato
                         where tipocontrato.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = tipocontrato.NOMBRE,
                             Value = tipocontrato.IIDTIPOCONTRATO.ToString()
                         }).ToList();

                lista.Insert(0, new SelectListItem { Text = "SELECCIONE", Value = "" });
                ViewBag.listaTipoContrato = lista;
            }
        }


        //llamando a los combos
        public void listarCombos()
        {
            listaTipoUsuarios();
            listaTipoContrato();
            listarComboSexo();
        }
   
       






        //vista para agregar empleado;
        public ActionResult Agregar() 
        {

            listarCombos();
            return View();
        }





        [HttpPost]
        public ActionResult Agregar(EmpleadoCLS oEmpleadoCLS)
        {

            if (!ModelState.IsValid)
            {
                listarCombos();
                return View(oEmpleadoCLS);
            }
            else 
            {
                using (var bd = new BDPasajeEntities1())
                {
                    Empleado oEmpleado = new Empleado();

                    oEmpleado.NOMBRE = oEmpleadoCLS.nombre;
                    oEmpleado.APPATERNO = oEmpleadoCLS.apPaterno;
                    oEmpleado.APMATERNO = oEmpleadoCLS.apMaterno;

                    oEmpleado.FECHACONTRATO = oEmpleadoCLS.fechaContrato;
                    oEmpleado.SUELDO = oEmpleadoCLS.sueldo;
                    oEmpleado.IIDTIPOUSUARIO = oEmpleadoCLS.iidTipousuario;
                    oEmpleado.IIDTIPOCONTRATO = oEmpleadoCLS.iidTipocontrato;
                    oEmpleado.IIDSEXO = oEmpleadoCLS.iidSexo;
                    oEmpleado.BHABILITADO = 1;

                    bd.Empleado.Add(oEmpleado);
                    bd.SaveChanges();
                }
               return RedirectToAction("Index");
            }         
        }
    }
}