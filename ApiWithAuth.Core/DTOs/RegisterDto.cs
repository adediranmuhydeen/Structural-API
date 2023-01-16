using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApiWithAuth.Core.DTOs
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string Username { get; set; }
        [Required]
        [PasswordPropertyText]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }
        [Required]
        [PasswordPropertyText]
        [StringLength(50, MinimumLength = 5)]
        public string ConfirmPassword { get; set; }
    }
}
