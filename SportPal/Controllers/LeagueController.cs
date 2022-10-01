using Microsoft.AspNetCore.Mvc;

namespace SportPal.Controllers
{
    public class LeagueController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Standings(string LeagueName)
        {
            // if param is empty, the user is sent back to the home page of League
            if (LeagueName == null)
            {
                return RedirectToAction("Index");
            }

            ViewData["LeagueName"] = LeagueName;

            return View();
        }

    }
}
