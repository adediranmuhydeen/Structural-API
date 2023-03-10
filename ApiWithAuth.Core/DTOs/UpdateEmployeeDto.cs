using ApiWithAuth.Core.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApiWithAuth.Core.DTOs
{
    public class UpdateEmployeeDto
    {
        public TitleE Title { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        [PasswordPropertyText]
        public string Password { get; set; }
        [Phone]
        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        [Required]
        public DepartmentE Department { get; set; }
        [Required]
        public double Salary { get; set; }
    }
}
