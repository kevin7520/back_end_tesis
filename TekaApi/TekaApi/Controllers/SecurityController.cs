using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
                    .SingleOrDefault(u => u.Correo == login.Correo);
                string passwprd1 = login.password;
                string password2 = user.Contraseña;
                bool validar = !BCrypt.Net.BCrypt.Verify(passwprd1, password2);
                if (user == null || !validar)
                {
                    var responseUnauthorized = new ResponseGlobal<string>
                    {
                        codigo = "401",
                        mensaje = "Correo o contraseña incorrectos",
                        data = null
                    };

                    return Unauthorized(responseUnauthorized);
                }

                var token = GenerateJwtToken(user);

                var response = new ResponseGlobal<string[]>
                {
                    codigo = "200",
                    mensaje = "Inicio de sesión exitoso",
                    data = new string[] { token, user.Rol.NombreRol }
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
            var privateKey = Convert.FromBase64String(_configuration["Jwt:Key"]); // Coloca aquí tu clave privada en formato base64

            var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(privateKey, out _);

            var key = new RsaSecurityKey(rsa)
            {
                KeyId = Guid.NewGuid().ToString()
            };

            var creds = new SigningCredentials(key, SecurityAlgorithms.RsaSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.NombreUsuario),
                    new Claim(ClaimTypes.Role, user.Rol.NombreRol)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = creds,
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
