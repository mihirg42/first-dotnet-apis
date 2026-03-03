using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Services.Implementaton
{
    public class SampleAPIService: ISampleAPIService
    {
        private readonly ISamapleAPIRepository _repository;

        public SampleAPIService(ISamapleAPIRepository repository)
        {
            _repository = repository;
        }

        public void AddEmployee(Models.Employee employee)
        {
            // retrieve current employees from repository
            var employeeData = _repository.Get();

            // check for null and existing key
            if (employeeData != null && employeeData.ContainsKey(employee.Name))
            {
                throw new Exception("Employee already exists");
            }

            if(employee.ContactNo.Length != 10)
            {
                throw new Exception("Contact number must be 10 digits");
            }

            _repository.AddEmployee(employee);
        }

        public Dictionary<string, Models.Employee> Get()
        {
            return _repository.Get();
        }

        public Dictionary<string, Employee> Delete(string name)
        {
            Dictionary<string, Employee> emp = _repository.Get();
            if (!emp.ContainsKey(name))
            {
                throw new Exception("Employee not found");
            }
            _repository.Delete(name);
            emp = _repository.Get();
            return emp;
        }

        public Dictionary<string, Models.Employee> Search(string search)
        {
            return _repository.Search(search);
        }

    }
}
