using examen_p1.Models;
using examen_p1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace examen_p1.Controllers
{
    public class PedidoController : Controller
    {
        // GET: Pedido
        public ActionResult RegistroPedido()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistroPedido(Pedido p)
        {
            PedidoService ps = new PedidoService();
            ps.guardarPedido(p);

            return View();
        }
    }
}