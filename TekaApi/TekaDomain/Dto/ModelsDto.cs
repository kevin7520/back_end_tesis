using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekaDomain.Entities;

namespace TekaDomain.Dto
{

    public class TecnicoDto
    {
        public int IdTecnico { get; set; }
        public string NombreTecnico { get; set; }
        public string Cedula { get; set; }
        public string TelefonoTecnico { get; set; }
        public EstadoTecnicoDto EstadoTecnico { get; set; }
    }

    public class EstadoTecnicoDto
    {
        public int IdEstado { get; set; }
        public string NombreEstado { get; set; }
    }

    public class CreateTecnicoDto
    {
        public string NombreTecnico { get; set; }
        public string Cedula { get; set; }
        public string TelefonoTecnico { get; set; }
        public int IdEstado { get; set; }
    }

    public class CreateClienteDto
    {
        public string Cedula { get; set; }
        public string Nombres { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public int IdCiudad { get; set; }
    }

    public class ClienteDto
    {
        public int IdCliente { get; set; }
        public string Cedula { get; set; }
        public string Nombres { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public CiudadDto Ciudad { get; set; }
    }

    public class CiudadDto
    {
        public int IdCiudad { get; set; }
        public string NombreCiudad { get; set; }
    }

    public class EstadoUsuarioDto
    {
        public int IdEstado { get; set; }
        public string NombreEstado { get; set; }
    }

    public class TipoServicioDto
    {
        public int IdTipoServicio { get; set; }
        public string NombreTipoServicio { get; set; }
    }

    public class HorarioDto
    {
        public int IdHorario { get; set; }
        public int IdTecnico { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
    }

    public class HorarioServicioDto
    {
        public int IdHorarioServicio { get; set; }
        public int IdHorario { get; set; }
        public int IdServicio { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
    }

    public class CreateHorarioDto
    {
        public int IdTecnico { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
    }

    public class CreateHorarioServicioDto
    {
        public int IdHorario { get; set; }
        public int IdServicio { get; set; }
    }

    public class CreateServicioDto
    {
        public int? IdCliente { get; set; }
        public int? IdTecnico { get; set; }
        public int? IdTipoServicio { get; set; }
        public int? IdEstadoServicio { get; set; }
        public double? valor { get; set; }
        public DateTime? FechaTentativaAtencion { get; set; }
        public List<CreateServicioProductoDto>? Productos { get; set; }
        public List<CreateServicioRespuestoDto>? RepuestoDto { get; set; }
    }

    public class EditServicioDto
    {
        public int? IdCliente { get; set; }
        public int? IdTecnico { get; set; }
        public int? IdTipoServicio { get; set; }
        public int? IdEstadoServicio { get; set; }
        public int? IdAlmacen { get; set; }
        public int? IdFactura { get; set; }
        public double? valor { get; set; }
        public DateTime? FechaTentativaAtencion { get; set; }
        public List<CreateServicioProductoDto>? Productos { get; set; }
        public List<CreateServicioRespuestoDto>? RepuestoDto { get; set; }
    }

    public class CreateServicioProductoDto
    {
        public int IdProducto { get; set; }
        public double Valor { get; set; }
        public string Serie { get; set; }
    }

    public class CreateServicioRespuestoDto
    {
        public int IdRepuesto { get; set; }
    }

    public class AlmacenDto
    {
        public int IdAlmacen { get; set; }
        public string NombreAlmacen { get; set; }
    }

    public class FacturaDto
    {
        public int Id { get; set; }
        public string NumeroFactura { get; set; }
    }

    public class RepuestoDto
    {
        public int IdRepuesto { get; set; }
        public string CodigoRepuesto { get; set; }
        public string NombreRepuesto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }

    public class ServicioDto
    {
        public int IdServicio { get; set; }
        public ClienteDto Cliente { get; set; }
        public TipoServicioDto? TipoServicio { get; set; }
        public List<ProductoDto>? Productos { get; set; }
        public List<RepuestoDto>? Repuestos { get; set; }
        public DateTime? FechaSolicitudServicio { get; set; }
        public DateTime? FechaTentativaAtencion { get; set; }
        public double? Valor { get; set; }
        public TecnicoDto? Tecnico { get; set; }
        public AlmacenDto? Almacen { get; set; }
        public FacturaDto? Factura { get; set; }
        public EstadoServicioDto EstadoServicioDto { get; set; }
        public List<HorarioDto> Horarios {  get; set; }
    }

    public class EstadoServicioDto
    {
        public int IdEstadoServicio { get; set; }
        public string NombreEstadoServicio { get; set; }
    }

    public class ProductoDto
    {
        public int IdProducto { get; set; }
        public int? IdCategoria { get; set; }
        public string CodigoProducto { get; set; }
        public string Modelo { get; set; }
        public int? IdEstadoProducto { get; set; }
        public string SerieProducto { get; set; }
        public decimal Precio { get; set; }
        public double? Valor { get; set; }
        public string? Serie { get; set; }
    }

    public class CreateProductoDto
    {
        public int IdCategoria { get; set; }
        public string CodigoProducto { get; set; }
        public string Modelo { get; set; }
        public int IdEstadoProducto { get; set; }
        public string SerieProducto { get; set; }
        public decimal Precio { get; set; }
    }

    public class UpdateProductoDto
    {
        public int IdCategoria { get; set; }
        public string Modelo { get; set; }
        public int IdEstadoProducto { get; set; }
        public string SerieProducto { get; set; }
        public decimal Precio { get; set; }
    }
    public class CreateProformaDto
    {
        public string DescripcionProducto { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public int? IdCliente { get; set; }
        public int? IdEstadoProforma { get; set; }
        public List<CreateDetalleProforma> Detalles { get; set; }
    }


    public class CreateDetalleProforma
    {
        public int IdRepuesto { get; set; }
        public int Cantidad { get; set; }
        public string DescripcionRepuesto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioFinal { get; set; }
    }
    public class CreatePedidoDto
    {
        public string DescripcionProducto { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public int? IdCliente { get; set; }
        public int? IdEstadoPedido { get; set; }
        public List<CreateDetallePedido> Detalles { get; set; }
    }

    public class CreateDetallePedido
    {
        public int IdRepuesto { get; set; }
        public int Cantidad { get; set; }
        public string DescripcionRepuesto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioFinal { get; set; }
    }

    public class UpdatePedidoDto
    {
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }
        public string TipoPedido { get; set; }
        public DateTime FechaPedido { get; set; }
        public int IdProducto { get; set; }
        public List<DetallePedidoDto> Detalles { get; set; }
    }

    public class PedidoDto
    {
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }
        public ClienteDto Cliente { get; set; }
        public string TipoPedido { get; set; }
        public DateTime FechaPedido { get; set; }
        public int IdProducto { get; set; }
        public ProductoDto Producto { get; set; }
        public List<DetallePedidoDto> Detalles { get; set; }
    }

    public class DetallePedidoDto
    {
        public int IdDetallePedido { get; set; }
        public int IdPedido { get; set; }
        public int Cantidad { get; set; }
        public string DescripcionRepuesto { get; set; }
    }
}
