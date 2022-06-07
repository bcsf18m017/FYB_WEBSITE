using FYP.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace FYP.Controllers
{
    public class ProductsController : Controller
    {
        private static int edit = -1;
        List<Category> categories = new List<Category>
            {
                new Category
                {
                    CategoryId=1,
                    Description="Mobile Phones"
                },
                new Category
                {
                    CategoryId=2,
                    Description="Laptops"
                },
                new Category
                {
                    CategoryId=3,
                    Description="Apliances"
                }

            };
        List<Product> products = new List<Product>
             {
                new Product
                {
                    ProductId = 1,
                    Title = "Product 1",
                    Description = "This is a product ncd cjwdvwdjvjw vjwvjwnvwovbwobvwvowvjo cjduwdbv2v wubviwnqeknf0iqehfpknwbv0uwbvow vjowdbvuqeivnwjobvhiebqerjb vhie v,nws",
                    Image = "https://m.media-amazon.com/images/I/81J5r9dANGL._SL1500_.jpg",
                    Price = 100,
                    CategoryId = 1,
                    Percentage = 10,
                    Daily = true,
                    Monthly = false,
                    CreatedBy = 1
                },
                new Product
                {
                    ProductId = 2,
                    Title = "Product 2",
                    Description = "This is a product",
                    Image = "https://www.google.com/images/branding/googlelogo/2x/googlelogo_color_272x92dp.png",
                    Price = 100,
                    CategoryId = 1,
                    Percentage = 10,
                    Daily = true,
                    Monthly = false,
                    CreatedBy = 1
                },
                new Product
                {
                    ProductId = 3,
                    Title = "Product 3",
                    Description = "This is a product",
                    Image = "https://www.google.com/images/branding/googlelogo/2x/googlelogo_color_272x92dp.png",
                    Price = 100,
                    CategoryId = 1,
                    Percentage = 10,
                    Daily = true,
                    Monthly = true,
                    CreatedBy = 1
                },

            };

        public ActionResult AllProducts()
        {
            @ViewBag.NavAction = "Products";
            return View(products);
        }
        public ActionResult ProductDetails()
        {
            @ViewBag.NavAction = "Products";
            if (Request["viewDetails"] != null)
            {
                if(products[Convert.ToInt32(Request["viewDetails"]) - 1].Daily)
                {
                    ViewBag.Type = "Daily";
                }
                else
                {
                    ViewBag.Type = "Monthly";
                }
                return View("ProductDetailsView", products[Convert.ToInt32(Request["viewDetails"])-1]);
            }
            else
            {
                return View("AllProducts","Products");
            }
        }

        public ActionResult deleteOrEdit()
        {
            int delete = Request["delete"] == null ? -1 : Convert.ToInt32(Request["delete"]);
            int edit = Request["edit"] == null ? -1 : Convert.ToInt32(Request["edit"]);
            if (edit != -1)
            {
                Product p = products[edit - 1];   
                return RedirectToAction("EditProduct",p);
            }
            else
            {
                //TODO delete product
                return RedirectToAction("AllProducts", "Products");

            }
        }
        public ActionResult EditProduct(Product p)
        {
            @ViewBag.NavAction = "Products";
            edit = 1;
            ViewBag.EditProductCategory = categories[p.CategoryId - 1].Description;
            ViewBag.EditProductPaymentType = p.Daily ? "Daily" : "Monthly";
            ViewBag.Categories = categories;
            return View(p);
        }
       
        


        public ActionResult AddProduct()
        {
            @ViewBag.NavAction = "Products";
            edit = -1;
            return View(categories);
        }
        [HttpPost]
        [ActionName("AddProduct")]
        public ActionResult AddProduct(string code,string title, string description, string image, string price, string category, string percentage, string type)
        {

            Product p = new Product { 
                ProductId= Convert.ToInt16(code), 
                Title =title,
                Description= description, 
                Image =image, 
                Price= Convert.ToInt32(price),
                CategoryId= 1, 
                Percentage= Convert.ToInt32(percentage),
                Daily= true,
                Monthly= false, 
                CreatedBy= 2 };
            if(edit==1)
            {
                //EDIT
                return RedirectToAction("AddProduct", "Products");
            }
            else
            {
                //ADD
            }
            return RedirectToAction("AllProducts","Products");
        }
    }
}
