using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TekaDomain;
using TekaDomain.Entities;
using TekaDomain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace TekaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CatalogoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Catalogo/Ciudades
        [HttpGet("Ciudades")]
        public async Task<IActionResult> GetCiudades()
        {
            try
            {
                var ciudades = await _context.Ciudades.ToListAsync();

                var ciudadesDto = ciudades.ConvertAll(ciudad => new CiudadDto
                {
                    IdCiudad = ciudad.IdCiudad,
                    NombreCiudad = ciudad.NombreCiudad
                });

                var response = new ResponseGlobal<IEnumerable<CiudadDto>>
                {
                    codigo = "200",
                    mensaje = "Ciudades recuperadas exitosamente",
                    data = ciudadesDto
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al recuperar las ciudades",
                    data = ex.Message
                };

                return StatusCode(500, response);
            }
        }
    }
}
