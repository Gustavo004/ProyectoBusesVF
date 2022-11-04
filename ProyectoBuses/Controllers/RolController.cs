using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoBuses.Models;

namespace ProyectoBuses.Controllers
{
    public class RolController : Controller
    {
        // GET: Rol
        public ActionResult Index()
        {
            List<RolCLS> listaRol = new List<RolCLS>();
            using (var bd = new BDPasajeEntities1())
            {
                listaRol = (from rol in bd.Rol
                            where rol.BHABILITADO == 1
                            select new RolCLS
                            {
                                iidRol = rol.IIDROL,
                                nombre = rol.NOMBRE,
                                descripcion = rol.DESCRIPCION
                            }).ToList();
            }
            return View(listaRol);
        }
        public ActionResult Filtro(string nombreRol)
        {
            List<RolCLS> listaRol = new List<RolCLS>();
            using (var bd = new BDPasajeEntities1())
            {
                if (nombreRol == null)
                {
                    listaRol = (from rol in bd.Rol
                                where rol.BHABILITADO == 1
                                select new RolCLS
                                {
                                    iidRol = rol.IIDROL,
                                    nombre = rol.NOMBRE,
                                    descripcion = rol.DESCRIPCION
                                }).ToList();
                }
                else
                {
                    listaRol = (from rol in bd.Rol
                                where rol.BHABILITADO == 1 && rol.NOMBRE.Contains(nombreRol)
                                select new RolCLS
                                {
                                    iidRol = rol.IIDROL,
                                    nombre = rol.NOMBRE,
                                    descripcion = rol.DESCRIPCION
                                }).ToList();
                }
            }
            return PartialView("_TablaRol", listaRol);
        }


        public string Guardar(RolCLS oRolCLS, int titulo)
        {

            //Habra un error si el campo llega vacio;
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
                        if (titulo.Equals(-1))
                        {
                            Rol oRol = new Rol();
                            oRol.NOMBRE = oRolCLS.nombre;
                            oRol.DESCRIPCION = oRolCLS.descripcion;
                            oRol.BHABILITADO = 1;
                            bd.Rol.Add(oRol);
                            rpta = bd.SaveChanges().ToString();
                            if (rpta == "0") rpta = "";
                        }
                        else
                        {
                            Rol oRol = bd.Rol.Where(p => p.IIDROL == titulo).First();
                            oRol.NOMBRE = oRolCLS.nombre;
                            oRol.DESCRIPCION = oRolCLS.descripcion;

                            bd.SaveChanges();
                            rpta = bd.SaveChanges().ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                rpta = "";
                Console.WriteLine(ex.ToString());
            }
            return rpta;
        }

        //Ojo aqui con la variable titulo;
        public JsonResult recuperarDatos(int titulo)
        {
            RolCLS ORolcls = new RolCLS();

            using (var bd = new BDPasajeEntities1())
            {
                Rol oRol = bd.Rol.Where(p => p.IIDROL == titulo).First();

                ORolcls.nombre = oRol.NOMBRE;
                ORolcls.descripcion = oRol.DESCRIPCION;


            }
            return Json(ORolcls, JsonRequestBehavior.AllowGet);
        }


        public string eliminarRol(RolCLS oRolCLS)
        {
               
            string rpta = ""; //Si esta vacio es porque hubo un error;
            try
            {
                int idrol = oRolCLS.iidRol;
                using (var bd = new BDPasajeEntities1() )
                {
                    
                    Rol oRol = bd.Rol.Where(p => p.IIDROL == idrol).First();
                    oRol.BHABILITADO = 0;

                    rpta = bd.SaveChanges().ToString();
                    //Aqui devuevlve 1 como cadena y eso esta bien 
                }
            }
            catch (Exception ex)
            {
                rpta = "";
                Console.WriteLine(ex.Message);
            }
            return rpta;
        }












    }
}