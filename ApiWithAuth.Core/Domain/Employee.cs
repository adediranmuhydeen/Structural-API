using ApiWithAuth.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace ApiWithAuth.Core.Domain
{
    public class Employee : BaseEntity
    {
        [Required]
        public DepartmentE Department { get; set; }
        [Required]
        public double Salary { get; set; }
        public string EmploymentDate { get; } = DateTime.MinValue.Date.ToString("d");
    }
}
