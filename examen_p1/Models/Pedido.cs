using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace examen_p1.Models
{
    public class Pedido
    {
        public string Nombres { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public string Tamaño { get; set; }
        public string Jamon { get; set; }
        public string Piña { get; set; }
        public string Champiñones { get; set; }

        public string FechaPedido { get; set; }
        public int CantidadPizzas { get; set; }
        public double Subtotal { get; set; }
    }
}