using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TekaDomain;
using TekaDomain.Dto;
using TekaDomain.Entities;

namespace TekaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public SecurityController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto createUserDto)
        {
            try
            {
                var usuario = new Usuario
                {
                    NombreUsuario = createUserDto.NombreUsuario,
                    Contraseña = BCrypt.Net.BCrypt.HashPassword(createUserDto.Contraseña),
                    Correo = createUserDto.Correo,
                    IdRol = createUserDto.IdRol,
                    IdEstado = createUserDto.IdEstado
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                var response = new ResponseGlobal<Usuario>
                {
                    codigo = "200",
                    mensaje = "Usuario creado exitosamente",
                    data = usuario
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al crear el usuario",
                    data = ex.Message
                };

                return StatusCode(500, response);
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto login)
        {
            try
            {
                var user = _context.Usuarios
                    .Include(u => u.Rol)
                    .SingleOrDefault(u => u.Correo == login.correo);

                if (user == null || !BCrypt.Net.BCrypt.Verify(login.password, user.Contraseña))
                {
                    var responseUnauthorized = new ResponseGlobal<string>
                    {
                        codigo = "401",
                        mensaje = "Contraseñas no coinciden",
                        data = null
                    };

                    return Unauthorized(responseUnauthorized);
                }

                var token = GenerateJwtToken(user);

                var response = new ResponseGlobal<string[]>
                {
                    codigo = "200",
                    mensaje = "Inicio de sesión exitoso",
                    data = [token, user.Rol.IdRol.ToString()]
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseGlobal<string>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al iniciar sesión",
                    data = ex.Message
                };

                return StatusCode(500, response);
            }
        }

        private string GenerateJwtToken(Usuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.NombreUsuario),
                    new Claim(ClaimTypes.Role, user.Rol.NombreRol)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}