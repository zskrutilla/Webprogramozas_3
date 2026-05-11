using System.ComponentModel.DataAnnotations;

namespace WebApplication_IoC.Models
{
    public enum Position
    {
        //Manager,
        //Developer,
        //Designer,
        //Tester,
        //DevOps
        [Display(Name = "Manager")]
        manager = 0,
        [Display(Name = "Developer")]
        developer = 1,
        [Display(Name = "Designer")]
        designer = 2,
        [Display(Name = "Tester team member")]
        tester = 3,
        [Display(Name = "DevOps member")]
        devOps = 4
    }

    public class Employee
    {
        public string? Name { get; set; }
        public int IdNumber { get; set; }
        public string? Department { get; set; }
        public Position Position { get; set; }
    }
}
