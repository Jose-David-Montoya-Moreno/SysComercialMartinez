﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SysComercialMartinez.EntidadesDeNegocio;
using SysComercialMartinez.LogicaDeNegocio;
using SysComercialMartinez.UI.AppWebAspNetCore.Models;

namespace SysComercialMartinez.UI.AppWebAspNetCore.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Authorize(Roles = "Administrador,Cajero")]
    public class DetalleVentaController : Controller
    {
        DetalleVentaBL DetalleVentaBL = new DetalleVentaBL();
        ProductoBL ProductoBL = new ProductoBL();
        VentaBL VentaBL = new VentaBL();
        public static int idVen;
        // GET: DetallePedidoController
        public async Task<IActionResult> Index(DetalleVenta pDetalleVenta = null)
        {
            if (pDetalleVenta == null)
                pDetalleVenta = new DetalleVenta();
            if (pDetalleVenta.Top_Aux == 0)
                pDetalleVenta.Top_Aux = 10;
            else if (pDetalleVenta.Top_Aux == -1)
                pDetalleVenta.Top_Aux = 0;
            var taskBuscar = DetalleVentaBL.BuscarIncluirVentaProductoAsync(pDetalleVenta);
            var taskObtenerTodosVentas = VentaBL.ObtenerTodosAsync();
            var taskObtenerTodosProducto = ProductoBL.ObtenerTodosAsync();
            var DetalleVenta = await taskBuscar;
            ViewBag.Top = pDetalleVenta.Top_Aux;
            ViewBag.Venta = await taskObtenerTodosVentas;
            ViewBag.Producto = await taskObtenerTodosProducto;
            return View(DetalleVenta);
        }

        // GET: DetallePedidoController/Details/5
        public async Task<IActionResult> Details(int IdDetalleVenta)
        {
            var DetalleVenta = await DetalleVentaBL.ObtenerPorIdAsync(new DetalleVenta{ IdDetalleVenta = IdDetalleVenta });
            DetalleVenta.Venta = await VentaBL.ObtenerPorIdAsync(new Venta { IdVenta = DetalleVenta.IdVenta});
            DetalleVenta.Producto = await ProductoBL.ObtenerPorIdProductoAsync(new Producto { IdProducto = DetalleVenta.IdProducto });

            return View(DetalleVenta);
        }

        // GET: DetallePedidoController/Create

        public async Task<IActionResult> Create()
        {
            ViewBag.Venta = await VentaBL.ObtenerTodosAsync();
            ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

        // POST: DetallePedidoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DetalleVenta pDetalleVenta)
        {
            try
            {
                int result = await DetalleVentaBL.CrearAsync(pDetalleVenta);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Venta = await VentaBL.ObtenerTodosAsync();
                ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
                return View(pDetalleVenta);
            }
        }

        // GET: DetallePedidoController/Edit/5
        public async Task<IActionResult> Edit(DetalleVenta pDetalleVenta)
        {
            var taskObtenerPorId = DetalleVentaBL.ObtenerPorIdAsync(pDetalleVenta);
            var taskObtenerTodosVentas = VentaBL.ObtenerTodosAsync();
            var taskObtenerTodosProducto = ProductoBL.ObtenerTodosAsync();
            var DetalleVenta = await taskObtenerPorId;
            ViewBag.Venta = await taskObtenerTodosVentas;
            ViewBag.Producto = await taskObtenerTodosProducto;
            ViewBag.Error = "";
            return View(DetalleVenta);
        }

        // POST: DetallePedidoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdDetalleVenta, DetalleVenta pDetalleVenta)
        {
            try
            {
                int result = await DetalleVentaBL.ModificarAsync(pDetalleVenta);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Venta = await VentaBL.ObtenerTodosAsync();
                ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
                return View(pDetalleVenta);
            }
        }

        // GET: DetallePedidoController/Delete/5
        public async Task<IActionResult> Delete(DetalleVenta pDetalleVenta)
        {
            var DetalleVenta = await DetalleVentaBL.ObtenerPorIdAsync(pDetalleVenta);
            DetalleVenta.Venta = await VentaBL.ObtenerPorIdAsync(new Venta { IdVenta = DetalleVenta.IdVenta });
            DetalleVenta.Producto = await ProductoBL.ObtenerPorIdProductoAsync(new Producto { IdProducto = DetalleVenta.IdProducto });
            ViewBag.Error = "";

            return View(DetalleVenta);
        }

        // POST: DetallePedidoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int IdDetalleVenta, DetalleVenta pDetalleVenta)
        {
            try
            {
                int result = await DetalleVentaBL.EliminarAsync(pDetalleVenta);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var DetalleVenta = await DetalleVentaBL.ObtenerPorIdAsync(pDetalleVenta);
                if (DetalleVenta == null)
                    DetalleVenta = new DetalleVenta();
                if (DetalleVenta.IdDetalleVenta> 0)
                    DetalleVenta.Venta = await VentaBL.ObtenerPorIdAsync(new Venta { IdVenta = DetalleVenta.IdVenta });
                if (DetalleVenta.IdDetalleVenta > 0)
                    DetalleVenta.Producto = await ProductoBL.ObtenerPorIdProductoAsync(new Producto { IdProducto = DetalleVenta.IdProducto });
                return View(DetalleVenta);
            }
        }


        List<DetalleVenta>? DetalleVentas;
        public async Task<IActionResult> Venta(int campo, DetalleVenta pDetalleVenta = null)
        {
            if (pDetalleVenta == null)
                pDetalleVenta = new DetalleVenta();
            if (pDetalleVenta.Top_Aux == 0)
                pDetalleVenta.Top_Aux = 10;
            else if (pDetalleVenta.Top_Aux == -1)
                pDetalleVenta.Top_Aux = 0;
            var taskBuscar = DetalleVentaBL.BuscarIncluirVentaProductoAsync(pDetalleVenta);
            var taskObtenerTodosVentas = VentaBL.ObtenerTodosAsync();
            var taskObtenerTodosProducto = ProductoBL.ObtenerTodosAsync();
            DetalleVentas = await taskBuscar;
            ViewBag.Top = pDetalleVenta.Top_Aux;
            ViewBag.Ventas = await taskObtenerTodosVentas;

            if (campo > 0)
            {
                Producto producto = new Producto();
                producto.Codigo = campo;
                List<Producto> ProductoBuscado = await ProductoBL.BuscarAsync(producto);
                ViewBag.Producto = ProductoBuscado;
            }
            else
            {
                ViewBag.Producto = await taskObtenerTodosProducto;
            }
            return View(DetalleVentas);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetalleVenta(DetalleVenta pDetalleVenta)
        {
            try
            {
                int result = await DetalleVentaBL.CrearAsync(pDetalleVenta);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Ventas = await VentaBL.ObtenerTodosAsync();
                ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
                return View(pDetalleVenta);
            }
        }



        [HttpPost("ProcesarFactura")]
        public async Task<IActionResult> ProcesarFactura( string NombreCliente, string DUI, string Direccion, string Correo, string Telefono, decimal total, decimal descuento, decimal impuesto, decimal totalpagado, int cantidad, int codigo, byte FormaPago, DateTime FechaVenta, decimal ValorTotal, List<DetalleVenta> detalleVentas)
        {
            Random random = new Random();

            Venta objVenta = new();
            objVenta.IdUsuario = global.idu;
            objVenta.NumeroVenta = random.Next(100000, 999999);
            objVenta.NombreCliente = NombreCliente ?? "N/A";
            objVenta.Direccion = Direccion ?? "N/A";
            objVenta.Correo = Correo ?? "N/A";
            objVenta.DUI = DUI ?? "N/A";
            objVenta.Total = total;
            objVenta.Descuento = descuento;
            objVenta.Impuesto = impuesto;
            objVenta.TotalPagado = totalpagado;
            objVenta.Telefono = Telefono ?? "N/A";
            objVenta.FormaPago = 1;
            objVenta.FechaVenta = DateTime.Now;

            //FacturaBL.CrearAsync(objFactura);
            await VentaBL.CrearAsync(objVenta);

            foreach (var detalle in detalleVentas)
            {
                Producto objProducto = new Producto();
                objProducto.IdProducto = detalle.IdProducto;
                objProducto = await ProductoBL.ObtenerPorIdProductoAsync(objProducto);


                objProducto.Cantidad = objProducto.Cantidad - detalle.Cantidad;
                await ProductoBL.ModificarAsync(objProducto);


                idVen = objVenta.IdVenta;
                detalle.IdVenta = objVenta.IdVenta;
                await DetalleVentaBL.CrearAsync(detalle);
            }

            var venta = new Ventas
            {
                ObjVenta = objVenta,
                DetalleVentas = detalleVentas
            };

            return RedirectToAction("ObtenerFactura");
        }

        [HttpGet("ObtenerFactura")]

        public async Task<IActionResult> ObtenerFactura()
        {

            DetalleVenta objdetalle = new DetalleVenta();
            objdetalle.IdVenta = idVen;
            List<DetalleVenta> ListaDetalle = await DetalleVentaBL.BuscarIncluirVentaProductoAsync(objdetalle);
            ViewBag.Ventas = ListaDetalle.FirstOrDefault().Venta;
            ViewBag.ListaDetalle = ListaDetalle;
            return View();
        }

        public async Task<IActionResult> Reportes(DetalleVenta pDetalleFactura, DateTime fInicio, DateTime fFinal, int NumeroVenta)
        {
            List<Venta> ventas = await VentaBL.ObtenerTodosAsync();
            List<DetalleVenta> detalleVentas = await DetalleVentaBL.ObtenerTodosAsync();

            if (NumeroVenta != 0)
            {
                ViewBag.Ventas = ventas.Where(r => r.IdVenta == NumeroVenta).ToList();
            }
            else if (fInicio.Year != 1 && fFinal.Year != 1)
            {
                ViewBag.Ventas = ventas.Where(r => r.FechaVenta.Date >= fInicio.Date && r.FechaVenta.Date <= fFinal.Date).ToList();
            }
            else
            {
                ViewBag.Ventas = ventas;
            }

            ViewBag.Detalles = detalleVentas;

            return View();
        }

    }

}
