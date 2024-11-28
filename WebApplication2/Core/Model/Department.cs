using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Core.Model
{
    public class Department : BaseEntity<long>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
