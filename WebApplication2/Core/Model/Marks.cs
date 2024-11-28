using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Core.Model
{
    public class Marks
    {
        public int Id { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public int StudentMarks { get; set; } = 0;
    }
}
