using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TekaDomain;
using TekaDomain.Entities;
using TekaDomain.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace TekaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PedidoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PedidoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPedidos()
        {
            try
            {
                var proforma = await _context.Pedidos.Include(p => p.Cliente).Include(p => p.EstadoPedido).ToListAsync();

                var response = new ResponseGlobal<IEnumerable<Pedido>>
                {
                    codigo = "200",
                    mensaje = "Tipos de servicios recuperados exitosamente",
                    data = proforma
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al recuperar las proformas",
                    data = ex.Message
                };

                return StatusCode(500, response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostPedidos([FromBody] CreatePedidoDto pedidoDto)
        {
            var pedido = new Pedido
            {
                IdCliente = pedidoDto.IdCliente,
                DescripcionProducto = pedidoDto.DescripcionProducto,
                Subtotal = pedidoDto.Subtotal,
                Iva = pedidoDto.Iva,
                Total = pedidoDto.Total,
                IdEstadoPedido = pedidoDto.IdEstadoPedido,
            };

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            var detalles = pedidoDto.Detalles.Select(d => new DetallePedido
            {
                IdPedido = pedido.IdPedido,
                IdRepuesto = d.IdRepuesto,
                Cantidad = d.Cantidad,
                DescripcionRepuesto = d.DescripcionRepuesto,
                PrecioUnitario = d.PrecioUnitario,
                PrecioFinal = d.PrecioFinal
            }).ToList();

            _context.DetallesPedido.AddRange(detalles);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPedidos), new { id = pedido.IdPedido }, pedido);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedidos(int id, [FromBody] CreatePedidoDto pedidoDto)
        {
            var proforma = await _context.Pedidos.FindAsync(id);

            if (proforma == null)
            {
                return NotFound(new ResponseGlobal<string>
                {
                    codigo = "404",
                    mensaje = "Pedido no encontrado",
                    data = null
                });
            }

            proforma.IdCliente = pedidoDto.IdCliente;
            proforma.DescripcionProducto = pedidoDto.DescripcionProducto;
            proforma.Subtotal = pedidoDto.Subtotal;
            proforma.Iva = pedidoDto.Iva;
            proforma.Total = pedidoDto.Total;
            proforma.IdEstadoPedido = pedidoDto.IdEstadoPedido;

            var detallesExistentes = await _context.DetallesPedido.Where(d => d.IdPedido == id).ToListAsync();
            _context.DetallesPedido.RemoveRange(detallesExistentes);

            var nuevosDetalles = pedidoDto.Detalles.Select(d => new DetallePedido
            {
                IdPedido = id,
                IdRepuesto = d.IdRepuesto,
                Cantidad = d.Cantidad,
                DescripcionRepuesto = d.DescripcionRepuesto,
                PrecioUnitario = d.PrecioUnitario,
                PrecioFinal = d.PrecioFinal
            }).ToList();
            _context.DetallesPedido.AddRange(nuevosDetalles);

            _context.Entry(proforma).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                var response = new ResponseGlobal<CreateProformaDto[]>
                {
                    codigo = "200",
                    mensaje = "Proforma actualizada con exito",
                    data = []
                };

                return Ok(response);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var response = new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al recuperar las series del producto",
                    data = ex.Message
                };

                return StatusCode(500, response);
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProformasId(int id)
        {
            try
            {
                var proforma = await _context.Pedidos.Include(p => p.Cliente).Include(p => p.EstadoPedido).Where(data => data.IdPedido == id).ToListAsync();

                var response = new ResponseGlobal<IEnumerable<Pedido>>
                {
                    codigo = "200",
                    mensaje = "Tipos de servicios recuperados exitosamente",
                    data = proforma
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al recuperar las proformas",
                    data = ex.Message
                };

                return StatusCode(500, response);
            }
        }
        [HttpGet("detallePedido/{id}")]
        public async Task<IActionResult> GetPedidosRepuestos(int id)
        {
            try
            {

                var proforma = await _context.DetallesPedido.Include(p => p.Pedido).Include(p => p.Repuesto).Where(data => data.IdPedido == id).ToListAsync();
                var response = new ResponseGlobal<IEnumerable<DetallePedido>>
                {
                    codigo = "200",
                    mensaje = "Tipos de servicios recuperados exitosamente",
                    data = proforma
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al recuperar las proformas",
                    data = ex.Message
                };

                return StatusCode(500, response);
            }
        }

    }
}
