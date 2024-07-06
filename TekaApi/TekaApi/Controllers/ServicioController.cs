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

        // GET: api/Servicio/Tecnicos
        [HttpGet("Tecnicos")]
        public async Task<IActionResult> GetTecnicos()
        {
            try
            {
                var tecnicos = await _context.Tecnicos
                                             .Include(t => t.EstadoTecnico)
                                             .ToListAsync();

                var tecnicosDto = tecnicos.ConvertAll(tecnico => new TecnicoDto
                {
                    IdTecnico = tecnico.IdTecnico,
                    NombreTecnico = tecnico.NombreTecnico,
                    Cedula = tecnico.Cedula,
                    TelefonoTecnico = tecnico.TelefonoTecnico,
                    EstadoTecnico = new EstadoTecnicoDto
                    {
                        IdEstado = tecnico.EstadoTecnico.IdEstado,
                        NombreEstado = tecnico.EstadoTecnico.NombreEstado
                    }
                });

                var response = new ResponseGlobal<IEnumerable<TecnicoDto>>
                {
                    codigo = "200",
                    mensaje = "Técnicos recuperados exitosamente",
                    data = tecnicosDto
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al recuperar los técnicos",
                    data = ex.Message
                };

                return StatusCode(500, response);
            }
        }

        // GET: api/Servicio/Clientes
        [HttpGet("Clientes")]
        public async Task<IActionResult> GetClientes()
        {
            try
            {
                var clientes = await _context.Clientes
                                             .Include(c => c.Ciudad)
                                             .ToListAsync();

                var clientesDto = clientes.ConvertAll(cliente => new ClienteDto
                {
                    IdCliente = cliente.IdCliente,
                    Cedula = cliente.Cedula,
                    Nombres = cliente.Nombres,
                    Telefono = cliente.Telefono,
                    Direccion = cliente.Direccion,
                    Correo = cliente.Correo,
                    Ciudad = new CiudadDto
                    {
                        IdCiudad = cliente.Ciudad.IdCiudad,
                        NombreCiudad = cliente.Ciudad.NombreCiudad
                    }
                });

                var response = new ResponseGlobal<IEnumerable<ClienteDto>>
                {
                    codigo = "200",
                    mensaje = "Clientes recuperados exitosamente",
                    data = clientesDto
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al recuperar los clientes",
                    data = ex.Message
                };

                return StatusCode(500, response);
            }
        }

        // GET: api/Servicio/EstadosTecnicos
        [HttpGet("EstadosTecnicos")]
        public async Task<IActionResult> GetEstadosTecnicos()
        {
            try
            {
                var estadosTecnicos = await _context.EstadosTecnico.ToListAsync();

                var estadosTecnicosDto = estadosTecnicos.ConvertAll(estado => new EstadoTecnicoDto
                {
                    IdEstado = estado.IdEstado,
                    NombreEstado = estado.NombreEstado
                });

                var response = new ResponseGlobal<IEnumerable<EstadoTecnicoDto>>
                {
                    codigo = "200",
                    mensaje = "Estados de técnicos recuperados exitosamente",
                    data = estadosTecnicosDto
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al recuperar los estados de técnicos",
                    data = ex.Message
                };

                return StatusCode(500, response);
            }
        }

        // GET: api/Servicio/EstadosClientes
        [HttpGet("EstadosClientes")]
        public async Task<IActionResult> GetEstadosClientes()
        {
            try
            {
                var estadosClientes = await _context.EstadosUsuario.ToListAsync();

                var estadosClientesDto = estadosClientes.ConvertAll(estado => new EstadoUsuarioDto
                {
                    IdEstado = estado.IdEstado,
                    NombreEstado = estado.NombreEstado
                });

                var response = new ResponseGlobal<IEnumerable<EstadoUsuarioDto>>
                {
                    codigo = "200",
                    mensaje = "Estados de clientes recuperados exitosamente",
                    data = estadosClientesDto
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al recuperar los estados de clientes",
                    data = ex.Message
                };

                return StatusCode(500, response);
            }
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

        // GET: api/Servicio/HorariosPorTecnico/{idTecnico}
        [HttpGet("HorariosPorTecnico/{idTecnico}")]
        public async Task<IActionResult> GetHorariosPorTecnico(int idTecnico)
        {
            try
            {
                var horarios = await _context.Horarios
                                             .Where(h => h.IdTecnico == idTecnico)
                                             .Include(h => h.Tecnico)
                                             .ToListAsync();

                var horariosDto = horarios.ConvertAll(horario => new HorarioDto
                {
                    IdHorario = horario.IdHorario,
                    IdTecnico = horario.IdTecnico,
                    Fecha = horario.Fecha,
                    HoraInicio = horario.HoraInicio,
                    HoraFin = horario.HoraFin,
                    NombreTecnico = horario.Tecnico.NombreTecnico
                });

                var response = new ResponseGlobal<IEnumerable<HorarioDto>>
                {
                    codigo = "200",
                    mensaje = "Horarios recuperados exitosamente",
                    data = horariosDto
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al recuperar los horarios",
                    data = ex.Message
                };

                return StatusCode(500, response);
            }
        }

        // POST: api/Servicio/CrearServicio
        [HttpPost("CrearServicio")]
        public async Task<IActionResult> CrearServicio([FromBody] ServicioDto servicioDto)
        {
            try
            {
                // Verificar que el cliente existe
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

                // Crear la entidad de servicio
                var servicio = new Servicio
                {
                    IdCliente = servicioDto.IdCliente,
                    IdTipoServicio = servicioDto.IdTipoServicio,
                    FechaTentativaAtencion = servicioDto.FechaTentativaAtencion,
                    FechaSolicitudServicio = DateTime.Now,
                    IdTecnico = servicioDto.IdTecnico, // Asignar técnico
                    IdEstadoServicio = servicioDto.IdEstadoServicio // Asignar estado del servicio
                };

                // Agregar el servicio a la base de datos
                _context.Servicios.Add(servicio);
                await _context.SaveChangesAsync();

                // Retornar la respuesta
                return Ok(new ResponseGlobal<ServicioDto>
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
        public async Task<IActionResult> EditarServicio(int id, [FromBody] ServicioDto servicioDto)
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

                // Actualizar la entidad de servicio
                servicio.IdTipoServicio = servicioDto.IdTipoServicio;
                servicio.FechaTentativaAtencion = servicioDto.FechaTentativaAtencion;
                servicio.IdTecnico = servicioDto.IdTecnico; // Asignar técnico
                servicio.IdEstadoServicio = servicioDto.IdEstadoServicio; // Asignar estado del servicio

                // Guardar los cambios en la base de datos
                _context.Servicios.Update(servicio);
                await _context.SaveChangesAsync();

                // Retornar la respuesta
                return Ok(new ResponseGlobal<ServicioDto>
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
    }
}
