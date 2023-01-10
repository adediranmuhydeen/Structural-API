using ApiWithAuth.Core.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiWithAuth.Core.Domain
{
    public abstract class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; } = new Guid().ToString().Substring(1, 9).ToUpper();
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
        [Required]
        [StringLength(20)]
        [PasswordPropertyText]
        public string Password { get; set; }
        [Phone]
        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; }


    }
}
