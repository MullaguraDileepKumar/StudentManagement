using WebApplication2.Core.Model;

namespace WebApplication2.Core.Dtos
{
    public class StudentView
    {
        public Student Student {  get; set; }
        public College College { get; set; }
        public Department Department { get; set; }
        public Semester Semester { get; set; }
        public Marks Marks { get; set; }

    }
}
