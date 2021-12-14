using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RadiostationWeb.Data;
using RadiostationWeb.Models;
using System.Collections.Generic;
using System.Linq;

namespace RadiostationWeb.Controllers
{
    
    public class EmployeeController : Controller
    {
        private readonly BDLab1Context _dbContext;
        public EmployeeController(BDLab1Context context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            var employees = _dbContext.Employees.ToList();
            return employees;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Employee employee = _dbContext.Employees.FirstOrDefault(x => x.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                var employeeView = new Employee
                {
                    Name = employee.Name,
                    Surname = employee.Surname,
                    Middlename = employee.Middlename

                };
                return new ObjectResult(employeeView);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }

            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();
            return Ok(employee);
        }


        [HttpPut]
        public IActionResult Put([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }
            if (!_dbContext.Employees.Any(t => t.Id == employee.Id))
            {
                return NotFound();
            }

            _dbContext.Employees.Update(employee);
            _dbContext.SaveChanges();
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Employee employee = _dbContext.Employees.FirstOrDefault(t => t.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
            return Ok(employee);
        }
    }
}

