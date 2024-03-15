using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MovieApp.Controllers;

public class HelloWorldController : Controller
{
    // 
    // GET: /HelloWorld/
    public IActionResult Index()
    {
        return View();
    }
    // 
    // GET: /HelloWorld/Welcome/ 
    public IActionResult Welcome(string name, int numTimes = 1)
    {
        //return HtmlEncoder.Default.Encode($"Hello {name}, NumTime is: {numTimes}");

        ViewData["Message"] = "Hello " + name;
        ViewData["NumTimes"] = numTimes;
        return View();
    }
}