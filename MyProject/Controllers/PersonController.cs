using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FYP.Controllers;
using IqbalElectronics.DB.Models;
using MyProject.Api_Calls;
using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Controllers
{
    public class PersonController : Controller
    {
        private static string edit="edit";
        private static string user = "Staff";
        private static List<Person> Persons;
        private static List<Person> Customers;
        private static List<Person> CustomersList;
        private static List<Person> StaffList;
        private static Person currentPerson;
        // GET: Staff
        public async Task<ActionResult> AllStaff()
        {
            if(Person.LoggedIn==null)
            {
                return RedirectToAction("SignIn", "Home");
            }
            user = "Staff";
            ViewBag.IsMessage = "No";
            if (Persons==null)
            {
                Persons = await ApiCalls.getAllPerson();
            }
            if(Customers==null)
            {
                await separateRoles();
            }
            ViewBag.CustomerOrStaff = "Staff";
            return View("AllPerson",StaffList);
        }
        private async Task<ActionResult> separateRoles()
        {
            Customers = await ApiCalls.getAllPersonByRole("Customer");
            if(Customers==null)
            {
                StaffList = Persons;
                CustomersList = new List<Person>();
            }
            else
            {
                CustomersList = new List<Person>();
                StaffList = new List<Person>();
                for (int i = 0; i < Persons.Count; i++)
                {
                    for (int j = 0; j < Customers.Count; j++)
                    {
                        if (Persons[i].Id == Customers[j].Id)
                        {
                            CustomersList.Add(Persons[i]);
                        }
                    }
                }
                for (int i = 0; i < Persons.Count; i++)
                {
                    bool flag = true;
                    for (int j = 0; j < Customers.Count; j++)
                    {
                        if (Persons[i].Id == Customers[j].Id)
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        StaffList.Add(Persons[i]);
                    }
                }
            }
            return null;
        }
        public async  Task<ActionResult> AllCustomer()
        {
            if (Person.LoggedIn == null)
            {
                return RedirectToAction("SignIn", "Home");
            }
            user = "Customer";
            ViewBag.IsMessage = "No";
            if (Persons == null)
            {
                Persons = await ApiCalls.getAllPerson();
            }
            if (Customers == null)
            {
                await separateRoles();
            }
            ViewBag.CustomerOrStaff = "Customer";
            return View("AllPerson", CustomersList);
        }
        public ActionResult StaffDetails()
        {
            if (Person.LoggedIn == null)
            {
                return RedirectToAction("SignIn", "Home");
            }
            user = "Staff";
            ViewBag.CustomerOrStaff = "Staff";
            ViewBag.IsMessage = "No";
            if (Request["viewDetails"] != null)
            {
                currentPerson = Persons.Find(p => p.Id == Request["viewDetails"]);
                ViewBag.CreatedBy= (Persons.Find(per=>per.Id==currentPerson.Id)).Name;
                return View("PersonDetailsView", currentPerson);
            }
            else
            {
                return View("AllPerson",StaffList);
            }
        }
        public ActionResult CustomerDetails()
        {
            if (Person.LoggedIn == null)
            {
                return RedirectToAction("SignIn", "Home");
            }            
            user = "Customer";
            ViewBag.CustomerOrStaff = "Customer";
            ViewBag.IsMessage = "No";
            if (Request["viewDetails"] != null)
            {
                currentPerson = Persons.Find(p => p.Id == Request["viewDetails"]);
                ViewBag.CreatedBy = (Persons.Find(per => per.Id == currentPerson.Id)).Name;
                return View("PersonDetailsView", currentPerson);
            }
            else
            {
                return View("AllPerson", "Person");
            }
        }
        public async Task<ActionResult> deleteOrEdit()
        {
            if (Person.LoggedIn == null)
            {
                return RedirectToAction("SignIn", "Home");
            }
            string delete = Request["delete"] == null ? "" : (Request["delete"]);
            string edit = Request["edit"] == null ? "" : (Request["edit"]);
            if (edit != "")
            {
                return RedirectToAction("EditPerson");
            }
            else
            {
                //TODO delete product
                bool flag = await ApiCalls.deletePerson(currentPerson.Id);
                Persons = await ApiCalls.getAllPerson();
                ViewBag.IsMessage = "Yes";
               if(flag)
                {
                    ViewBag.AllPersonMessage = "Deleted Successfully";
                }
               else
                {
                    ViewBag.AllPersonMessage = "Unable to Delete Now.Try Again Later";
                }
               
                Persons = await ApiCalls.getAllPerson();
                await separateRoles();
                OrdersController.Orders = null;
                OrdersController.orderDetails = null;
                if (Person.LoggedIn.Id == currentPerson.Id)
                {
                    Person.LoggedIn = null;
                    RedirectToAction("SignIn", "Home");
                }
                ViewBag.CustomerOrStaff = user;
                if(user=="Staff")
                {
                    return View("AllPerson", StaffList);
                }
                else
                {
                    return View("AllPerson", CustomersList);
                }
            }
        }
        public ActionResult EditPerson()
        {
            if (Person.LoggedIn == null)
            {
                return RedirectToAction("SignIn", "Home");
            }
            edit = "edit";
            ViewBag.Now = DateTime.Now;
            ViewBag.CustomerOrStaff = user;
            return View(currentPerson);
        }
        


        public ActionResult AddStaff()
        {
            edit = "add";
            ViewBag.CustomerOrStaff = "Staff";
            ViewBag.Now = DateTime.Now;
            return View("AddPerson",Persons);
        }
        [HttpPost]
        [ActionName("AddStaff")]
        public async Task<ActionResult> AddStaff(string name, string address, string phone, string cnic, string salary)
        {
            if (Person.LoggedIn == null)
            {
                return RedirectToAction("SignIn", "Home");
            }
            String image;
            HttpPostedFileBase file = Request.Files["image"];
            if (file != null && file.ContentLength > 0)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, file.InputStream)
                };
                var uploadResult = ProductsController.cloudinary.Upload(uploadParams);
                string[] tokens = uploadResult.Uri.ToString().Split('/');
                image = tokens[tokens.Length - 2] + "/" + tokens[tokens.Length - 1];
            }
            else
            {
                if (edit == "edit")
                {
                    image = currentPerson.Image;
                }
                else
                {
                    image = "v1655680460/wevuh9kves3jozribf7p.png";
                }
            }
            PersonModel p = new PersonModel();
            if(edit=="edit")
            {
                p=new PersonModel
                {
                
                    Id = "87f17041-e0bb-4cbf-a0a7-a90599cc8914",
                    Name = name,
                    Address = address,
                    Phone = currentPerson.PhoneNumber,
                    Password = currentPerson.PhoneNumber,
                    Cnic = cnic,
                    Image = image,
                    Salary = Convert.ToInt32(salary),
                    CreatedOn = DateTime.Now,
                    CreatedBy = Person.LoggedIn.Id
                };
            }
            else
            {
                p = new PersonModel
                {

                    Id = "87f17041-e0bb-4cbf-a0a7-a90599cc8914",
                    Name = name,
                    Address = address,
                    Phone = phone,
                    Password = phone,
                    Cnic = cnic,
                    Image = image,
                    Salary = Convert.ToInt32(salary),
                    CreatedOn = DateTime.Now,
                    CreatedBy = Person.LoggedIn.Id
                };
            }
           

            var personReturned=new Person();
            if(edit=="edit")
            {
                personReturned = await ApiCalls.updatePerson(currentPerson.Id, p);
            }
            else
            {
                personReturned = await ApiCalls.addPerson("Staff", p);
            }
            if(personReturned!=null)
            {
                if(edit!="edit")
                {
                    ViewBag.AllPersonMessage = "Staff Added Successfully";
                }
                else
                {
                    ViewBag.AllPersonMessage = "Staff Edited Successfully";
                }
            }
            else
            {
                if(edit=="edit")
                {
                    ViewBag.AllPersonMessage = "Staff Not Edited.Try Again Later";
                }
                else
                {
                    ViewBag.AllPersonMessage = "Staff Not Added.Try Again Later";
                }
            }
            ViewBag.IsMessage = "Yes";
            ViewBag.CustomerOrStaff = "Staff";
            Persons = await ApiCalls.getAllPerson();
            await separateRoles();
            return View("AllPerson", StaffList);
        }
        public ActionResult AddCustomer()
        {
            if (Person.LoggedIn == null)
            {
                return RedirectToAction("SignIn", "Home");
            }
            edit = "add";
            ViewBag.CustomerOrStaff = "Customer";
            ViewBag.Now = DateTime.Now;
            return View("AddPerson",Persons);
        }
        [HttpPost]
        [ActionName("AddCustomer")]
        public async Task<ActionResult> AddCustomer(string name, string address, string phone, string cnic, string salary,string guarantee1,string guarantee2)
        {
            if (Person.LoggedIn == null)
            {
                return RedirectToAction("SignIn", "Home");
            }
            String image;
            HttpPostedFileBase file = Request.Files["image"];
            if (file != null && file.ContentLength > 0)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, file.InputStream)
                };
                var uploadResult = ProductsController.cloudinary.Upload(uploadParams);
                string[] tokens = uploadResult.Uri.ToString().Split('/');
                image = tokens[tokens.Length - 2] + "/" + tokens[tokens.Length - 1];
            }
            else
            {
                if (edit == "edit")
                {
                    image = currentPerson.Image;
                }
                else
                {
                    image = "v1655680460/wevuh9kves3jozribf7p.png";
                }
            }
            PersonModel p = new PersonModel();
            if (edit == "edit")
            {
                p = new PersonModel
                {

                    Id = "87f17041-e0bb-4cbf-a0a7-a90599cc8914",
                    Name = name,
                    Address = address,
                    Phone = currentPerson.PhoneNumber,
                    Password = currentPerson.PhoneNumber,
                    Cnic = cnic,
                    Image = image,
                    Salary = Convert.ToInt32(salary),
                    CreatedOn = DateTime.Now,
                    CreatedBy = Person.LoggedIn.Id
                };
            }
            else
            {
                p = new PersonModel
                {

                    Id = "87f17041-e0bb-4cbf-a0a7-a90599cc8914",
                    Name = name,
                    Address = address,
                    Phone = phone,
                    Password = phone,
                    Cnic = cnic,
                    Image = image,
                    Salary = Convert.ToInt32(salary),
                    CreatedOn = DateTime.Now,
                    CreatedBy = Person.LoggedIn.Id
                };
            }

            var personReturned = new Person();
            if (edit == "edit")
            {
                personReturned = await ApiCalls.updatePerson(currentPerson.Id, p);
            }
            else
            {
                personReturned = await ApiCalls.addPerson("Customer", p);
            }
            if (personReturned != null)
            {
                if (edit != "edit")
                {
                    ViewBag.AllPersonMessage = "Customer Added Successfully";
                }
                else
                {
                    ViewBag.AllPersonMessage = "Custmer Edited Successfully";
                }
            }
            else
            {
                if (edit == "edit")
                {
                    ViewBag.AllPersonMessage = "Customer Not Edited.Try Again Later";
                }
                else
                {
                    ViewBag.AllPersonMessage = "Customer Not Added.Try Again Later";
                }
            }
            ViewBag.IsMessage = "Yes";
            ViewBag.CustomerOrStaff = "Customer";
            Persons = await ApiCalls.getAllPerson();
            await separateRoles();
            return View("AllPerson", CustomersList);
        }



    }
}