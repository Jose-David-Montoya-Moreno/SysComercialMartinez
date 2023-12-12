using Microsoft.EntityFrameworkCore;
using SysComercialMartinez.AccesoADatos;
using SysComercialMartinez.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysComercialMartinez.AccesoADatos
{
    public class DetalleCompraDAL
    {
        public static async Task<int> CrearAsync(DetalleCompra pDetalleCompra)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                
                bdContexto.Add(pDetalleCompra);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModificarAsync(DetalleCompra pDetalleCompra)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var detallecompra = await bdContexto.DetalleCompra.FirstOrDefaultAsync(s => s.IdDetalleCompra == pDetalleCompra.IdDetalleCompra);
                detallecompra.IdProducto = pDetalleCompra.IdProducto;
                detallecompra.IdCompra = pDetalleCompra.IdCompra;
                detallecompra.Cantidad = pDetalleCompra.Cantidad;
                detallecompra.ValorTotal = pDetalleCompra.ValorTotal;
                bdContexto.Update(detallecompra);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(DetalleCompra pDetalleCompra)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var detallecompra = await bdContexto.DetalleCompra.FirstOrDefaultAsync(s => s.IdDetalleCompra == pDetalleCompra.IdDetalleCompra);
                bdContexto.DetalleCompra.Remove(detallecompra);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<DetalleCompra> ObtenerPorIdAsync(DetalleCompra pDetalleCompra)
        {
            var detallecompra = new DetalleCompra();
            using (var bdContexto = new BDContexto())
            {
                detallecompra = await bdContexto.DetalleCompra.FirstOrDefaultAsync(s => s.IdDetalleCompra == pDetalleCompra.IdDetalleCompra);
            }
            return detallecompra;
        }
        public static async Task<List<DetalleCompra>> ObtenerTodosAsync()
        {
            var DetalleCompra = new List<DetalleCompra>();
            using (var bdContexto = new BDContexto())
            {
                DetalleCompra = await bdContexto.DetalleCompra.Include(p => p.Producto).ToListAsync();
            }
            return DetalleCompra;
        }
        internal static IQueryable<DetalleCompra> QuerySelect(IQueryable<DetalleCompra> pQuery, DetalleCompra pDetalleCompra)
        {
            if (pDetalleCompra.IdDetalleCompra > 0)
                pQuery = pQuery.Where(s => s.IdDetalleCompra == pDetalleCompra.IdDetalleCompra);
            if (pDetalleCompra.IdCompra > 0)
                pQuery = pQuery.Where(s => s.IdCompra == pDetalleCompra.IdCompra);
            if (pDetalleCompra.IdProducto > 0)
                pQuery = pQuery.Where(s => s.IdProducto == pDetalleCompra.IdProducto);
            //if (pDetalleFactura.Codigo > 0)
            //    pQuery = pQuery.Where(s => s.Codigo == pDetalleFactura.Codigo);
            //if (pDetalleFactura.Cantidad > 0)
            //    pQuery = pQuery.Where(s => s.Cantidad == pDetalleFactura.Cantidad);          
            //if (!string.IsNullOrWhiteSpace(pDetalleFactura.FormaDePago))
            //    pQuery = pQuery.Where(s => s.FormaDePago.Contains(pDetalleFactura.FormaDePago));

            
            //if (!string.IsNullOrWhiteSpace(pDetalleFactura.FechaEmision.ToString()))
            //    pQuery = pQuery.Where(s => s.FechaEmision.ToString().Contains(pDetalleFactura.FechaEmision.ToString()));
            //if (pDetalleFactura.ValorTotal > 0)
            //    pQuery = pQuery.Where(s => s.ValorTotal == pDetalleFactura.ValorTotal);



            pQuery = pQuery.OrderByDescending(s => s.IdDetalleCompra).AsQueryable();
            if (pDetalleCompra.Top_Aux > 0)
                pQuery = pQuery.Take(pDetalleCompra.Top_Aux).AsQueryable();
            return pQuery;
        }

        public static async Task<List<DetalleCompra>> BuscarAsync(DetalleCompra pDetalleCompra)
        {
            var DetalleCompras = new List<DetalleCompra>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.DetalleCompra.AsQueryable();
                select = QuerySelect(select, pDetalleCompra);
                DetalleCompras = await select.ToListAsync();
            }
            return DetalleCompras;
        }
        public static async Task<List<DetalleCompra>> BuscarIncluirCompraProductoAsync(DetalleCompra pDetalleCompra)
        {
            var DetalleCompra = new List<DetalleCompra>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.DetalleCompra.AsQueryable();
                select = QuerySelect(select, pDetalleCompra).Include(s => s.Compra).Include(s => s.Producto).AsQueryable();
                DetalleCompra = await select.ToListAsync();
            }
            return DetalleCompra;
        }
    }
}
