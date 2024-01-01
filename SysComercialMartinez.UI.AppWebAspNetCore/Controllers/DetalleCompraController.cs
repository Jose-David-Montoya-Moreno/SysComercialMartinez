using Microsoft.AspNetCore.Authentication.Cookies;
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

    public class DetalleCompraController : Controller
    {

        DetalleCompraBL DetalleCompraBL = new DetalleCompraBL();
        ProductoBL ProductoBL = new ProductoBL();
        CompraBL CompraBL = new CompraBL();
        public static int idComp;
        // GET: DetallePedidoController
        public async Task<IActionResult> Index(DetalleCompra pDetalleCompra = null)
        {
            if (pDetalleCompra == null)
                pDetalleCompra = new DetalleCompra();
            if (pDetalleCompra.Top_Aux == 0)
                pDetalleCompra.Top_Aux = 10;
            else if (pDetalleCompra.Top_Aux == -1)
                pDetalleCompra.Top_Aux = 0;
            var taskBuscar = DetalleCompraBL.BuscarIncluirCompraProductoAsync(pDetalleCompra);
            var taskObtenerTodosCompra = CompraBL.ObtenerTodosAsync();
            var taskObtenerTodosProducto = ProductoBL.ObtenerTodosAsync();
            var DetalleCompra = await taskBuscar;
            ViewBag.Top = pDetalleCompra.Top_Aux;
            ViewBag.Compra = await taskObtenerTodosCompra;
            ViewBag.Producto = await taskObtenerTodosProducto;
            return View(DetalleCompra);
        }

        // GET: DetallePedidoController/Details/5
        public async Task<IActionResult> Details(int IdDetalleCompra)
        {
            var DetalleCompra = await DetalleCompraBL.ObtenerPorIdAsync(new DetalleCompra { IdDetalleCompra = IdDetalleCompra });
            DetalleCompra.Compra = await CompraBL.ObtenerPorIdAsync(new Compra { IdCompra = DetalleCompra.IdCompra });
            DetalleCompra.Producto = await ProductoBL.ObtenerPorIdProductoAsync(new Producto { IdProducto = DetalleCompra.IdProducto });

            return View(DetalleCompra);
        }

        // GET: DetallePedidoController/Create

        public async Task<IActionResult> Create()
        {
            ViewBag.Compra = await CompraBL.ObtenerTodosAsync();
            ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

        // POST: DetallePedidoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DetalleCompra pDetalleCompra)
        {
            try
            {
                int result = await DetalleCompraBL.CrearAsync(pDetalleCompra);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Compra = await CompraBL.ObtenerTodosAsync();
                ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
                return View(pDetalleCompra);
            }
        }

        // GET: DetallePedidoController/Edit/5
        public async Task<IActionResult> Edit(DetalleCompra pDetalleCompra)
        {
            var taskObtenerPorId = DetalleCompraBL.ObtenerPorIdAsync(pDetalleCompra);
            var taskObtenerTodosCompra = CompraBL.ObtenerTodosAsync();
            var taskObtenerTodosProducto = ProductoBL.ObtenerTodosAsync();
            var DetalleCompra = await taskObtenerPorId;
            ViewBag.Compra = await taskObtenerTodosCompra;
            ViewBag.Producto = await taskObtenerTodosProducto;
            ViewBag.Error = "";
            return View(DetalleCompra);
        }

        // POST: DetallePedidoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdDetalleCompra, DetalleCompra pDetalleCompra)
        {
            try
            {
                int result = await DetalleCompraBL.ModificarAsync(pDetalleCompra);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Compra = await CompraBL.ObtenerTodosAsync();
                ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
                return View(pDetalleCompra);
            }
        }

        // GET: DetallePedidoController/Delete/5
        public async Task<IActionResult> Delete(DetalleCompra pDetalleCompra)
        {
            var DetalleCompra = await DetalleCompraBL.ObtenerPorIdAsync(pDetalleCompra);
            DetalleCompra.Compra = await CompraBL.ObtenerPorIdAsync(new Compra { IdCompra = DetalleCompra.IdCompra });
            DetalleCompra.Producto = await ProductoBL.ObtenerPorIdProductoAsync(new Producto { IdProducto = DetalleCompra.IdProducto });
            ViewBag.Error = "";

            return View(DetalleCompra);
        }

        // POST: DetallePedidoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int IdDetalleCompra, DetalleCompra pDetalleCompra)
        {
            try
            {
                int result = await DetalleCompraBL.EliminarAsync(pDetalleCompra);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var DetalleCompra = await DetalleCompraBL.ObtenerPorIdAsync(pDetalleCompra);
                if (DetalleCompra == null)
                    DetalleCompra = new DetalleCompra();
                if (DetalleCompra.IdDetalleCompra > 0)
                    DetalleCompra.Compra = await CompraBL.ObtenerPorIdAsync(new Compra { IdCompra = DetalleCompra.IdCompra });
                if (DetalleCompra.IdDetalleCompra > 0)
                    DetalleCompra.Producto = await ProductoBL.ObtenerPorIdProductoAsync(new Producto { IdProducto = DetalleCompra.IdProducto });
                return View(DetalleCompra);
            }
        }

        
        public async Task<IActionResult> Compra(int? campo, DetalleCompra pDetalleCompra = null)
        {
            if (pDetalleCompra == null)
                pDetalleCompra = new DetalleCompra();
            if (pDetalleCompra.Top_Aux == 0)
                pDetalleCompra.Top_Aux = 10;
            else if (pDetalleCompra.Top_Aux == -1)
                pDetalleCompra.Top_Aux = 0;
            List<DetalleCompra>? DetalleCompra;
            var taskBuscar = DetalleCompraBL.BuscarIncluirCompraProductoAsync(pDetalleCompra);
            var taskObtenerTodosCompras = DetalleCompraBL.BuscarIncluirCompraProductoAsync(pDetalleCompra);
            var taskObtenerTodosProducto = ProductoBL.ObtenerTodosAsync();
            DetalleCompra = await taskBuscar;
            ViewBag.Top = pDetalleCompra.Top_Aux;
            ViewBag.Compras = await taskObtenerTodosCompras;
            ViewBag.Producto = await taskObtenerTodosProducto;
         
            return View(DetalleCompra);
        }

        [HttpPost("ProcesarCompra")]
        public async Task<IActionResult> ProcesarCompra( decimal total, decimal totalpagado,byte FormaPago, DateTime FechaCompra, List<DetalleCompra> detalleCompras)
        {
            Random random = new Random();

            Compra objCompra = new();
            objCompra.IdUsuario = global.idu;
            objCompra.NumeroCompra = random.Next(100000, 999999);
            objCompra.Total = total;
            objCompra.FormaPago = 1;
            objCompra.TotalPago = totalpagado;
            objCompra.FechaCompra = DateTime.Now;

            await CompraBL.CrearAsync(objCompra);

            foreach (var detalle in detalleCompras)
            {
                Producto objProducto = new Producto();
                objProducto.IdProducto = detalle.IdProducto;
                objProducto = await ProductoBL.ObtenerPorIdProductoAsync(objProducto);


                objProducto.Cantidad = objProducto.Cantidad + detalle.Cantidad;
                await ProductoBL.ModificarAsync(objProducto);


                idComp = objCompra.IdCompra;
                detalle.IdCompra = objCompra.IdCompra;
                await DetalleCompraBL.CrearAsync(detalle);
            }



            return RedirectToAction("ObtenerCompra");
        }

        [HttpGet("ObtenerCompra")]

        public async Task<IActionResult> ObtenerCompra()
        {

            DetalleCompra objdetalle = new DetalleCompra();
            objdetalle.IdCompra = idComp;
            List<DetalleCompra> ListaDetalle = await DetalleCompraBL.BuscarIncluirCompraProductoAsync(objdetalle);
            ViewBag.Compras = ListaDetalle.FirstOrDefault().Compra;
            ViewBag.ListaDetalle = ListaDetalle;
            return View();
        }

        public async Task<IActionResult> Reportes(DetalleCompra pDetalleFactura, DateTime fInicio, DateTime fFinal, int NumeroCompra)
        {
            List<Compra> compras = await CompraBL.ObtenerTodosAsync();
            List<DetalleCompra> detalleCompras = await DetalleCompraBL.ObtenerTodosAsync();

            if (NumeroCompra != 0)
            {
                ViewBag.Compras = compras.Where(r => r.IdCompra == NumeroCompra).ToList();
            }
            else if (fInicio.Year != 1 && fFinal.Year != 1)
            {
                ViewBag.Compras = compras.Where(r => r.FechaCompra.Date >= fInicio.Date && r.FechaCompra.Date <= fFinal.Date).ToList();
            }
            else
            {
                ViewBag.Compras = compras;
            }

            ViewBag.Detalles = detalleCompras;

            return View();
        }
    }

}
