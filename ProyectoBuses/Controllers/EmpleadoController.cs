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
            //Listado de usarios  Desde la base de datos ;
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



        //Creando una lista para lista el Sexo;
        List<SelectListItem> listaSexo = null;
        public void listarComboSexo() 
        {
            //Agregar;
           
            //Abriendo una conexion a la db
            using (var bd = new BDPasajeEntities1() ) 
            {
                listaSexo = (from sexo in bd.Sexo
                         where sexo.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = sexo.NOMBRE,
                             Value = sexo.IIDSEXO.ToString()

                         }).ToList();
                listaSexo.Insert(0, new SelectListItem { Text = "SELECCIONE", Value = "" });

              //Esto lo almaceno en un ViewBag para posterior pasarlo a la vista agregar desde aqui se recupera para la vista;
              ViewBag.listaSexo = listaSexo;
            }
        }





        //Listando tipoUsuario
        List<SelectListItem> listaTipoUsuario = null;
        public void listaTipoUsuarios() 
        {
           
            using (var bd = new BDPasajeEntities1() ) 
            {
                listaTipoUsuario = (from tipousuario in bd.TipoUsuario
                         where tipousuario.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = tipousuario.NOMBRE,
                             Value = tipousuario.IIDTIPOUSUARIO.ToString()

                         }).ToList();
                listaTipoUsuario.Insert(0, new SelectListItem { Text = "SELECCIONE", Value = "" });
                ViewBag.listaTipoUsuarios = listaTipoUsuario;
            }     
        }




        //Listando tipoContrato
       List<SelectListItem> listaTipoContratos = null;
        public void listaTipoContrato()
        {
            
            using (var bd = new BDPasajeEntities1() )
            {
                listaTipoContratos = (from tipocontrato in bd.TipoContrato
                         where tipocontrato.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = tipocontrato.NOMBRE,
                             Value = tipocontrato.IIDTIPOCONTRATO.ToString()
                         }).ToList();

                listaTipoContratos.Insert(0, new SelectListItem { Text = "SELECCIONE", Value = "" });
                ViewBag.listaTipoContrato = listaTipoContratos;
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

            //Se invoca a los ViewBags;
            ViewBag.lista = listaTipoUsuario;
            ViewBag.lista = listaTipoContratos;
            ViewBag.lista = listaSexo;

            return View();
        }




        [HttpPost]
        public ActionResult Agregar(EmpleadoCLS oEmpleadoCLS)
        {
            int nregistroAfectados = 0;
            string nombre = oEmpleadoCLS.nombre;
            string apPaterno = oEmpleadoCLS.apPaterno;
            string apMaterno = oEmpleadoCLS.apMaterno;
            using (var bd=new BDPasajeEntities1())
            {
                nregistroAfectados = bd.Empleado.Where(
                    p => p.NOMBRE.Equals(nombre) &&
                    p.APPATERNO.Equals(apPaterno) &&
                    p.APMATERNO.Equals(apMaterno)).Count();
            }

                if (!ModelState.IsValid || nregistroAfectados>=1)
                {
                if (nregistroAfectados >= 1) oEmpleadoCLS.mensajeError = "El empleado ya existe";
                    listarCombos();

                    ViewBag.lista = listaTipoUsuario;
                    ViewBag.lista = listaTipoContratos;
                    ViewBag.lista = listaSexo;

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

        //Vista para el action Result
        public ActionResult Editar(int id) 
        {
            //Creando la clase OmarcaCLS
            EmpleadoCLS oEmpleadoCLS = new EmpleadoCLS();
            //Abriendo la conexion con la base de datos;
            using (var bd = new BDPasajeEntities1())
            {
                //Recuperando el objeto y trayendo solo la primera fila es por ello que usamos el metodo.first();
                Empleado OEmpleado = bd.Empleado.Where(p => p.IIDEMPLEADO.Equals(id)).First();

                //Recuperando los parametros para pasarlo al  modelo
                oEmpleadoCLS.idempleado = OEmpleado.IIDEMPLEADO;
                oEmpleadoCLS.nombre = OEmpleado.NOMBRE;
                oEmpleadoCLS.apPaterno = OEmpleado.APPATERNO;
                oEmpleadoCLS.apMaterno = OEmpleado.APMATERNO;
                oEmpleadoCLS.fechaContrato = (DateTime)OEmpleado.FECHACONTRATO;
                oEmpleadoCLS.sueldo = (decimal)OEmpleado.SUELDO;

                //Listas desplegables;
                oEmpleadoCLS.iidTipousuario = (int) OEmpleado.IIDTIPOUSUARIO;
                oEmpleadoCLS.iidTipocontrato = (int) OEmpleado.IIDTIPOCONTRATO;
                oEmpleadoCLS.iidSexo = (int) OEmpleado.IIDSEXO; 

                //Listando a los combos y viewBags
                ViewBag.lista = listaTipoUsuario;
                ViewBag.lista = listaTipoContratos;
                ViewBag.lista = listaSexo;
                listarCombos();
            }
           return View(oEmpleadoCLS);
        }


        [HttpPost]
        public ActionResult Editar(EmpleadoCLS oEmpleadoCLS)
        {
            int nregistroAfectados = 0;
            int idEmpleado = oEmpleadoCLS.idempleado;
            string nombre = oEmpleadoCLS.nombre;
            string apPaterno = oEmpleadoCLS.apPaterno;
            string apMaterno = oEmpleadoCLS.apMaterno;
            using (var bd = new BDPasajeEntities1())
            {
                nregistroAfectados = bd.Empleado.Where(p => p.NOMBRE.Equals(nombre)
                  && p.APPATERNO.Equals(apPaterno)
                  && p.APMATERNO.Equals(apMaterno)
                  && !p.IIDEMPLEADO.Equals(idEmpleado)).Count();
            }
            if (!ModelState.IsValid || nregistroAfectados >=1)
            {
                if (nregistroAfectados >= 1) oEmpleadoCLS.mensajeError = "Ya existe el empleado";
                listarCombos();
                return View(oEmpleadoCLS);
            }

            //Abriendo la conexion con la base de datos;
            using (var bd = new BDPasajeEntities1() )
            {
                //Recuperando el objeto y trayendo solo la primera fila es por ello que usamos el metodo.first();
                Empleado oEmpleado = bd.Empleado.Where(p => p.IIDEMPLEADO.Equals(idEmpleado)).First();

                //Recuperando los parametros para pasarlo al  modelo
                oEmpleado.IIDEMPLEADO = oEmpleadoCLS.idempleado;
                oEmpleado.NOMBRE = oEmpleadoCLS.nombre ;
                oEmpleado.APPATERNO = oEmpleadoCLS.apPaterno;
                oEmpleado.APMATERNO = oEmpleadoCLS.apMaterno ;
                oEmpleado.FECHACONTRATO = oEmpleadoCLS.fechaContrato;
                oEmpleado.SUELDO = oEmpleadoCLS.sueldo;

                //Listas desplegables;
                oEmpleado.IIDTIPOUSUARIO = oEmpleadoCLS.iidTipousuario;
                oEmpleado.IIDTIPOCONTRATO = oEmpleadoCLS.iidTipocontrato;
                oEmpleado.IIDSEXO = oEmpleadoCLS.iidSexo;

                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Eliminar(int txtIdEmpleado)
        {
            using(var bd=new BDPasajeEntities1())
            {
                Empleado emp = bd.Empleado.Where(p => p.IIDEMPLEADO.Equals(txtIdEmpleado)).First();
                emp.BHABILITADO = 0;
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}