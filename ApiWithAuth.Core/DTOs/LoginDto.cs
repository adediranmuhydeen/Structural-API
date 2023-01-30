using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApiWithAuth.Core.DTOs
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [PasswordPropertyText]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }
    }
}
