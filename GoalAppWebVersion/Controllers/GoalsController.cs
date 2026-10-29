using Microsoft.AspNetCore.Mvc;

namespace GoalAppWebVersion.Controllers
{
    public class GoalsController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}
