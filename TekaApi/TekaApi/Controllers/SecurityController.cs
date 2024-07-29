using Microsoft.AspNetCore.Authorization;
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
                if(user == null)
                {
                    var responseUnauthorized = new ResponseGlobal<string>
                    {
                        codigo = "401",
                        mensaje = "Correo Incorrecto",
                        data = null
                    };

                    return Unauthorized(responseUnauthorized);
                }
                else
                {
                    string password2 = user.Contraseña;
                    bool validar = BCrypt.Net.BCrypt.Verify(passwprd1, password2);
                    if (!validar)
                    {
                        var responseUnauthorized = new ResponseGlobal<string>
                        {
                            codigo = "401",
                            mensaje = "Contraseña Incorrecta",
                            data = null
                        };

                        return Unauthorized(responseUnauthorized);
                    }

                    var token = GenerateJwtToken(user);

                    var response = new ResponseGlobal<string[]>
                    {
                        codigo = "200",
                        mensaje = "Inicio de sesión exitoso",
                        data = new string[] { token, user.Rol.NombreRol, user.IdUsuario.ToString() }
                    };

                    return Ok(response);
                }
                
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

        [Authorize]
        [HttpGet("datos/usuario/{idUsuario}")]
        public async Task<IActionResult> GetNombre(int idUsuario)
        {
            try
            {
                var cliente = await _context.Usuarios.FindAsync(idUsuario);
                if (cliente == null)
                {
                    return NotFound(new ResponseGlobal<IEnumerable<ClienteDto>>
                    {
                        codigo = "404",
                        mensaje = "Cliente no encontrado",
                        data = null
                    });
                }

                var clienteDto = new ClienteDto
                {
                    // Asigna los campos de Cliente a ClienteDto
                    IdCliente = cliente.IdUsuario,
                    Nombres = cliente.NombreUsuario,
                    // Otros campos que necesites mapear
                };

                var response = new ResponseGlobal<ClienteDto>
                {
                    codigo = "200",
                    mensaje = "Cliente recuperado exitosamente",
                    data = clienteDto
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseGlobal<ClienteDto>
                {
                    codigo = "500",
                    mensaje = "Ocurrió un error al recuperar el cliente",
                    data = null
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
