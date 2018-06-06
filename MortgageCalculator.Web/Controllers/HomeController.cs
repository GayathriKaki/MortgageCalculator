using System.Web.Mvc;

namespace MortgageCalculator.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}