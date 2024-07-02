using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TekaDomain.Entities
{
    [Table("Rol")]
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }
        [Required]
        public string NombreRol { get; set; }
    }

    [Table("EstadoUsuario")]
    public class EstadoUsuario
    {
        [Key]
        public int IdEstado { get; set; }
        [Required]
        public string NombreEstado { get; set; }
    }

    [Table("Usuario")]
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
        public int IdRol { get; set; }
        public int IdEstado { get; set; }

        [ForeignKey("IdRol")]
        public Rol Rol { get; set; }

        [ForeignKey("IdEstado")]
        public EstadoUsuario EstadoUsuario { get; set; }
    }

    [Table("Ciudad")]
    public class Ciudad
    {
        [Key]
        public int IdCiudad { get; set; }
        [Required]
        public string NombreCiudad { get; set; }
    }

    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        [Required]
        public string Cedula { get; set; }
        [Required]
        public string Nombres { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public int IdCiudad { get; set; }

        [ForeignKey("IdCiudad")]
        public Ciudad Ciudad { get; set; }
    }

    [Table("Tecnico")]
    public class Tecnico
    {
        [Key]
        public int IdTecnico { get; set; }
        [Required]
        public string NombreTecnico { get; set; }
        [Required]
        public string Cedula { get; set; }
        [Required]
        public string TelefonoTecnico { get; set; }
        [Required]
        public string EstadoTecnico { get; set; }
    }

    [Table("Servicio")]
    public class Servicio
    {
        [Key]
        public int IdServicio { get; set; }
        public int IdCliente { get; set; }
        public int IdTecnico { get; set; }
        [Required]
        public string TipoServicio { get; set; }
        [Required]
        public DateTime FechaTentativaAtencion { get; set; }
        [Required]
        public string Estado { get; set; }

        [ForeignKey("IdCliente")]
        public Cliente Cliente { get; set; }

        [ForeignKey("IdTecnico")]
        public Tecnico Tecnico { get; set; }
    }

    [Table("Producto")]
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }
        [Required]
        public string NombreCategoria { get; set; }
        [Required]
        public string CodigoProducto { get; set; }
        [Required]
        public string Modelo { get; set; }
        [Required]
        public string Estado { get; set; }
        [Required]
        public string SerieProducto { get; set; }
        [Required]
        public decimal Precio { get; set; }
    }

    [Table("Repuesto")]
    public class Repuesto
    {
        [Key]
        public int IdRepuesto { get; set; }
        [Required]
        public string CodigoRepuesto { get; set; }
        [Required]
        public string NombreRepuesto { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public decimal Precio { get; set; }
    }

    [Table("Proforma")]
    public class Proforma
    {
        [Key]
        public int IdProforma { get; set; }
        public int IdCliente { get; set; }
        [Required]
        public DateTime FechaCompra { get; set; }
        [Required]
        public string NumeroFactura { get; set; }
        [Required]
        public string NombreAlmacen { get; set; }

        [ForeignKey("IdCliente")]
        public Cliente Cliente { get; set; }
    }

    [Table("DetalleProforma")]
    public class DetalleProforma
    {
        [Key]
        public int IdDetalleProforma { get; set; }
        public int IdProforma { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public string DescripcionRepuesto { get; set; }
        [Required]
        public decimal PrecioUnitario { get; set; }
        [Required]
        public decimal PrecioFinal { get; set; }

        [ForeignKey("IdProforma")]
        public Proforma Proforma { get; set; }
    }

    [Table("Pedido")]
    public class Pedido
    {
        [Key]
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }
        [Required]
        public string TipoPedido { get; set; }
        [Required]
        public DateTime FechaPedido { get; set; }

        [ForeignKey("IdCliente")]
        public Cliente Cliente { get; set; }
    }

    [Table("DetallePedido")]
    public class DetallePedido
    {
        [Key]
        public int IdDetallePedido { get; set; }
        public int IdPedido { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public string DescripcionRepuesto { get; set; }

        [ForeignKey("IdPedido")]
        public Pedido Pedido { get; set; }
    }
}
