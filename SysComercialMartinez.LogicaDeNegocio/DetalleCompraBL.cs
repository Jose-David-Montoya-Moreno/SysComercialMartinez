using SysComercialMartinez.AccesoADatos;
using SysComercialMartinez.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysComercialMartinez.LogicaDeNegocio
{
    public class DetalleCompraBL
    {

        public async Task<int> CrearAsync(DetalleCompra pDetalleCompra)
        {
            return await DetalleCompraDAL.CrearAsync(pDetalleCompra);
        }
        public async Task<int> ModificarAsync(DetalleCompra pDetalleCompra)
        {
            return await DetalleCompraDAL.ModificarAsync(pDetalleCompra);
        }
        public async Task<int> EliminarAsync(DetalleCompra pDetalleCompra)
        {
            return await DetalleCompraDAL.EliminarAsync(pDetalleCompra);
        }
        public async Task<DetalleCompra> ObtenerPorIdAsync(DetalleCompra pDetalleCompra)
        {
            return await DetalleCompraDAL.ObtenerPorIdAsync(pDetalleCompra);
        }
        public async Task<List<DetalleCompra>> ObtenerTodosAsync()
        {
            return await DetalleCompraDAL.ObtenerTodosAsync();
        }
        public async Task<List<DetalleCompra>> BuscarAsync(DetalleCompra pDetalleCompra)
        {
            return await DetalleCompraDAL.BuscarAsync(pDetalleCompra);
        }

        public async Task<List<DetalleCompra>> BuscarIncluirCompraProductoAsync(DetalleCompra pDetalleCompra)
        {
            return await DetalleCompraDAL.BuscarIncluirCompraProductoAsync(pDetalleCompra);
        }
    }
}
