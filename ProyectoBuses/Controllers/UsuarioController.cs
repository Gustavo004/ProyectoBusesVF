using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoBuses.Models;
using System.Transactions;
using System.Security.Cryptography;
using System.Text;

namespace ProyectoBuses.Controllers
{
    public class UsuarioController : Controller
    {
        public void listaPersonas()
        {
            List<SelectListItem> listaPersonas = new List<SelectListItem>();
            using (var bd = new BDPasajeEntities1())
            {
                List<SelectListItem> listaCliente = (from item in bd.Cliente
                                                     where item.BHABILITADO == 1
                                                     && item.bTieneUsuario != 1
                                                     select new SelectListItem
                                                     {
                                                         Text = item.NOMBRE + " " + item.APPATERNO + " " + item.APMATERNO+ "(C)",
                                                         Value = item.IIDCLIENTE.ToString()
                                                     }).ToList();
                List<SelectListItem> listaEmpleado = (from item in bd.Empleado
                                                     where item.BHABILITADO == 1
                                                     && item.bTieneUsuario != 1
                                                     select new SelectListItem
                                                     {
                                                         Text = item.NOMBRE + " " + item.APPATERNO + " " + item.APMATERNO+ "(E)",
                                                         Value = item.IIDEMPLEADO.ToString()
                                                     }).ToList();
                listaPersonas.AddRange(listaCliente);
                listaPersonas.AddRange(listaEmpleado);
                listaPersonas = listaPersonas.OrderBy(p => p.Text).ToList();
                listaPersonas.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaPersonas = listaPersonas;
            }
        }
        public void listaRol()
        {
            List<SelectListItem> listaRol;
            using (var bd = new BDPasajeEntities1())
            {
                listaRol = (from item in bd.Rol
                                                 where item.BHABILITADO == 1
                                                 select new SelectListItem
                                                 {
                                                     Text = item.NOMBRE,
                                                     Value = item.IIDROL.ToString()
                                                 }).ToList();
                listaRol.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
            }
            ViewBag.listaRol = listaRol;
        }
        public ActionResult Filtrar(UsuarioCLS oUsuarioCLS)
        {
            string nombrePersona = oUsuarioCLS.nombrePersona;
            listaPersonas();
            listaRol();
            List<UsuarioCLS> listaUsuario = new List<UsuarioCLS>();
            using (var bd = new BDPasajeEntities1())
            {
                if (oUsuarioCLS.nombrePersona == null)
                {
                    List<UsuarioCLS> listaUsuarioCliente = (from usuario in bd.Usuario
                                                            join cliente in bd.Cliente
                                                            on usuario.IID equals
                                                            cliente.IIDCLIENTE
                                                            join rol in bd.Rol
                                                            on usuario.IIDROL equals rol.IIDROL
                                                            where usuario.bhabilitado == 1
                                                            && usuario.TIPOUSUARIO == "C"
                                                            select new UsuarioCLS
                                                            {
                                                                iidusuario = usuario.IIDUSUARIO,
                                                                nombrePersona = cliente.NOMBRE + " " + cliente.APPATERNO + " " + cliente.APMATERNO,
                                                                nombreusuario = usuario.NOMBREUSUARIO,
                                                                nombreRol = rol.NOMBRE,
                                                                nombreTipoEmpleado = "Cliente"
                                                            }).ToList();
                    List<UsuarioCLS> listaUsuarioEmpleado = (from usuario in bd.Usuario
                                                             join empleado in bd.Empleado
                                                             on usuario.IID equals
                                                             empleado.IIDEMPLEADO
                                                             join rol in bd.Rol
                                                             on usuario.IIDROL equals rol.IIDROL
                                                             where usuario.bhabilitado == 1
                                                             && usuario.TIPOUSUARIO == "E"
                                                             select new UsuarioCLS
                                                             {
                                                                 iidusuario = usuario.IIDUSUARIO,
                                                                 nombrePersona = empleado.NOMBRE + " " + empleado.APPATERNO + " " + empleado.APMATERNO,
                                                                 nombreusuario = usuario.NOMBREUSUARIO,
                                                                 nombreRol = rol.NOMBRE,
                                                                 nombreTipoEmpleado = "Empleado"
                                                             }).ToList();
                    listaUsuario.AddRange(listaUsuarioCliente);
                    listaUsuario.AddRange(listaUsuarioEmpleado);
                    listaUsuario = listaUsuario.OrderBy(p => p.iidusuario).ToList();
                }
                else
                {
                    List<UsuarioCLS> listaUsuarioCliente = (from usuario in bd.Usuario
                                                            join cliente in bd.Cliente
                                                            on usuario.IID equals
                                                            cliente.IIDCLIENTE
                                                            join rol in bd.Rol
                                                            on usuario.IIDROL equals rol.IIDROL
                                                            where usuario.bhabilitado == 1 
                                                            &&(
                                                            cliente.NOMBRE.Contains(nombrePersona)
                                                            || cliente.APPATERNO.Contains(nombrePersona)
                                                            || cliente.APMATERNO.Contains(nombrePersona)
                                                            )
                                                            && usuario.TIPOUSUARIO == "C"
                                                            select new UsuarioCLS
                                                            {
                                                                iidusuario = usuario.IIDUSUARIO,
                                                                nombrePersona = cliente.NOMBRE + " " + cliente.APPATERNO + " " + cliente.APMATERNO,
                                                                nombreusuario = usuario.NOMBREUSUARIO,
                                                                nombreRol = rol.NOMBRE,
                                                                nombreTipoEmpleado = "Cliente"
                                                            }).ToList();
                    List<UsuarioCLS> listaUsuarioEmpleado = (from usuario in bd.Usuario
                                                             join empleado in bd.Empleado
                                                             on usuario.IID equals
                                                             empleado.IIDEMPLEADO
                                                             join rol in bd.Rol
                                                             on usuario.IIDROL equals rol.IIDROL
                                                             where usuario.bhabilitado == 1
                                                             && (
                                                            empleado.NOMBRE.Contains(nombrePersona)
                                                            || empleado.APPATERNO.Contains(nombrePersona)
                                                            || empleado.APMATERNO.Contains(nombrePersona)
                                                            )
                                                             && usuario.TIPOUSUARIO == "E"
                                                             select new UsuarioCLS
                                                             {
                                                                 iidusuario = usuario.IIDUSUARIO,
                                                                 nombrePersona = empleado.NOMBRE + " " + empleado.APPATERNO + " " + empleado.APMATERNO,
                                                                 nombreusuario = usuario.NOMBREUSUARIO,
                                                                 nombreRol = rol.NOMBRE,
                                                                 nombreTipoEmpleado = "Empleado"
                                                             }).ToList();
                    listaUsuario.AddRange(listaUsuarioCliente);
                    listaUsuario.AddRange(listaUsuarioEmpleado);
                    listaUsuario = listaUsuario.OrderBy(p => p.iidusuario).ToList();
                }
            }
            return PartialView("_TablaUsuario",listaUsuario);
        }
        // GET: Usuario
        public ActionResult Index()
        {
            listaPersonas();
            listaRol();
            List<UsuarioCLS> listaUsuario=new List<UsuarioCLS>();
            using(var bd=new BDPasajeEntities1())
            {
                List<UsuarioCLS> listaUsuarioCliente = (from usuario in bd.Usuario
                                                        join cliente in bd.Cliente
                                                        on usuario.IID equals
                                                        cliente.IIDCLIENTE
                                                        join rol in bd.Rol
                                                        on usuario.IIDROL equals rol.IIDROL
                                                        where usuario.bhabilitado == 1
                                                        && usuario.TIPOUSUARIO == "C"
                                                        select new UsuarioCLS
                                                        {
                                                            iidusuario = usuario.IIDUSUARIO,
                                                            nombrePersona = cliente.NOMBRE + " " + cliente.APPATERNO+" "+cliente.APMATERNO,
                                                            nombreusuario=usuario.NOMBREUSUARIO,
                                                            nombreRol=rol.NOMBRE,
                                                            nombreTipoEmpleado="Cliente"
                                                        }).ToList();
                List<UsuarioCLS> listaUsuarioEmpleado = (from usuario in bd.Usuario
                                                        join empleado in bd.Empleado
                                                        on usuario.IID equals
                                                        empleado.IIDEMPLEADO
                                                        join rol in bd.Rol
                                                        on usuario.IIDROL equals rol.IIDROL
                                                        where usuario.bhabilitado == 1
                                                        && usuario.TIPOUSUARIO == "E"
                                                        select new UsuarioCLS
                                                        {
                                                            iidusuario = usuario.IIDUSUARIO,
                                                            nombrePersona = empleado.NOMBRE + " " + empleado.APPATERNO + " " + empleado.APMATERNO,
                                                            nombreusuario = usuario.NOMBREUSUARIO,
                                                            nombreRol = rol.NOMBRE,
                                                            nombreTipoEmpleado = "Empleado"
                                                        }).ToList();
                listaUsuario.AddRange(listaUsuarioCliente);
                listaUsuario.AddRange(listaUsuarioEmpleado);
                listaUsuario = listaUsuario.OrderBy(p => p.iidusuario).ToList();
            }
            return View(listaUsuario);
        }
        public int Guardar(UsuarioCLS oUsuarioCLS, int titulo)
        {
            int rpta=0;
            try
            {
                using (var bd = new BDPasajeEntities1())
                {
                    using(var transaccion=new TransactionScope())
                    {
                        if (titulo == 1)
                        {
                            Usuario oUsuario = new Usuario();
                            oUsuario.NOMBREUSUARIO = oUsuarioCLS.nombreusuario;
                            SHA256Managed sha = new SHA256Managed();
                            byte[] byteContra = Encoding.Default.GetBytes(oUsuarioCLS.contra);
                            byte[] byteContraCifrado = sha.ComputeHash(byteContra);
                            string cadenaContraCifrado=BitConverter.ToString(byteContraCifrado).Replace("-", "");
                            oUsuario.CONTRA = cadenaContraCifrado;
                            oUsuario.TIPOUSUARIO = oUsuarioCLS.nombrePersona.Substring(oUsuarioCLS.nombrePersona.Length - 2,1);
                            oUsuario.IID = oUsuarioCLS.iid;
                            oUsuario.IIDROL = oUsuarioCLS.iidrol;
                            oUsuario.bhabilitado = 1;
                            bd.Usuario.Add(oUsuario);
                            if (oUsuario.TIPOUSUARIO.Equals("C"))
                            {
                                Cliente oCliente = bd.Cliente.Where(p => p.IIDCLIENTE.Equals(oUsuarioCLS.iid)).First();
                                oCliente.bTieneUsuario = 1;
                            }
                            else
                            {
                                Empleado oEmpleado = bd.Empleado.Where(p => p.IIDEMPLEADO.Equals(oUsuarioCLS.iid)).First();
                                oEmpleado.bTieneUsuario = 1;
                            }
                            rpta=bd.SaveChanges();
                            transaccion.Complete();
                        }

                    }
                }
            }
            catch(Exception ex)
            {
                rpta = 0;
                Console.WriteLine(ex.Message);
            }
            return rpta;
        }
    }
}