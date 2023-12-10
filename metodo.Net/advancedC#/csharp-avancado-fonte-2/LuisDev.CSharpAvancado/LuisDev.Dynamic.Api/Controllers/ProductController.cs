using Microsoft.AspNetCore.Mvc;

namespace LuisDev.Dynamic.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet("v1")]
        public async Task<IActionResult> Get()
        {
            return Ok(new { name = "John", age = 30 });
        }

        [HttpGet("v2")]
        public async Task<IActionResult> Getv2()
        {
            List<object> objectsList = new List<object>();
            var teste = new { type = "book", title = "The Great Gatsby", author = "F. Scott Fitzgerald", pages = 218 };
            objectsList.Add(new { type = "book", title = "The Great Gatsby", author = "F. Scott Fitzgerald", pages = 218 });
            objectsList.Add(new { type = "electronics", name = "Smartphone", brand = "Samsung", price = 599.99 });
            objectsList.Add(new { type = "clothing", item = "T-shirt", size = "M", color = "Blue" });
            return Ok(objectsList);

        }
    }
}
