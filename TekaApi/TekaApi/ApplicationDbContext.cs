using Microsoft.EntityFrameworkCore;
using TekaDomain.Entities;

namespace TekaApi
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Rol> Roles { get; set; }
        public DbSet<EstadoUsuario> EstadosUsuario { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<EstadoTecnico> EstadosTecnico { get; set; }
        public DbSet<Tecnico> Tecnicos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<EstadoProducto> EstadosProducto { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Repuesto> Repuestos { get; set; }
        public DbSet<EstadoProforma> EstadosProforma { get; set; }
        public DbSet<Proforma> Proformas { get; set; }
        public DbSet<DetalleProforma> DetallesProforma { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallesPedido { get; set; }
        public DbSet<TipoServicio> TipoServicios { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<HorarioServicio> HorarioServicios { get; set; }
        public DbSet<EstadoServicio> EstadoServicios { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<ServicioProducto> ServicioProductos { get; set; }
        public DbSet<Almacen> Almacen { get; set; }
        public DbSet<Factura> Factura { get; set; }
        public DbSet<ServicioRepuesto> ServicioRepuestos { get;set; }

    }
}
