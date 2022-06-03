using examen_p1.Models;
using examen_p1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Web;
using System.Web.Mvc;

namespace examen_p1.Controllers
{
    public class PedidoController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult RegistroPedido()
        {
            
            ViewBag.pedidos = null;
            return View();
        }

        [HttpPost]
        public ActionResult RegistroPedido(Pedido p)
        {
            PedidoService ps = new PedidoService();
            ps.guardarPedido(p);

            ViewBag.pedidos = ps.getPedidosByCliente(p.Nombres);
            ViewBag.nombre_cliente = p.Nombres;

            return View("RegistroPedido");

            //return View();
        }


        public ActionResult TerminarPedido()
        {
            ViewBag.pedidos = null;

            ViewBag.nombre_cliente = null;
            ViewBag.fecha = null;

            return View();

        }

        [HttpPost]
        public ActionResult TerminarPedido(string nombre_cliente)
        {
            string nombre = Request.QueryString["nombre_cliente"].ToString();

            PedidoService ps = new PedidoService();

            ViewBag.pedidos = ps.getPedidosByCliente(nombre);

            ViewBag.nombre_cliente = nombre;
            ViewBag.fecha = DateTime.Now;

            return View("TerminarPedido");

        }

        public ActionResult Pagar(string nombre_cliente)
        {
            string nombre = Request.QueryString["nombre_cliente"].ToString();

            PedidoService ps = new PedidoService();
            List<string> pedidosPagados = new List<string>();

            pedidosPagados = ps.getPedidosByCliente(nombre);

            ps.actualizarPedido(pedidosPagados);

            return View("Index");
        }

        public ActionResult Ventas()
        {
            ViewBag.pedidos = null;
            return View();
        }

        [HttpPost]
        public ActionResult Ventas(Consulta c)
        {

            PedidoService ps = new PedidoService();

            ViewBag.pedidos = null;
            ViewBag.filtro = "";

            if (c.TipoBusqueda.Equals("Dias"))
            {
                ViewBag.pedidos = ps.getPedidosByDia(c.FiltroDia);
                ViewBag.filtro = "Dia: " + ps.getDiaSemana(c.FiltroDia);
            }
            else if (c.TipoBusqueda.Equals("Mes"))
            {
                ViewBag.pedidos = ps.getPedidosByMes(Convert.ToInt32(c.FiltroMes));
                ViewBag.filtro = "Mes: " + ps.getMesAnio(c.FiltroMes);
            }

            return View("Ventas");
        }




    }
}