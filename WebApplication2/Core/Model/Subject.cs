using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Core.Model
{
    public class Subject : BaseEntity<long>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string SubjectCode { get; set; }
       
        [Required]
        public int CollegeId { get; set; }
        [Required]
        public int SemesterId { get; set; }
        [Required]
        public int DepartmentId { get; set; }
    }
}
