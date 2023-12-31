﻿using Microsoft.EntityFrameworkCore;
using SysComercialMartinez.AccesoADatos;
using SysComercialMartinez.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysComercialMartinez.AccesoADatos
{
    public class CompraDAL
    {
        public static async Task<int> CrearAsync(Compra pCompra)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pCompra);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModificarAsync(Compra pCompra)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Compra = await bdContexto.Compra.FirstOrDefaultAsync(s => s.IdCompra == pCompra.IdCompra);
                Compra.IdUsuario = pCompra.IdUsuario;
                Compra.FormaPago = pCompra.FormaPago;
                Compra.NumeroCompra = pCompra.NumeroCompra;
                Compra.TotalPago = pCompra.TotalPago;
                Compra.Total = pCompra.Total;
                bdContexto.Update(Compra);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(Compra pCompra)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Compra = await bdContexto.Compra.FirstOrDefaultAsync(s => s.IdCompra == pCompra.IdCompra);
                bdContexto.Compra.Remove(Compra);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Compra> ObtenerPorIdAsync(Compra pCompra)
        {
            var Compra = new Compra();
            using (var bdContexto = new BDContexto())
            {
                Compra = await bdContexto.Compra.FirstOrDefaultAsync(s => s.IdCompra == pCompra.IdCompra);
            }
            return Compra;
        }

        public static async Task<List<Compra>> ObtenerTodosAsync()
        {
            var Compra = new List<Compra>();
            using (var bdContexto = new BDContexto())
            {
                Compra = await bdContexto.Compra.Include(c => c.DetalleCompra).ThenInclude(p => p.Producto).ToListAsync();
            }
            return Compra;
        }
        internal static IQueryable<Compra> QuerySelect(IQueryable<Compra> pQuery, Compra pCompra)
        {
            //Para enteros y decimales
            if (pCompra.IdCompra > 0)
                pQuery = pQuery.Where(s => s.IdCompra == pCompra.IdCompra);
            if (pCompra.IdUsuario > 0)
                pQuery = pQuery.Where(s => s.IdUsuario == pCompra.IdUsuario);
            pQuery = pQuery.OrderByDescending(s => s.IdCompra).AsQueryable();
            if (pCompra.Top_Aux > 0)
                pQuery = pQuery.Take(pCompra.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<Compra>> BuscarAsync(Compra pCompra)
        {
            var Compra = new List<Compra>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Compra.AsQueryable();
                select = QuerySelect(select, pCompra);
                Compra = await select.ToListAsync();
            }
            return Compra;
        }

        public static async Task<List<Compra>> BuscarIncluirUsuarioAsync(Compra pCompra)
        {
            var Compra = new List<Compra>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Compra.AsQueryable();
                select = QuerySelect(select, pCompra).Include(s => s.Usuario).AsQueryable();
                Compra = await select.ToListAsync();
            }
            return Compra;
        }
    }
}


