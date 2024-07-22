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

        // GET: api/Pedido
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoDto>>> GetPedidos()
        {
            var pedidos = await _context.Pedidos
                                        .Include(p => p.Cliente)
                                            .ThenInclude(c => c.Ciudad)
                                        .Include(p => p.Producto)
                                            .ThenInclude(pr => pr.Categoria)
                                        .Include(p => p.Producto)
                                            .ThenInclude(pr => pr.EstadoProducto)
                                        .Select(p => new PedidoDto
                                        {
                                            IdPedido = p.IdPedido,
                                            IdCliente = p.IdCliente,
                                            Cliente = new ClienteDto
                                            {
                                                IdCliente = p.Cliente.IdCliente,
                                                Cedula = p.Cliente.Cedula,
                                                Nombres = p.Cliente.Nombres,
                                                Telefono = p.Cliente.Telefono,
                                                Direccion = p.Cliente.Direccion,
                                                Correo = p.Cliente.Correo,
                                                Ciudad = new CiudadDto
                                                {
                                                    IdCiudad = p.Cliente.Ciudad.IdCiudad,
                                                    NombreCiudad = p.Cliente.Ciudad.NombreCiudad
                                                }
                                            },
                                            TipoPedido = p.TipoPedido,
                                            FechaPedido = p.FechaPedido,
                                            IdProducto = p.IdProducto,
                                            Producto = new ProductoDto
                                            {
                                                IdProducto = p.Producto.IdProducto,
                                                CodigoProducto = p.Producto.CodigoProducto,
                                                Modelo = p.Producto.Modelo,
                                                SerieProducto = p.Producto.SerieProducto,
                                                Precio = p.Producto.Precio,
                                            },
                                            Detalles = _context.DetallesPedido
                                                               .Where(d => d.IdPedido == p.IdPedido)
                                                               .Select(d => new DetallePedidoDto
                                                               {
                                                                   IdDetallePedido = d.IdDetallePedido,
                                                                   IdPedido = d.IdPedido,
                                                                   Cantidad = d.Cantidad,
                                                                   DescripcionRepuesto = d.DescripcionRepuesto
                                                               }).ToList()
                                        })
                                        .ToListAsync();

            return Ok(new ResponseGlobal<IEnumerable<PedidoDto>>
            {
                codigo = "200",
                mensaje = "Pedidos recuperados exitosamente",
                data = pedidos
            });
        }

        // GET: api/Pedido/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoDto>> GetPedido(int id)
        {
            var pedido = await _context.Pedidos
                                       .Include(p => p.Cliente)
                                           .ThenInclude(c => c.Ciudad)
                                       .Include(p => p.Producto)
                                           .ThenInclude(pr => pr.Categoria)
                                       .Include(p => p.Producto)
                                           .ThenInclude(pr => pr.EstadoProducto)
                                       .Select(p => new PedidoDto
                                       {
                                           IdPedido = p.IdPedido,
                                           IdCliente = p.IdCliente,
                                           Cliente = new ClienteDto
                                           {
                                               IdCliente = p.Cliente.IdCliente,
                                               Cedula = p.Cliente.Cedula,
                                               Nombres = p.Cliente.Nombres,
                                               Telefono = p.Cliente.Telefono,
                                               Direccion = p.Cliente.Direccion,
                                               Correo = p.Cliente.Correo,
                                               Ciudad = new CiudadDto
                                               {
                                                   IdCiudad = p.Cliente.Ciudad.IdCiudad,
                                                   NombreCiudad = p.Cliente.Ciudad.NombreCiudad
                                               }
                                           },
                                           TipoPedido = p.TipoPedido,
                                           FechaPedido = p.FechaPedido,
                                           IdProducto = p.IdProducto,
                                           Producto = new ProductoDto
                                           {
                                               IdProducto = p.Producto.IdProducto,
                                               CodigoProducto = p.Producto.CodigoProducto,
                                               Modelo = p.Producto.Modelo,
                                               SerieProducto = p.Producto.SerieProducto,
                                               Precio = p.Producto.Precio,
                                           },
                                           Detalles = _context.DetallesPedido
                                                              .Where(d => d.IdPedido == p.IdPedido)
                                                              .Select(d => new DetallePedidoDto
                                                              {
                                                                  IdDetallePedido = d.IdDetallePedido,
                                                                  IdPedido = d.IdPedido,
                                                                  Cantidad = d.Cantidad,
                                                                  DescripcionRepuesto = d.DescripcionRepuesto
                                                              }).ToList()
                                       })
                                       .FirstOrDefaultAsync(p => p.IdPedido == id);

            if (pedido == null)
            {
                return NotFound(new ResponseGlobal<string>
                {
                    codigo = "404",
                    mensaje = "Pedido no encontrado",
                    data = null
                });
            }

            return Ok(new ResponseGlobal<PedidoDto>
            {
                codigo = "200",
                mensaje = "Pedido recuperado exitosamente",
                data = pedido
            });
        }

        // POST: api/Pedido
        [HttpPost]
        public async Task<ActionResult<Pedido>> CreatePedido([FromBody] CreatePedidoDto pedidoDto)
        {
            var pedido = new Pedido
            {
                IdCliente = pedidoDto.IdCliente,
                TipoPedido = pedidoDto.TipoPedido,
                FechaPedido = pedidoDto.FechaPedido,
                IdProducto = pedidoDto.IdProducto
            };

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            var detalles = pedidoDto.Detalles.Select(d => new DetallePedido
            {
                IdPedido = pedido.IdPedido,
                Cantidad = d.Cantidad,
                DescripcionRepuesto = d.DescripcionRepuesto
            }).ToList();

            _context.DetallesPedido.AddRange(detalles);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPedido), new { id = pedido.IdPedido }, pedido);
        }

        // PUT: api/Pedido/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePedido(int id, [FromBody] UpdatePedidoDto pedidoDto)
        {
            if (id != pedidoDto.IdPedido)
            {
                return BadRequest(new ResponseGlobal<string>
                {
                    codigo = "400",
                    mensaje = "ID de pedido no coincide",
                    data = null
                });
            }

            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
            {
                return NotFound(new ResponseGlobal<string>
                {
                    codigo = "404",
                    mensaje = "Pedido no encontrado",
                    data = null
                });
            }

            pedido.IdCliente = pedidoDto.IdCliente;
            pedido.TipoPedido = pedidoDto.TipoPedido;
            pedido.FechaPedido = pedidoDto.FechaPedido;
            pedido.IdProducto = pedidoDto.IdProducto;

            // Actualizar los detalles del pedido
            var detallesExistentes = await _context.DetallesPedido.Where(d => d.IdPedido == id).ToListAsync();
            _context.DetallesPedido.RemoveRange(detallesExistentes);

            var nuevosDetalles = pedidoDto.Detalles.Select(d => new DetallePedido
            {
                IdPedido = id,
                Cantidad = d.Cantidad,
                DescripcionRepuesto = d.DescripcionRepuesto
            }).ToList();
            _context.DetallesPedido.AddRange(nuevosDetalles);

            _context.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
                {
                    return NotFound(new ResponseGlobal<string>
                    {
                        codigo = "404",
                        mensaje = "Pedido no encontrado",
                        data = null
                    });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Pedido/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
            {
                return NotFound(new ResponseGlobal<string>
                {
                    codigo = "404",
                    mensaje = "Pedido no encontrado",
                    data = null
                });
            }

            var detalles = await _context.DetallesPedido.Where(d => d.IdPedido == id).ToListAsync();
            _context.DetallesPedido.RemoveRange(detalles);
            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return Ok(new ResponseGlobal<string>
            {
                codigo = "200",
                mensaje = "Pedido eliminado exitosamente",
                data = null
            });
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.IdPedido == id);
        }
    }
}
