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

        [HttpGet("Serie/Validador/{serie}")]
        public async Task<IActionResult> GetSeries(string serie)
        {
            try
            {
                bool existeAlMenosUnElemento = await _context.ServicioProductos
                                             .AnyAsync(data => data.Serie == serie);

                if (existeAlMenosUnElemento)
                {
                    var response = new ResponseGlobal<IEnumerable<string>>
                    {
                        codigo = "999",
                        mensaje = "Número de serie ya existe",
                        data = null
                    };
                    return Ok(response);
                }
                else
                {
                    var response = new ResponseGlobal<IEnumerable<string>>
                    {
                        codigo = "200",
                        mensaje = "OK",
                        data = null
                    };
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                var response = new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al recuperar las series del producto",
                    data = ex.Message
                };

                return StatusCode(500, response);
            }
        }

        // GET: api/Servicio/TiposServicios
        [HttpGet("EstadosServicios")]
        public async Task<IActionResult> GetEstadosServicios()
        {
            try
            {
                var estadisServicios = await _context.EstadoServicios.ToListAsync();

                var estadisServiciosDto = estadisServicios.ConvertAll(tipo => new EstadoServicioDto
                {
                    IdEstadoServicio = tipo.IdEstadoServicio,
                    NombreEstadoServicio = tipo.NombreEstadoServicio
                });

                var response = new ResponseGlobal<IEnumerable<EstadoServicioDto>>
                {
                    codigo = "200",
                    mensaje = "Estados de servicios recuperados exitosamente",
                    data = estadisServiciosDto
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al recuperar los estados de servicios",
                    data = ex.Message
                };

                return StatusCode(500, response);
            }
        }

        [HttpGet("almacenes")]
        public async Task<IActionResult> GetAlmacenes()
        {
            try
            {
                var estadisServicios = await _context.Almacen.ToListAsync();

                var estadisServiciosDto = estadisServicios.ConvertAll(tipo => new AlmacenDto
                {
                    IdAlmacen = tipo.IdAlmacen,
                    NombreAlmacen = tipo.NombreAlmacen
                });

                var response = new ResponseGlobal<IEnumerable<AlmacenDto>>
                {
                    codigo = "200",
                    mensaje = "Almacenes recuperados exitosamente",
                    data = estadisServiciosDto
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al recuperar los almacenes",
                    data = ex.Message
                };

                return StatusCode(500, response);
            }
        }

        // POST: api/Servicio
        [HttpPost]
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

                // Verificar que el técnico existe
                if (servicioDto.IdTecnico != null)
                {
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

                // Verificar que el estado del servicio existe
                if (servicioDto.IdEstadoServicio != null)
                {
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

                // Crear la entidad de servicio
                var servicio = new Servicio
                {
                    IdCliente = servicioDto.IdCliente,
                    IdTipoServicio = servicioDto.IdTipoServicio,
                    FechaTentativaAtencion = servicioDto.FechaTentativaAtencion,
                    FechaSolicitudServicio = DateTime.Now,
                    IdTecnico = servicioDto.IdTecnico,
                    IdEstadoServicio = servicioDto.IdEstadoServicio,
                };

                // Agregar el servicio a la base de datos
                _context.Servicios.Add(servicio);
                await _context.SaveChangesAsync();

                // Asignar productos al servicio
                if (servicioDto.Productos != null && servicioDto.Productos.Any())
                {
                    foreach (var productoDto in servicioDto.Productos)
                    {
                        var producto = await _context.Productos.FindAsync(productoDto.IdProducto);
                        if (producto == null)
                        {
                            return NotFound(new ResponseGlobal<string>
                            {
                                codigo = "404",
                                mensaje = $"Producto con Id {productoDto.IdProducto} no encontrado",
                                data = null
                            });
                        }

                        var servicioProducto = new ServicioProducto
                        {
                            IdServicio = servicio.IdServicio,
                            IdProducto = productoDto.IdProducto,
                            Valor = productoDto.Valor, // Asegúrate de que este campo esté en el DTO
                            Serie = productoDto.Serie // Asegúrate de que este campo esté en el DTO
                        };

                        _context.ServicioProductos.Add(servicioProducto);
                    }

                    await _context.SaveChangesAsync();
                }

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

        [HttpPost("factura")]
        public async Task<IActionResult> CrearFactura([FromBody] Factura facturaDto)
        {
            try
            {
                _context.Factura.Add(facturaDto);
                await _context.SaveChangesAsync();

                return Ok(new ResponseGlobal<Factura>
                {
                    codigo = "200",
                    mensaje = "Factura creado exitosamente",
                    data = facturaDto
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al crear la factura",
                    data = ex.Message
                });
            }
        }

        // PUT: api/Servicio/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarServicio(int id, [FromBody] EditServicioDto servicioDto)
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

                // Verificar que el técnico existe
                if (servicioDto.IdTecnico != null)
                {
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

                // Verificar que el estado del servicio existe
                if (servicioDto.IdEstadoServicio != null)
                {
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

                // Actualizar la entidad de servicio
                servicio.IdCliente = servicioDto.IdCliente;
                servicio.IdTipoServicio = servicioDto.IdTipoServicio;
                servicio.FechaTentativaAtencion = servicioDto.FechaTentativaAtencion;
                servicio.IdTecnico = servicioDto.IdTecnico;
                servicio.IdEstadoServicio = servicioDto.IdEstadoServicio;
                servicio.Valor = servicioDto.valor;
                servicio.IdFactura = servicioDto.IdFactura;
                servicio.IdAlmacen = servicioDto.IdAlmacen;

                // Guardar los cambios en la base de datos
                _context.Servicios.Update(servicio);
                await _context.SaveChangesAsync();

                // Actualizar productos asignados al servicio
                var existingProductos = _context.ServicioProductos.Where(sp => sp.IdServicio == id).ToList();
                _context.ServicioProductos.RemoveRange(existingProductos);

                if (servicioDto.Productos != null && servicioDto.Productos.Any())
                {
                    foreach (var productoDto in servicioDto.Productos)
                    {
                        var producto = await _context.Productos.FindAsync(productoDto.IdProducto);
                        if (producto == null)
                        {
                            return NotFound(new ResponseGlobal<string>
                            {
                                codigo = "404",
                                mensaje = $"Producto con Id {productoDto.IdProducto} no encontrado",
                                data = null
                            });
                        }

                        var servicioProducto = new ServicioProducto
                        {
                            IdServicio = servicio.IdServicio,
                            IdProducto = productoDto.IdProducto,
                            Valor = productoDto.Valor,
                            Serie = productoDto.Serie
                        };

                        _context.ServicioProductos.Add(servicioProducto);
                    }

                    await _context.SaveChangesAsync();
                }

                // Editar productos asignados al servicio
                var existingRespuestos = _context.ServicioRepuestos.Where(sp => sp.IdServicio == id).ToList();
                _context.ServicioRepuestos.RemoveRange(existingRespuestos);

                if (servicioDto.RepuestoDto != null && servicioDto.RepuestoDto.Any())
                {
                    foreach (var respuestoDto in servicioDto.RepuestoDto)
                    {
                        var producto = await _context.Repuestos.FindAsync(respuestoDto.IdRepuesto);
                        if (producto == null)
                        {
                            return NotFound(new ResponseGlobal<string>
                            {
                                codigo = "404",
                                mensaje = $"Respuestoss con Id {respuestoDto.IdRepuesto} no encontrado",
                                data = null
                            });
                        }

                        var servicioProducto = new ServicioRepuesto
                        {
                            IdServicio = servicio.IdServicio,
                            IdRepuesto = respuestoDto.IdRepuesto,
                        };

                        _context.ServicioRepuestos.Add(servicioProducto);
                    }

                    await _context.SaveChangesAsync();
                }

                // Retornar la respuesta
                return Ok(new ResponseGlobal<EditServicioDto>
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
                    .OrderByDescending(c => c.IdServicio)
                    .ToListAsync();

                var serviciosDto = servicios.Select(servicio => new ServicioDto
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
                    Productos = _context.ServicioProductos.Where(sp => sp.IdServicio == servicio.IdServicio).Include(sp => sp.Producto).Select(sp => new ProductoDto
                    {
                        IdProducto = sp.Producto.IdProducto,
                        IdCategoria = sp.Producto.IdCategoria,
                        CodigoProducto = sp.Producto.CodigoProducto,
                        Modelo = sp.Producto.Modelo,
                        IdEstadoProducto = sp.Producto.IdEstadoProducto,
                        SerieProducto = sp.Producto.SerieProducto,
                        Precio = sp.Producto.Precio,
                        Valor = sp.Valor,
                        Serie = sp.Serie
                    }).ToList(),
                    Horarios = _context.HorarioServicios.Where(h=> h.IdServicio == servicio.IdServicio).Include( h=> h.Horario).Select( h=> new HorarioDto
                    {
                        Fecha = h.Horario.Fecha,
                        HoraFin = h.Horario.HoraFin,
                        HoraInicio = h.Horario.HoraInicio,
                        IdHorario = h.Horario.IdHorario,
                    }).ToList(),
                    FechaSolicitudServicio = servicio.FechaSolicitudServicio,
                    FechaTentativaAtencion = servicio.FechaTentativaAtencion,
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
                    } : null,
                    Almacen = servicio.Almacen != null ?
                    new AlmacenDto {
                        NombreAlmacen = servicio.Almacen.NombreAlmacen
                    }
                    : null,
                    Factura = servicio.Factura != null ?
                    new FacturaDto
                    {
                        NumeroFactura = servicio.Factura.NumeroFactura
                    }
                    : null,
                    Repuestos = _context.ServicioRepuestos.Where(r => r.IdServicio == servicio.IdServicio).Include(r => r.Repuesto).Select(r => new RepuestoDto
                    {
                        IdRepuesto = r.Repuesto.IdRepuesto,
                        CodigoRepuesto = r.Repuesto.CodigoRepuesto,
                        Cantidad = r.Repuesto.Cantidad,
                        NombreRepuesto = r.Repuesto.NombreRepuesto,
                        Precio = r.Repuesto.Precio
                    }).ToList(),
                    Valor = servicio.Valor
                }).ToList();

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

        // Post: api/Servicio/AsignarHorarioTecnico
        [HttpPost("AsignarHorarioTecnico")]
        public async Task<IActionResult> AsignarHorarioTecnico(CreateHorarioServicioDto horarioServicioDto)
        {
            try
            {
                var horarioServicio = new HorarioServicio
                {
                    IdHorario = horarioServicioDto.IdHorario,
                    IdServicio = horarioServicioDto.IdServicio,
                };

                _context.HorarioServicios.Add(horarioServicio);
                await _context.SaveChangesAsync();

                var response = new ResponseGlobal<HorarioServicio>
                {
                    codigo = "201",
                    mensaje = "Horario asignado exitosamente",
                    data = horarioServicio
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al asignado los horarios",
                    data = ex.Message
                };

                return StatusCode(500, response);
            }
        }
    }
}