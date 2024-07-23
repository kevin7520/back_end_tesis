using System;
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

    [Table("EstadoTecnico")]
    public class EstadoTecnico
    {
        [Key]
        public int IdEstado { get; set; }
        [Required]
        public string NombreEstado { get; set; }
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
        public int IdEstado { get; set; }

        [ForeignKey("IdEstado")]
        public EstadoTecnico EstadoTecnico { get; set; }
    }

    [Table("TipoServicio")]
    public class TipoServicio
    {
        [Key]
        public int IdTipoServicio { get; set; }
        
        [Required]
        [StringLength(100)]
        public string NombreTipoServicio { get; set; }

        [StringLength(100)]
        public string? Opciones { get; set; }

        [StringLength(100)]
        public string? Valor1 { get; set; }

        [StringLength(100)]
        public string? Valor2 { get; set; }

        [StringLength(100)]
        public string? Valor3 { get; set; }

        [StringLength(100)]
        public string? Valor4 { get; set; }
    }

    [Table("EstadoServicio")]
    public class EstadoServicio
    {
        [Key]
        public int IdEstadoServicio { get; set; }
        [Required]
        [StringLength(100)]
        public string NombreEstadoServicio { get; set; }
    }

    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        [Required]
        [StringLength(100)]
        public string NombreCategoria { get; set; }
    }

    [Table("EstadoProducto")]
    public class EstadoProducto
    {
        [Key]
        public int IdEstadoProducto { get; set; }
        [Required]
        [StringLength(100)]
        public string NombreEstadoProducto { get; set; }
    }

    [Table("Producto")]
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }
        public int IdCategoria { get; set; }
        [Required]
        [StringLength(50)]
        public string CodigoProducto { get; set; }
        [Required]
        [StringLength(50)]
        public string Modelo { get; set; }
        public int IdEstadoProducto { get; set; }
        [Required]
        [StringLength(50)]
        public string SerieProducto { get; set; }
        [Required]
        public decimal Precio { get; set; }

        [ForeignKey("IdCategoria")]
        public Categoria Categoria { get; set; }

        [ForeignKey("IdEstadoProducto")]
        public EstadoProducto EstadoProducto { get; set; }
    }

    [Table("ServicioProducto")]
    public class ServicioProducto
    {
        [Key]
        public int IdServicioProducto { get; set; }
        [Required]
        public int IdServicio { get; set; }
        [Required]
        public int IdProducto { get; set; }
        public double Valor { get; set; }
        public string Serie { get; set; }

        [ForeignKey("IdServicio")]
        public Servicio Servicio { get; set; }

        [ForeignKey("IdProducto")]
        public Producto Producto { get; set; }
    }

    [Table("ServicioRepuesto")]
    public class ServicioRepuesto
    {
        [Key]
        public int IdServicioRepuesto { get; set; }
        [Required]
        public int IdServicio { get; set; }
        [Required]
        public int IdRepuesto { get; set; }

        [ForeignKey("IdServicio")]
        public Servicio Servicio { get; set; }

        [ForeignKey("IdRepuesto")]
        public Repuesto Repuesto { get; set; }
    }

    [Table("Servicio")]
    public class Servicio
    {
        [Key]
        public int IdServicio { get; set; }
        public int? IdCliente { get; set; }
        public int? IdTecnico { get; set; }
        public int? IdTipoServicio { get; set; }
        public int? IdEstadoServicio { get; set; }
        public int? IdFactura { get; set; }
        public int? IdAlmacen { get; set; }
        public DateTime? FechaTentativaAtencion { get; set; }
        public DateTime? FechaSolicitudServicio { get; set; }
        public double? Valor { get; set; }

        [ForeignKey("IdCliente")]
        public Cliente Cliente { get; set; }

        [ForeignKey("IdTecnico")]
        public Tecnico Tecnico { get; set; }

        [ForeignKey("IdTipoServicio")]
        public TipoServicio TipoServicio { get; set; }

        [ForeignKey("IdEstadoServicio")]
        public EstadoServicio EstadoServicio { get; set; }

        [ForeignKey("IdAlmacen")]
        public Almacen Almacen { get; set; }

        [ForeignKey("IdFactura")]
        public Factura Factura { get; set; }
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

    [Table("EstadoProforma")]
    public class EstadoProforma
    {
        [Key]
        public int IdEstadoProforma { get; set; }
        [Required]
        [StringLength(50)]
        public string NombreEstadoProforma { get; set; }
    }

    [Table("Proforma")]
    public class Proforma
    {
        [Key]
        public int IdProforma { get; set; }
        [Required]
        public string DescripcionProducto { get; set; }
        [Required]
        public decimal Subtotal { get; set; }
        [Required]
        public decimal Iva { get; set; }
        [Required]
        public decimal Total { get; set; }
        public int? IdCliente { get; set; }
        public int? IdEstadoProforma { get; set; }

        [ForeignKey("IdCliente")]
        public Cliente Cliente { get; set; }

        [ForeignKey("IdEstadoProforma")]
        public EstadoProforma EstadoProforma { get; set; }
    }

    [Table("DetalleProforma")]
    public class DetalleProforma
    {
        [Key]
        public int IdDetalleProforma { get; set; }
        public int IdProforma { get; set; }
        public int IdRepuesto { get; set; }
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
        [ForeignKey("IdRepuesto")]
        public Repuesto Repuesto { get; set; }
    }

    [Table("EstadoPedido")]
    public class EstadoPedido
    {
        [Key]
        public int IdEstadoPedido { get; set; }
        [Required]
        [StringLength(50)]
        public string NombreEstadoPedido{ get; set; }
    }

    [Table("Pedido")]
    public class Pedido
    {
        [Key]
        public int IdPedido { get; set; }
        [Required]
        public string DescripcionProducto { get; set; }
        [Required]
        public decimal Subtotal { get; set; }
        [Required]
        public decimal Iva { get; set; }
        [Required]
        public decimal Total { get; set; }
        public int? IdCliente { get; set; }
        public int? IdEstadoPedido { get; set; }

        [ForeignKey("IdCliente")]
        public Cliente Cliente { get; set; }

        [ForeignKey("IdEstadoPedido")]
        public EstadoPedido EstadoPedido { get; set; }
    }

    [Table("DetallePedido")]
    public class DetallePedido
    {
        [Key]
        public int IdDetallePedido { get; set; }
        public int IdPedido { get; set; }
        public int IdRepuesto { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public string DescripcionRepuesto { get; set; }
        [Required]
        public decimal PrecioUnitario { get; set; }
        [Required]
        public decimal PrecioFinal { get; set; }

        [ForeignKey("IdPedido")]
        public Pedido Pedido { get; set; }
        [ForeignKey("IdRepuesto")]
        public Repuesto Repuesto { get; set; }
    }

    [Table("Horario")]
    public class Horario
    {
        [Key]
        public int IdHorario { get; set; }
        [Required]
        public int IdTecnico { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public TimeSpan HoraInicio { get; set; }
        [Required]
        public TimeSpan HoraFin { get; set; }

        [ForeignKey("IdTecnico")]
        public Tecnico Tecnico { get; set; }
    }

    [Table("HorarioServicio")]
    public class HorarioServicio
    {
        [Key]
        public int IdHorarioServicio { get; set; }
        [Required]
        public int IdHorario { get; set; }
        [Required]
        public int IdServicio { get; set; }

        [ForeignKey("IdHorario")]
        public Horario Horario { get; set; }

        [ForeignKey("IdServicio")]
        public Servicio Servicio { get; set; }
    }

    [Table("Factura")]
    public class Factura
    {
        [Key]
        public int IdFactura { get; set; }
        public DateTime FechaCompra { get; set; }
        public string NumeroFactura { get; set; }
    }
    [Table("Almacen")]
    public class Almacen
    {
        [Key]
        public int IdAlmacen { get; set; }
        public string NombreAlmacen { get; set; }
    }

}
