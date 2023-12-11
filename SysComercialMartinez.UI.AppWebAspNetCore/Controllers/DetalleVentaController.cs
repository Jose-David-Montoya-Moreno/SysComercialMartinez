﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysComercialMartinez.EntidadesDeNegocio;
using SysComercialMartinez.LogicaDeNegocio;

namespace SysComercialMartinez.UI.AppWebAspNetCore.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    //[Authorize(Roles = "SuperAdmin")]
    public class DetalleVentaController : Controller
    {
        DetalleVentaBL DetalleVentaBL = new DetalleVentaBL();
        ProductoBL ProductoBL = new ProductoBL();
        VentaBL VentaBL = new VentaBL();
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
    }
}