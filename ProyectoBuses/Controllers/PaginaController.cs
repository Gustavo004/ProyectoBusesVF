﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoBuses.Models;
namespace ProyectoBuses.Controllers
{
    public class PaginaController : Controller
    {
        // GET: Pagina
        public ActionResult Index()
        {
            List<PaginaCLS> listaPagina = new List<PaginaCLS>();
            using (var bd = new BDPasajeEntities1())
            {
                listaPagina = (from pagina in bd.Pagina
                               where pagina.BHABILITADO == 1
                               select new PaginaCLS
                               {
                                   iidpagina = pagina.IIDPAGINA,
                                   mensaje = pagina.MENSAJE,
                                   controlador = pagina.CONTROLADOR,
                                   accion = pagina.ACCION
                               }).ToList();
            }
                return View(listaPagina);
        }
        public ActionResult Filtrar(Pagina oPaginaCLS)
        {
            string mensaje = oPaginaCLS.MENSAJE;
            List<PaginaCLS> listaPagina = new List<PaginaCLS>();
            using (var bd=new BDPasajeEntities1())
            {
                if (mensaje == null)
                {
                    listaPagina = (from pagina in bd.Pagina
                                   where pagina.BHABILITADO == 1
                                   select new PaginaCLS
                                   {
                                       iidpagina = pagina.IIDPAGINA,
                                       mensaje = pagina.MENSAJE,
                                       controlador = pagina.CONTROLADOR,
                                       accion = pagina.ACCION
                                   }).ToList();
                }
                else
                {
                    listaPagina = (from pagina in bd.Pagina
                                   where pagina.BHABILITADO == 1 && pagina.MENSAJE.Contains(mensaje)
                                   select new PaginaCLS
                                   {
                                       iidpagina = pagina.IIDPAGINA,
                                       mensaje = pagina.MENSAJE,
                                       controlador = pagina.CONTROLADOR,
                                       accion = pagina.ACCION
                                   }).ToList();
                }
            }
            return PartialView("_TablaPagina",listaPagina);
        }
        public int Guardar(Pagina oPaginaCLS,int titulo)
        {
            int rpta = 0;
            using(var bd=new BDPasajeEntities1())
            {
                if (titulo == 1)
                {
                    Pagina oPagina = new Pagina();
                    oPagina.MENSAJE = oPaginaCLS.MENSAJE;
                    oPagina.ACCION = oPaginaCLS.ACCION;
                    oPagina.CONTROLADOR = oPaginaCLS.CONTROLADOR;
                    oPagina.BHABILITADO = 1;
                    bd.Pagina.Add(oPagina);
                    rpta=bd.SaveChanges();

                }
            }
            return rpta;

        }
    }
}