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
            pi.Ingredientes = p.Ingredientes;

            var datosPedido = c.Nombres + "," + c.Direccion + "," + c.Telefono + "," + pi.Tamaño + "," + pi.Ingredientes + ","+ p.FechaPedido + "," + p.Subtotal + Environment.NewLine;
            var datosCliente = c.Nombres + "," + c.Direccion + "," + c.Telefono + "," + Environment.NewLine;
            var datosPizza = pi.Tamaño + "," + pi.Ingredientes + "," + Environment.NewLine;


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

        public string[] getPedidosByCliente(string cliente)
        {
            string[] pedidosCliente = null;

            string[] datos = leerPedidos();

            List<string> lista_pedidos = new List<string>();

            foreach (string registro in datos)
            {
                foreach(string item in registro.Split(','))
                {
                    if (item.Equals(cliente))
                    {
                        lista_pedidos.Add(registro);
                    }
                }
            }

            pedidosCliente = lista_pedidos.ToArray();

            return pedidosCliente;
        }

        public string[] getPedidosByDia(string dia)
        {
            string[] pedidosDia = null;

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

            pedidosDia = lista_pedidos.ToArray();

            return pedidosDia;
        }

        public string[] getPedidosByMes(int mes)
        {
            string[] pedidosMes = null;

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

            pedidosMes = lista_pedidos.ToArray();

            return pedidosMes;
        }

    }
}