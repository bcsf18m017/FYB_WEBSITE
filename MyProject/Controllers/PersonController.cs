using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Controllers
{
    public class PersonController : Controller
    {
        List<PersonModel> persons = new List<PersonModel>
        {
            new PersonModel
            {
                Id = "1",
                Name = "Noman",
                Address = "h#7,st#15 ramgarh main Bazar Mughallpura,Karachi",
                Phone = "0300-1234567",
                Password = "12345",
                Cnic = "3520126349331",
                Image = "https://res.cloudinary.com/nomancloudinary/image/upload/v1654599479/z4eezx4pmi0i0sjaorkd.jpg",
                Salary = 10000,
                CreatedOn = DateTime.Now,
                CreatedBy = "1"
            },
            new PersonModel
            {
                Id = "2",
                Name = "Noman",
                Address = "Karachi",
                Phone = "0300-1234567",
                Password = "12345",
                Cnic = "123456789",
                Image = "https://m.media-amazon.com/images/I/81J5r9dANGL._SL1500_.jpg",
                Salary = 100,
                CreatedOn = DateTime.Now,
                CreatedBy = "1"
            },
            new PersonModel
            {
                Id = "3",
                Name = "Noman",
                Address = "Karachi",
                Phone = "0300-1234567",
                Password = "12345",
                Cnic = "123456789",
                Image = "https://m.media-amazon.com/images/I/81J5r9dANGL._SL1500_.jpg",
                Salary = 100,
                CreatedOn = DateTime.Now,
                CreatedBy = "1"
            }
            
        };

        // GET: Staff
        public ActionResult AllStaff()
        {
            ViewBag.CustomerOrStaff = "Staff";
            return View("AllPerson",persons);
        }

        public ActionResult AllCustomer()
        {
            ViewBag.CustomerOrStaff = "Customer";
            return View("AllPerson", persons);
        }
        public ActionResult StaffDetails()
        {
            ViewBag.CustomerOrStaff = "Staff";
            if (Request["viewDetails"] != null)
            {
                PersonModel p = persons[Convert.ToInt32(Request["viewDetails"]) - 1];
                ViewBag.CreatedBy= persons[Convert.ToInt32(p.CreatedBy)].Name;
                return View("PersonDetailsView", p);
            }
            else
            {
                return View("AllPerson", "Person");
            }
        }
        public ActionResult CustomerDetails()
        {
            ViewBag.CustomerOrStaff = "Customer";
            if (Request["viewDetails"] != null)
            {
                PersonModel p = persons[Convert.ToInt32(Request["viewDetails"]) - 1];
                ViewBag.CreatedBy = persons[Convert.ToInt32(p.CreatedBy)].Name;
                return View("PersonDetailsView", p);
            }
            else
            {
                return View("AllPerson", "Person");
            }
        }
        public ActionResult deleteOrEdit()
        {
            int delete = Request["delete"] == null ? -1 : Convert.ToInt32(Request["delete"]);
            int edit = Request["edit"] == null ? -1 : Convert.ToInt32(Request["edit"]);
            if (edit != -1)
            {
                PersonModel p = persons[edit - 1];
                return RedirectToAction("EditPerson", p);
            }
            else
            {
                //TODO delete product
                return RedirectToAction("AllPerson", "Person");

            }
        }
        public ActionResult AddStaff()
        {
            ViewBag.CustomerOrStaff = "Staff";
            ViewBag.Now = DateTime.Now;
            return View("AddPerson",persons);
        }
        public ActionResult AddCustomer()
        {
            ViewBag.CustomerOrStaff = "Customer";
            ViewBag.Now = DateTime.Now;
            return View("AddPerson",persons);
        }



    }
}