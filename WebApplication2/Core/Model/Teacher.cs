using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace WebApplication2.Core.Model
{
    public class Teacher : BaseEntity<long>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public long Salary { get; set; }
        public int CollegeId { get; set; }
        public int SubjectId { get; set; }
        public int AttendanceId { get; set; }
        public int DepartmentId { get; set; }
        public int EnrollmentId { get; set; }

    }
}
