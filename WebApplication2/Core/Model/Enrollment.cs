namespace WebApplication2.Core.Model
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public bool IsActive { get; set; } 
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
    }
}
