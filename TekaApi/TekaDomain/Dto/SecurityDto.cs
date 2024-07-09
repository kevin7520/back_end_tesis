using System.ComponentModel.DataAnnotations;

namespace TekaDomain.Dto
{
    public class LoginDto
    {
        public string Correo { get; set; }
        public string password { get; set; }
    }

    public class CreateUserDto
    {
        [Required]
        public string NombreUsuario { get; set; }

        [Required]
        public string Contraseña { get; set; }

        [Required]
        public string Correo { get; set; }

        [Required]
        public int IdRol { get; set; }

        [Required]
        public int IdEstado { get; set; }
    }
}

