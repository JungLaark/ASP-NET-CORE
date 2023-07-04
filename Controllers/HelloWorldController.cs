using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace ASP_CORE_MVC.Controllers {
    public class HelloWorldController : Controller {

        public IActionResult Index() {
            return View();
        }

        /*viewData 는 dynamic 한 Dictionary 이다. */
        public IActionResult Welcome(string name, int num = 1) {
            ViewData["Message"] = "Hello " + name;
            ViewData["num"] = num;

            return View();
        }
    }
}
