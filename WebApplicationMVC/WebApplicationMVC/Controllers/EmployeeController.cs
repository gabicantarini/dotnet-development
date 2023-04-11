using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplicationMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        //added an employee list
        List<Employee> employees;
        public EmployeeController()
        {
            employees = new List<Employee>();
            employees.Add(new Employee { ID = 1, Name = "Raul", ContactNumber = 999999999, Address = " Test Address" });
            employees.Add(new Employee { ID = 2, Name = "Paula", ContactNumber = 888888888, Address = " Test Address2" });
        }

        [HttpGet(Name = "EmployeeList")]
        public IEnumerable<Employee> Get()
        {
            return employees;
        }


        public Employee Get (int id)
        {
            return employees.FirstOrDefault(x => x.ID.Equals(id));
        }
        


    }
}
