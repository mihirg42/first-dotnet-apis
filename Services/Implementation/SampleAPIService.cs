using WebApplication2.Models;
using WebApplication2.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace WebApplication2.Services.Implementation
{
    public class SampleAPIService: ISampleAPIService
    {
        private readonly ISampleAPIRepository _repository;

        private readonly IConfiguration _config;

        public SampleAPIService(ISampleAPIRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
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
        public string GenerateToken(Employee employee)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );

            var credentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256
            );

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, employee.Name),
                new Claim(JwtRegisteredClaimNames.Email, employee.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Salary", employee.Salary.ToString()),
                new Claim("ContactNo", employee.ContactNo),
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
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
