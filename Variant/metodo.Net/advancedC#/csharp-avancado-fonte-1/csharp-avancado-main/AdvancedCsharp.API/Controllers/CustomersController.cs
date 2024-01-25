using System;
using AdvancedCsharp.OopConsole.Infrastructure;
using AdvancedCsharp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedCsharp.API.Controllers
{
	[ApiController]
	[Route("api/customers")]
	public class CustomersController : ControllerBase
	{
		[HttpGet]
		public IActionResult DoAll([FromServices] ICustomerRepository repository)
		{
            var customer = new Customer("LuisDev");
            var id = repository.Add(customer);

            var existingCustomer = repository.GetCustomerById(id);

            return Ok(existingCustomer);
		}
	}
}

