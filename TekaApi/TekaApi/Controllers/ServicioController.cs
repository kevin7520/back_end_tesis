using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TekaDomain;
using TekaDomain.Dto;

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
    }
}
