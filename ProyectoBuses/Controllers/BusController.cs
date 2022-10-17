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
        public ActionResult Index(BusCLS oBusCls)
        {
            List<BusCLS> listaBus = null;
            List<BusCLS> listaFiltrado = null;
            ListarCombos();
            int idBus = oBusCls.iidbus;
            string placa = oBusCls.placa;
            int idModelo = oBusCls.iidModelo;
            int idsucursal = oBusCls.iisucursal;
            int idTipoBus = oBusCls.iidTipoBus;

            using (var bd = new BDPasajeEntities1())
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
                                placa = bus.PLACA,
                                nombreModelo = tipomodelo.NOMBRE,
                                nombreSucursal = sucursal.NOMBRE,
                                nombreTipoBus = tipobus.NOMBRE,
                                iidModelo=tipomodelo.IIDMODELO,
                                iisucursal=sucursal.IIDSUCURSAL,
                                iidTipoBus=tipobus.IIDTIPOBUS
                            }).ToList();
                if (idBus == 0 && placa == null && idModelo == 0 && idsucursal == 0 && idTipoBus == 0)
                    listaFiltrado = listaBus;
                else
                {
                    //filtro por bus
                    if (oBusCls.iidbus >=0)
                    {
                        listaBus = listaBus.Where(p => p.iidbus.ToString().Contains(oBusCls.iidbus.ToString())).ToList();
                    }
                    //filtro por placa
                    if (oBusCls.placa!=null)
                    {
                        listaBus = listaBus.Where(p => p.placa.Contains(oBusCls.placa)).ToList();
                    }
                    //filtro por Modelo
                    if (oBusCls.iidModelo != 0)
                    {
                        listaBus = listaBus.Where(p => p.iidModelo.ToString().Contains(oBusCls.iidModelo.ToString())).ToList();
                    }
                    //filtro por Sucursal
                    if (oBusCls.iisucursal != 0)
                    {
                        listaBus = listaBus.Where(p => p.iisucursal.ToString().Contains(oBusCls.iisucursal.ToString())).ToList();
                    }
                    //filtro por Bus
                    if (oBusCls.iidbus != 0)
                    {
                        listaBus = listaBus.Where(p => p.iidbus.ToString().Contains(oBusCls.iidbus.ToString())).ToList();
                    }
                    listaFiltrado = listaBus;
                }
            }
            return View(listaFiltrado);
        }


        //ComboBox Para nombre de Nombre Sucursal

        List<SelectListItem> listaSucursal;
        public void ListarComboSucursal()
        {
            //Agregar        

            using (var bd = new BDPasajeEntities1() )
            {
                listaSucursal = (from sucursal in bd.Sucursal
                         where sucursal.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = sucursal.NOMBRE,
                             Value = sucursal.IIDSUCURSAL.ToString()



                         }).ToList();
                listaSucursal.Insert(0, new SelectListItem { Text = "SELECCIONE", Value = "" });
                ViewBag.listaSucursal = listaSucursal;
            }
        }

        //ComboBox Para nombre de Nombre Modelos

        //Agregar
        List<SelectListItem> listaModelo;
        public void ListarComboModelo()
        {
           
            using (var bd = new BDPasajeEntities1())
            {
                listaModelo = (from modelo in bd.Modelo
                         where modelo.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = modelo.NOMBRE,
                             Value = modelo.IIDMODELO.ToString()



                         }).ToList();
                listaModelo.Insert(0, new SelectListItem { Text = "SELECCIONE", Value = "" });
                ViewBag.listaModelo = listaModelo;
            }
        }


        //ComboBox Para nombre de Nombre Marcas
        List<SelectListItem> listaMarca;
        public void ListarComboMarca()
        {
            //Agregar

            using (var bd = new BDPasajeEntities1())
            {
                listaMarca = (from marca in bd.Marca
                         where marca.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = marca.NOMBRE,
                             Value = marca.IIDMARCA.ToString()



                         }).ToList();
                listaMarca.Insert(0, new SelectListItem { Text = "SELECCIONE", Value = "" });
                ViewBag.listaMarca = listaMarca;
            }
        }

        //ComboBox Para nombre de Nombre TipoBus
        List<SelectListItem> listaTipoBus;
        public void ListarTipoBus()
        {
            //Agregar

            using (var bd = new BDPasajeEntities1())
            {
                listaTipoBus = (from tipoBus in bd.TipoBus
                         where tipoBus.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = tipoBus.NOMBRE,
                             Value = tipoBus.IIDTIPOBUS.ToString()



                         }).ToList();
                listaTipoBus.Insert(0, new SelectListItem { Text = "SELECCIONE", Value = "" });
                ViewBag.listaTipoBus = listaTipoBus;
            }
        }



        //Listando a tods los combos
        public void ListarCombos()
        {
            ListarComboSucursal();
            ListarComboModelo();
            ListarComboMarca();
            ListarTipoBus();

        }



        //Vista para poder agregar un BUS;
        public ActionResult Agregar()
        {
            ListarCombos();
            return View();
        }


        //Metodo Agregar
        [HttpPost]
        public ActionResult Agregar(BusCLS oBusCLS) 
        {
            int nregistrosEncontrados = 0;
            string placa = oBusCLS.placa;
            using(var bd=new BDPasajeEntities1())
            {
                nregistrosEncontrados = bd.Bus.Where(p => p.PLACA.Equals(placa)).Count();
            }
            if (!ModelState.IsValid || nregistrosEncontrados>=1)
            {
                if (nregistrosEncontrados >= 1) oBusCLS.mensajeError = "Ya existe el bus";
                ListarCombos();


                return View(oBusCLS);
            }
            else 
            {
                using (var bd = new BDPasajeEntities1())
                {
                    Bus oBus = new Bus();

                    oBus.IIDBUS = oBusCLS.iidbus;
                    oBus.IIDSUCURSAL = oBusCLS.iisucursal;
                    oBus.IIDTIPOBUS = oBusCLS.iidTipoBus;

                    oBus.PLACA = oBusCLS.placa;
                    oBus.FECHACOMPRA = oBusCLS.fechaCompra;
                    oBus.IIDMODELO = oBusCLS.iidModelo;
                    oBus.NUMEROFILAS = oBusCLS.numeroFilas;
                    oBus.NUMEROCOLUMNAS = oBusCLS.numeroColumnas;
                    oBus.BHABILITADO = 1;
                    oBus.DESCRIPCION = oBusCLS.descripcion;
                    oBus.OBSERVACION = oBusCLS.observacion;
                    oBus.IIDMARCA = oBusCLS.iidmarca;

                    bd.Bus.Add(oBus);
                    bd.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }

        
        public ActionResult Editar(int id) 
        {
            //Recuperando el combo
            ListarCombos();

            //Creando la clase BusCLS
            BusCLS oBusCLS = new BusCLS();
            //Abriendo la conexion con la base de datos;
            using (var bd = new BDPasajeEntities1() )
            {
                //Recuperando el objeto y trayendo solo la primera fila es por ello que usamos el metodo.first();
                Bus oBus = bd.Bus.Where(p => p.IIDBUS.Equals(id)).First();

                //Recuperando los parametros para pasarlo al  modelo
                oBusCLS.iidbus= oBus.IIDBUS;
                oBusCLS.iisucursal = (int)oBus.IIDSUCURSAL;
                oBusCLS.iidTipoBus= (int)oBus.IIDTIPOBUS ;

                oBusCLS.placa= oBus.PLACA ;
                oBusCLS.fechaCompra= (DateTime)oBus.FECHACOMPRA;
                oBusCLS.iidModelo = (int)oBus.IIDMODELO;
                oBusCLS.numeroFilas= (int)oBus.NUMEROFILAS;
                oBusCLS.numeroColumnas = (int)oBus.NUMEROCOLUMNAS;
                oBus.BHABILITADO = 1;
                oBusCLS.descripcion= oBus.DESCRIPCION;
                oBusCLS.observacion= oBus.OBSERVACION;
                oBusCLS.iidmarca= (int)oBus.IIDMARCA;
                      
            }
           return View(oBusCLS);
        }

        [HttpPost]
        public ActionResult Editar(BusCLS oBusCLS) 
        {
            int idBus = oBusCLS.iidbus;
            int nregistrosEncontrados = 0;
            string placa = oBusCLS.placa;
            using (var bd = new BDPasajeEntities1())
            {
                nregistrosEncontrados = bd.Bus.Where(p => p.PLACA.Equals(placa)
                && !p.IIDBUS.Equals(idBus)).Count();
            }
            if (!ModelState.IsValid || nregistrosEncontrados>=1)
            {
                if (nregistrosEncontrados >= 1) oBusCLS.mensajeError = "El bus ya existe";
                ListarCombos();
                return View(oBusCLS);
            }

            using (var bd = new BDPasajeEntities1() ) 
            {
                Bus oBus = bd.Bus.Where(p => p.IIDBUS.Equals(idBus)).First();            
                oBus.IIDSUCURSAL = oBusCLS.iisucursal;
                oBus.IIDTIPOBUS = oBusCLS.iidTipoBus;
                oBus.PLACA = oBusCLS.placa;
                oBus.FECHACOMPRA = oBusCLS.fechaCompra;
                oBus.IIDMODELO = oBusCLS.iidModelo;
                oBus.NUMEROFILAS = oBusCLS.numeroFilas;
                oBus.NUMEROCOLUMNAS = oBusCLS.numeroColumnas;
                oBus.DESCRIPCION = oBusCLS.descripcion;
                oBus.OBSERVACION = oBusCLS.observacion;
                oBus.IIDMARCA = oBusCLS.iidmarca;
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Eliminar(int iidbus)
        {
            using(var bd=new BDPasajeEntities1())
            {
                Bus oBus = bd.Bus.Where(p => p.IIDBUS.Equals(iidbus)).First();
                oBus.BHABILITADO = 0;
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}