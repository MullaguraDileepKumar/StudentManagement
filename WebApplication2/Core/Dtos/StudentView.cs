using WebApplication2.Core.Model;

namespace WebApplication2.Core.Dtos
{
    public class StudentView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public College College { get; set; }
        public Department Department { get; set; }
        public Semester Semester { get; set; }
        public Marks Marks { get; set; }

    }
}
