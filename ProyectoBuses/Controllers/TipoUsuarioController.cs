using System;
using ProyectoBuses.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProyectoBuses.Controllers
{
    public class TipoUsuarioController : Controller
    {
        private TipoUsuarioCLS oTipoVal;
        private bool buscarTipoUsuario(TipoUsuarioCLS oTipoUsuarioCLS)
        {
            bool busquedaId = true;
            bool busquedaNombre = true;
            bool busquedaDescripcionb = true;
            if (oTipoVal.iidTipoUsuario > 0)
                busquedaId = oTipoUsuarioCLS.iidTipoUsuario.ToString().Contains(oTipoVal.iidTipoUsuario.ToString());
            if (oTipoVal.nombre != null)
                busquedaNombre=oTipoUsuarioCLS.nombre.ToString().Contains(oTipoVal.nombre.ToString());
            if (oTipoVal.descripcion != null)
                busquedaDescripcionb = oTipoUsuarioCLS.descripcion.ToString().Contains(oTipoVal.descripcion.ToString());
            return (busquedaId && busquedaNombre && busquedaDescripcionb);
        }
        // GET: TipoUsuario
        public ActionResult Index(TipoUsuarioCLS oTipoUsuario)
        {
            oTipoVal = oTipoUsuario;
            List<TipoUsuarioCLS> listaTipoUsuario = null;
            List<TipoUsuarioCLS> listaFiltrado = null;
            int idTipoUsario = oTipoUsuario.iidTipoUsuario;
            string nombre = oTipoUsuario.nombre;
            string descripcion = oTipoUsuario.descripcion;
            using (var bd = new BDPasajeEntities1())
            {
                listaTipoUsuario = (from tipoUsuario in bd.TipoUsuario
                                    where tipoUsuario.BHABILITADO == 1
                                    select new TipoUsuarioCLS
                                    {
                                        iidTipoUsuario = tipoUsuario.IIDTIPOUSUARIO,
                                        nombre = tipoUsuario.NOMBRE,
                                        descripcion = tipoUsuario.DESCRIPCION
                                    }).ToList();
                if (idTipoUsario == 0 && nombre == null && descripcion == null)
                    listaFiltrado = listaTipoUsuario;
                else
                {
                    Predicate<TipoUsuarioCLS> pred = new Predicate<TipoUsuarioCLS>(buscarTipoUsuario);
                    listaFiltrado=listaTipoUsuario.FindAll(pred);
                }

                   
                
            }
            return View(listaFiltrado);
        }
    }
}