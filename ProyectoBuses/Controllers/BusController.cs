using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoBuses.Models;

namespace ProyectoBuses.Controllers
{
    public class BusController : Controller
    {
        // GET: Bus
        public ActionResult Index()
        {
            List<BusCLS> listaBus = null;

            using (var bd = new BDPasajeEntities1() ) 
            {
                listaBus = (from bus in bd.Bus              
                            join sucursal in bd.Sucursal //1er Join:Sucursal
                            on bus.IIDSUCURSAL equals sucursal.IIDSUCURSAL
                            join tipobus in bd.TipoBus //2do Join TipoBus
                            on bus.IIDTIPOBUS equals tipobus.IIDTIPOBUS
                            join tipomodelo in bd.Modelo //3er Join que Modelo
                            on bus.IIDMODELO equals tipomodelo.IIDMODELO
                            where bus.BHABILITADO == 1
                            select new BusCLS
                            {
                                iidbus = bus.IIDBUS,
                                placa=bus.PLACA,
                                nombreModelo=tipomodelo.NOMBRE,
                                nombreSucursal = sucursal.NOMBRE,
                                nombreTipoBus=tipobus.NOMBRE
                            }).ToList();
            
            }
            return View(listaBus);
        }
    }
}