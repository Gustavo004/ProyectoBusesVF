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

            int nregistrosEncontrados = 0;
            string nombreMarca = oMarcaCLS.nombre;

            using (var bd = new BDPasajeEntities1() ) 
            {
                nregistrosEncontrados = bd.Marca.Where(p => p.NOMBRE.Equals(nombreMarca)).Count();
            }

            //Validando que no este llegando nada vacio o que no haya duplicados;
            if (!ModelState.IsValid || nregistrosEncontrados>=1)
            {
                if (nregistrosEncontrados >= 1) oMarcaCLS.mensajeError = "El nombre Marca ya existe";

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
            int nRegistrosEncontrados = 0;
            //AVANZA AQUI:
            int idMarca_ = oMarcaCLS.iddmarca;


            using (var bd = new BDPasajeEntities1() ) 
            {
             nRegistrosEncontrados = bd.Marca.Where(p => p.NOMBRE.Equals(nombreMarca) && !p.IIDMARCA.Equals(idMarca_)).Count();
            }


            if (!ModelState.IsValid || nRegistrosEncontrados>=1)
            {
                if (nRegistrosEncontrados >= 1) oMarcaCLS.mensajeError = "Ya se encuentra registrada la marca";


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


        //Es una eliminacion logica por lo que simplemente le vamos a cambiar el estado de de 1 a 0

        /*ESTADO DE VISIBILIDAD
          1 = VISIBLE 
          0 = NO VISIBLE
        */
        public ActionResult Eliminar(int iddmarca) 
        {
            //Abriendo la conexion;
            using (var bd = new BDPasajeEntities1()  ) 
            {
                Marca oMarca = bd.Marca.Where(p => p.IIDMARCA.Equals(iddmarca)).First();

                //Cambiandole el estado de visibilidad;
                oMarca.BHABILITADO = 0;

                //Guardando los cambios ;
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }








    }
}