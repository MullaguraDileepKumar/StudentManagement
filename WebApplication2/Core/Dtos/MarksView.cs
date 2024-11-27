using WebApplication2.Core.Model;

namespace WebApplication2.Core.Dtos
{
    public class MarksView
    {
       //public int StudentId { get; set; }
        public Student Student { get; set; }
        public College CollegeId { get; set; }
        public Subject SubjectId { get; set; }
        public class College : IdWithName
        { 
           public string Address { get; set; }
        }
        public Marks Marks { get; set; }
        public class Subject :IdWithName
        {
            public string SubjectCode { get; set; }
            public string StudentId { get; set; }
            public int CollegeId { get; set; }
            public int SemesterId { get; set; }
        }

        public class IdWithName
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

    }
}
