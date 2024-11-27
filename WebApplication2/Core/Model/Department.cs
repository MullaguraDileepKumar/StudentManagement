namespace WebApplication2.Core.Model
{
    public class Department : BaseEntity<long>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
