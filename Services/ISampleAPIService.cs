using WebApplication2.Models;

namespace WebApplication2.Services
{
    public interface ISampleAPIService
    {
        public void AddEmployee(Models.Employee employee);
        public Dictionary<string, Models.Employee> Get();
        public Dictionary<string, Employee> Delete(string name);
        public Dictionary<string, Models.Employee> Search(string search);
    }
}
