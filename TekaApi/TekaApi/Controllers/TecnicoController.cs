using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TekaDomain.Dto;
using TekaDomain;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using TekaDomain.Entities;

namespace TekaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TecnicoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TecnicoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Tecnicos
        [HttpGet]
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

        // POST: api/Tecnicos
        [HttpPost]
        public async Task<IActionResult> CreateTecnico([FromBody] CreateTecnicoDto createTecnicoDto)
        {
            try
            {
                if (createTecnicoDto == null)
                {
                    return BadRequest(new ResponseGlobal<string>
                    {
                        codigo = "400",
                        mensaje = "Datos de técnico inválidos",
                        data = null
                    });
                }

                var tecnico = new Tecnico
                {
                    NombreTecnico = createTecnicoDto.NombreTecnico,
                    Cedula = createTecnicoDto.Cedula,
                    TelefonoTecnico = createTecnicoDto.TelefonoTecnico,
                    IdEstado = createTecnicoDto.IdEstado
                };

                _context.Tecnicos.Add(tecnico);
                await _context.SaveChangesAsync();


                var estadoTecnico =  _context.EstadosTecnico.SingleOrDefault(u => u.IdEstado == createTecnicoDto.IdEstado); ;
                var tecnicoDto = new TecnicoDto
                {
                    IdTecnico = tecnico.IdTecnico,
                    NombreTecnico = tecnico.NombreTecnico,
                    Cedula = tecnico.Cedula,
                    TelefonoTecnico = tecnico.TelefonoTecnico,
                    EstadoTecnico = estadoTecnico != null ? new EstadoTecnicoDto
                    {
                        IdEstado = estadoTecnico.IdEstado,
                        NombreEstado = estadoTecnico.NombreEstado
                    } : null
                };

                var response = new ResponseGlobal<TecnicoDto>
                {
                    codigo = "201",
                    mensaje = "Técnico creado exitosamente",
                    data = tecnicoDto
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al crear el técnico",
                    data = ex.Message
                };

                return StatusCode(500, response);
            }
        }

        // GET: api/Tecnico/EstadosTecnicos
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

        // GET: api/Tecnico/HorariosPorTecnico/{idTecnico}
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

        // Post: api/Tecnico/CrearHorario
        [HttpPost("CrearHorario")]
        public async Task<IActionResult> CrearHorario(CreateHorarioDto horarioDto)
        {
            try
            {
                var horario = new Horario
                {
                    Fecha = horarioDto.Fecha,
                    HoraInicio = horarioDto.HoraInicio,
                    HoraFin = horarioDto.HoraFin,
                    IdTecnico = horarioDto.IdTecnico,
                };

                _context.Horarios.Add(horario);
                await _context.SaveChangesAsync();

                var response = new ResponseGlobal<Horario>
                {
                    codigo = "201",
                    mensaje = "Horario creado exitosamente",
                    data = horario
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al crear los horarios",
                    data = ex.Message
                };

                return StatusCode(500, response);
            }
        }
    }
}
