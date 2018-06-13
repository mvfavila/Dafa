using System.Web.Mvc;

namespace DAFA.Presentation.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        private static StartupTimer timer;

        public ActionResult Index()
        {
            if(timer == null)
            {
                timer = new StartupTimer();
                timer.StartEventWarningWatcher();
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}