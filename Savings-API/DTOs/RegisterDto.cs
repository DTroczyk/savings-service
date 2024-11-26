using System.ComponentModel.DataAnnotations;

namespace Savings_API.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "UserName is required.")]
        public required string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
    }
}
