using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Repositories.Implementation
{
    public class SampleAPIRepository : ISamapleAPIRepository
    {
        private static Dictionary<string, Employee> _employeeData = new Dictionary<string, Employee>();

        public void AddEmployee(Employee employee)
        {
            _employeeData[employee.Name] = employee;
        }

        public Dictionary<string, Employee> Get()
        { 
            return _employeeData; 
        }

        public Dictionary<string, Employee> Delete(string name)
        {
            _employeeData.Remove(name);
            return _employeeData;
        }

        public Dictionary<string, Employee> Search(string search)
        {
            IEnumerable<KeyValuePair<string, Employee>> filtered = _employeeData
                .Where(x => x.Value.Name.Contains(search) || x.Value.Email.Contains(search) || x.Value.ContactNo.Contains(search));

            var result = new Dictionary<string, Employee>();
            foreach (KeyValuePair<string, Employee> emp in filtered)
            {
                result.Add(emp.Key, emp.Value);
            }
            return result;
        }
    }
}
