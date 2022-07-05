using FYP.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Web;
using MyProject.Models;
using System.Net.Http;
using System.Web.UI.WebControls;
using IqbalElectronics.DB.Models;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using MyProject.Api_Calls;
using MyProject.Controllers;

namespace FYP.Controllers
{
    public class ProductsController : Controller
    {

        public static List<Product> Products;
        public static List<Category> Categories;
       static  Account account = new Account(
      "nomancloudinary",
      "265779579282175",
      "mIrPL8wcDU1crT3Dk4eC37usiT4");
        private static Product currentProduct;
        public static Cloudinary cloudinary = new Cloudinary(account);
        public static  FileUpload uploader { get; set; }
        private static  readonly HttpClient httpClient;
        
        public  ProductsController()
        {
            uploader = new FileUpload();
        }
        private static int edit = -1;
       
        public async Task<ActionResult> AllProducts()
        {
            if (Person.LoggedIn == null)
            {
                return RedirectToAction("SignIn", "Home");
            }
            ViewBag.IsCategoryResult = "No";
            ViewBag.DeleteModal = "No";
            if (Products == null)
            {
                Products=await ApiCalls.getAllProducts();
            }
            if (Categories == null)
            {
                Categories=await ApiCalls.getAllCategories();
            }
            return View(Products);
        }
        public ActionResult ProductDetails()
        {
            if (Person.LoggedIn == null)
            {
                return RedirectToAction("SignIn", "Home");
            }            
            if (Request["viewDetails"] != null)
            {
                Product product = Products.Find(p => p.Id == Request["viewDetails"]);
                if (product.Daily &&product.Monthly)
                {
                    ViewBag.Type = "Both";
                }
                else if(product.Daily)
                {
                    ViewBag.Type = "Daily";
                }
                else
                {
                    ViewBag.Type = "Monthy";
                }
                currentProduct = product;
                return View("ProductDetailsView",product);
            }
            else
            {
                return View("AllProducts",Products);
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
                return RedirectToAction("EditProduct");
            }
            else
            {
                //TODO delete product
                bool flag=await ApiCalls.deleteProduct(currentProduct.Id);
                Products = await ApiCalls.getAllProducts();
                ViewBag.DeleteModal = "Yes";
                OrdersController.Orders = null;
                OrdersController.orderDetails = null;
                return View("AllProducts", Products);

            }
        }
        public ActionResult EditProduct()
        {
            if (Person.LoggedIn == null)
            {
                return RedirectToAction("SignIn", "Home");
            }
            edit = 1;
            ViewBag.EditProductCategory = currentProduct.Category.Description;
            if(currentProduct.Daily&&currentProduct.Monthly)
            {
                ViewBag.EditProductPaymentType = "Both";
            }
            else
            {
                ViewBag.EditProductPaymentType = currentProduct.Daily ? "Daily" : "Monthly";
            }
            ViewBag.Categories = Categories;
            return View(currentProduct);
        }
       
        


        public ActionResult AddProduct()
        {
            if (Person.LoggedIn == null)
            {
                return RedirectToAction("SignIn", "Home");
            }            
            edit = -1;
            ViewBag.IsCategoryResult = "No";
            return View(Categories);
        }
        [HttpPost]
        [ActionName("AddProduct")]
        public async Task<ActionResult> AddProduct(string code,string title, string description, string price, string category, string percentage, string type,string installment)
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
                var uploadResult = cloudinary.Upload(uploadParams);
                string[]tokens = uploadResult.Uri.ToString().Split('/');
                image = tokens[tokens.Length - 2]+"/"+ tokens[tokens.Length - 1];
            }
            else
            {
                if(edit==1)
                {
                    image = currentProduct.Image;
                }
                else
                {
                    image = "v1655587791/acoulh6gomep3j0uawnr.png";
                }
            }
            bool dailyFlag=false, monthlyFlag=false;
            if(type.Equals("Daily"))
            {
                dailyFlag = true;
            }
            else if(type.Equals("Monthly"))
            {
                monthlyFlag = true;
            }
            else
            {
                dailyFlag = true;
                monthlyFlag = true;
            }
            ProductModel p = new ProductModel { 
                ProductId="string", 
                Title =title,
                Description= description, 
                Image = image, 
                Price = Convert.ToInt32(price),
                CategoryId = Categories.Find(x => x.Description == category).Id,
                Percentage = Convert.ToInt32(percentage),
                Daily = dailyFlag,
                Monthly = monthlyFlag,
                MinimumInstallments = Convert.ToInt32(installment),
                CreatedBy = Person.LoggedIn.Id };
            if(edit==-1)
            {
                await ApiCalls.postProduct(p);
            }
            else
            {
                await ApiCalls.updateProduct(currentProduct.Id, p);
            }

            Products = await ApiCalls.getAllProducts();
            return View("AllProducts", Products);
        }
        
        public async Task<ActionResult> AddCategory(string description)
        {
            if (Person.LoggedIn == null)
            {
                return RedirectToAction("SignIn", "Home");
            }
            bool flag=await ApiCalls.addCategory(description);
            Categories = await ApiCalls.getAllCategories();
            if(flag)
            {
                ViewBag.CategoryAdd = "Yes";
            }
            else
            { 
                ViewBag.CategoryAdd = "No";
            }
            ViewBag.IsCategoryResult = "Yes";
            return View("AddProduct", Categories);
        }
    }

  
}
