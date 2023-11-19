using Microsoft.EntityFrameworkCore;
using SysComercialMartinez.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SysComercialMartinez.AccesoADatos
{
    public class BDContexto : DbContext
    {
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<DetalleVenta> DetalleVenta { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Compra> Compra { get; set; }
        public DbSet<DetalleCompra> DetalleCompra { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }
        public DbSet<Venta> Venta { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

            
        {
            optionsBuilder.UseSqlServer(@"workstation id=BDComercialMartinez.mssql.somee.com; packet size = 4096; user id=José0407_SQLLogin_1; pwd=rz363vdvcw;data source=BDComercialMartinez.mssql.somee.com; persist security info=False;initial catalog=BDComercialMartinez;Encrypt=False;TrustServerCertificate=False;");
            //optionsBuilder.UseSqlServer(@"Data Source=OSCARROMERO\SQLEXPRESS;Initial Catalog=SysInventarioFacturacion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

    }
}
