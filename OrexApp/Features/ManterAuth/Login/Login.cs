using System.ComponentModel.DataAnnotations;

namespace OrexApp.Features.ManterAuth.Login
{
    public class Login
    {
        [EmailAddress]
        public required string Email { get; set; }

        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}