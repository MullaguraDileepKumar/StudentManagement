using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Core.Model
{
    public class TeacherSubject
    {
        public int Id { get; set; }
        [Required]
        public int TeacherId { get; set; }
        [Required]
        public int SubjectId { get; set; }
    }
}
