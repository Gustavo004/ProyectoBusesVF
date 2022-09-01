using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoBuses.Models;

namespace ProyectoBuses.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {

            //Creando la lista;
            List<ClienteCLS> listaClientes = null;

            //Abriendo conexion bd
            using (var bd = new BDPasajeEntities1())
            {
                listaClientes = (from clientes in bd.Cliente
                                 where clientes.BHABILITADO == 1

                                 select new ClienteCLS
                                 {
                                     idcliente = clientes.IIDCLIENTE,
                                     nombre = clientes.NOMBRE,
                                     appaterno = clientes.APPATERNO,
                                     apmaterno = clientes.APMATERNO,
                                     email = clientes.EMAIL,
                                     direccion = clientes.DIRECCION,
                                     telefonoCelular = clientes.TELEFONOCELULAR
                                 }).ToList();
            }
         return View(listaClientes);
        }

        //Recuperando los valores del Sexo
        List<SelectListItem> listaSexo;
        private void LlenarSexo() 
        {
            using (var bd = new BDPasajeEntities1() ) 
            {
                listaSexo = (from Sexo in bd.Sexo
                             where Sexo.BHABILITADO == 1
                             select new SelectListItem
                             {
                                 Text = Sexo.NOMBRE,
                                 Value = Sexo.IIDSEXO.ToString()
                             }).ToList();

                listaSexo.Insert(0, new SelectListItem { Text = "SELECCIONE", Value = ""});
            }
        }


        public ActionResult Agregar() 
        {
            LlenarSexo();
            ViewBag.lista = listaSexo;


            return View();
        }


        [HttpPost]
        public ActionResult Agregar(ClienteCLS oClienteCLS) 
        {
            if (!ModelState.IsValid)
            {
                LlenarSexo();
                ViewBag.lista = listaSexo;

                return View(oClienteCLS);
            }
            else
            {

                using (var bd = new BDPasajeEntities1())
                {
                    Cliente oCliente = new Cliente();

                    oCliente.NOMBRE = oClienteCLS.nombre;
                    oCliente.APPATERNO = oClienteCLS.appaterno;
                    oCliente.APMATERNO = oClienteCLS.apmaterno;
                    oCliente.EMAIL = oClienteCLS.email;
                    oCliente.DIRECCION = oClienteCLS.direccion;
                    oCliente.IIDSEXO = oClienteCLS.idsexo;
                    oCliente.TELEFONOFIJO = oClienteCLS.telefonoFijo;
                    oCliente.TELEFONOCELULAR = oClienteCLS.telefonoCelular;
                    oCliente.BHABILITADO = 1;

                    bd.Cliente.Add(oCliente);

                    bd.SaveChanges();
                }
             return RedirectToAction("Index");
            }
        }












    }
}