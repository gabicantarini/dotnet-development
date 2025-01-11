using Microsoft.AspNetCore.Mvc;
using SpendSmart.Models;
using System.Diagnostics;

namespace SpendSmart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ExpenseDbContext _context; //add dependency injection

        public HomeController(ILogger<HomeController> logger, ExpenseDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Expenses()
        {
            var allExpenses = _context.Expenses.ToList(); //get all expenses and put into a list
            return View(allExpenses);
        }

        public IActionResult CreateExpense(int? id)
        {
            return View();
        }

        public IActionResult EditExpense() 
        {
            return View();        
        }

        public IActionResult CreatedExpenseForm(Expense model) 
        { 
            _context.Expenses.Add(model); //to access expense table
            _context.SaveChanges();
            return RedirectToAction("Expenses");
        }

        public IActionResult EditedExpenseForm(Expense model)
        {
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
