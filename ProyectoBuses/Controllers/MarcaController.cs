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
            using (var bd = new BDPasajeEntities1())
            {
                //Creando una lista y recorriendo el modelo ;
                listaMarca = (from marca in bd.Marca
                              where marca.BHABILITADO == 1

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
                using (var bd = new BDPasajeEntities1())
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


        //Vista para el action Result
        public ActionResult Editar(int id)
        {
            //Creando la clase OmarcaCLS
            MarcaCLS oMarcaCLS = new MarcaCLS();

            //Abriendo la conexion con la base de datos;
            using (var bd = new BDPasajeEntities1())
            {
                //Recuperando el objeto y trayendo solo la primera fila es por ello que usamos el metodo.first();
                Marca Omarca = bd.Marca.Where(p => p.IIDMARCA.Equals(id)).First();

                //Recuperando los parametros para pasarlo al  modelo
                oMarcaCLS.iddmarca = Omarca.IIDMARCA;
                oMarcaCLS.nombre = Omarca.NOMBRE;
                oMarcaCLS.descripcion = Omarca.DESCRIPCION;
            }

            //Del modelo lo mandamos a la vista del programador
            return View(oMarcaCLS);
        }


        //Editar
        [HttpPost]
        public ActionResult Editar(MarcaCLS oMarcaCLS)
        {

            if (!ModelState.IsValid)
            {
                return View(oMarcaCLS);
            }

            //Recuperando el ID
            int idMarca = oMarcaCLS.iddmarca;

            using (var bd = new BDPasajeEntities1())
            {
                Marca OMarca = bd.Marca.Where(p => p.IIDMARCA.Equals(idMarca)).First();
                OMarca.NOMBRE = oMarcaCLS.nombre;
                OMarca.DESCRIPCION = oMarcaCLS.descripcion;
                bd.SaveChanges();

            }

            return RedirectToAction("Index");
        }

    }
}