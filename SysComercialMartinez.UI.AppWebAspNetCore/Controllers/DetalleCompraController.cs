using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysComercialMartinez.EntidadesDeNegocio;
using SysComercialMartinez.LogicaDeNegocio;


namespace SysComercialMartinez.UI.AppWebAspNetCore.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    //[Authorize(Roles = "SuperAdmin")]

    public class DetalleCompraController : Controller
    {

        DetalleCompraBL DetalleCompraBL = new DetalleCompraBL();
        ProductoBL ProductoBL = new ProductoBL();
        CompraBL CompraBL = new CompraBL();
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
        public async Task<IActionResult> ProcesarCompra( decimal total, decimal totalpagado,byte FormadaDePago, List<DetalleCompra> detalleCompras)
        {
            Random random = new Random();

            Compra objCompra = new();
            objCompra.IdUsuario = global.idu;
            objCompra.NumeroCompra = random.Next(100000, 999999);
            objCompra.Total = total;
            objCompra.TotalPago = totalpagado;
            

            //FacturaBL.CrearAsync(objFactura);
            await CompraBL.CrearAsync(objCompra);


            DetalleFactura DetalleFacturas = new();
            DetalleFacturas.Codigo = random.Next(100000, 999999);
            DetalleFacturas.FechaEmision = DateTime.Now;
            DetalleFacturas.FormaDePago = 1;

            foreach (var detalle in detalleFacturas)
            {
                Producto objProducto = new Producto();
                objProducto.IdProducto = detalle.IdProducto;
                objProducto = await ProductoBL.ObtenerPorIdProductoAsync(objProducto);


                objProducto.Cantidad = objProducto.Cantidad - detalle.Cantidad;
                await ProductoBL.ModificarAsync(objProducto);


                idFac = objFactura.IdFactura;
                detalle.IdFactura = objFactura.IdFactura;
                await detalle_facturaBL.CrearAsync(detalle);
            }

            var venta = new Venta
            {
                ObjFactura = objFactura,
                DetalleFacturas = detalleFacturas
            };

            return RedirectToAction("ObtenerFactura");
        }
    }

}
