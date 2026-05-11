using System.ComponentModel.DataAnnotations;

namespace WebApplication_REST.Models
{
    public enum Positions { Manager, Tester, Developer, ScrumMaster }
    public class Employee
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public Positions Position { get; set; }
        public decimal Salary { get; set; }
        public DateTime Hired { get; set; }
        public bool IsActive { get; set; }
    }
}
