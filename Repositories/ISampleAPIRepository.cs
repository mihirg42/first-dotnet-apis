using WebApplication2.Models;


namespace WebApplication2.Repositories
{
    public interface ISampleAPIRepository
    {
        public void AddEmployee(Employee employee);
        public Dictionary<string, Employee> Get();
        public Dictionary<string, Employee> Delete(string name);
        public Dictionary<string, Employee> Search(string search);
    }
}
