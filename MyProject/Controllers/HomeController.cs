using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Iqbal_Electronics.Controllers
{
    public class HomeController : Controller
    {


        public async Task<ActionResult> SignIn()
        {
            ViewBag.LoginModal = "No";
            return View();
        }
        [HttpPost]
        [ActionName("SignIn")]
        public ActionResult SignIn(string phone, string password)
        {
            if (phone == "123" && password == "123")
            {
                return RedirectToAction("AllProducts","Products");
            }
            else
            {
                ViewBag.LoginModal = "Yes";
                return View("SignIn");
            }
        }

        public ActionResult Dashboard()
        {
            return View("Dashboard");
        }

        public ActionResult Privacy()
        {
            return View();
        }


    }
}