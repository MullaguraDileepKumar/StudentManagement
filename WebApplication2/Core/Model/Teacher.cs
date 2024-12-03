using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Core.Model
{
    public class Teacher : BaseEntity<long>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public char Gender { get; set; }
        [Required]
        public DateTime HireDate { get; set; }
        public long Salary { get; set; }
        [Required]
        public int CollegeId { get; set; }
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public int DepartmentId { get; set; }
    }
}
