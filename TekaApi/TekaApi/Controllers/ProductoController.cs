using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TekaDomain;
using TekaDomain.Dto;
using TekaDomain.Entities;

namespace TekaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Producto
        [HttpGet]
        public async Task<IActionResult> GetProductos()
        {
            try
            {
                var productos = await _context.Productos
                                                .Include(p => p.Categoria)
                                                .Include(p => p.EstadoProducto)
                                                .ToListAsync();

                var response = new ResponseGlobal<IEnumerable<Producto>>
                {
                    codigo = "200",
                    mensaje = "Productos recuperados exitosamente",
                    data = productos
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al recuperar los productos",
                    data = ex.Message
                };

                return StatusCode(500, response);
            }
        }

        [HttpGet("Repuestos")]
        public async Task<IActionResult> GetRepuestos()
        {
            try
            {
                var productos = await _context.Repuestos.ToListAsync();

                var response = new ResponseGlobal<IEnumerable<Repuesto>>
                {
                    codigo = "200",
                    mensaje = "Productos recuperados exitosamente",
                    data = productos
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al recuperar los repuestos",
                    data = ex.Message
                };

                return StatusCode(500, response);
            }
        }

        // GET: api/Producto/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducto(int id)
        {
            try
            {
                var producto = await _context.Productos
                                              .Include(p => p.Categoria)
                                              .Include(p => p.EstadoProducto)
                                              .FirstOrDefaultAsync(p => p.IdProducto == id);

                if (producto == null)
                {
                    var responseNotFound = new ResponseGlobal<string>
                    {
                        codigo = "404",
                        mensaje = "Producto no encontrado",
                        data = null
                    };

                    return NotFound(responseNotFound);
                }

                var response = new ResponseGlobal<Producto>
                {
                    codigo = "200",
                    mensaje = "Producto recuperado exitosamente",
                    data = producto
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al recuperar el producto",
                    data = ex.Message
                };

                return StatusCode(500, response);
            }
        }

        // POST: api/Producto
        [HttpPost]
        public async Task<IActionResult> CreateProducto([FromBody] CreateProductoDto productoDto)
        {
            try
            {
                // Validar que el CodigoProducto y SerieProducto sean únicos
                if (await _context.Productos.AnyAsync(p => p.CodigoProducto == productoDto.CodigoProducto))
                {
                    var responseConflict = new ResponseGlobal<string>
                    {
                        codigo = "409",
                        mensaje = "El Código de Producto ya existe",
                        data = null
                    };

                    return Conflict(responseConflict);
                }

                var producto = new Producto
                {
                    IdCategoria = productoDto.IdCategoria,
                    CodigoProducto = productoDto.CodigoProducto,
                    Modelo = productoDto.Modelo,
                    IdEstadoProducto = productoDto.IdEstadoProducto,
                    SerieProducto = productoDto.SerieProducto,
                    Precio = productoDto.Precio
                };

                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();

                var response = new ResponseGlobal<Producto>
                {
                    codigo = "201",
                    mensaje = "Producto creado exitosamente",
                    data = producto
                };

                return CreatedAtAction(nameof(GetProducto), new { id = producto.IdProducto }, response);
            }
            catch (Exception ex)
            {
                var response = new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al crear el producto",
                    data = ex.Message
                };

                return StatusCode(500, response);
            }
        }

        // PUT: api/Producto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(int id, [FromBody] UpdateProductoDto productoDto)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                var responseNotFound = new ResponseGlobal<string>
                {
                    codigo = "404",
                    mensaje = "Producto no encontrado",
                    data = null
                };

                return NotFound(responseNotFound);
            }


            producto.IdCategoria = productoDto.IdCategoria;
            producto.Modelo = productoDto.Modelo;
            producto.IdEstadoProducto = productoDto.IdEstadoProducto;
            producto.SerieProducto = productoDto.SerieProducto;
            producto.Precio = productoDto.Precio;

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                var response = new ResponseGlobal<Producto>
                {
                    codigo = "200",
                    mensaje = "Producto actualizado exitosamente",
                    data = producto
                };

                return Ok(response);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    var responseNotFound = new ResponseGlobal<string>
                    {
                        codigo = "404",
                        mensaje = "Producto no encontrado",
                        data = null
                    };

                    return NotFound(responseNotFound);
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                var response = new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al actualizar el producto",
                    data = ex.Message
                };

                return StatusCode(500, response);
            }
        }

        // DELETE: api/Producto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                var responseNotFound = new ResponseGlobal<string>
                {
                    codigo = "404",
                    mensaje = "Producto no encontrado",
                    data = null
                };

                return NotFound(responseNotFound);
            }


            producto.IdEstadoProducto = 2;
            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                var response = new ResponseGlobal<Producto>
                {
                    codigo = "200",
                    mensaje = "Producto eliminado exitosamente",
                    data = producto
                };

                return Ok(response);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    var responseNotFound = new ResponseGlobal<string>
                    {
                        codigo = "404",
                        mensaje = "Producto no encontrado",
                        data = null
                    };

                    return NotFound(responseNotFound);
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                var response = new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al eliminar el producto",
                    data = ex.Message
                };

                return StatusCode(500, response);
            }
        }

        // GET: api/Producto/Categorias
        [HttpGet("Categorias")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            return await _context.Categorias.ToListAsync();
        }

        // GET: api/Producto/Estados
        [HttpGet("Estados")]
        public async Task<ActionResult<IEnumerable<EstadoProducto>>> GetEstados()
        {
            return await _context.EstadosProducto.ToListAsync();
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.IdProducto == id);
        }
    }
}