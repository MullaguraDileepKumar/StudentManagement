using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Core.Model
{
    public class Attendance
    {
        public int Id { get; set; }
        [Required]
        public int PersonId { get; set; }
        [Required]
        public string RoleId {  get; set; } = string.Empty;
        [Required]
        public double AttendancePercentage { get; set; } = 0;
    }
}
