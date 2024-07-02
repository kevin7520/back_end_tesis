using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekaDomain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Rol
    {
        [Key]
        public int IdRol { get; set; }
        [Required]
        public string NombreRol { get; set; }
    }

    public class EstadoUsuario
    {
        [Key]
        public int IdEstado { get; set; }
        [Required]
        public string NombreEstado { get; set; }
    }

    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        [Required]
        public string NombreUsuario { get; set; }
        [Required]
        public string Contraseña { get; set; }
        [Required]
        public string Correo { get; set; }
        [ForeignKey("Rol")]
        public int IdRol { get; set; }
        [ForeignKey("EstadoUsuario")]
        public int IdEstado { get; set; }
        public Rol Rol { get; set; }
        public EstadoUsuario EstadoUsuario { get; set; }
    }

    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        [Required]
        [StringLength(100)]
        public string NombreCategoria { get; set; }
    }

    public class EstadoProducto
    {
        [Key]
        public int IdEstadoProducto { get; set; }
        [Required]
        [StringLength(100)]
        public string NombreEstadoProducto { get; set; }
    }

    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }
        [ForeignKey("Categoria")]
        public int IdCategoria { get; set; }
        [Required]
        [StringLength(100)]
        public string CodigoProducto { get; set; }
        [Required]
        [StringLength(100)]
        public string Modelo { get; set; }
        [Required]
        [StringLength(100)]
        public string SerieProducto { get; set; }
        [ForeignKey("EstadoProducto")]
        public int IdEstadoProducto { get; set; }
        public Categoria Categoria { get; set; }
        public EstadoProducto EstadoProducto { get; set; }
    }

}
