using Microsoft.AspNetCore.Mvc;

namespace BlankSolution.MVC.Areas.Admin.Controlllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
