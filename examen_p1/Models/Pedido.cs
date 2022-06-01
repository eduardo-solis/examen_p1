using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace examen_p1.Models
{
    public class Pedido
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Dia { get; set; }
        public string Mes { get; set; }
        public string Anio { get; set; }

        public string Tamaño { get; set; }
        public int Ingredientes { get; set; }

        public double Subtotal { get; set; }
    }
}