using examen_p1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace examen_p1.Services
{
    public class PedidoService
    {

        public void guardarPedido (Pedido p)
        {

            p.Subtotal = calcularSubtotal(p);

            var datosPedido = p.Nombres + "," + p.Direccion + "," + p.Telefono + "," + p.Tamaño + "," + p.Jamon + "," + p.Piña + "," + p.Champiñones + ","+ p.FechaPedido + "," + p.CantidadPizzas + "," + p.Subtotal + Environment.NewLine;
            
            var pedidos = HttpContext.Current.Server.MapPath("~/App_Data/pedidos.txt");

            File.AppendAllText(@pedidos, datosPedido);

        }

        public void guardarPedido(List<string> pedidos)
        {

            var archivo = HttpContext.Current.Server.MapPath("~/App_Data/pedidos.txt");

            File.Delete(archivo);

            string datosPedido = "";

            foreach(string pedido in pedidos)
            {
                datosPedido += pedido + Environment.NewLine;
            }

            var datos = HttpContext.Current.Server.MapPath("~/App_Data/pedidos.txt");

            File.AppendAllText(@datos, datosPedido);

        }

        public void guardarVenta(List<string> pedidos)
        {

            string datosPedido = "";

            foreach (string pedido in pedidos)
            {
                datosPedido += pedido + Environment.NewLine;
            }

            var datos = HttpContext.Current.Server.MapPath("~/App_Data/ventas.txt");

            File.AppendAllText(@datos, datosPedido);

        }

        public string[] leerPedidos()
        {
            
            string dataFile = HttpContext.Current.Server.MapPath("~/App_Data/pedidos.txt");
            string[] datosPedidos = null;

            if (File.Exists(dataFile))
            {
                datosPedidos = File.ReadAllLines(dataFile);
            }

            return datosPedidos;
        }

        public string[] leerVentas()
        {
            string dataFile = HttpContext.Current.Server.MapPath("~/App_Data/ventas.txt");
            string[] datosPedidos = null;

            if (File.Exists(dataFile))
            {
                datosPedidos = File.ReadAllLines(dataFile);
            }

            return datosPedidos;
        }

        public void actualizarPedido(List<string> pedidos)
        {
            string[] oldData = leerPedidos();
            List<string> d = new List<string>();
            List<string> dv = new List<string>();

            for (int i = 0; i < oldData.Length; i++)
            {
                
                for(int j = 0; j < pedidos.Count; j++)
                {

                    if (!oldData[i].Equals(pedidos[j]) && d.Contains(oldData[i]))
                    {
                        d.Add(oldData[i]);
                    }
                    else if (!dv.Contains(pedidos[i]))
                    {
                        dv.Add(pedidos[i]);
                    }

                }
                
            }

            guardarPedido(d);
            guardarVenta(dv);
        }

        public List<string> getPedidosByCliente(string cliente)
        {
            string[] datos = leerPedidos();

            List<string> lista_pedidos = new List<string>();

            foreach (string registro in datos)
            {
                string[] x = registro.Split(',');

                if(x[0].Equals(cliente))
                {
                    lista_pedidos.Add(registro);
                    //DateTime localDate = DateTime.Now;

                    //string year = "";
                    //string month = "";
                    //string day = "";

                    //string fecha = x[7];

                    //year = fecha.Substring(0, 4);
                    //month = fecha.Substring(5, 2);
                    //day = fecha.Substring(8);

                    //DateTime dt = new DateTime(year: Convert.ToInt32(year), month: Convert.ToInt32(month), day: Convert.ToInt32(day));

                    //if (dt.Day == localDate.Day && dt.Month == localDate.Month && dt.Year == localDate.Year)
                    //{
                    //    lista_pedidos.Add(registro);

                    //}
                }

                
            }

            return lista_pedidos;
        }

        public List<string> getPedidosByDia(string dia)
        {
            List<string> lista_pedidos = new List<string>();

            string[] datos = leerVentas();

            foreach (string registro in datos)
            {

                string[] x = registro.Split(',');

                DateTime localDate = DateTime.Now;

                string year = "";
                string month = "";
                string day = "";

                string fecha = x[7];

                year = fecha.Substring(0, 4);
                month = fecha.Substring(5, 2);
                day = fecha.Substring(8);

                DateTime dt = new DateTime(year: Convert.ToInt32(year), month: Convert.ToInt32(month), day: Convert.ToInt32(day));

                string diaSemana = Convert.ToString(dt.DayOfWeek);
                if (diaSemana.Equals(dia))
                {
                    lista_pedidos.Add(registro);
                }

            }

            return lista_pedidos;
        }

        public List<string> getPedidosByMes(int mes)
        {
            List<string> lista_pedidos = new List<string>();

            string[] datos = leerVentas();

            foreach (string registro in datos)
            {

                string[] x = registro.Split(',');

                DateTime localDate = DateTime.Now;

                string year = "";
                string month = "";
                string day = "";

                string fecha = x[7];

                year = fecha.Substring(0, 4);
                month = fecha.Substring(5, 2);
                day = fecha.Substring(8);

                DateTime dt = new DateTime(year: Convert.ToInt32(year), month: Convert.ToInt32(month), day: Convert.ToInt32(day));

                int mesAnio = Convert.ToInt32(dt.Month);
                if (mesAnio == mes)
                {
                    lista_pedidos.Add(registro);
                }

            }

            return lista_pedidos;
        }

        public double calcularSubtotal(Pedido p)
        {

            double subtotal = 0;
            double total;

            switch (p.Tamaño)
            {
                case "Chica":
                    subtotal = 40;
                    break;
                case "Mediana":
                    subtotal = 80;
                    break;
                case "Grande":
                    subtotal = 120;
                    break;
                default:
                    subtotal = 40;
                    break;
            }

            if (p.Jamon == "Jamon")
            {
                subtotal += 10;
            }
            if (p.Piña == "Piña")
            {
                subtotal += 10;
            }
            if (p.Champiñones == "Champiñones")
            {
                subtotal += 10;
            }

            total = subtotal * p.CantidadPizzas;

            return total;

        }

        public string getUltimoCliente()
        {
            string cliente = "";

            string[] datos = leerPedidos();

            cliente = datos[datos.Length - 1];

            string nombre = "";

            foreach (string item in cliente.Split(','))
            {

                try
                {
                    DateTime localDate = DateTime.Now;

                    string year = "";
                    string month = "";
                    string day = "";

                    year = item.Substring(0, 4);
                    month = item.Substring(5, 7);
                    day = item.Substring(8);

                    DateTime dt = new DateTime(year: Convert.ToInt32(year), month: Convert.ToInt32(month), day: Convert.ToInt32(day));

                    if (dt.Day == localDate.Day && dt.Month == localDate.Month && dt.Year == localDate.Year)
                    {
                        string[] datosCliente = cliente.Split(',');
                        nombre = datosCliente[0];
                        break;
                    }

                }
                catch (Exception ex)
                {
                    continue;
                }

            }

            return nombre;
        }

        public string getDiaSemana(string day)
        {
            string dia = "";

            switch (day)
            {
                case "Moday":
                    dia = "Lunes";
                    break;
                case "Tuesday":
                    dia = "Martes";
                    break;
                case "Wednesday":
                    dia = "Miercoles";
                    break;
                case "Thursday":
                    dia = "Jueves";
                    break;
                case "Friday":
                    dia = "Viernes";
                    break;
                case "Saturday":
                    dia = "Sabado";
                    break;
                case "Sunday":
                    dia = "Domingo";
                    break;
            }

            return dia;
        }

        public string getMesAnio(string mounth)
        {
            string mes = "";

            switch (mounth)
            {
                case "01":
                    mes = "Enero";
                    break;
                case "02":
                    mes = "Febrero";
                    break;
                case "03":
                    mes = "Marzo";
                    break;
                case "04":
                    mes = "Abril";
                    break;
                case "05":
                    mes = "Mayo";
                    break;
                case "06":
                    mes = "Junio";
                    break;
                case "07":
                    mes = "Julio";
                    break;
                case "08":
                    mes = "Agosto";
                    break;
                case "09":
                    mes = "Septiembre";
                    break;
                case "10":
                    mes = "Octubre";
                    break;
                case "11":
                    mes = "Noviembre";
                    break;
                case "12":
                    mes = "Diciembre";
                    break;
            }

            return mes;
        }


    }
}