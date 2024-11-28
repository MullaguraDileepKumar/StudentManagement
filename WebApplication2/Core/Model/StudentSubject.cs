using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Core.Model
{
    public class StudentSubject
    {
        public int Id { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int SubjectId { get; set; }
    }
}
