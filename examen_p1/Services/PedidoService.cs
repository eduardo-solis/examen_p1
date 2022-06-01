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

            var c = new Cliente();
            var pi = new Pizza();

            c.Nombres = p.Nombres;
            c.Direccion = p.Direccion;
            c.Telefono = p.Telefono;

            pi.Tamaño = p.Tamaño;
            pi.Jamon = p.Jamon;
            pi.Piña = p.Piña;
            pi.Champiñones = p.Champiñones;

            p.Subtotal = calcularSubtotal(p);

            var datosPedido = c.Nombres + "," + c.Direccion + "," + c.Telefono + "," + pi.Tamaño + "," + pi.Jamon + "," + pi.Piña + "," + pi.Champiñones + ","+ p.FechaPedido + "," + p.CantidadPizzas + "," + p.Subtotal + Environment.NewLine;
            var datosCliente = c.Nombres + "," + c.Direccion + "," + c.Telefono + Environment.NewLine;
            var datosPizza = pi.Tamaño + "," + pi.Jamon + "," + pi.Piña + "," + pi.Champiñones + Environment.NewLine;


            var pedidos = HttpContext.Current.Server.MapPath("~/App_Data/pedidos.txt");
            var clientes = HttpContext.Current.Server.MapPath("~/App_Data/clientes.txt");
            var pizzas = HttpContext.Current.Server.MapPath("~/App_Data/pizzas.txt");

            File.AppendAllText(@pedidos, datosPedido);
            File.AppendAllText(@clientes, datosCliente);
            File.AppendAllText(@pizzas, datosPizza);

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

        public List<string> getPedidosByCliente(string cliente)
        {
            string[] datos = leerPedidos();

            List<string> lista_pedidos = new List<string>();

            foreach (string registro in datos)
            {
                foreach(string item in registro.Split(','))
                {
                    if (item.Equals(cliente))
                    {
                        foreach(string item2 in registro.Split(',')) 
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
                                    lista_pedidos.Add(registro);
                                    break;
                                }

                            }
                            catch (Exception ex)
                            {
                                continue;
                            }
                        }
                    }
                }
            }

            return lista_pedidos;
        }

        public List<string> getPedidosByDia(string dia)
        {
            List<string> lista_pedidos = new List<string>();

            string[] datos = leerPedidos();

            foreach (string registro in datos)
            {
                foreach(string item in registro.Split(','))
                {
                    try
                    {
                        // 2000-01-09
                        string year = "";
                        string month = "";
                        string day = "";

                        year = item.Substring(0, 4);
                        month = item.Substring(5, 7);
                        day = item.Substring(8);

                        DateTime dt = new DateTime(year: Convert.ToInt32(year), month: Convert.ToInt32(month), day: Convert.ToInt32(day));
                        string diaSemana = Convert.ToString(dt.DayOfWeek);
                        if (diaSemana.Equals(dia))
                        {
                            lista_pedidos.Add(registro);
                        }
                    }
                    catch(Exception ex)
                    {
                        continue;
                    }
                }
            }

            return lista_pedidos;
        }

        public List<string> getPedidosByMes(int mes)
        {
            List<string> lista_pedidos = new List<string>();

            string[] datos = leerPedidos();

            foreach (string registro in datos)
            {
                foreach (string item in registro.Split(','))
                {
                    try
                    {
                        // 2000-01-09
                        string year = "";
                        string month = "";
                        string day = "";

                        year = item.Substring(0, 4);
                        month = item.Substring(5, 7);
                        day = item.Substring(8);

                        DateTime dt = new DateTime(year: Convert.ToInt32(year), month: Convert.ToInt32(month), day: Convert.ToInt32(day));
                        int mesAnio = Convert.ToInt32(dt.Month);
                        if (mesAnio == mes)
                        {
                            lista_pedidos.Add(registro);
                        }
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
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

    }
}