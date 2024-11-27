namespace WebApplication2.Core.Model
{
    public class Attendance
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string RoleId {  get; set; } = string.Empty;
        public double AttendancePercentage { get; set; }
    }
}
