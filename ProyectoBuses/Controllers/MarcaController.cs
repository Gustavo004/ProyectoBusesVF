using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//Recuerda el usar;
using ProyectoBuses.Models;

namespace ProyectoBuses.Controllers
{
    public class MarcaController : Controller
    {
        // GET: Marca
        public ActionResult Index()
        {
            List<MarcaCLS> listaMarca = null;

            //Abriendo conexión a la base de datos;
            using (var bd  = new BDPasajeEntities1() )
            {
                //Creando una lista y recorriendo el modelo ;
                 listaMarca = (from marca in bd.Marca
                               where marca.BHABILITADO==1

                                             select new MarcaCLS                                            
                                             {
                                                 iddmarca = marca.IIDMARCA,
                                                 nombre = marca.NOMBRE,
                                                 descripcion = marca.DESCRIPCION
                                             }).ToList();
            }

            return View(listaMarca);
        }

        //Creando la vista para la marca;
        public ActionResult Agregar() 
        {



            return View();
        }


        //Creando el metodo de agregar para el procesamiento de informacion;
        [HttpPost]
        public ActionResult Agregar(MarcaCLS oMarcaCLS)
        {
            //Validando que no este llegando nada vacio;
            if (!ModelState.IsValid)
            {
                return View(oMarcaCLS);
            }
            else 
            {
                using(var bd=new BDPasajeEntities1() )
                {
                    Marca oMarca = new Marca();

                    oMarca.NOMBRE = oMarcaCLS.nombre;
                    oMarca.DESCRIPCION = oMarcaCLS.descripcion;
                    oMarca.BHABILITADO = 1;
                    bd.Marca.Add(oMarca);
                    bd.SaveChanges();
                }      
            }
            return RedirectToAction("Index");
        }








    }
}