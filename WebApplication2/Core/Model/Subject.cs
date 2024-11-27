namespace WebApplication2.Core.Model
{
    public class Subject : BaseEntity<long>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SubjectCode { get; set; }
        public string StudentId { get; set; }
        public int CollegeId { get; set; }
        public int SemesterId { get; set; }
        public int DepartmentId { get; set; }
    }
}
