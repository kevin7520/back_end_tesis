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
    public class ServicioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ServicioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Servicio/TiposServicios
        [HttpGet("TiposServicios")]
        public async Task<IActionResult> GetTiposServicios()
        {
            try
            {
                var tiposServicios = await _context.TipoServicios.ToListAsync();

                var tiposServiciosDto = tiposServicios.ConvertAll(tipo => new TipoServicioDto
                {
                    IdTipoServicio = tipo.IdTipoServicio,
                    NombreTipoServicio = tipo.NombreTipoServicio
                });

                var response = new ResponseGlobal<IEnumerable<TipoServicioDto>>
                {
                    codigo = "200",
                    mensaje = "Tipos de servicios recuperados exitosamente",
                    data = tiposServiciosDto
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al recuperar los tipos de servicios",
                    data = ex.Message
                };

                return StatusCode(500, response);
            }
        }

        // POST: api/Servicio/CrearServicio
        [HttpPost("CrearServicio")]
        public async Task<IActionResult> CrearServicio([FromBody] CreateServicioDto servicioDto)
        {
            try
            {
                // Verificar que el cliente existe
                if (servicioDto.IdCliente != null)
                {
                    var cliente = await _context.Clientes.FindAsync(servicioDto.IdCliente);
                    if (cliente == null)
                    {
                        return NotFound(new ResponseGlobal<string>
                        {
                            codigo = "404",
                            mensaje = "Cliente no encontrado",
                            data = null
                        });
                    }
                }

                if (servicioDto.IdTecnico != null)
                {
                    // Verificar que el técnico existe
                    var tecnico = await _context.Tecnicos.FindAsync(servicioDto.IdTecnico);
                    if (tecnico == null)
                    {
                        return NotFound(new ResponseGlobal<string>
                        {
                            codigo = "404",
                            mensaje = "Técnico no encontrado",
                            data = null
                        });
                    }
                }

                if (servicioDto.IdEstadoServicio != null)
                {
                    // Verificar que el estado del servicio existe
                    var estadoServicio = await _context.EstadoServicios.FindAsync(servicioDto.IdEstadoServicio);
                    if (estadoServicio == null)
                    {
                        return NotFound(new ResponseGlobal<string>
                        {
                            codigo = "404",
                            mensaje = "Estado del servicio no encontrado",
                            data = null
                        });
                    }
                }

                if (servicioDto.IdProducto != null)
                {
                    // Verificar que el estado del servicio existe
                    var producto = await _context.Productos.FindAsync(servicioDto.IdProducto);
                    if (producto == null)
                    {
                        return NotFound(new ResponseGlobal<string>
                        {
                            codigo = "404",
                            mensaje = "Producto no encontrado",
                            data = null
                        });
                    }
                }

                // Crear la entidad de servicio
                var servicio = new Servicio
                {
                    IdCliente = servicioDto.IdCliente,
                    IdTipoServicio = servicioDto.IdTipoServicio,
                    FechaTentativaAtencion = servicioDto.FechaTentativaAtencion,
                    FechaSolicitudServicio = DateTime.Now,
                    IdTecnico = servicioDto.IdTecnico,
                    IdEstadoServicio = servicioDto.IdEstadoServicio
                };

                // Agregar el servicio a la base de datos
                _context.Servicios.Add(servicio);
                await _context.SaveChangesAsync();

                // Retornar la respuesta
                return Ok(new ResponseGlobal<CreateServicioDto>
                {
                    codigo = "200",
                    mensaje = "Servicio creado exitosamente",
                    data = servicioDto
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al crear el servicio",
                    data = ex.Message
                });
            }
        }

        // PUT: api/Servicio/EditarServicio/{id}
        [HttpPut("EditarServicio/{id}")]
        public async Task<IActionResult> EditarServicio(int id, [FromBody] CreateServicioDto servicioDto)
        {
            try
            {
                // Verificar que el servicio existe
                var servicio = await _context.Servicios.FindAsync(id);
                if (servicio == null)
                {
                    return NotFound(new ResponseGlobal<string>
                    {
                        codigo = "404",
                        mensaje = "Servicio no encontrado",
                        data = null
                    });
                }

                // Verificar que el cliente existe
                if (servicioDto.IdCliente != null)
                {
                    var cliente = await _context.Clientes.FindAsync(servicioDto.IdCliente);
                    if (cliente == null)
                    {
                        return NotFound(new ResponseGlobal<string>
                        {
                            codigo = "404",
                            mensaje = "Cliente no encontrado",
                            data = null
                        });
                    }
                }

                if (servicioDto.IdTecnico != null)
                {
                    // Verificar que el técnico existe
                    var tecnico = await _context.Tecnicos.FindAsync(servicioDto.IdTecnico);
                    if (tecnico == null)
                    {
                        return NotFound(new ResponseGlobal<string>
                        {
                            codigo = "404",
                            mensaje = "Técnico no encontrado",
                            data = null
                        });
                    }
                }

                if (servicioDto.IdEstadoServicio != null)
                {
                    // Verificar que el estado del servicio existe
                    var estadoServicio = await _context.EstadoServicios.FindAsync(servicioDto.IdEstadoServicio);
                    if (estadoServicio == null)
                    {
                        return NotFound(new ResponseGlobal<string>
                        {
                            codigo = "404",
                            mensaje = "Estado del servicio no encontrado",
                            data = null
                        });
                    }
                }

                if (servicioDto.IdProducto != null)
                {
                    // Verificar que el estado del servicio existe
                    var producto = await _context.Productos.FindAsync(servicioDto.IdProducto);
                    if (producto == null)
                    {
                        return NotFound(new ResponseGlobal<string>
                        {
                            codigo = "404",
                            mensaje = "Producto no encontrado",
                            data = null
                        });
                    }
                }

                // Actualizar la entidad de servicio
                servicio.IdTipoServicio = servicioDto.IdTipoServicio;
                servicio.FechaTentativaAtencion = servicioDto.FechaTentativaAtencion;
                servicio.IdTecnico = servicioDto.IdTecnico; // Asignar técnico
                servicio.IdEstadoServicio = servicioDto.IdEstadoServicio; // Asignar estado del servicio
                servicio.Valor = servicioDto.Valor;
                servicio.IdProducto = servicioDto.IdProducto;

                // Guardar los cambios en la base de datos
                _context.Servicios.Update(servicio);
                await _context.SaveChangesAsync();

                // Retornar la respuesta
                return Ok(new ResponseGlobal<CreateServicioDto>
                {
                    codigo = "200",
                    mensaje = "Servicio actualizado exitosamente",
                    data = servicioDto
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al actualizar el servicio",
                    data = ex.Message
                });
            }
        }
       
        // GET: api/Servicios
        [HttpGet]
        public async Task<IActionResult> ListarServicios()
        {
            try
            {
                var servicios = await _context.Servicios
                    .Include(s => s.Cliente)
                    .ThenInclude(c => c.Ciudad)
                    .Include(s => s.Tecnico)
                    .ThenInclude(t => t.EstadoTecnico)
                    .Include(s => s.TipoServicio)
                    .Include(s => s.EstadoServicio)
                    .Include(s => s.Producto)
                    .ToListAsync();

                var serviciosDto = servicios.ConvertAll(servicio => new ServicioDto
                {
                    IdServicio = servicio.IdServicio,
                    Cliente = servicio.Cliente != null ? new ClienteDto
                    {
                        IdCliente = servicio.Cliente.IdCliente,
                        Cedula = servicio.Cliente.Cedula,
                        Nombres = servicio.Cliente.Nombres,
                        Telefono = servicio.Cliente.Telefono,
                        Direccion = servicio.Cliente.Direccion,
                        Correo = servicio.Cliente.Correo,
                        Ciudad = servicio.Cliente.Ciudad != null ? new CiudadDto
                        {
                            IdCiudad = servicio.Cliente.Ciudad.IdCiudad,
                            NombreCiudad = servicio.Cliente.Ciudad.NombreCiudad
                        } : null
                    } : null,
                    TipoServicio = servicio.TipoServicio != null ? new TipoServicioDto
                    {
                        IdTipoServicio = servicio.TipoServicio.IdTipoServicio,
                        NombreTipoServicio = servicio.TipoServicio.NombreTipoServicio
                    } : null,
                    Producto = servicio.Producto != null ? new ProductoDto
                    {
                        IdProducto = servicio.Producto.IdProducto,
                        IdCategoria = servicio.Producto.IdCategoria,
                        CodigoProducto = servicio.Producto.CodigoProducto,
                        Modelo = servicio.Producto.Modelo,
                        IdEstadoProducto = servicio.Producto.IdEstadoProducto,
                        SerieProducto = servicio.Producto.SerieProducto,
                        Precio = servicio.Producto.Precio
                    } : null,
                    FechaSolicitudServicio = servicio.FechaSolicitudServicio,
                    FechaTentativaAtencion = servicio.FechaTentativaAtencion,
                    Valor = servicio.Valor,
                    Tecnico = servicio.Tecnico != null ? new TecnicoDto
                    {
                        IdTecnico = servicio.Tecnico.IdTecnico,
                        NombreTecnico = servicio.Tecnico.NombreTecnico,
                        Cedula = servicio.Tecnico.Cedula,
                        TelefonoTecnico = servicio.Tecnico.TelefonoTecnico,
                        EstadoTecnico = servicio.Tecnico.EstadoTecnico != null ? new EstadoTecnicoDto
                        {
                            IdEstado = servicio.Tecnico.EstadoTecnico.IdEstado,
                            NombreEstado = servicio.Tecnico.EstadoTecnico.NombreEstado
                        } : null
                    } : null,
                    EstadoServicioDto = servicio.EstadoServicio != null ? new EstadoServicioDto
                    {
                        IdEstadoServicio = servicio.EstadoServicio.IdEstadoServicio,
                        NombreEstadoServicio = servicio.EstadoServicio.NombreEstadoServicio
                    } : null
                });

                var response = new ResponseGlobal<IEnumerable<ServicioDto>>
                {
                    codigo = "200",
                    mensaje = "Servicios recuperados exitosamente",
                    data = serviciosDto
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al recuperar los servicios",
                    data = ex.Message
                };

                return StatusCode(500, response);
            }
        }
    }
}