using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    public class Employee
    {
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }
    }
}
