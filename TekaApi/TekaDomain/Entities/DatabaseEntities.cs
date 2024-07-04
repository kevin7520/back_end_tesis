﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        public int IdEstado { get; set; }

        [ForeignKey("IdEstado")]
        public EstadoTecnico EstadoTecnico { get; set; }
    }

    [Table("EstadoTecnico")]
    public class EstadoTecnico
    {
        [Key]
        public int IdEstado { get; set; }
        [Required]
        public string NombreEstado { get; set; }
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
        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
        public int IdEstadoProforma { get; set; }

        [ForeignKey("IdCliente")]
        public Cliente Cliente { get; set; }

        [ForeignKey("IdEstadoProforma")]
        public EstadoProforma EstadoProforma { get; set; }

        [ForeignKey("IdProducto")]
        public Producto Producto { get; set; }
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
        public int IdProducto { get; set; }

        [ForeignKey("IdCliente")]
        public Cliente Cliente { get; set; }

        [ForeignKey("IdProducto")]
        public Producto Producto { get; set; }
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