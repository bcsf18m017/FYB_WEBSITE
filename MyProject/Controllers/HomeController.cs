using MyProject.Models;
using MyProject.Controllers;
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
                PersonModel.LoggedIn = new PersonModel
                {
                    Id = "69",
                    Name = "Admin",
                    Address = "h#7,st#15 ramgarh main Bazar Mughallpura,Karachi",
                    Phone = "0300-1234567",
                    Password = "12345",
                    Cnic = "3520126349331",
                    Image = "https://res.cloudinary.com/nomancloudinary/image/upload/v1654599479/z4eezx4pmi0i0sjaorkd.jpg",
                    Salary = 10000,
                    CreatedOn = DateTime.Now,
                    CreatedBy = "1"
                };
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