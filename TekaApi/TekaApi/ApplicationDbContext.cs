using Microsoft.EntityFrameworkCore;
using TekaDomain.Entities;

namespace TekaApi
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Rol> Roles { get; set; }
        public DbSet<EstadoUsuario> EstadoUsuarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<EstadoProducto> EstadoProductos { get; set; }
        public DbSet<Producto> Productos { get; set; }
    }
}
