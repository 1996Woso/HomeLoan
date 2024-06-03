using Home_Loan_App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Home_Loan_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //public IActionResult SearchCustomers(string Loan_Application_Status)
        //{
        //    var customer = _logger.Customer.Where(C=>C.Loan_Application_Status.Contains(Status)
        //    return View();
        //}
    }
}
