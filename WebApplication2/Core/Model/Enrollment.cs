using System.ComponentModel.DataAnnotations;
using WebApplication2.Core.Constants;

namespace WebApplication2.Core.Model
{
    public class Enrollment
    {
        public int Id { get; set; }
        [Required]
        public int PersonId { get; set; }
        [Required]
        public RoleTypes RoleType { get; set; }
        [Required]
        public bool IsActive { get; set; } 
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
    }

}
