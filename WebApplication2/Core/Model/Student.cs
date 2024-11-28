using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sieve.Attributes;

namespace WebApplication2.Core.Model
{
    public class Student : BaseEntity<long>
    {
        [Sieve(CanSort = true, CanFilter = true)]
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(11)")]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        [Required]
        public int CollegeId { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public int SemesterId { get; set; }
        [Required]
        public int AttendanceId { get; set; }
        [Required]
        public string StatusId { get; set; }
        [Required]
        public int EnrollmentId { get; set; }
        
        public int GradeId { get; set; }
        public double Percentage { get; set; }
    }
}
