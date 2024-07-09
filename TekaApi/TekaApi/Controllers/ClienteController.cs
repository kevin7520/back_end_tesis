using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TekaDomain.Dto;
using TekaDomain;
using Microsoft.AspNetCore.Authorization;
using TekaDomain.Entities;

namespace TekaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet()]
        public async Task<IActionResult> GetClientes()
        {
            try
            {
                var clientes = await _context.Clientes
                                             .Include(c => c.Ciudad)
                                             .OrderByDescending(c => c.IdCliente)
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

        // POST: api/Clientes
        [HttpPost]
        public async Task<IActionResult> CreateCliente([FromBody] CreateClienteDto createClienteDto)
        {
            try
            {
                if (createClienteDto == null)
                {
                    return BadRequest(new ResponseGlobal<string>
                    {
                        codigo = "400",
                        mensaje = "Datos de cliente inválidos",
                        data = null
                    });
                }

                var cliente = new Cliente
                {
                    Cedula = createClienteDto.Cedula,
                    Nombres = createClienteDto.Nombres,
                    Telefono = createClienteDto.Telefono,
                    Direccion = createClienteDto.Direccion,
                    Correo = createClienteDto.Correo,
                    IdCiudad = createClienteDto.IdCiudad
                };

                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();

                var response = new ResponseGlobal<Cliente>
                {
                    codigo = "201",
                    mensaje = "Cliente creado exitosamente",
                    data = cliente
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al crear el cliente",
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
