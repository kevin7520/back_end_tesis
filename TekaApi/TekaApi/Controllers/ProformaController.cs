using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TekaDomain.Dto;
using TekaDomain;
using TekaDomain.Entities;

namespace TekaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProformaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProformaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProformas()
        {
            try
            {
                var proforma = await _context.Proformas.Include(p => p.Cliente).Include(p => p.EstadoProforma).ToListAsync();

                var response = new ResponseGlobal<IEnumerable<Proforma>>
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
        public async Task<IActionResult> PostProformas([FromBody] CreateProformaDto pedidoDto)
        {
            var pedido = new Proforma
            {
                IdCliente = pedidoDto.IdCliente,
                DescripcionProducto = pedidoDto.DescripcionProducto,
                Subtotal = pedidoDto.Subtotal,
                Iva = pedidoDto.Iva,
                Total = pedidoDto.Total,
                IdEstadoProforma = pedidoDto.IdEstadoProforma,
            };

            _context.Proformas.Add(pedido);
            await _context.SaveChangesAsync();

            var detalles = pedidoDto.Detalles.Select(d => new DetalleProforma
            {
                IdProforma = pedido.IdProforma,
                IdRepuesto = d.IdRepuesto,
                Cantidad = d.Cantidad,
                DescripcionRepuesto = d.DescripcionRepuesto,
                PrecioUnitario = d.PrecioUnitario,
                PrecioFinal = d.PrecioFinal
            }).ToList();

            _context.DetallesProforma.AddRange(detalles);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProformas), new { id = pedido.IdProforma }, pedido);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProformasId(int id)
        {
            try
            {
                var proforma = await _context.Proformas.Include(p => p.Cliente).Include(p => p.EstadoProforma).Where(data => data.IdProforma == id).ToListAsync();

                var response = new ResponseGlobal<IEnumerable<Proforma>>
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
        [HttpGet("detalleProforma/{id}")]
        public async Task<IActionResult> GetProformasRepuestos(int id)
        {
            try
            {

                var proforma = await _context.DetallesProforma.Include(p => p.Proforma).Include(p => p.Repuesto).Where(data => data.IdProforma == id).ToListAsync();
                var response = new ResponseGlobal<IEnumerable<DetalleProforma>>
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
