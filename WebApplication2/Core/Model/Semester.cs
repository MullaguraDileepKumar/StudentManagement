using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Core.Model
{
    public class Semester
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

}
