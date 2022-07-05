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
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Json;
using IqbalElectronics.DB.Models;
using MyProject.Api_Calls;

namespace Iqbal_Electronics.Controllers
{
    public class HomeController : Controller
    {
      
        public async Task<ActionResult> SignIn()
        {
            Person.LoggedIn =null;
            if(ViewBag.LoginModel!="Yes")
            {
                ViewBag.LoginModal = "No";
            }
            return View();
        }
        [HttpPost]
        [ActionName("SignIn")]
        public async Task<ActionResult> SignIn(string phone, string password)
        {
            LoginModel login = new LoginModel
            {
                Phone = phone,
                Password = password
            };
            var person=await ApiCalls.validateUser(login);
            if(person==null)
            {
                ViewBag.LoginModal = "Yes";
                return View("SignIn");
            }
            else{
                Person.LoggedIn = person;
                ViewBag.LoginModel = "No";
                return RedirectToAction("AllProducts","Products");
            }
        }

        public ActionResult Dashboard()
        {
            return View("Dashboard");
        }

    }
}